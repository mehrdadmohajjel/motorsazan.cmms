using System;
using System.Web.Mvc;
using Motorsazan.CMMS.Client.Api;
using Motorsazan.CMMS.Client.Filters;
using Motorsazan.CMMS.Shared.Enums;
using Motorsazan.CMMS.Shared.Models.Input.DelayInWorkOrderReport;
using Motorsazan.CMMS.Shared.Models.Output.ProductiveWorkOrder;
using Motorsazan.CMMS.Shared.Utilities;

namespace Motorsazan.CMMS.Client.Controllers
{
    public class DelayInWorkOrderReportController: BaseController
    {
        public static OutputGetStopTypeList[] GetAllStopTypeList() =>
            ApiList.GetStopTypeList();

        public static OutputGetWorkOrderStatusTypeList[] GetWorkOrderStatusTypeList() =>
            ApiList.GetWorkOrderStatusTypeList();

        public ActionResult Grid(DatePeriodType dateType = DatePeriodType.RecentMonth, string startShamsiDate = "",
            string endPersianDate = "")
        {
            const string partialViewUrl = "~/Views/DelayInWorkOrderReport/Grid/Grid.cshtml";
            DateTime enStartDate;
            DateTime enEndDate;
            (enStartDate, enEndDate) = Tools.NormalizeDates(startShamsiDate, endPersianDate, dateType);

            var apiParam = new InputGetDelayInWorkOrderReportByCondition {EndDate = enEndDate, StartDate = enStartDate};

            var dataSource = ApiList.GetDelayInWorkOrderReportByCondition(apiParam);
            return PartialView(partialViewUrl, dataSource);
        }

        [AccessToFormValidation(FormCode = "022")]
        public ActionResult Index() => View();

        public ActionResult ShowWorkOrderDetail(long delayTypeId, DatePeriodType dateType = DatePeriodType.RecentMonth,
            string startShamsiDate = "", string endPersianDate = "")
        {
            const string partialViewUrl = "~/Views/DelayInWorkOrderReport/Grid/WorkOrderDetailGrid.cshtml";

            if(delayTypeId == -1)
            {
                return PartialView(partialViewUrl);
            }

            DateTime enStartDate;
            DateTime enEndDate;
            (enStartDate, enEndDate) = Tools.NormalizeDates(startShamsiDate, endPersianDate, dateType);

            var item = new InputGetWorkOrderByDelayTypeId
            {
                DelayTypeId = delayTypeId, StartDate = enStartDate, EndDate = enEndDate
            };
            var dataSource = ApiList.GetWorkOrderByDelayTypeId(item);
            ViewData["DelayTypeId"] = delayTypeId;
            return PartialView(partialViewUrl, dataSource);
        }
    }
}