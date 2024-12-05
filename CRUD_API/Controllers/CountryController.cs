using CRUD_API.Data;
using CRUD_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private CountryRepository _repo;
        
        public CountryController(CountryRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult GetAllCountries()
        {
            var countries = _repo.SelectAllCountries();
            return Ok(countries);
        }


        [HttpDelete("{CountryID}")]
        public IActionResult Delete(int CountryID)
        {
            bool IsDeleted = _repo.DeleteCountry(CountryID);
            if (IsDeleted)
            {
                return Ok();
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Post([FromForm] CountryModel model)
        {
            Dictionary<String, dynamic> response = new Dictionary<string, dynamic>();
            try
            {
                bool IsInserted = _repo.Insert(model);
                if (IsInserted)
                {
                    response.Add("Status", true);
                    response.Add("Message", "Country Added Successfully");
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

        [HttpPut("CountryID")]
        public IActionResult Put(int CountryID, [FromForm] CountryModel model)
        {
            Dictionary<String, dynamic> response = new Dictionary<string, dynamic>();
            try
            {
                model.CountryID = CountryID;
                bool IsUpdated = _repo.Update(model);
                if (IsUpdated)
                {
                    response.Add("Status", true);
                    response.Add("Message", "Country Updated Successfully");
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
