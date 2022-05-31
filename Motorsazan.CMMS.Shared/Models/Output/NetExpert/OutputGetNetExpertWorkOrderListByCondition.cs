namespace Motorsazan.CMMS.Shared.Models.Output.NetExpert
{
    public class OutputGetNetExpertWorkOrderListByCondition
    {
        public long WorkOrderId { get; set; }

        public string WorkOrderStatusTypeId { get; set; }

        public string WorkOrderStatusTypeName { get; set; }

        public string StopTypeTitle { get; set; }

        public int WorkOrderSerial { get; set; }

        public string MachineName { get; set; }

        public string OldMachineCode { get; set; }

        public long WorkOrderTypeId { get; set; }

        public string WorkOrderTypeTitle { get; set; }

        public string CreationPersianDate { get; set; }

        public string CreationTime { get; set; }

        public string DepartmentName { get; set; }

        public string MachineFaultCode { get; set; }

        public string Description { get; set; }

        public long? MachineId { get; set; }

        public int DepartmentId { get; set; }
        
        public string OperationCode { get; set; }

        public string WorkOrderHavalehNOStatus { get; set; }
    }
}
