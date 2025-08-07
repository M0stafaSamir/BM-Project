using JopApplication.Core.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JopApplication.Services.Commands.Application
{
    public record DeleteApplicationCommand(int applicationId):IRequest<ApiResponse<bool>>
    {
    }
}
