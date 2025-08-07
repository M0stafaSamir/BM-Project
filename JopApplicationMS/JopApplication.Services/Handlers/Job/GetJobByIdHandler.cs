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
    public class GetJobByIdHandler : IRequestHandler<GetJobByIdQuery, GetJobDto>
    {
        private readonly IJobRepository _repository;
        private readonly IMapper _mapper;

        public GetJobByIdHandler(IJobRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetJobDto?> Handle(GetJobByIdQuery request, CancellationToken cancellationToken)
        {
            var job = await _repository.GetByIdAsync(request.Id);

            return job != null
                ? _mapper.Map<GetJobDto>(job)
                : null;
        }
    }
}
