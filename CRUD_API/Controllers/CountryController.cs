using CRUD_API.Data;
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
    }
}
