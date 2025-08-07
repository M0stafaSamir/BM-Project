using JopApplication.Core.DTOs.Job;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JopApplication.Services.Commands.Job
{
    public record UpdateJobCommand(UpdateJobDto jobDto,int id):IRequest<GetJobDto>
    {
    }
}
