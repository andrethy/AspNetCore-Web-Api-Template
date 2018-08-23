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

        /// <summary>
        /// Adds the entity, and returns it with updated properties, such as Id.
        /// </summary>
        /// <param name="entity">The entity to add to the database</param>
        /// <returns></returns>
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

        /// <summary>
        /// Deletes the entity
        /// </summary>
        /// <param name="entity">The entity to be deleted</param>
        /// <returns></returns>
        public async Task Delete(T entity)
        {
            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes the entity by its Id
        /// </summary>
        /// <param name="id">The id of the entity</param>
        /// <returns></returns>
        public async Task DeleteById(int id)
        {
            var entity = await GetById(id);
            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Gets an entity by its Id
        /// </summary>
        /// <param name="id">The id of the entity</param>
        /// <returns></returns>
        public async Task<T> GetById(int id)
        {
            return await context.Set<T>().SingleOrDefaultAsync(e => e.Id == id);
        }

        /// <summary>
        /// Returns a list of all entries of the entity
        /// </summary>
        /// <returns></returns>
        public async Task<List<T>> List()
        {
            return await context.Set<T>().ToListAsync();
        }

        /// <summary>
        /// Update the specified entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task Update(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
    }
}
