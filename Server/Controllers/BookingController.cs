﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Shared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;




namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly AppDBContext _appDBContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;

        public BookingsController(AppDBContext appDBContext, IWebHostEnvironment webHostEnvironment, IMapper mapper, UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _appDBContext = appDBContext;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
        }

        #region CRUD operations

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //.Include(booking => booking.User)
            List<Booking> Bookings = await _appDBContext.Bookings.Include(b => b.AppUser).ToListAsync();

            return Ok(Bookings);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            Booking Booking = await GetBookingByBookingId(id, false);

            return Ok(Booking);
        }

        [AllowAnonymous]
        [Route("book")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BookingDTO bookingToCreateDTO)
        {
            AppUser user = await GetUserById(bookingToCreateDTO.UserEmail);

            Car car = await GetCarbyId(bookingToCreateDTO.CarId);

            Console.WriteLine("carr: "+car.Make);

            Booking booking =  new Booking();//

            booking.BookingId = bookingToCreateDTO.BookingDTOId;
            booking.StartTime = bookingToCreateDTO.StartTime.ToString();
            booking.AppUser = user;
            booking.Location = bookingToCreateDTO.Location;
            booking.CarId = bookingToCreateDTO.CarId;
            booking.Car = car;


            if (bookingToCreateDTO == null || !ModelState.IsValid)
                return BadRequest();
            

            await _appDBContext.Bookings.AddAsync(booking);


            bool changesPersistedToDatabase = await PersistChangesToDatabase();

            if (changesPersistedToDatabase == false)
            {
                return StatusCode(500, "Something went wrong on our side. Please contact the administrator.");
            }
            else
            {
                return NoContent();
            }


        }


       
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] BookingDTO updatedBookingDTO)
        {
            try
            {
                if ( updatedBookingDTO == null || id != updatedBookingDTO.BookingDTOId)
                {
                    return BadRequest(ModelState);
                }

                bool exists = await _appDBContext.Bookings.AnyAsync(Booking => Booking.BookingId == id);

                if (exists == false)
                {
                    return NotFound();
                }

               

                Booking _book = new Booking();

                _book.BookingId = updatedBookingDTO.BookingDTOId;
                _book.CarId = updatedBookingDTO.CarId;
                _book.IsCreated = updatedBookingDTO.IsCreated;
                _book.StartTime = updatedBookingDTO.StartTime;
                _book.Cost = updatedBookingDTO.Cost;
                _book.UserEmail = updatedBookingDTO.UserEmail;
                _book.Location = updatedBookingDTO.Location;
                _book.IsComplete = true;

                if (_book.IsComplete == true)
                {

                    _book.StopTime = DateTime.UtcNow.ToString("dd/MM/yyyy hh:mm");
                    Console.WriteLine("Booking Complete");

                }

                _appDBContext.Bookings.Update(_book);

                bool changesPersistedToDatabase = await PersistChangesToDatabase();

                if (changesPersistedToDatabase == false)
                {
                    return StatusCode(500, "Something went wrong on our side. Please contact the administrator.");
                }
                else
                {
                    return Created("Created",_book);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Something went wrong on our side. Please contact the administrator. Error message: {e.Message}.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest(ModelState);
                }

                bool exists = await _appDBContext.Bookings.AnyAsync(Booking => Booking.BookingId == id);

                if (exists == false)
                {
                    return NotFound();
                }

                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }

                Booking BookingToDelete = await GetBookingByBookingId(id, false);

                _appDBContext.Bookings.Remove(BookingToDelete);

                bool changesPersistedToDatabase = await PersistChangesToDatabase();

                if (changesPersistedToDatabase == false)
                {
                    return StatusCode(500, "Something went wrong on our side. Please contact the administrator.");
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Something went wrong on our side. Please contact the administrator. Error message: {e.Message}.");
            }
        }

        #endregion

        #region Utility methods

        [NonAction]
        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<bool> PersistChangesToDatabase()
        {
            int amountOfChanges = await _appDBContext.SaveChangesAsync();

            return amountOfChanges > 0;
        }

        [NonAction]
        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<Booking> GetBookingByBookingId(string Id, bool withCars)
        {
            //Booking BookingToGet = null;

            Booking BookingToGet = await _appDBContext.Bookings.Where(Booking => Booking.BookingId.ToLower() == Id.ToLower()).FirstOrDefaultAsync();


            

            return BookingToGet;
        }

        [NonAction]
        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<AppUser> GetUserById(string emailIn)
        {
             

            AppUser UserToGet = await _userManager.FindByEmailAsync(emailIn);


            return UserToGet;
        }

        [NonAction]
        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<Car> GetCarbyId(int carId)
        {


            Car carToGet = await _appDBContext.Cars
                .Include(car => car.Category)
                .FirstAsync(car => car.CarId == carId);

            return carToGet;
        }

        #endregion
    }
}