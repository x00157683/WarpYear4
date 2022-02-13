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
    public class CarsController : ControllerBase        //localhost:5001/api/controller
    {
        private readonly AppDBContext _appDBContext;

        public CarsController(AppDBContext appDBContext)
        {

            _appDBContext = appDBContext;


        }
        //                 http//website.com/api/Cars/get url

        [HttpGet]
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public async Task<IActionResult> Get()
        {

            List<Car> Cars = await _appDBContext.Cars.ToListAsync();


            return Ok(Cars);
        }


    }
}
