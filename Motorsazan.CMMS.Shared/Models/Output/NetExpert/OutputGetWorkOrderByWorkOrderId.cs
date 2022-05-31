using System;

namespace Motorsazan.CMMS.Shared.Models.Output.NetExpert
{
    public class OutputGetWorkOrderByWorkOrderId
    {
        public long WorkOrderId { get; set; }

        public long WorkOrderSerial { get; set; }

        public long WorkOrderTypeId { get; set; }

        public string WorkOrderTypeTitle { get; set; }

        public int UserId { get; set; }

        public string RegistrarUser { get; set; }

        public long ReceiverMaintenanceGroupId { get; set; }

        public string MaintenanceGroupName { get; set; }

        public long PmSchedulingInfoId { get; set; }

        public long MachineId { get; set; }

        public string OldMachineCode { get; set; }

        public int DepartmentId { get; set; }

        public string DepartmentTitle { get; set; }

        public long OperationItemId { get; set; }

        public long FaultOperationItemId { get; set; }

        public long StopTypeId { get; set; }

        public long RelativeWorkOrderId { get; set; }

        public string Description { get; set; }

        public bool IsPrinted { get; set; }

        public bool HasScrap { get; set; }

        public string ScrapLicenseNumber { get; set; }

        public string ScrapSolution { get; set; }

        public string ScrapReview { get; set; }

        public bool IsScrapAccepted { get; set; }

        public int RepairingRate { get; set; }

        public DateTime CreationDateTime { get; set; }

        public string ShamsiCreationDateTime { get; set; }

        public string CreationTime { get; set; }

        public int DurationInMinute { get; set; }

        public string FailureDescription { get; set; }

        public long WorkOrderStatusTypeId { get; set; }
    }
}