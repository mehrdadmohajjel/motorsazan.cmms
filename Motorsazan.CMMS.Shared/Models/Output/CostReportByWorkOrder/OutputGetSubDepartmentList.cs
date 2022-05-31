using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motorsazan.CMMS.Shared.Models.Output.CostReportByWorkOrder
{
    public class OutputGetSubDepartmentList
    {
        public int SubDepartmentId { get; set; }
        public string SubDepartmentName { get; set; }
        public string ShowName{ get; set; }
        public int ParentSubDepartment{ get; set; }
    }
}
