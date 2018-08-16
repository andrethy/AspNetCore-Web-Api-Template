using Microsoft.EntityFrameworkCore;
using SolutionName.Core.Entities;
using SolutionName.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SolutionName.Infrastructure.Data
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ExampleContext context;

        public Repository(ExampleContext context)
        {
            this.context = context;
        }

        public async Task<T> Add(T entity)
        {
            try
            {
                context.Set<T>().Add(entity);
                await context.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Delete(T entity)
        {
            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            var entity = await GetById(id);
            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await context.Set<T>().SingleOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<T>> List()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task Update(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
    }
}
