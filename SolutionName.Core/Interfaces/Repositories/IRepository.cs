using SolutionName.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SolutionName.Core.Interfaces.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> GetById(int id);
        Task<List<T>> List();
        Task<T> Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task DeleteById(int id);
    }
}
