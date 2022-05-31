namespace Motorsazan.CMMS.Shared.Models.Output.MachineMttrMtbfReport
{
    public class OutputGetMachineMTTRAndMTBFReportByCondition
    {
        public long OperationItemId { get; set; }

        public string MaintenanceGroupName { get; set; }

        public string OperationItemCode { get; set; }

        public string OperationItemName { get; set; }

        public int WorkOrderCountHasFaultCode { get; set; }

        public decimal PersonHour { get; set; }

        public decimal PureRepairingTimeInHour { get; set; }

        public decimal ImpureRepairingTimeInHour { get; set; }

        public decimal PureMTTR { get; set; }

        public decimal ImpureMTTR { get; set; }

        public decimal PureMTBF { get; set; }

        public decimal ImpureMTBF { get; set; }

    }
}
