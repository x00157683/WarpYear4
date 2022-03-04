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
    public class LicensesController : ControllerBase
    {
        private readonly AppDBContext _appDBContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public LicensesController(AppDBContext appDBContext, IWebHostEnvironment webHostEnvironment)
        {
            _appDBContext = appDBContext;
            _webHostEnvironment = webHostEnvironment;
        }

        #region CRUD operations

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<License> Licenses = await _appDBContext.Licenses.ToListAsync();

            return Ok(Licenses);
        }

    
  


        // website.com/api/Licenses/2
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            License License = await GetLicenseByLicenseId(id);

            return Ok(License);
        }



        [HttpPost]
        public async Task<IActionResult> Create([FromBody] License LicenseToCreate)
        {
            try
            {
                if (LicenseToCreate == null)
                {
                    return BadRequest(ModelState);
                }

                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }

                await _appDBContext.Licenses.AddAsync(LicenseToCreate);

                bool changesPersistedToDatabase = await PersistChangesToDatabase();

                if (changesPersistedToDatabase == false)
                {
                    return StatusCode(500, "Something went wrong broski on our side .");
                }
                else
                {
                    return Created("Create", LicenseToCreate);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Something aint quite right son Error message: {e.Message}.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] License updatedLicense)
        {
            try
            {
                if (id < 1 || updatedLicense == null || id != updatedLicense.LicenseId)
                {
                    return BadRequest(ModelState);
                }

                bool exists = await _appDBContext.Licenses.AnyAsync(License => License.LicenseId == id);

                if (exists == false)
                {
                    return NotFound();
                }

                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }

                _appDBContext.Licenses.Update(updatedLicense);

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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id < 1)
                {
                    return BadRequest(ModelState);
                }

                bool exists = await _appDBContext.Licenses.AnyAsync(License => License.LicenseId == id);

                if (exists == false)
                {
                    return NotFound();
                }

                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }

                License LicenseToDelete = await GetLicenseByLicenseId(id);



                _appDBContext.Licenses.Remove(LicenseToDelete);

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
        private async Task<License> GetLicenseByLicenseId(int LicenseId)
        {
            License LicenseToGet = null;

            LicenseToGet = await _appDBContext.Licenses.FirstAsync(License => License.LicenseId == LicenseId);


            return LicenseToGet;
        }

        #endregion
    }
}