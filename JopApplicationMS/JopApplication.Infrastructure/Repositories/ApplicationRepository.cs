using JopApplication.Core.Interfaces.Repositories;
using JopApplication.Core.Models;
using JopApplication.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JopApplication.Infrastructure.Repositories
{
    public class ApplicationRepository : IApplicationRepository
    {

        private readonly JobAppDbContext _dbContext;

        public ApplicationRepository(JobAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Application> AddAsync(Application entity)
        {
            await _dbContext.Applications.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return await GetByIdAsync(entity.Id);
        }

        public async Task<bool> AlreadyApplied(string userId, int jobId)
        {
            return await _dbContext.Applications
                .AnyAsync(a => a.JobId == jobId && a.UserId == userId);
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

        public async Task<IEnumerable<Application>> GetAllAsync()
        {
            return await _dbContext.Applications.Include(a => a.User).Include(a => a.Job).ToListAsync();
        }

        public async Task<List<Application>> GetAllByJobIdAsync(int id)
        {
            return await _dbContext.Applications.Where(a=>a.JobId == id).Include(a => a.User)
                .Include(a => a.Job)
                .ToListAsync();
        }

        public async Task<List<Application>> GetAllByUserIdAsync(string id)
        {
            return await _dbContext.Applications.Include(a => a.User).Include(a => a.Job).Where(a => a.UserId == id).ToListAsync();

        }

        public async Task<int> GetApplicationsCountAsync(int jobId)
        {
            return await _dbContext.Applications
                .CountAsync(a => a.JobId == jobId);
        }

        public async Task<Application?> GetByIdAsync(int id)
        {

            return await _dbContext.Applications.Include(a=>a.User).Include(a => a.Job)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Application?> UpdateAsync(int id, Application entity)
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
