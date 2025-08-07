using JopApplication.Core.DTOs.Job;
using JopApplication.Core.Interfaces.Repositories;
using JopApplication.Core.Models;
using JopApplication.Core.Responses;
using JopApplication.Infrastructure.DBContexts;
using JopApplication.Services.Commands;
using JopApplication.Services.Commands.Job;
using JopApplication.Services.Queries;
using JopApplication.Services.Queries.Job;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Threading.Tasks;

namespace JopApplicationMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {

        private readonly IMediator _mediator;




        public JobController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateJob([FromBody] CreateJobDto jobDto)
        {
            Log.Information("Creating a new job with details: {@jobb}", jobDto);

            if (jobDto == null)
            {
                return BadRequest(ApiResponse<string>.FailResponse("Job Creation Faild"));
            }
            var createdJob = await _mediator.Send(new CreateJobCommand(jobDto));

            Log.Information("Created job with details: {@jobb}", createdJob);

            return Ok(ApiResponse<GetJobDto>.SuccessResponse(createdJob, "Job Added Successfuly"));
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllJobs()
        {
            var jobs = await _mediator.Send(new GetAllJobsQuery());
            if (jobs == null || jobs.Count == 0)
            {
                return NotFound(ApiResponse<List<GetJobDto>>.FailResponse("No Jobs Found"));
            }

            return Ok(ApiResponse<List<GetJobDto>>.SuccessResponse(jobs, "Jobs Retrieved Successfully"));

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJob([FromBody] UpdateJobDto jobDto, int id)
        {
            if (jobDto == null)
            {
                return BadRequest(ApiResponse<string>.FailResponse("Invalid Job Data"));
            }
            var updatedJob = await _mediator.Send(new UpdateJobCommand(jobDto, id));
            Log.Information("Updated job with ID {JobId} with details: {@JobDto}", id, jobDto);
            return Ok(ApiResponse<GetJobDto>.SuccessResponse(updatedJob, "Job Updated Successfully"));


        }
        [HttpGet("search")]
        public async Task<IActionResult> GetJobByTitleOrCompany([FromQuery] string? title, string? company)
        {
            if (string.IsNullOrWhiteSpace(title) && string.IsNullOrWhiteSpace(company))
            {
                return BadRequest(ApiResponse<string>.FailResponse("At least one search parameter (title or company) must be provided."));
            }

            var jobs = await _mediator.Send(new GetJobsByTitleOrCompanyQuery(title ?? string.Empty, company ?? string.Empty));
            if (jobs == null || jobs.Count == 0)
            {
                return NotFound(ApiResponse<List<GetJobDto>>.FailResponse("No Jobs Found"));
            }
            return Ok(ApiResponse<List<GetJobDto>>.SuccessResponse(jobs, "Jobs Retrieved Successfully"));
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet("getById/{id:int}")]
        public async Task<IActionResult> GetJobById(int id)
        {
            var job = await _mediator.Send(new GetJobByIdQuery(id));
            if (job == null)
            {
                return NotFound(ApiResponse<GetJobDto>.FailResponse("Job Not Found"));
            }
            return Ok(ApiResponse<GetJobDto>.SuccessResponse(job, "Job Retrieved Successfully"));
        }



    }
}
