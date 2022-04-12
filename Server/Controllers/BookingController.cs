using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Shared.Models;
using System.Collections.Generic;
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




        // website.com/api/Bookings/2
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Booking Booking = await GetBookingByBookingId(id, false);

            return Ok(Booking);
        }



        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BookingDTO bookingToCreateDTO)
        {
            AppUser user = await GetUserById(bookingToCreateDTO.UserEmail);
           
            Booking booking = new Booking();//_mapper.Map<Booking>(bookingToCreateDTO);
            booking.StartTime = DateTime.UtcNow.ToString();
            booking.AppUser = user;
            booking.Location = bookingToCreateDTO.Location;
            booking.Cost = bookingToCreateDTO.Cost;


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
        public async Task<IActionResult> Update(int id, [FromBody] Booking updatedBookingDTO)
        {
            try
            {
                if (id < 1 || updatedBookingDTO == null || id != updatedBookingDTO.BookingId)
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

                Booking updatedBooking = _mapper.Map<Booking>(updatedBookingDTO);

                if (updatedBooking.IsComplete == true)
                {

                    //updatedBooking.StopTime = DateTime.UtcNow.ToString("dd/MM/yyyy hh:mm");
                    Console.WriteLine("Booking Complete");

                }

                _appDBContext.Bookings.Update(updatedBooking);

                bool changesPersistedToDatabase = await PersistChangesToDatabase();

                if (changesPersistedToDatabase == false)
                {
                    return StatusCode(500, "Something went wrong on our side. Please contact the administrator.");
                }
                else
                {
                    return Created("Created",updatedBooking);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Something went wrong on our side. Please contact the administrator. Error message: {e.Message}.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id < 1)
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
        private async Task<Booking> GetBookingByBookingId(int BookingId, bool withCars)
        {
            Booking BookingToGet = null;

            BookingToGet = await _appDBContext.Bookings.FirstAsync(Booking => Booking.BookingId == BookingId);
            

            return BookingToGet;
        }

        [NonAction]
        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<AppUser> GetUserById(string emailIn)
        {
             

            AppUser UserToGet = await _userManager.FindByEmailAsync(emailIn);


            return UserToGet;
        }

        #endregion
    }
}