namespace Motorsazan.CMMS.Shared.Models.Output.PLCReport
{
    public class OutputGetPlcMachineAvailabilityByMachineId
    {
        public long MachineId { get; set; }

        public string MachineName { get; set; }

        public string OldMachineCode { get; set; }

        public string PLCRecordPersianCreationDate { get; set; }

        public string StartTime { get; set; }

        public string FinishTime { get; set; }

        public decimal AccessTime { get; set; }
    }
}