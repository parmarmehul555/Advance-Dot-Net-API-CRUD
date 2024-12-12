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

        [HttpPost]
        public IActionResult Post([FromForm] CityModel model)
        {
            Dictionary<String, dynamic> response = new Dictionary<string, dynamic>();
            try
            {
                bool IsInserted = _CityRepo.Insert(model);
                if (IsInserted)
                {
                    response.Add("Status", true);
                    response.Add("Message", "City Added Successfully");
                    return Ok(response);
                }
                response.Add("Status", false);
                response.Add("Message", "Something Wents wrong!");
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                response.Add("Status", false);
                response.Add("Message", ex.Message);
                return BadRequest(response);
            }
        }

        [HttpPut("CityID")]
        public IActionResult Put(int CityID, [FromForm] CityModel model)
        {
            Dictionary<String, dynamic> response = new Dictionary<string, dynamic>();
            try
            {
                model.CityID = CityID;
                bool IsUpdated = _CityRepo.Update(model);
                if (IsUpdated)
                {
                    response.Add("Status", true);
                    response.Add("Message", "City Updated Successfully");
                    return Ok(response);
                }
                response.Add("Status", false);
                response.Add("Message", "Something Wents wrong!");
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                response.Add("Status", false);
                response.Add("Message", ex.Message);
                return BadRequest(response);
            }
        }

    }
}
