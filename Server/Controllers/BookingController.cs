using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Email;
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
        private readonly IEmailSender _emailSender;
        //private readonly EmailHelper _emailHelper;

        public BookingsController(AppDBContext appDBContext, IEmailSender emailSender, IWebHostEnvironment webHostEnvironment, IMapper mapper, UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _appDBContext = appDBContext;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
            _emailSender = emailSender;
            //_emailHelper = emailHelper;
        }

        #region CRUD operations

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //.Include(booking => booking.User)
            List<Booking> Bookings = await _appDBContext.Bookings.ToListAsync(); //Include(b => b.AppUser)

            return Ok(Bookings);
        }
        [HttpGet]
        [Route("email")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            //.Include(booking => booking.User)
            List<Booking> Bookings = await _appDBContext.Bookings.Where(b=>b.UserEmail == email).ToListAsync(); //Include(b => b.AppUser)

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
          
            //AppUser user = await GetUserByEmail(bookingToCreateDTO.UserEmail);

            Car car = await GetCarbyId(bookingToCreateDTO.CarId);
            Booking booking = new Booking()
            {

                BookingId = bookingToCreateDTO.BookingDTOId,
                //StartTime = bookingToCreateDTO.StartTime.ToString(),
                UserEmail = bookingToCreateDTO.UserEmail,
                //AppUser = _user,
                Location = bookingToCreateDTO.Location,
                IsComplete = false,
                IsCreated = true,
                CarId = bookingToCreateDTO.CarId,
                Car = car,
            };

            await _emailSender.SendEmailAsync("deemclean46@gmail.com", "Booking Sucessful", "New Booking has been created " + booking.BookingId);
 
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
                await _emailSender.SendEmailAsync("deemclean46@gmail.com","Booking Sucessful","New Booking has been created "+booking.BookingId);

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
                _book.IsComplete = false;

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
        private async Task<AppUser> GetUserByEmail(string emailIn)
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