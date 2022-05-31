namespace Motorsazan.CMMS.Shared.Models.Output.MachineManagement
{
    public class OutputGetMachineOilAndLubricationInfo
    {
        public long Id { get; set; }
        public long MachineId { get; set; }
        public string PredictiveItemTitle { get; set; }
        public string MaterialName { get; set; }
        public string MaterialProducer { get; set; }
        public decimal TankVolume { get; set; }
        public string Unit { get; set; }
        public decimal StandardValue { get; set; }
        public decimal Period { get; set; }
        public decimal AnnualUseage { get; set; }
    }
}