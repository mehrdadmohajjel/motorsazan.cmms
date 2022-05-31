using System;
using System.Web.Mvc;
using Motorsazan.CMMS.Client.Api;
using Motorsazan.CMMS.Client.Filters;
using Motorsazan.CMMS.Shared.Enums;
using Motorsazan.CMMS.Shared.Models.Input.NetExpert;
using Motorsazan.CMMS.Shared.Models.Input.ProductiveWorkOrder;
using Motorsazan.CMMS.Shared.Models.Input.ScrapedWorkOrderReport;
using Motorsazan.CMMS.Shared.Models.Output.ProductiveWorkOrder;
using Motorsazan.CMMS.Shared.Utilities;

namespace Motorsazan.CMMS.Client.Controllers
{
    public class ScrapedWorkOrderReportController: BaseController
    {
        public ActionResult DetailRow()
        {
            const string partialViewUrl = "~/Views/ScrapedWorkOrderReport/Grid/DetailRow.cshtml";
            return PartialView(partialViewUrl);
        }

        public static OutputGetMaintenanceGroupList[] GetMaintenanceGroupList() =>
            ApiList.GetMaintenanceGroupList();

        public static OutputGetWorkOrderStatusTypeList[] GetWorkOrderStatusTypeList() =>
            ApiList.GetWorkOrderStatusTypeList();

        public ActionResult Grid(DatePeriodType dateType = DatePeriodType.RecentMonth, string startShamsiDate = "",
            string endPersianDate = "")
        {
            const string partialViewUrl = "~/Views/ScrapedWorkOrderReport/Grid/Grid.cshtml";
            if(dateType == (DatePeriodType) (-1))
            {
                return PartialView(partialViewUrl);
            }

            DateTime enStartDate;
            DateTime enEndDate;
            (enStartDate, enEndDate) = Tools.NormalizeDates(startShamsiDate, endPersianDate, dateType);

            var apiParam = new InputGetScrapedWorkOrderReportByCondition {EndDate = enEndDate, StartDate = enStartDate};

            var dataSource = ApiList.GetScrapedWorkOrderReportByCondition(apiParam);
            return PartialView(partialViewUrl, dataSource);
        }

        [AccessToFormValidation(FormCode = "021")]
        public ActionResult Index() => View();

        public ActionResult RegistrarInfoModal(InputGetWorkOrderRegistrantInfoByWorkOrderId input)
        {
            const string partialViewUrl = "~/Views/ScrapedWorkOrderReport/Grid/RegistrarInfoModal.cshtml";

            var workOrderRegistrantInfoDataSource = ApiList.GetWorkOrderRegistrantInfoByWorkOrderId(input);

            return PartialView(partialViewUrl, workOrderRegistrantInfoDataSource);
        }

        public ActionResult ShowActionHistory(long workOrderId)
        {
            var apiParam = new InputGetActionOrDelayListByWorkOrderId {WorkOrderId = workOrderId};
            var token = GetUserToken();
            var partailViewUrl = "~/Views/ScrapedWorkOrderReport/Grid/ShowActionHistoryPartial.cshtml";

            var dataSource = ApiList.GetActionOrDelayListByWorkOrderId(apiParam, token);
            return PartialView(partailViewUrl, dataSource);
        }

        public ActionResult ShowReferenceHistory(long workOrderId)
        {
            var apiParam = new InputGetWorKOrderReferralListByWorkOrderId {WorkOrderId = workOrderId};
            var token = GetUserToken();
            const string partailViewUrl = "~/Views/ScrapedWorkOrderReport/Grid/ShowReferenceHistoryPartial.cshtml";

            var dataSource = ApiList.GetWorKOrderReferralListByWorkOrderId(apiParam, token);
            return PartialView(partailViewUrl, dataSource);
        }

        public ActionResult WorKOrderConsumableList(long workOrderId)
        {
            var apiParam = new InputGetWorKOrderConsumableListByWorkOrderId {WorkOrderId = workOrderId};
            var token = GetUserToken();
            var partailViewUrl = "~/Views/ScrapedWorkOrderReport/Grid/ShowWorKOrderConsumableList.cshtml";

            var dataSource = ApiList.GetWorKOrderConsumableListByWorkOrderId(apiParam, token);
            return PartialView(partailViewUrl, dataSource);
        }
    }
}