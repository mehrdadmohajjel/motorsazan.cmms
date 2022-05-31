namespace Motorsazan.CMMS.Shared.Models.Output.NetExpert
{
    public class OutputGetInspectionDetailByWorkOrderId
    {
        public long InspectionDetailId { get; set; }

        public string ActionName { get; set; }

        public decimal PersonHour { get; set; }

        public long MaintenanceGroupId { get; set; }



    }
}
