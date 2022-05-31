using Motorsazan.CMMS.Shared.Models.Input.AllWorkOrderStatusReport;
using Motorsazan.CMMS.Shared.Models.Output.AllWorkOrderStatusReport;

namespace Motorsazan.CMMS.Client.Api
{
    public static partial class ApiList
    {
        public static OutputGetAllWorkOrderStatusReportByCondition[] GetAllWorkOrderStatusReportListByCondition(
            InputAllWorkOrderStatusReportListByCondition values, string token)
        {
            var outputGetAllWorkOrderStatusReportListByCondition = new[]
            {
                new OutputGetAllWorkOrderStatusReportByCondition
                {
                    ID = 1,
                    WorkOrderSerial = "13991012",
                    WorkOrderStatusTitle = "WorkOrderStatusTitle1",
                    WorkOrderDate = "1399/10/10",
                    StopTypeTitle = "تلفن",
                    Requester = "milad",
                    MaintenanceGroupName = "برق",
                    WorkOrderType = "غیر تولیدی",
                    OperationItem = "OperationItem1",
                    SubDepartment = "1012",
                    MachineName = "nedvel 1998",
                    MachineCode = "95-135-48"
                },
                new OutputGetAllWorkOrderStatusReportByCondition
                {
                    ID = 2,
                    WorkOrderSerial = "13991013",
                    WorkOrderStatusTitle = "WorkOrderStatusTitle2",
                    WorkOrderDate = "1399/10/11",
                    StopTypeTitle = "پیشگیری",
                    Requester = "milad",
                    MaintenanceGroupName = "برق",
                    WorkOrderType = "تولیدی",
                    OperationItem = "OperationItem2",
                    SubDepartment = "1013",
                    MachineName = "nedvel 2010",
                    MachineCode = "92-175-44"
                }
            };
            return outputGetAllWorkOrderStatusReportListByCondition;
        }
    }
}