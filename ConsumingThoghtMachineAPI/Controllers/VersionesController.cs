using ConsumingThoghtMachineAPI.Models.Customers;
using ConsumingThoghtMachineAPI.Models.Version;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;


namespace ConsumingThoghtMachineAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VersionesController : Controller
    {     
       
        JsonSerializerOptions options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
        private readonly IConfiguration _config;

        public VersionesController(IConfiguration config)
        {
            _config = config;
        }
        [HttpGet("vault-version")]
        public async Task<ActionResult> GetVaultVersion()
        {

            
            using (HttpClient client = new HttpClient())
            {
                // header
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));              
                client.DefaultRequestHeaders.Add("X-Auth-Token", _config.GetValue<string>(
                "Token"));

                try
                {
                    HttpResponseMessage response = await client.GetAsync(_config.GetValue<string>("baseUrl") + _config.GetValue<string>("uriVersion"));

                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                       VersionInfo? generalVersion = JsonConvert.DeserializeObject<VersionInfo>(content);
                        return Ok(generalVersion);
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

       
        

        

        [HttpPost("customerPostmanV2")]
        public async Task<ActionResult> AddNewConsumerPostmanV2(Customer_Creation customer)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://core-api.partner-integrations-sandbox.tmachine.io/v1/customers");
            request.Headers.Add("X-Auth-Token", "A0007739360961820061205!uHdAcHQpIeWXf2UPrttSRzbMPMtByDV83ehTr8Bl+ChVdFcMnxyZJD8L4yegPWY6weUKG8AnEiM2faOfRNqQXglFtbY=");



            var serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };



            var jsonData = JsonConvert.SerializeObject(customer, serializerSettings);
            StringContent content = new StringContent(jsonData, null, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return Ok(await response.Content.ReadAsStringAsync());
        }
    }
}
    