using AutoMapper;
using JopApplication.Core.DTOs.Job;
using JopApplication.Core.Interfaces.Repositories;
using JopApplication.Services.Commands.Job;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace JopApplication.Services.Handlers.Job
{
    public class UpdateJobHandler : IRequestHandler<UpdateJobCommand, GetJobDto>
    {
        private readonly IJobRepository _repository;
        private readonly IMapper _mapper;

        public UpdateJobHandler(IJobRepository jobRepository, IMapper mapper)
        {
            _repository = jobRepository;
            _mapper = mapper;

        }

        public async Task<GetJobDto> Handle(UpdateJobCommand request, CancellationToken cancellationToken)
        {
            var job = await _repository.GetByIdAsync(request.id);
            if(job == null)
                throw new KeyNotFoundException($"Job with ID {request.id} not found.");
          
            _mapper.Map(request.jobDto, job);

            var updaedJob = await _repository.UpdateAsync(request.id, job);
            return _mapper.Map(updaedJob, new GetJobDto());
        }
    }
}
