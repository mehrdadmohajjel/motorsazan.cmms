using Motorsazan.CMMS.Client.Api;
using Motorsazan.CMMS.Shared.Enums;
using Motorsazan.CMMS.Shared.Models.Input.CostReportByWorkOrder;
using Motorsazan.CMMS.Shared.Models.Output.CostReportByWorkOrder;
using Motorsazan.CMMS.Shared.Models.Output.ProductiveWorkOrder;
using Motorsazan.CMMS.Shared.Utilities;
using System.Web.Mvc;

namespace Motorsazan.CMMS.Client.Controllers
{
    public class CostReportByWorkOrderController: BaseController
    {
        public ActionResult FilterFormDepartmentIdCombo()
        {
            const string partialViewUrl =
                "~/Views/CostReportByWorkOrder/FilterForm/SelectByDepartment/FilterFormDepartmentIdCombo.cshtml";

            var getDepartmentList = new[]
            {
                new OutputGetDepartmentList
                {
                    DepartmentId = 1, DepartmentName = "DepartmentName1", ShowName = "امور 1"
                },
                new OutputGetDepartmentList
                {
                    DepartmentId = 2, DepartmentName = "DepartmentName2", ShowName = "امور 2"
                },
                new OutputGetDepartmentList
                {
                    DepartmentId = 3, DepartmentName = "DepartmentName3", ShowName = "امور 3"
                }
            };

            var allDepartment = new OutputGetDepartmentList { DepartmentId = 0, ShowName = "همه" };

            var dataSource = Tools.PrependGetAllItemToArray(getDepartmentList, allDepartment);

            return PartialView(partialViewUrl, dataSource);
        }


        public ActionResult FilterFormSubDepartmentIdCombo()
        {
            const string partialViewUrl =
                "~/Views/CostReportByWorkOrder/FilterForm/SelectByDepartment/FilterFormSubDepartmentIdCombo.cshtml";

            var getDepartmentList = new[]
            {
                new OutputGetSubDepartmentList
                {
                    SubDepartmentId = 1,
                    SubDepartmentName = "SubDepartmentName1",
                    ShowName = "خط 1",
                    ParentSubDepartment = 1
                },
                new OutputGetSubDepartmentList
                {
                    SubDepartmentId = 2,
                    SubDepartmentName = "SubDepartmentName2",
                    ShowName = "خط 2",
                    ParentSubDepartment = 1
                },
                new OutputGetSubDepartmentList
                {
                    SubDepartmentId = 3,
                    SubDepartmentName = "SubDepartmentName3",
                    ShowName = "خط 3",
                    ParentSubDepartment = 2
                },
                new OutputGetSubDepartmentList
                {
                    SubDepartmentId = 4,
                    SubDepartmentName = "SubDepartmentName4",
                    ShowName = "خط 4",
                    ParentSubDepartment = 2
                }
            };

            var allDepartment = new OutputGetSubDepartmentList { SubDepartmentId = 0, ShowName = "همه" };

            var dataSource = Tools.PrependGetAllItemToArray(getDepartmentList, allDepartment);

            return PartialView(partialViewUrl, dataSource);
        }

        public static OutputGetMaintenanceGroupList[] GetWorkOrderMaintenanceGroupList() =>
            ApiList.GetMaintenanceGroupList();

