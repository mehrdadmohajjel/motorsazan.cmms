using System.Web.Mvc;
using Motorsazan.CMMS.Client.Api;
using Motorsazan.CMMS.Client.Filters;
using Motorsazan.CMMS.Shared.Enums;
using Motorsazan.CMMS.Shared.Models.Input.EmployeeCostReport;
using Motorsazan.CMMS.Shared.Models.Output.EmployeePerformanceReport;
using Motorsazan.CMMS.Shared.Models.Output.ProductiveWorkOrder;
using Motorsazan.CMMS.Shared.Utilities;

namespace Motorsazan.CMMS.Client.Controllers
{
    public class EmployeeCostReportController: BaseController
    {
        public ActionResult FilterFormEmployeeIdCombo()
        {
            const string partialViewUrl = "~/Views/EmployeeCostReport/FilterForm/FilterFormEmployeeCombo.cshtml";

            var getEmployeeList = ApiList.GetAllMaintenanceGroupMemberList();

            return PartialView(partialViewUrl, getEmployeeList);
        }

        public static OutputGetMaintenanceGroupList[] GetWorkOrderMaintenanceGroupList() =>
            ApiList.GetMaintenanceGroupList();

        public ActionResult Grid(InputGetEmployeeCostReportByCondition input, string startDate,
            string endDate, DatePeriodType datePeriodType)
        {
            const string partialViewUrl = "~/Views/EmployeeCostReport/Grid/Grid.cshtml";

            (input.StartDate, input.EndDate) = Tools.NormalizeDates(startDate, endDate, datePeriodType);

            var employeeCostReportDataSource = ApiList.GetEmployeeCostReportByCondition(input);

            return PartialView(partialViewUrl, employeeCostReportDataSource);
        }

        [AccessToFormValidation(FormCode = "031")]
        public ActionResult Index() => View();
    }
}