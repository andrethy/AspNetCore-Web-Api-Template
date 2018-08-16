using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SolutionName.Common.Model.Example;
using SolutionName.Core.Entities;
using SolutionName.Core.Interfaces.Repositories;
using SolutionName.Core.Interfaces.Services;

namespace SolutionName.Core.Services
{
    public class ExampleService : IExampleService
    {
        private readonly IExampleRepository exampleRepository;

        public ExampleService(IExampleRepository exampleRepository)
        {
            this.exampleRepository = exampleRepository;
        }

        public async Task CreateExample(CreateExampleModel createExampleModel)
        {
            var example = new Example()
            {
                CreatedDate = DateTime.Now,
                Name = createExampleModel.Name
            };

            await exampleRepository.Add(example);
        }

        public async Task DeleteAllExamples()
        {
            await exampleRepository.DeleteAllExamples();
        }

        public async Task<IEnumerable<Example>> GetAllExamples()
        {
            return await exampleRepository.GetAllExamples();
        }
    }
}
