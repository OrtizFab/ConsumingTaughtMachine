using ConsumingThoghtMachineAPI.Models.Accounts;

namespace ConsumingThoghtMachineAPI.DTOs.Accounts
{
    public class AccountsDTO
    {
        public List<Accounts> accounts  { get; set; }
        public Instance_param_vals instance_param_vals { get; set; }
        public Derived_instance_param_vals derived_instance_Param_Vals { get; set; }
        public Accounting accounting { get; set; }
       
        public class Accounts
        {
            public string id { get; set; }
            public string name { get; set; }
            public string product_id { get; set; }
            public string product_version_id { get; set; }
            public List<string> permitted_denominations { get; set; }
            public string status { get; set; }
            public string opening_timestamp { get; set; }
            public string? closing_timestamp { get; set; }
            public List<string> stakeholder_ids { get; set; }
            public  Instance_param_vals instance_Param_vals { get; set; }
        }
        
       
        public class Instance_param_vals
        {
            public string initial_deposit { get; set; }
            public string linked_acccount_id { get; set; }
            public string total_ftb_in_months { get; set; }
            
        }

        public class Derived_instance_param_vals
        {

        }
        public AccountsDTO()
        {
            accounts = new List<Accounts>();
            instance_param_vals = new Instance_param_vals();
            derived_instance_Param_Vals = new Derived_instance_param_vals();
            accounting = new Accounting();
        }
    }
}
