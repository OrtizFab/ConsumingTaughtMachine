using Confluent.Kafka;
using ConsumingThoghtMachineAPI.DTOs.Accounts;
using ConsumingThoghtMachineAPI.Models.Accounts;
using ConsumingThoghtMachineAPI.Wrapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;

namespace ConsumingThoghtMachineAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountsController : Controller
    {
        JsonSerializerOptions options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
        private readonly IConfiguration _config;
        private readonly ProducerConfig _pConfig;

        public AccountsController(IConfiguration config, ProducerConfig pConfig)
        {
            _config = config;
            _pConfig = pConfig;
        }
        [HttpGet("Accounts")]
        public async Task<ActionResult> GetAccounts()
        {

            using (HttpClient client = new HttpClient())
            {
                // header
                client.DefaultRequestHeaders.Add("X-Auth-Token", _config.GetValue<string>(
                "Token"));
                try
                {
                    HttpResponseMessage response = await client.GetAsync(_config.GetValue<string>("baseUrl") + _config.GetValue<string>("uriAccount"));


                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        AccountsDTO? lst = JsonConvert.DeserializeObject<AccountsDTO>(content); 
                        if(lst != null)
                        {
                            return Ok(lst);
                        }
                     
                        return BadRequest("null");
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

        [HttpGet("account/{id}")]
        public async Task<ActionResult> GetAccountsbyId(string id)
        {
            
            using (HttpClient client = new HttpClient())
            {
                // header
                client.DefaultRequestHeaders.Add("X-Auth-Token", _config.GetValue<string>(
                "Token"));
                try
                {
                    HttpResponseMessage response = await client.GetAsync(_config.GetValue<string>("baseUrl") + "v1/accounts/" + id);


                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        Accounts? lst = JsonConvert.DeserializeObject<Accounts>(content);
                        string serializedOrder = JsonConvert.SerializeObject(lst);

                        Console.WriteLine("========");
                        Console.WriteLine("Info: AccountController => GET => Recieved a new account:");
                        Console.WriteLine(serializedOrder);
                        Console.WriteLine("=========");

                        var producer = new ProducerWrapper(this._pConfig, "mytopic");
                        await producer.writeMessage(serializedOrder);
                        return Ok(lst);
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

        [HttpPost("createAccount")]
        public async Task<ActionResult> postCustomer(Accounts accounts)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://core-api.partner-integrations-sandbox.tmachine.io/v1/accounts");
                request.Headers.Add("X-Auth-Token", "A0007739360961820061205!uHdAcHQpIeWXf2UPrttSRzbMPMtByDV83ehTr8Bl+ChVdFcMnxyZJD8L4yegPWY6weUKG8AnEiM2faOfRNqQXglFtbY=");

                var serializerSettings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                };

                var jsonData = JsonConvert.SerializeObject(accounts, serializerSettings);
                StringContent content = new StringContent(jsonData, null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                return Ok(await response.Content.ReadAsStringAsync());
            }
            catch {
                throw new Exception();
            }
           
        }          

    }
}
