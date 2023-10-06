using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            return Ok(await _complainService.GetComplaints());


        }
        [HttpGet]
        [Route("GetComplain/{id}")]
        public async Task<ActionResult<ServiceResponse<Complain>>> GetComplain(int id)
        {
            return Ok(await _complainService.GetComplaint(id));
        }

        [HttpDelete]
        [Route("DeleteComplain/{id}")]
        public async Task<ActionResult<ServiceResponse<Complain>>> DeleteComplain(int id)
        {
            return Ok(await _complainService.DeleteComplaint(id));
        }
        [HttpPost]
        [Route("AddComplain")]
        public async Task<ActionResult<ServiceResponse<AddComplainDto>>> AddComplain(AddComplainDto complain)
        {
            return Ok(await _complainService.AddComplaint(complain));
        }
        
    }
}