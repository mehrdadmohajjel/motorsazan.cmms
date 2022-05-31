using System.Web.Mvc;
using Motorsazan.CMMS.Client.Api;
using Motorsazan.CMMS.Client.Filters;
using Motorsazan.CMMS.Shared.Enums;
using Motorsazan.CMMS.Shared.Models.Input.Inspection;
using Motorsazan.CMMS.Shared.Models.Input.PerformanceReportByDepartment;
using Motorsazan.CMMS.Shared.Models.Output.Inspection;
using Motorsazan.CMMS.Shared.Models.Output.ProductiveWorkOrder;
using Motorsazan.CMMS.Shared.Utilities;

namespace Motorsazan.CMMS.Client.Controllers
{
    public class PerformanceReportByDepartmentController: BaseController
    {
        public ActionResult FilterFormDepartmentCombo()
        {
            const string partialViewUrl =
                "~/Views/PerformanceReportByDepartment/FilterForm/FilterFormDepartmentIdCombo.cshtml";

            var token = GetUserToken();

            var getDepartmentList = ApiList.GetAllMainDepartment(token);

            var allDepartment = new OutputGetAllMainDepartment {DepartmentId = 0, Title = "همه"};

            var dataSource = Tools.PrependGetAllItemToArray(getDepartmentList, allDepartment);

            return PartialView(partialViewUrl, dataSource);
        }

        public ActionResult FilterFormSubDepartmentListByMainDepartmentId(
            InputGetSubDepartmentListByMainDepartmentId value)
        {
            const string partialViewUrl =
                "~/Views/PerformanceReportByDepartment/FilterForm/FilterFormSubDepartmentIdCombo.cshtml";
            var subDepartmentList = ApiList.GetSubDepartmentListByMainDepartmentId(value);

            return PartialView(partialViewUrl, subDepartmentList);
        }

        public static OutputGetMaintenanceGroupList[] GetAllMaintenanceGroupList() =>
            ApiList.GetMaintenanceGroupList();

        public ActionResult Grid(InputGetDepartmentPerformanceReportByCondition input, string startDate, string endDate,
            DatePeriodType datePeriodType)
        {
            const string partialViewUrl = "~/Views/PerformanceReportByDepartment/Grid/Grid.cshtml";

            if(input.DepartmentId == -1)
            {
                return PartialView(partialViewUrl);
            }

            (input.StartDate, input.EndDate) = Tools.NormalizeDates(startDate, endDate, datePeriodType);

            var dataSource = ApiList.GetDepartmentPerformanceReportByCondition(input);


            return PartialView(partialViewUrl, dataSource);
        }

        [AccessToFormValidation(FormCode = "033")]
        public ActionResult Index() => View();
    }
}