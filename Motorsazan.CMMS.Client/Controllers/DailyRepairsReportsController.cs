using System.Web.Mvc;
using Motorsazan.CMMS.Client.Api;
using Motorsazan.CMMS.Client.Filters;
using Motorsazan.CMMS.Shared.Enums;
using Motorsazan.CMMS.Shared.Models.Input.DailyRepairsReports;
using Motorsazan.CMMS.Shared.Models.Output.ProductiveWorkOrder;
using Motorsazan.CMMS.Shared.Utilities;

namespace Motorsazan.CMMS.Client.Controllers
{
    public class DailyRepairsReportsController: BaseController
    {
        public ActionResult FillResultGrid(InputGetMaintenanceDailyReportByCondition input,
            string persianStartDate, string persianEndDate, DatePeriodType datePeriodType)
        {
            const string partialViewUrl = "~/Views/DailyRepairsReports/Grid/ResultGrid.cshtml";

            (input.StartDate, input.EndDate) =
                Tools.NormalizeDates(persianStartDate, persianEndDate, datePeriodType);

            if(input.WorkOrderStatusTypeId == -1)
            {
                return PartialView(partialViewUrl);
            }

            var apiResult = ApiList.GetMaintenanceDailyReportByCondition(input);

            return PartialView(partialViewUrl, apiResult);
        }

        public static OutputGetStopTypeList[] GetAllStopTypeList() =>
            ApiList.GetStopTypeList();

        public ActionResult GetProductiveWorkOrderAllStatusList()
        {
            const string partialViewUrl = "~/Views/DailyRepairsReports/FilterForm/FilterFormStatusTypeCombo.cshtml";

            var workOrderStatusList = GetWorkOrderStatusList();
            var getAllWorkOrderStatusList = new OutputGetWorkOrderStatusTypeList
            {
                WorkOrderStatusTypeId = 0, TypeName = "همه موارد"
            };

            var dataSource = Tools.PrependGetAllItemToArray
                (workOrderStatusList, getAllWorkOrderStatusList);

            return PartialView(partialViewUrl, dataSource);
        }

        public static OutputGetMaintenanceGroupList[] GetWorkOrderMaintenanceGroupList() =>
            ApiList.GetMaintenanceGroupList();

        public OutputGetWorkOrderStatusTypeList[] GetWorkOrderStatusList() => ApiList.GetWorkOrderStatusTypeList();

        [AccessToFormValidation(FormCode = "023")]
        public ActionResult Index() => View();
    }
}