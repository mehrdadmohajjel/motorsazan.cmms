namespace Motorsazan.CMMS.Shared.Models.Output.MachineManagement
{
    public class OutputGetMachineBuiltInfo
    {
        public string MachineName { get; set; }
        public string OldMachineCode { get; set; }
        public string Model { get; set; }
        public string ImportDate { get; set; }
        public string CountryName { get; set; }
        public string BuiltYear { get; set; }
        public string MachineLocation { get; set; }
        public string MachineLevelName { get; set; }
    }
}