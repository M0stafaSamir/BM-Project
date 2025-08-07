using JopApplication.Core.Interfaces.Repositories;
using JopApplication.Core.Responses;
using JopApplication.Services.Commands.Application;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JopApplication.Services.Handlers.Application
{
    public class DeleteApplicationHandler : IRequestHandler<DeleteApplicationCommand, ApiResponse<bool>>
    {

        private readonly IApplicationRepository _repository;


        public DeleteApplicationHandler(IApplicationRepository repository)
        {
            _repository = repository;
        }


        public async Task<ApiResponse<bool>> Handle(DeleteApplicationCommand request, CancellationToken cancellationToken)
        {
            var isDeleted= await _repository.DeleteAsync(request.applicationId);
            if (isDeleted)
            {
                return ApiResponse<bool>.SuccessResponse(isDeleted, "Application Remove Successfuly!");
            }
            return ApiResponse<bool>.FailResponse("Faild to remove Application");
        }
    }
}
