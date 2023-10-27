using Aspose.Cells.Utility;
using Aspose.Cells;
using Confluent.Kafka;
using ConsumingThoghtMachineAPI.DTOs.Customers;
using ConsumingThoghtMachineAPI.Models.Customers;
using ConsumingThoghtMachineAPI.Models.Version;
using ConsumingThoghtMachineAPI.Repositories;
using ConsumingThoghtMachineAPI.Wrapper;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Reflection.PortableExecutable;
using System.Text.Json;

namespace ConsumingThoghtMachineAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : Controller
    {
        JsonSerializerOptions options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
        private readonly IConfiguration _config;
        private readonly ProducerConfig _pConfig;
        private readonly ICSVService _service;

        public CustomersController(IConfiguration config, ICSVService service, ProducerConfig pconfig)
        {
            _config = config;
            _pConfig = pconfig;
            _service = service;
        }
        [HttpGet("Customers")]
        public async Task<ActionResult> GetCustomers()
        {

            using (HttpClient client = new HttpClient())
            {
                // header
                client.DefaultRequestHeaders.Add("X-Auth-Token", _config.GetValue<string>(
                "Token"));

                try
                {
                    HttpResponseMessage response = await client.GetAsync(_config.GetValue<string>("baseUrl") + _config.GetValue<string>("uriCustomers"));


                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        // List<Clients> lst = new List<Clients>();
                        Customers_out lst = JsonConvert.DeserializeObject<Customers_out>(content);
                        //createSCV("files.csv");
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

        [HttpGet("customers/{id}")]
        public async Task<ActionResult> GetCustomersbyId(string id)
        {

            using (HttpClient client = new HttpClient())
            {
                // header
                client.DefaultRequestHeaders.Add("X-Auth-Token", _config.GetValue<string>(
                "Token"));

                try
                {
                    HttpResponseMessage response = await client.GetAsync(_config.GetValue<string>("baseUrl") + "v1/customers/" + id);


                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        CustomersDTO? lst = JsonConvert.DeserializeObject<CustomersDTO>(content);
                        createSCV(content);
                       
                        //Serialize 
                        //string serializedOrder = JsonConvert.SerializeObject(lst);

                        //Console.WriteLine("========");
                        //Console.WriteLine("Info: CustomersController => GET => Recieved a new customer:");
                        //Console.WriteLine(serializedOrder);
                        //Console.WriteLine("=========");

                        //var producer = new ProducerWrapper(this._pConfig, "payment");
                        //await producer.writeMessage(serializedOrder);
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
        [HttpPost("createCustomer")]
        public async Task<ActionResult> postCustoumer(Customer_Creation customer)
        {
            try
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
               createSCV(await response.Content.ReadAsStringAsync());
                return Ok(await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void createSCV(string content)
        {
            long n = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
            // create a  Workbook object empty
            var workbook = new Workbook();

            // accede to the default empty sheet
            var worksheet = workbook.Worksheets[0];

            // establish JsonLayoutOptions to format
            var layoutOptions = new JsonLayoutOptions();
            layoutOptions.ArrayAsTable = false;

            // import JSON data  to CSV
            JsonUtility.ImportData(content, worksheet.Cells, 0, 0, layoutOptions);

            // save  CSV file
            workbook.Save("lst" + n + ".csv", SaveFormat.CSV);
        }

     }
}
