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
    public class GetAllUserApplicationsHandler : IRequestHandler<GetAllUserApplicationsQuery, ApiResponse<List<GetApplicationDto>>>
    {
        private readonly IApplicationRepository _repository;
        private readonly IMapper _mapper;


        public GetAllUserApplicationsHandler(IApplicationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ApiResponse<List<GetApplicationDto>>> Handle(GetAllUserApplicationsQuery request, CancellationToken cancellationToken)
        {
            var apps = await _repository.GetAllByUserIdAsync(request.userId);
            if (apps == null )
            {
                return ApiResponse<List<GetApplicationDto>>.FailResponse("No applications found for this user.");
            }
            var responseDtos = _mapper.Map<List<GetApplicationDto>>(apps);
            return ApiResponse<List<GetApplicationDto>>.SuccessResponse(responseDtos, "Applications retrieved successfully.");



        }
    }
}
