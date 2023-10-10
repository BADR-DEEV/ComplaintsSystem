using complainSystem.Helpers;
using complainSystem.models;
using complainSystem.models.ComplainDto;
using complainSystem.models.Complains;
using complainSystem.Services.ComplainService;

using Microsoft.AspNetCore.Mvc;

namespace complainSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComplainController : ControllerBase
    {
        public IComplainService _complainService { get; }
        public ComplainController(IComplainService complainService)
        {
            _complainService = complainService;
        }

        [HttpGet]
        [Route("GetAllComplains")]
        public async Task<ActionResult<ServiceResponse<List<Complain>>>> GetAllComplains()
        {
            Helpers<List<Complain>> helper = new();
            return helper.HandleResponse(await _complainService.GetComplaints());
        }

        [HttpGet]
        [Route("GetComplain/{id}")]
        public async Task<ActionResult<ServiceResponse<Complain>>> GetComplain(int id)
        {
            Helpers<Complain> helper = new();
            return helper.HandleResponse(await _complainService.GetComplaint(id));
        }

        [HttpDelete]
        [Route("DeleteComplain/{id}")]
        public async Task<ActionResult<ServiceResponse<Complain>>> DeleteComplain(int id)
        {
            Helpers<Complain> helper = new();
            return helper.HandleResponse(await _complainService.DeleteComplaint(id));
        }

        [HttpPost]
        [Route("AddComplain")]
        public async Task<ActionResult<ServiceResponse<Complain>>> AddComplain(AddComplainDto complain)
        {
            Helpers<Complain> helper = new();
            return helper.HandleResponse(await _complainService.AddComplaint(complain));
        }

        [HttpPut]
        [Route("UpdateComplain/{id}")]
        public async Task<ActionResult<ServiceResponse<Complain>>> UpdateComplain(UpdateComplainDto complain)
        {
            Helpers<Complain> helper = new();
            return helper.HandleResponse(await _complainService.UpdateComplaint(complain));
        }

    }
}