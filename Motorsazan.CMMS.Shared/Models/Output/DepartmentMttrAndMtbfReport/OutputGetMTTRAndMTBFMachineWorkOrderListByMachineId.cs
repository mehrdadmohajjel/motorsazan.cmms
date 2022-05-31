namespace Motorsazan.CMMS.Shared.Models.Output.DepartmentMttrAndMtbfReport
{
    public class OutputGetMTTRAndMTBFMachineWorkOrderListByMachineId
    {
        public long WorkOrderId { get; set; }

        public string WorkOrderTypeTitle { get; set; }

        public string WorkOrderCreationDate { get; set; }

        public string StopTypeTitle { get; set; }

        public string MaintenanceGroupName { get; set; }

        public string OldMachineCode { get; set; }
       
        public string MachineName { get; set; }

        public string WorkOrderSerial { get; set; }

        public string OperationCode { get; set; }

        public string OperationItemName { get; set; }

        public string Description { get; set; }

        public string WorkOrderStatusTypeShowName { get; set; }


    }
}
