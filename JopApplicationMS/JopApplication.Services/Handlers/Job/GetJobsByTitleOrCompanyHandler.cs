using AutoMapper;
using JopApplication.Core.DTOs.Job;
using JopApplication.Core.Interfaces.Repositories;
using JopApplication.Services.Queries.Job;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JopApplication.Services.Handlers.Job
{
    public class GetJobsByTitleOrCompanyHandler : IRequestHandler<GetJobsByTitleOrCompanyQuery, List<GetJobDto>>
    {
        private readonly IJobRepository _repository;
        private readonly IMapper _mapper;

        public GetJobsByTitleOrCompanyHandler(IJobRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetJobDto>> Handle(GetJobsByTitleOrCompanyQuery request, CancellationToken cancellationToken)
        {


            var jobs = await _repository.SearchByTitleORCompanyAsync(request.title,request.company);
            if (jobs == null )
            {
                return new List<GetJobDto>();
            }
            return _mapper.Map<List<GetJobDto>>(jobs);

        }
    }
}
