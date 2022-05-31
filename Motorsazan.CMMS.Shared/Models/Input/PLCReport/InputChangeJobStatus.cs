namespace Motorsazan.CMMS.Shared.Models.Input.PLCReport
{
    public class InputChangeJobStatus
    {
        public string jobName { get; set; }
        public int jobNewStatus { get; set; }
        public string projectName { get; set; }
    }
}