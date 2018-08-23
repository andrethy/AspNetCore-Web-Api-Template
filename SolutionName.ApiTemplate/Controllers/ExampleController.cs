using Microsoft.AspNetCore.Mvc;
using SolutionName.Common.Model.Example;
using SolutionName.Core.Entities;
using SolutionName.Core.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SolutionName.ApiTemplate.Controllers
{
    //Note the missing [ApiController]! 
    //The ApiController attribute would overwrite the ValidateModelStateFilter MVC filter, and eliminate the custom error handling for modelstates
    [Produces("application/json")]
    [Route("api/[controller]")]
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
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
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
        [ProducesResponseType(500)]
        public async Task<IActionResult> PostExample([FromBody] CreateExampleModel createExampleModel)
        {
            var createdExampleId = await exampleService.CreateExample(createExampleModel);
            return Ok(createdExampleId);
        }

        /// <summary>
        /// Deletes all "Example" entities
        /// </summary>
        [HttpDelete]
        [Route("DeleteAllExamples")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteAllExamples()
        {
            await exampleService.DeleteAllExamples();
            return Ok();
        }
    }
}