namespace Motorsazan.CMMS.Shared.Models.Output.MachineManagement
{
    public class OutputGetTopMachineListByCondition
    {
        public long MachineId { get; set; }

        public string MachineName { get; set; }

        public string BuilderMachineCode { get; set; }

        public string OldMachineCode { get; set; }

        public string OperationCode { get; set; }

        public string ControlSystemTypeName { get; set; }

        public string MachineLevelName { get; set; }

        public string MachineStatusTypeShowName { get; set; }

        public string DepartmentName { get; set; }

        public string MachineTypeName { get; set; }
    }
}