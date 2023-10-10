using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using complainSystem.models;
using Microsoft.AspNetCore.Mvc;

namespace complainSystem.Helpers
{
    public class Helpers<T> : ControllerBase
    {
        public ServiceResponse<T> response { get; set; } = new ServiceResponse<T>();
        public ActionResult<ServiceResponse<T>> HandleResponse(ServiceResponse<T> response)
        {
            switch (response.StatusCode)
            {
                case 500:
                    return StatusCode(500, response);

                case 400:
                    return BadRequest(response);
                case 200:
                    return Ok(response);
                case 404:
                    return NotFound(response);

                default:
                    return BadRequest(response);
            }
        }

        

    }
}