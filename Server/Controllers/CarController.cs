using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly AppDBContext _appDBContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;

        public CarsController(AppDBContext appDBContext, IWebHostEnvironment webHostEnvironment, IMapper mapper)
        {
            _appDBContext = appDBContext;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
        }

        #region CRUD operations

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Car> cars = await _appDBContext.Cars.Include(cars => cars.Category).ToListAsync();

            return Ok(cars);
        }

  

        // website.com/api/cars/2
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Car car = await GetCarByCarId(id);

            return Ok(car);
        }

        // website.com/api/cars/2
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CarDTO carToCreateDTO)
        {
            try
            {
                if (carToCreateDTO == null)
                {
                    return BadRequest(ModelState);
                }

                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }

                Car carToCreate = _mapper.Map<Car>(carToCreateDTO);

                await _appDBContext.Cars.AddAsync(carToCreate);

                bool changesPersistedToDatabase = await PersistChangesToDatabase();

                if (changesPersistedToDatabase == false)
                {
                    return StatusCode(500, "Something went wrong broski on our side .");
                }
                else
                {
                    return Created("Create", carToCreate);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Something aint quite right son Error message: {e.Message}.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CarDTO updatedCarDTO)
        {
            try
            {
                if (id < 1 || updatedCarDTO == null || id != updatedCarDTO.CarDTOId)
                {
                    return BadRequest(ModelState);
                }

                bool exists = await _appDBContext.Cars.AnyAsync(car => car.CarId == id);

                if (exists == false)
                {
                    return NotFound();
                }

                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }
                Car updatedCar = _mapper.Map<Car>(updatedCarDTO);

                _appDBContext.Cars.Update(updatedCar);

                bool changesPersistedToDatabase = await PersistChangesToDatabase();

                if (changesPersistedToDatabase == false)
                {
                    return StatusCode(500, "Something went wrong on our side. Please contact the administrator.");
                }
                else
                {
                    return Created("Created",updatedCar);
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

                bool exists = await _appDBContext.Cars.AnyAsync(car => car.CarId == id);

                if (exists == false)
                {
                    return NotFound();
                }

                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }

                Car carToDelete = await GetCarByCarId(id);



                _appDBContext.Cars.Remove(carToDelete);

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
        private async Task<Car> GetCarByCarId(int carId)
        {
            Car carToGet = await _appDBContext.Cars
                   .Include(car => car.Category)
                   .FirstAsync(car => car.CarId == carId);

            return carToGet;
        }

        #endregion
    }
}