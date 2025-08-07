using JopApplication.Core.DTOs.Application;
using JopApplication.Core.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JopApplication.Services.Queries.Application
{
    public record GetAllUserApplicationsQuery(string userId):IRequest<ApiResponse<List<GetApplicationDto>>>
    {
    }
}
