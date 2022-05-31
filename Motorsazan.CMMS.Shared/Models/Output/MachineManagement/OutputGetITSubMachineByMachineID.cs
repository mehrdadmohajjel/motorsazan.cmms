using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motorsazan.CMMS.Shared.Models.Output.MachineManagement
{
    public class OutputGetSubMachineByMachineID
    {
        public long? MachineId { get; set; }
        public long? ParentMachineId { get; set; }
        public long? MachineContorlId { get; set; }
        public string MachineContorlName { get; set; }
        public long? MachineLevelId { get; set; }
        public string MachineLevelName { get; set; }
        public long? MachineTypeId { get; set; }
        public string MachineTypeTitle { get; set; }
        public string OldMachineCode { get; set; }
        public string OperationCode { get; set; }
        public string MachineName { get; set; }
        public string MachineSerial { get; set; }
        public string MachineStatus { get; set; }
        public long? MachineStatusId { get; set; }
    }
}
