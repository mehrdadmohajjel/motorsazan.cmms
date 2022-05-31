using System.Web.Mvc;
using Motorsazan.CMMS.Client.Api;
using Motorsazan.CMMS.Client.Filters;
using Motorsazan.CMMS.Shared.Enums;
using Motorsazan.CMMS.Shared.Models.Input.MaintenanceGroupCostReport;
using Motorsazan.CMMS.Shared.Models.Output.ProductiveWorkOrder;
using Motorsazan.CMMS.Shared.Utilities;

namespace Motorsazan.CMMS.Client.Controllers
{
    public class MaintenanceGroupCostReportController: BaseController
    {
        public ActionResult FilterFormMaintenanceGroupIdCombo()
        {
            const string partialViewUrl =
                "~/Views/MaintenanceGroupCostReport/FilterForm/FilterFormMaintenanceGroupCombo.cshtml";

            var getMaintenanceGroupList = GetAllMaintenanceGroupList();

            var allMaintenanceGroup =
                new OutputGetMaintenanceGroupList {MaintenanceGroupId = 0, MaintenanceGroupName = "همه"};

            var dataSource = Tools.PrependGetAllItemToArray(getMaintenanceGroupList, allMaintenanceGroup);

            return PartialView(partialViewUrl, dataSource);
        }

        public static OutputGetMaintenanceGroupList[] GetAllMaintenanceGroupList() =>
            ApiList.GetMaintenanceGroupList();

        public ActionResult Grid(InputGetMaintenanceGroupCostReportByCondition input, string startDate, string endDate,
            DatePeriodType datePeriodType = DatePeriodType.CurrentDay)
        {
            const string partialViewUrl = "~/Views/MaintenanceGroupCostReport/Grid/Grid.cshtml";

            (input.StartDate, input.EndDate) =
                Tools.NormalizeDates(startDate, endDate, datePeriodType);

            var maintenanceGroupCostReportListDataSource = ApiList.GetMaintenanceGroupCostReportByCondition(input);

            return PartialView(partialViewUrl, maintenanceGroupCostReportListDataSource);
        }

        [AccessToFormValidation(FormCode = "030")]
        public ActionResult Index() => View();
    }
}