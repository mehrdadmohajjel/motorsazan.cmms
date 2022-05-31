namespace Motorsazan.CMMS.Shared.Models.Output.PreventiveMaintenancesScheduling
{
    public class OutputGetOperationItemListByPMSchedulingInfoId
    {
        public long PMSchedulingInfoId { get; set; } 

        public string MachineName { get; set; }
         
        public string OldMachineCode { get; set; }

        public long MachineId { get; set; }

        public string MaintenanceGroupName { get; set; }

        public long OperationItemId { get; set; }

        public string OperationItemName { get; set; }

        public string OperationItemCode { get; set; }

        public string PersianPreferDate { get; set; }

        public int PersonHour { get; set; }

        public string ParentMachineName { get; set; }

        public string ParentOperationCode { get; set; }

    }
}
