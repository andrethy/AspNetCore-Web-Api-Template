using Newtonsoft.Json;
using SolutionName.Common.Model.Example;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SolutionName.IntegrationTest.Controller
{
    public class ExampleControllerTest : BaseTest
    {
        [Fact]
        public async Task Test_CreateExample_Success()
        {
            //Arrange
            var content = JsonConvert.SerializeObject(new CreateExampleModel()
            {
                Name = "Test example",
                Age = 100
            });
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            // Act
            var response = await Client.PostAsync("/api/Example", stringContent);
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.True(response.IsSuccessStatusCode, "Response string: " + responseString);
            Assert.True(int.TryParse(responseString, out int throwaway), "Couldn't parse response string to an example Id");
        }
    }
}
