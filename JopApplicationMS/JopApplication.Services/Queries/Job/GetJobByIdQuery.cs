using JopApplication.Core.DTOs.Job;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JopApplication.Services.Queries.Job
{
    public record GetJobByIdQuery(int Id) : IRequest<GetJobDto>
    {
    }
}
