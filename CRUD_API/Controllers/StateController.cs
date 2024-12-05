using CRUD_API.Data;
using CRUD_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly StateRepository _StateRepo;
        public StateController(StateRepository stateRepo)
        {
            _StateRepo = stateRepo;
        }

        [HttpGet]
        public IActionResult SelectAllStates()
        {
            List<StateModel> states = _StateRepo.SelectAll();
            return Ok(states);
        }

        [HttpDelete("{StateID}")]
        public IActionResult Delete(int StateID)
        {
            bool IsDeleted = _StateRepo.Delete(StateID);
            if (IsDeleted)
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
