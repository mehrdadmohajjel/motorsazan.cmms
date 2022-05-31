using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motorsazan.CMMS.Shared.Models.Output.MachineManagement
{
    public class OutputGetStoreListForSparePart
    {
        public string StoreCode { get; set; }

        public string StoreName { get; set; }

        public int RackCode { get; set; }
    }
}