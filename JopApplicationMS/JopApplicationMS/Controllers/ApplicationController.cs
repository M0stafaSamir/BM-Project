using JopApplication.Core.DTOs.Application;
using JopApplication.Core.DTOs.Job;
using JopApplication.Core.Models;
using JopApplication.Core.Responses;
using JopApplication.Services.Commands.Application;
using JopApplication.Services.Queries.Application;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JopApplicationMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {

        private readonly IMediator _mediator;
        public ApplicationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "User")]
        [HttpPost("apply")]
        public async Task<IActionResult> assignToApplication([FromForm] CreateApplicationDto applicationDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (applicationDto == null)
            {
                return BadRequest(ApiResponse<string>.FailResponse("Application Faild"));
            }
            var createdApplication = await _mediator.Send(new CreateApplicationCommand(userId, applicationDto));

            if (!createdApplication.Success)
            {
                return BadRequest(createdApplication);

            }
            return Ok(createdApplication);
        }
        [Authorize(Roles = "User")]
        [HttpGet("myApplications")]
        public async Task<IActionResult> GetMyApplications()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var applications = await _mediator.Send(new GetAllUserApplicationsQuery(userId));
            if (applications == null)
            {
                return NotFound(applications);
            }
            return Ok(applications);

        }

        [Authorize(Roles = "Admin")]
        [HttpGet("JobApplication/{id}")]
        public async Task<IActionResult> GetAppilcationByJob(int id )
        {
            var applications = await _mediator.Send(new GetAllJobApplicationsQuery(id));
            if (applications == null)
            {
                return NotFound(applications);
            }
            return Ok(applications);

        }

        [Authorize(Roles = "User")]
        [HttpDelete("deleteApplication/{id}")]
        public async Task<IActionResult> DeleteApplication (int id)
        {
            var IsDeleted = await _mediator.Send(new DeleteApplicationCommand(id));
            if (IsDeleted.Success)
            {
                return Ok(IsDeleted);
            }
            return BadRequest(IsDeleted);


        }
    }
}
