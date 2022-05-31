using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motorsazan.CMMS.Shared.Models.Output.MachineManagement
{
    public class OutputGetMachineSparepartList
    {
        public long MachineSparePartId { get; set; }
        public long MachineId { get; set; }
        public string MachineName { get; set; }
        public long StockID { get; set; }
        public string StockName { get; set; }
        public long TaminType { get; set; }
        public long ImportanceType { get; set; }
        public string SparepartShamsiDate { get; set; }
        public string StockTechNo { get; set; }

    }
}
