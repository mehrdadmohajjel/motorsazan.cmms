using System;

namespace Motorsazan.CMMS.Shared.Models.Input.ProductiveWorkOrder
{
    public class InputGetProductiveWorkOrderGird
   {
       public long StatusID { get; set; } = 0;
       public DateTime StartDate { get; set; }
       public DateTime EndDate { get; set; }
       public int EmployeeEID { get; set; }
   }
}
