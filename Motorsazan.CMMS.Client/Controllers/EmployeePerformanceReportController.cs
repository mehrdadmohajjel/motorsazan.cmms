using System.Web.Mvc;
using Motorsazan.CMMS.Client.Api;
using Motorsazan.CMMS.Client.Filters;
using Motorsazan.CMMS.Shared.Enums;
using Motorsazan.CMMS.Shared.Models.Input.EmployeePerformanceReport;
using Motorsazan.CMMS.Shared.Models.Output.EmployeePerformanceReport;
using Motorsazan.CMMS.Shared.Models.Output.ProductiveWorkOrder;
using Motorsazan.CMMS.Shared.Utilities;

namespace Motorsazan.CMMS.Client.Controllers
{
    public class EmployeePerformanceReportController: BaseController
    {
        public ActionResult FilterFormEmployeeIdCombo()
        {
            const string partialViewUrl = "~/Views/EmployeePerformanceReport/FilterForm/FilterFormEmployeeCombo.cshtml";

            var getEmployeeList = ApiList.GetAllMaintenanceGroupMemberList();

            return PartialView(partialViewUrl, getEmployeeList);
        }

        public static OutputGetMaintenanceGroupList[] GetWorkOrderMaintenanceGroupList() =>
            ApiList.GetMaintenanceGroupList();

        public ActionResult Grid(InputGetEmployeePerformanceReportByCondition input, string startDate, string endDate,
            DatePeriodType datePeriodType = DatePeriodType.CurrentDay)
        {
            const string partialViewUrl = "~/Views/EmployeePerformanceReport/Grid/Grid.cshtml";

            (input.StartDate, input.EndDate) =
                Tools.NormalizeDates(startDate, endDate, datePeriodType);

            var employeePerformanceReportListDataSource = ApiList.GetEmployeePerformanceReportByCondition(input);

            return PartialView(partialViewUrl, employeePerformanceReportListDataSource);
        }

        [AccessToFormValidation(FormCode = "035")]
        public ActionResult Index() => View();
    }
}