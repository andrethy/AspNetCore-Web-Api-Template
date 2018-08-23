using SolutionName.Common.Model.Example;
using SolutionName.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SolutionName.Core.Interfaces.Services
{
    public interface IExampleService
    {
        Task<int> CreateExample(CreateExampleModel createExampleModel);
        Task<IEnumerable<Example>> GetAllExamples();
        Task DeleteAllExamples();
    }
}
