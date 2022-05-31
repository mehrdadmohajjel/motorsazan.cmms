namespace Motorsazan.CMMS.Shared.Models.Output.MachineManagement
{
    public class OutputGetMachineElectricalInfo
    {
        public long Id { get; set; }

        public string Characteristic { get; set; }

        public string Producer { get; set; }

        public decimal Power { get; set; }

        public decimal Rpm { get; set; }

        public decimal Voltage { get; set; }

        public decimal Current { get; set; }

        public string CurrentType { get; set; }
    }
}