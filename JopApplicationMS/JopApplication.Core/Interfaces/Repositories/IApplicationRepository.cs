using JopApplication.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JopApplication.Core.Interfaces.Repositories
{
    public interface IApplicationRepository:IGenericRepository<Application,int>
    {
        Task<List<Application>> GetAllByUserIdAsync(string id);
        Task<List<Application>> GetAllByJobIdAsync(int id);

        Task<bool> AlreadyApplied(string userId,int jobId);

        Task<int> GetApplicationsCountAsync(int jobId);
    }
}
