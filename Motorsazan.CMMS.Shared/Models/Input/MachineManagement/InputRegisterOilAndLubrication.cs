namespace Motorsazan.CMMS.Shared.Models.Input.MachineManagement
{
    public class InputRegisterOilAndLubrication
    {
        public long MachineId { get; set; }

        public string PredictiveItemTitle { get; set; }

        public string MaterialName { get; set; }

        public string MaterialProducer { get; set; }

        public decimal TankVolume { get; set; }

        public string Unit { get; set; }

        public decimal StandardValue { get; set; }

        public decimal Period { get; set; }

        public string AnnualUsage { get; set; }
    }
}