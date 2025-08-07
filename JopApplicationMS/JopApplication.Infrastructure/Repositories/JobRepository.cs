using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JopApplication.Core.Interfaces.Repositories;
using JopApplication.Core.Models;
using JopApplication.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;

namespace JopApplication.Infrastructure.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly JobAppDbContext _dbContext;

        public JobRepository(JobAppDbContext dbContext)
        {
            _dbContext= dbContext;
        }

        public async Task<Job> AddAsync(Job entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return await GetByIdAsync(entity.Id);
        }

        public async Task<bool> DeleteAsync(int Id)
        {
           var job = await GetByIdAsync(Id);
            if (job != null) 
            {
                _dbContext.Remove(job);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Job>> GetAllAsync()
        {
            return await _dbContext.Jobs.Include(j => j.CreatedBy).Include(j => j.Applications).ToListAsync();
        }

        public async Task<Job?> GetByIdAsync(int id)
        {
            var job= await _dbContext.Jobs.Include(j=>j.CreatedBy).Include(j=>j.Applications).FirstOrDefaultAsync(j=>j.Id == id);

            if (job != null) 
            {
                return job;
            }
            return null;
        }

        public async Task<IEnumerable<Job>?> GetJobsBySalaryRangeAsync(decimal minSalary, decimal maxSalary)
        {
            return await _dbContext.Jobs
                .Include(j => j.CreatedBy)
                .Include(j => j.Applications)
                .Where(j => j.Salary >= minSalary && j.Salary <= maxSalary)
                .ToListAsync();
        }

        public async Task<IEnumerable<Job>?> SearchByTitleORCompanyAsync(string title, string company)
        {
            var query=  _dbContext.Jobs.Include(j => j.CreatedBy).Include(j => j.Applications).AsQueryable();

            if(!string.IsNullOrEmpty(title))
                query = query.Where(j => j.Title.ToLower().Contains(title.ToLower()));
            if (!string.IsNullOrEmpty(company))
                query = query.Where(j => j.Company.ToLower().Contains(company.ToLower()));

            return await query.ToListAsync();



        }

    

        public async Task<Job?> UpdateAsync(int id, Job entity)
        {
            if (entity != null) 
            {
                 _dbContext.Update(entity);
                await _dbContext.SaveChangesAsync();
                return await GetByIdAsync(entity.Id);
            }
            return null;
        }
    }
}
