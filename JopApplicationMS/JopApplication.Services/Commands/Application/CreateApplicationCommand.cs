using JopApplication.Core.DTOs.Application;
using JopApplication.Core.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JopApplication.Services.Commands.Application
{
    public record CreateApplicationCommand(string userId,CreateApplicationDto createApplicationDto) : IRequest<ApiResponse<GetApplicationDto>>
    {
    }
}
