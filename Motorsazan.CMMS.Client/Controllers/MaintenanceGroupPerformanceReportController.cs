using System.Web.Mvc;
using Motorsazan.CMMS.Client.Api;
using Motorsazan.CMMS.Client.Filters;
using Motorsazan.CMMS.Shared.Enums;
using Motorsazan.CMMS.Shared.Models.Input.MaintenanceGroupPerformanceReport;
using Motorsazan.CMMS.Shared.Models.Output.ProductiveWorkOrder;
using Motorsazan.CMMS.Shared.Utilities;

namespace Motorsazan.CMMS.Client.Controllers
{
    public class MaintenanceGroupPerformanceReportController: BaseController
    {
        public ActionResult FilterFormMaintenanceGroupIdCombo()
        {
            const string partialViewUrl =
                "~/Views/MaintenanceGroupPerformanceReport/FilterForm/FilterFormMaintenanceGroupCombo.cshtml";

            var getMaintenanceGroupList = GetAllMaintenanceGroupList();

            var allMaintenanceGroup =
                new OutputGetMaintenanceGroupList {MaintenanceGroupId = 0, MaintenanceGroupName = "همه"};

            var dataSource = Tools.PrependGetAllItemToArray(getMaintenanceGroupList, allMaintenanceGroup);


            return PartialView(partialViewUrl, dataSource);
        }

        public static OutputGetMaintenanceGroupList[] GetAllMaintenanceGroupList() =>
            ApiList.GetMaintenanceGroupList();

        public ActionResult Grid(InputGetMaintenanceGroupPerformanceReportByCondition input,
            string startDate, string endDate, DatePeriodType datePeriodType)
        {
            const string partialViewUrl = "~/Views/MaintenanceGroupPerformanceReport/Grid/Grid.cshtml";

            if(input.MaintenanceGroupId == -1)
            {
                return PartialView(partialViewUrl);
            }

            (input.StartDate, input.EndDate) = Tools.NormalizeDates(startDate, endDate, datePeriodType);

            var dataSource = ApiList.GetMaintenanceGroupPerformanceReportByCondition(input);


            return PartialView(partialViewUrl, dataSource);
        }

        [AccessToFormValidation(FormCode = "034")]
        public ActionResult Index() => View();
    }
}