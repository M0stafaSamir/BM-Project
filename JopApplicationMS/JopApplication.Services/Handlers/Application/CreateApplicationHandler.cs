using AutoMapper;
using JopApplication.Core.DTOs.Application;
using JopApplication.Core.DTOs.Job;
using JopApplication.Core.Interfaces.Repositories;
using JopApplication.Core.Responses;
using JopApplication.Services.Commands.Application;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JopApplication.Services.Handlers.Application
{
    public class CreateApplicationHandler : IRequestHandler<CreateApplicationCommand, ApiResponse<GetApplicationDto>>
    {

        private readonly IApplicationRepository _repository;
        private readonly IMapper _mapper;


        public CreateApplicationHandler(IApplicationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ApiResponse<GetApplicationDto>> Handle(CreateApplicationCommand request, CancellationToken cancellationToken)
        {
            int ApplicarionsCount = await _repository.GetApplicationsCountAsync(request.createApplicationDto.JobId);
            if (ApplicarionsCount > 2) 
                return ApiResponse<GetApplicationDto>.FailResponse("Application for this job has ended more than 2 applied");


            bool alreadyApplied = await _repository.AlreadyApplied(request.userId, request.createApplicationDto.JobId);
            if (alreadyApplied)
                return ApiResponse<GetApplicationDto>.FailResponse("You have already applied for this job");

            //file upload 
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(request.createApplicationDto.ResumeFile.FileName)}";
            var filePath = Path.Combine(uploadsFolder, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await request.createApplicationDto.ResumeFile.CopyToAsync(stream);
            }
            var relativePath = $"/uploads/{fileName}";
            ////////

            var application = _mapper.Map<JopApplication.Core.Models.Application> (request.createApplicationDto);
            application.Resume = relativePath;
            application.UserId = request.userId;
            var cretedApplication = await _repository.AddAsync(application);
            var responseDto = _mapper.Map<GetApplicationDto>(cretedApplication);

            return ApiResponse<GetApplicationDto>.SuccessResponse(responseDto, "Applied to this job Successfully");
        }


    }
}
