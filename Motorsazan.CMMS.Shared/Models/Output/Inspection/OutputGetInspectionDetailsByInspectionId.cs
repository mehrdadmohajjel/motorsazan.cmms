namespace Motorsazan.CMMS.Shared.Models.Output.Inspection
{
    public class OutputGetInspectionDetailsByInspectionId
    {
        public long InspectionDetailId { get; set; }

        public string ActionName { get; set; }

        public decimal PersonHour { get; set; }
    }
}
