namespace ConsumingThoghtMachineAPI.Models.Version
{
    public class VersionInfo
    {
        public Version version { get; set; }
        public string pre_release { get; set; }
        public string build { get; set; }
    }

    public class Version
    {
        public int major { get; set; }
        public int minor { get; set; }
        public int patch { get; set; }
        public string label { get; set; }
    }
}
