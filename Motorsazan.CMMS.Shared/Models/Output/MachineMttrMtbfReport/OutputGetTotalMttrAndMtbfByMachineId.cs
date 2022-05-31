namespace Motorsazan.CMMS.Shared.Models.Output.MachineMttrMtbfReport
{
    public class OutputGetTotalMttrAndMtbfByMachineId
    {
        public long MachineId { get; set; }

        public int FinishedWorkOrderCount { get; set; }

        public int NotFinishedWorkOrderCount { get; set; }

        public int TotalWorkOrderCount { get; set; }

        public int AccessTimeInHour { get; set; }

        public int PureRepairingTimeInHour { get; set; }

        public int ImpureRepairingTimeInHour { get; set; }

        public decimal PureMTTR { get; set; }

        public decimal ImpureMTTR { get; set; }

        public decimal PureMTBF { get; set; }

        public decimal ImpureMTBF { get; set; }

        public decimal PureEA { get; set; }

        public decimal ImpureEA { get; set; }

    }
}
