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

        [HttpPost]
        public IActionResult Post([FromForm] StateModel model)
        {
            Dictionary<String, dynamic> response = new Dictionary<string, dynamic>();
            try
            {
                bool IsInserted = _StateRepo.Insert(model);
                if (IsInserted)
                {
                    response.Add("Status", true);
                    response.Add("Message", "State Added Successfully");
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

        [HttpPut("StateID")]
        public IActionResult Put(int StateID, [FromForm] StateModel model)
        {
            Dictionary<String, dynamic> response = new Dictionary<string, dynamic>();
            try
            {
                model.StateID = StateID;
                bool IsUpdated = _StateRepo.Update(model);
                if (IsUpdated)
                {
                    response.Add("Status", true);
                    response.Add("Message", "State Updated Successfully");
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

        [HttpGet("StateDropdown")]
        public IActionResult StateDropdown()
        {
            List<StateDropDown> stateDropDown = _StateRepo.State_DropDown();
            return Ok(stateDropDown);
        }
    }
}
