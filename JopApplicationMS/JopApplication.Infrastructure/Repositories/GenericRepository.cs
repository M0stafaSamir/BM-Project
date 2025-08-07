//using JopApplication.Core.Interfaces.Repositories;
//using JopApplication.Core.Models;
//using JopApplication.Infrastructure.DBContext;
//using JopApplication.Infrastructure.DBContexts;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace JopApplication.Infrastructure.Repositories
//{
//    public class GenericRepository<T> : IGenericRepository<T> where T : class
//    {
//        private readonly JobAppDbContext _context;
//        private readonly DbSet<T> _dbSet;

//        public GenericRepository(JobAppDbContext context)
//        {
//            _context = context;
//            _dbSet = context.Set<T>();

//        }

//        public async Task AddAsync(T entity)
//        {
//            await _dbSet.AddAsync(entity);
//            await _context.SaveChangesAsync();
//        }

//        public void Delete(T entity)
//        {
//            _dbSet.Remove(entity);
//            _context.SaveChanges();

//        }

//        public async Task<IEnumerable<T>> GetAllAsync()
//        {
//            return await _dbSet.ToListAsync();
//        }

//        public async Task<T?> GetByIdAsync(int id)
//        {
//            return await _dbSet.FindAsync(id);
//        }

//        public void Update(T entity)
//        {
//            _dbSet.Update(entity);
//            _context.SaveChanges();

//        }


//    }
//}
