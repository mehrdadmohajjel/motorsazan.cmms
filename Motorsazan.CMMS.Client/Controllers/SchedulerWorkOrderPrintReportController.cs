using Motorsazan.CMMS.Client.Api;
using Motorsazan.CMMS.Shared.Enums;
using Motorsazan.CMMS.Shared.Models.Input.NetExpert;
using Motorsazan.CMMS.Shared.Models.Input.ProductiveWorkOrder;
using Motorsazan.CMMS.Shared.Models.Input.SchedulerWorkOrderPrintReport;
using Motorsazan.CMMS.Shared.Models.Output.ProductiveWorkOrder;
using Motorsazan.CMMS.Shared.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Motorsazan.CMMS.Client.Filters;

namespace Motorsazan.CMMS.Client.Controllers
{
    public class SchedulerWorkOrderPrintReportController : BaseController
    {
        public ActionResult DetailRow(bool isPreventive)
        {
            ViewData["isPreventive"] = isPreventive;

            const string partialViewUrl = "~/Views/SchedulerWorkOrderPrintReport/Grid/DetailRow.cshtml";
            return PartialView(partialViewUrl);
        }
        public ActionResult FilterFormWorkOrderStatus()
        {
            const string partialViewUrl = "~/Views/SchedulerWorkOrderPrintReport/FilterForm/FilterFormWorkOrderStatusPartial.cshtml";

            var workOrderStatus = ApiList.GetWorkOrderStatusTypeList();

            var allStatus = new OutputGetWorkOrderStatusTypeList { WorkOrderStatusTypeId = 0, TypeName = "همه وضعیت ها" };

            var dataSource = Tools.PrependGetAllItemToArray(workOrderStatus, allStatus);

            return PartialView(partialViewUrl, dataSource);

        }
        public ActionResult Grid(int workOrderTypeId, long workOrderStatusTypeId, DatePeriodType dateType = DatePeriodType.RecentWeek, string startShamsiDate = "", string endPersianDate = "")
        {
            const string partailViewUrl = "~/Views/SchedulerWorkOrderPrintReport/Grid/Grid.cshtml";

            if(workOrderTypeId == -1)
            {
                return PartialView(partailViewUrl);
            }

            DateTime enStartDate;
            DateTime enEndDate;
            (enStartDate, enEndDate) = Tools.NormalizeDates(startShamsiDate, endPersianDate, dateType);

            var apiParam = new InputGetSchedulerWorkOrderReportByCondition
            {
                WorkOrderType = workOrderTypeId,
                WorkOrderStatusTypeId = workOrderStatusTypeId,
                EndDate = enEndDate,
                StartDate = enStartDate
            };

            var dataSource = ApiList.GetSchedulerWorkOrderReportByCondition(apiParam);
            return PartialView(partailViewUrl, dataSource);

        }

        public static OutputGetMaintenanceGroupList[] GetMaintenanceGroupList() =>
            ApiList.GetMaintenanceGroupList();

        public static OutputGetWorkOrderStatusTypeList[] GetWorkOrderStatusTypeList() =>
            ApiList.GetWorkOrderStatusTypeList();

        [AccessToFormValidation(FormCode = "019")]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowActionHistory(long workOrderId)
        {
            var apiParam = new InputGetActionOrDelayListByWorkOrderId
            {
                WorkOrderId = workOrderId
            };
            var token = GetUserToken();
            var partailViewUrl = "~/Views/SchedulerWorkOrderPrintReport/Grid/ShowActionHistoryPartial.cshtml";

            var dataSource = ApiList.GetActionOrDelayListByWorkOrderId(apiParam, token);
            return PartialView(partailViewUrl, dataSource);
        }

        public ActionResult ShowReferenceHistory(long workOrderId)
        {
            var apiParam = new InputGetWorKOrderReferralListByWorkOrderId
            {
                WorkOrderId = workOrderId
            };
            var token = GetUserToken();
            const string partailViewUrl = "~/Views/SchedulerWorkOrderPrintReport/Grid/ShowReferenceHistoryPartial.cshtml";

            var dataSource = ApiList.GetWorKOrderReferralListByWorkOrderId(apiParam, token);
            return PartialView(partailViewUrl, dataSource);
        }
        public static OutputGetStopTypeList[] GetAllStopTypeList() =>
            ApiList.GetStopTypeList();

        public ActionResult WorKOrderConsumableList(long workOrderId)
        {
            var apiParam = new InputGetWorKOrderConsumableListByWorkOrderId
            {
                WorkOrderId = workOrderId
            };
            var token = GetUserToken();
            var partailViewUrl = "~/Views/SchedulerWorkOrderPrintReport/Grid/ShowWorKOrderConsumableList.cshtml";

            var dataSource = ApiList.GetWorKOrderConsumableListByWorkOrderId(apiParam, token);
            return PartialView(partailViewUrl, dataSource);
        }
        public ActionResult RegistrarInfoModal(InputGetWorkOrderRegistrantInfoByWorkOrderId input)
        {
            const string partialViewUrl = "~/Views/SchedulerWorkOrderPrintReport/Grid/RegistrarInfoModal.cshtml";

            var workOrderRegistrantInfoDataSource = ApiList.GetWorkOrderRegistrantInfoByWorkOrderId(input);

            return PartialView(partialViewUrl, workOrderRegistrantInfoDataSource);
        }
    }
}