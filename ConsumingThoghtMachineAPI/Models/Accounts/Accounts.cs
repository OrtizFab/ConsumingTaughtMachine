namespace ConsumingThoghtMachineAPI.Models.Accounts
{
    public class Accounts
    {
        public string id { get; set; }
        public string name { get; set; }
        public string product_id { get; set; }
        public string product_version_id { get; set; }
        public List<string> stakeholder_ids { get; set; }
        public string status { get; set; }
        public string opening_timestamp { get; set; }
        public string closing_timestamp { get; set; }
       public List<string> permitted_denominations { get; set; }
        
        public Instance_param_vals instance_Param_Vals { get; set; }
        public Accounting Accounting { get; set; }
        
    }
}
