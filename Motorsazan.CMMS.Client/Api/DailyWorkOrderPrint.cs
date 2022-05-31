using Motorsazan.CMMS.Shared.Models.Input.DailyWorkOrderPrint;
using Motorsazan.CMMS.Shared.Models.Output.DailyWorkOrderPrint;

namespace Motorsazan.CMMS.Client.Api
{
    public partial class ApiList
    {
        public static OutputGetDailyWorkOrderPrintReport[] GetDailyWorkOrderPrintReport(InputGetDailyWorkOrderPrintReport values, string token)
        {
            if(!string.IsNullOrEmpty(token) && values.EndDate != null)
            {
                var workOrderList = new OutputGetDailyWorkOrderPrintReport[2];

                workOrderList[0] = new OutputGetDailyWorkOrderPrintReport
                {
                    MaintenanceGroupName = "مکانیک",
                    MaintenanceGroupID = 1,
                    WorkOrderID = 1001,
                    WorkOrderSerial = "13991012",
                    WorkOrderStatusID = 101,
                    WorkOrderStatusTitle = "ارجاع شده",
                    StopTypeID = 1,
                    WorkOrderStopTitle = "تلفن",
                    WorkOrderType = "غیر تولیدی",
                    EmployeeName = "امیر خادم نژاد",
                };

                workOrderList[1] = new OutputGetDailyWorkOrderPrintReport
                {
                    MaintenanceGroupName = "مکانیک",
                    MaintenanceGroupID = 1,
                    WorkOrderID = 1002,
                    WorkOrderSerial = "13991013",
                    WorkOrderStatusID = 101,
                    WorkOrderStatusTitle = "ارجاع شده",
                    StopTypeID = 1,
                    WorkOrderStopTitle = "تلفن",
                    WorkOrderType = "غیر تولیدی",
                    EmployeeName = "امیر خادم نژاد",
                };


                return workOrderList;
            }
            else
            {
                return null;
            }
        }

    }
}