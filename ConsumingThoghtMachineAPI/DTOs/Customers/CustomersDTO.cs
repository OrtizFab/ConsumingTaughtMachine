using ConsumingThoghtMachineAPI.Models.Customers;
using System.Security.Principal;
using static ConsumingThoghtMachineAPI.Models.Customers.Customer_Creation;

namespace ConsumingThoghtMachineAPI.DTOs.Customers
{
    
    public class CustomersDTO
    {
        public string? id { get; set; }
        public string status { get; set; }
        public List<Identifiers> identifiers { get; set; }
        public Customer_Details?  customer_details  { get; set; }

        public class Customer_Details
        {
            public string title { get; set; }
            public string first_name { get; set; }
            public string middle_name { get; set; }
            public string last_name { get; set; }
            public string dob { get; set; }
            public string gender { get; set; }
            public string nationality { get; set; }
            public string email_address { get; set; }
            public string mobile_phone_number { get; set; }
            public string home_phone_number { get; set; }
            public string business_phone_number { get; set; }
            public string contact_method { get; set; }
            public string country_of_residence { get; set; }
            public string country_of_taxation { get; set; }
            public string accessibility { get; set; }
        
        }
        public CustomersDTO()
        {
            identifiers = new List<Identifiers>();
            customer_details = new Customer_Details();  
           
        }

    }
}
