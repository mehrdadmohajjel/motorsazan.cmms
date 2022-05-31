namespace Motorsazan.CMMS.Shared.Models.Input.MachineManagement
{
    public class InputAddMachineElectricalInfo
    {
        public long MachineId { get; set; }

        public string Characteristic { get; set; }

        public string Producer { get; set; }

        public string Power { get; set; }

        public string Rpm { get; set; }

        public string Voltage { get; set; }

        public decimal Current { get; set; }

        public string CurrentType { get; set; }
    }
}