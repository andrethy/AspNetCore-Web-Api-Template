using Microsoft.EntityFrameworkCore;
using SolutionName.Core.Entities;
using SolutionName.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolutionName.Infrastructure.Data
{
    public class ExampleRepository : Repository<Example>, IExampleRepository
    {
        private readonly ExampleContext context;

        public ExampleRepository(ExampleContext context) : base(context)
        {
            this.context = context;
        }

        /// <summary>
        /// Deletes all entries of the entity 'Example'
        /// </summary>
        /// <returns></returns>
        public async Task DeleteAllExamples()
        {
            var examples = await List();
            context.RemoveRange(examples);
            await context.SaveChangesAsync();
        }
    }
}
