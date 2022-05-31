using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motorsazan.CMMS.Shared.Models.Output.CostReportByWorkOrder
{
    public class OutputGetDepartmentList
    {
        public int DepartmentId { get; set; }
        public string  DepartmentName { get; set; }
        public string ShowName { get; set; }
    }
}
