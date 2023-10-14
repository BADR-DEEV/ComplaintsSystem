using System.Security.Claims;
using complainSystem.Helpers;
using complainSystem.models;
using complainSystem.models.ComplainDto;
using complainSystem.models.Complains;
using complainSystem.Services.ComplainService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace complainSystem.Controllers
{
    [Authorize (Roles = "Admin, User")]
    [ApiController]
    [Route("api/[controller]")]

    public class ComplaintController : ControllerBase
    {
        public IComplainService _complainService { get; }
        public ComplaintController(IComplainService complainService)
        {
            _complainService = complainService;
        }

        [HttpGet]
        [Route("GetComplaints")]

        public async Task<ActionResult<ServiceResponse<List<Complain>>>> GetAllComplains()
        {


            Helpers<List<Complain>> helper = new();
            return helper.HandleResponse(await _complainService.GetComplaints());
        }

        [HttpGet]
        [Route("GetComplaints/{id}")]
        public async Task<ActionResult<ServiceResponse<Complain>>> GetComplain(int id)
        {
            Helpers<Complain> helper = new();
            return helper.HandleResponse(await _complainService.GetComplaint(id));
        }

        [HttpDelete]
        [Route("DeleteComplaint/{id}")]
        public async Task<ActionResult<ServiceResponse<Complain>>> DeleteComplain(int id)
        {
            Helpers<Complain> helper = new();
            return helper.HandleResponse(await _complainService.DeleteComplaint(id));
        }

        [HttpPost]
        [Route("AddComplaint")]
        public async Task<ActionResult<ServiceResponse<Complain>>> AddComplain(AddComplainDto complain)
        {
            Helpers<Complain> helper = new();
            return helper.HandleResponse(await _complainService.AddComplaint(complain));
        }

        [HttpPut]
        [Route("UpdateComplaint/{id}")]
        public async Task<ActionResult<ServiceResponse<Complain>>> UpdateComplain(UpdateComplainDto complain)
        {
            Helpers<Complain> helper = new();
            return helper.HandleResponse(await _complainService.UpdateComplaint(complain));
        }

    }
}