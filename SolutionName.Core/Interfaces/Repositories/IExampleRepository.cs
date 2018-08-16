using SolutionName.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SolutionName.Core.Interfaces.Repositories
{
    //Use this repository to expand on functionality from IRepository
    public interface IExampleRepository : IRepository<Example>
    {
        Task DeleteAllExamples();
        Task<IEnumerable<Example>> GetAllExamples();
    }
}
