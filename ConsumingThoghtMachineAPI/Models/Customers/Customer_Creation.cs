using static ConsumingThoghtMachineAPI.Models.Customers.Customer_Creation;


namespace ConsumingThoghtMachineAPI.Models.Customers
{
    public class Customer_Creation
    {

        public string request_id { get; set; }
        public Customer customer { get; set; }
        

        public class CustomerDetails
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

    }
    public class Customer
    {
        public string id { get; set; }
        public string status { get; set; }
        public List<Identifiers> identifiers { get; set; }
        public CustomerDetails customer_details { get; set; }
        // public additional_details additional_Details { get; set; }

    }

}

