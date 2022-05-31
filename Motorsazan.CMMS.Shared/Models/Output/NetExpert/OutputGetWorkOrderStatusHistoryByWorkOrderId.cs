namespace Motorsazan.CMMS.Shared.Models.Output.NetExpert
{
    public class OutputGetWorkOrderStatusHistoryByWorkOrderId
    {
        public long WorkOrderStatusId { get; set; }

        public string WorkOrderStatusTypeShowName { get; set; }

        public string UserName { get; set; }

        public string Description { get; set; }

        public string PersianStatusCreationDate { get; set; }
    }
}