using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motorsazan.CMMS.Shared.Models.Output.PreventiveMaintenancesSchedulingReport
{
   public class OutputGetPreventiveMaintenanceSchedulingReportByCondition
    {
        public long PMSchedulingInfoId { get; set; }
        
        public string MiterMeasuringTypeShowName { get; set; }
       
        public int DestinationTimeOrWeek { get; set; }
       
        public int SourceTimeOrWeek { get; set; }
       
        public string MachineName { get; set; }
       
        public string MaintenanceGroupName { get; set; }
       
        public string OperationItemName { get; set; }
       
        public string OperationItemCode { get; set; }
       
        public int DoneCount { get; set; }
       
        public int DurationFromCreationDate { get; set; }
        
        public string PersianWorkOrderPreferDate { get; set; }
    }
}
