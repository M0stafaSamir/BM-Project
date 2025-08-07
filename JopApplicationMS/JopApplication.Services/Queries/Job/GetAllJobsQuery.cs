using JopApplication.Core.DTOs.Job;
using JopApplication.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JopApplication.Services.Queries.Job
{
    public record GetAllJobsQuery:IRequest<List<GetJobDto>>;
  
}
