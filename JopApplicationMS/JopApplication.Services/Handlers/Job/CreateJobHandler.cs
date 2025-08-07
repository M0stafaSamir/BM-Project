using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JopApplication.Core.Interfaces.Repositories;
using AutoMapper;
using JopApplication.Core.DTOs.Job;
using JopApplication.Services.Commands.Job;

namespace JopApplication.Services.Handlers.Job
{


    internal class CreateJobHandler : IRequestHandler<CreateJobCommand, GetJobDto>
    {
        private readonly IJobRepository _repository;
        private readonly IMapper _mapper;

        public CreateJobHandler(IJobRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<GetJobDto> Handle(CreateJobCommand request, CancellationToken cancellationToken)
        {
            var job = _mapper.Map<JopApplication.Core.Models.Job>(request.jobDto);
            var cretedJob = await _repository.AddAsync(job);
            return _mapper.Map<GetJobDto>(cretedJob);
        }
    }
}
