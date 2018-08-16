using System.Threading.Tasks;
using SolutionName.Common.Model.Example;
using SolutionName.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SolutionName.Core.Entities;

namespace SolutionName.ApiTemplate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExampleController : ControllerBase
    {
        private readonly IExampleService exampleService;

        public ExampleController(IExampleService exampleService)
        {
            this.exampleService = exampleService;
        }

        /// <summary>
        /// Gets a collection of all "Example" entities
        /// </summary>
        /// <returns>A list of all "Example" entities</returns>
        [HttpGet]
        public async Task<IEnumerable<Example>> GetAllExamples()
        {
            return await exampleService.GetAllExamples();
        }

        /// <summary>
        /// Creates an "Example" entity
        /// </summary>
        /// <param name="createExampleModel">The model giving the necessary attributes to make an "Example"</param>
        [HttpPost]
        [ProducesResponseType(200)]
        public async Task<IActionResult> PostExample(CreateExampleModel createExampleModel)
        {
            await exampleService.CreateExample(createExampleModel);
            return Ok();
        }

        /// <summary>
        /// Deletes all "Example" entities
        /// </summary>
        [HttpDelete]
        [Route("DeleteAllExamples")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> DeleteAllExamples()
        {
            await exampleService.DeleteAllExamples();
            return Ok();
        }
    }
}