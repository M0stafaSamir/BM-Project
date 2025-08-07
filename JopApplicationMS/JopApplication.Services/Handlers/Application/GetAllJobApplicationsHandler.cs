using AutoMapper;
using JopApplication.Core.DTOs.Application;
using JopApplication.Core.Interfaces.Repositories;
using JopApplication.Core.Responses;
using JopApplication.Services.Queries.Application;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JopApplication.Services.Handlers.Application
{
    public class GetAllJobApplicationsHandler : IRequestHandler<GetAllJobApplicationsQuery, ApiResponse<List<GetApplicationDto>>>
    {

        private readonly IApplicationRepository _repository;
        private readonly IMapper _mapper;


        public GetAllJobApplicationsHandler(IApplicationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<ApiResponse<List<GetApplicationDto>>> Handle(GetAllJobApplicationsQuery request, CancellationToken cancellationToken)
        {
            var applications = await _repository.GetAllByJobIdAsync(request.Jobid);
            if (applications == null )
            {
                return ApiResponse<List<GetApplicationDto>>.FailResponse("No applications found for this job.");
            }
            var responseDto = _mapper.Map<List<GetApplicationDto>>(applications);
            return ApiResponse<List<GetApplicationDto>>.SuccessResponse(responseDto, "Applications retrieved successfully.");

        }
    }
}
