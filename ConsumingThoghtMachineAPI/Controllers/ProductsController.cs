using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ConsumingThoghtMachineAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : Controller
    {
        JsonSerializerOptions options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
        private readonly IConfiguration _config;

        public ProductsController(IConfiguration config)
        {
            _config = config;
        }
        [HttpGet("Products")]
        public async Task<ActionResult> GetProducts()
        {

            using (HttpClient client = new HttpClient())
            {
                // header
                client.DefaultRequestHeaders.Add("X-Auth-Token", _config.GetValue<string>(
                "Token"));

                try
                {
                    HttpResponseMessage response = await client.GetAsync(_config.GetValue<string>("baseUrl") + _config.GetValue<string>("uriProducts"));


                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        return Ok(content);
                    }
                    else
                    {
                        return BadRequest(response.StatusCode);
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
