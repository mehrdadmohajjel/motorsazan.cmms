using System;

namespace Motorsazan.CMMS.Shared.Models.Input.CostReportByWorkOrder
{
   public class InputGetCostReportByWorkOrderByCondition
    {
        public int DepartmentId { get; set; }

        public int SubDepartmentId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int WorkOrderId { get; set; }
    }
}