        public ActionResult Grid(InputGetCostReportByWorkOrderByCondition input, DatePeriodType datePeriodType,
            string persianStartDate, string persianEndDate)
        {
            const string partialViewUrl = "~/Views/CostReportByWorkOrder/Grid/Grid.cshtml";
            var token = GetUserToken();

            (input.StartDate, input.EndDate) = Tools.NormalizeDates(persianStartDate, persianEndDate, datePeriodType);

            if(input.WorkOrderId < 0)
            {
                var costReportByMachineListDataSource = new[]
                {
                    new OutputGetCostReportByWorkOrderByCondition
                    {
                        ID = 1,
                        WorkOrderSerial = "WorkOrderSerial1",
                        EmployeeName = "EmployeeName1",
                        Department = "Department1",
                        SubDepartment = "SubDepartment1",
                        MachineName = "MachineName1",
                        OldMachineCode = "MachineName1",
                        FailureCode = "FailureCode1",
                        FailureRoot = "FailureRoot",
                        WorkOrderDate = input.StartDate,
                        FinishDate = input.EndDate,
                        MaintenanceGroupName = "MaintenanceGroupTitle1",
                        ActionTime = 4,
                        Salary = 200
                    },
                    new OutputGetCostReportByWorkOrderByCondition
                    {
                        ID = 2,
                        WorkOrderSerial = "WorkOrderSerial1",
                        EmployeeName = "EmployeeName1",
                        Department = "Department1",
                        SubDepartment = "SubDepartment1",
                        MachineName = "MachineName1",
                        OldMachineCode = "MachineName1",
                        FailureCode = "FailureCode1",
                        FailureRoot = "FailureRoot",
                        WorkOrderDate = input.StartDate,
                        FinishDate = input.EndDate,
                        MaintenanceGroupName = "MaintenanceGroupTitle1",
                        ActionTime = 200,
                        Salary = 300
                    },
                    new OutputGetCostReportByWorkOrderByCondition
                    {
                        ID = 3,
                        WorkOrderSerial = "WorkOrderSerial1",
                        EmployeeName = "EmployeeName2",
                        Department = "Department1",
                        SubDepartment = "SubDepartment1",
                        MachineName = "MachineName1",
                        OldMachineCode = "MachineName1",
                        FailureCode = "FailureCode1",
                        FailureRoot = "FailureRoot",
                        WorkOrderDate = input.StartDate,
                        FinishDate = input.EndDate,
                        MaintenanceGroupName = "MaintenanceGroupTitle1",
                        ActionTime = 200,
                        Salary = 300
                    },
                    new OutputGetCostReportByWorkOrderByCondition
                    {
                        ID = 4,
                        WorkOrderSerial = "WorkOrderSerial1",
                        EmployeeName = "EmployeeName2",
                        Department = "Department1",
                        SubDepartment = "SubDepartment1",
                        MachineName = "MachineName1",
                        OldMachineCode = "MachineName1",
                        FailureCode = "FailureCode1",
                        FailureRoot = "FailureRoot",
                        WorkOrderDate = input.StartDate,
                        FinishDate = input.EndDate,
                        MaintenanceGroupName = "MaintenanceGroupTitle1"+token,
                        ActionTime = 23,
                        Salary = 100
                    }
                };
                return PartialView(partialViewUrl, costReportByMachineListDataSource);
            }
            else
            {
                var costReportByMachineListDataSource = new[]
                {
                    new OutputGetCostReportByWorkOrderByCondition
                    {
                        ID = 1,
                        WorkOrderSerial = "WorkOrderSerial1",
                        EmployeeName = "EmployeeName1",
                        Department = "Department1",
                        SubDepartment = "SubDepartment1",
                        MachineName = "MachineName1",
                        OldMachineCode = "MachineName1",
                        FailureCode = "FailureCode1",
                        FailureRoot = "FailureRoot",
                        WorkOrderDate = input.StartDate,
                        FinishDate = input.EndDate,
                        MaintenanceGroupName = "MaintenanceGroupTitle1",
                        ActionTime = 4,
                        Salary = 200
                    },
                    new OutputGetCostReportByWorkOrderByCondition
                    {
                        ID = 2,
                        WorkOrderSerial = "WorkOrderSerial1",
                        EmployeeName = "EmployeeName1",
                        Department = "Department1",
                        SubDepartment = "SubDepartment1",
                        MachineName = "MachineName1",
                        OldMachineCode = "MachineName1",
                        FailureCode = "FailureCode1",
                        FailureRoot = "FailureRoot",
                        WorkOrderDate = input.StartDate,
                        FinishDate = input.EndDate,
                        MaintenanceGroupName = "MaintenanceGroupTitle1",
                        ActionTime = 200,
                        Salary = 300
                    },
                    new OutputGetCostReportByWorkOrderByCondition
                    {
                        ID = 3,
                        WorkOrderSerial = "WorkOrderSerial1",
                        EmployeeName = "EmployeeName2",
                        Department = "Department1",
                        SubDepartment = "SubDepartment1",
                        MachineName = "MachineName1",
                        OldMachineCode = "MachineName1",
                        FailureCode = "FailureCode1",
                        FailureRoot = "FailureRoot",
                        WorkOrderDate = input.StartDate,
                        FinishDate = input.EndDate,
                        MaintenanceGroupName = "MaintenanceGroupTitle1",
                        ActionTime = 200,
                        Salary = 300
                    },
                    new OutputGetCostReportByWorkOrderByCondition
                    {
                        ID = 4,
                        WorkOrderSerial = "WorkOrderSerial1",
                        EmployeeName = "EmployeeName2",
                        Department = "Department1",
                        SubDepartment = "SubDepartment1",
                        MachineName = "MachineName1",
                        OldMachineCode = "MachineName1",
                        FailureCode = "FailureCode1",
                        FailureRoot = "FailureRoot",
                        WorkOrderDate = input.StartDate,
                        FinishDate = input.EndDate,
                        MaintenanceGroupName = "MaintenanceGroupTitle1",
                        ActionTime = 23,
                        Salary = 100
                    }
                };
                return PartialView(partialViewUrl, costReportByMachineListDataSource);
            }
        }

        // GET: CostReportByWorkOrder
        public ActionResult Index() => View();
    }
}