using JopApplication.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JopApplication.Core.Interfaces.Repositories
{
    public interface IJobRepository : IGenericRepository<Job,int>
    {
        Task<IEnumerable<Job>?> SearchByTitleORCompanyAsync(string title,string company);
        Task<IEnumerable<Job>?> GetJobsBySalaryRangeAsync(decimal minSalary, decimal maxSalary);




    }

}
