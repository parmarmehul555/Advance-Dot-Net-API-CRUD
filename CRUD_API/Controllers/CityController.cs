using CRUD_API.Data;
using CRUD_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly CityRepository _CityRepo;
        public CityController(CityRepository cityRepo)
        {
            _CityRepo = cityRepo;
        }


        #region SelectAll
        [HttpGet]
        public IActionResult SelectAllCities()
        {
            List<CityModel> cities = _CityRepo.SelectAll();
            return Ok(cities);
        }
        #endregion

        #region Delete City

        [HttpDelete("{CityID}")]
        public IActionResult DeleteCity(int CityID)
        {
            try
            {
                bool isDeleted = _CityRepo.Delete(CityID);

                if (isDeleted)
                {
                    return Ok(new
                    {
                        Status = "Success",
                        Message = "City deleted successfully."
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        Status = "Error",
                        Message = "City not found or could not be deleted."
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Status = "Error",
                    Message = "An unexpected error occurred.",
                    Details = ex.Message
                });
            }
        }
        #endregion

    }
}
