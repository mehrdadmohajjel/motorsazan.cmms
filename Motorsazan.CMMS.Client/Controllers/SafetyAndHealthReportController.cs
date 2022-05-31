using System;
using System.Web.Mvc;
using Motorsazan.CMMS.Client.Api;
using Motorsazan.CMMS.Client.Filters;
using Motorsazan.CMMS.Shared.Enums;
using Motorsazan.CMMS.Shared.Models.Input.NetExpert;
using Motorsazan.CMMS.Shared.Models.Input.ProductiveWorkOrder;
using Motorsazan.CMMS.Shared.Models.Input.SafetyAndHealthReport;
using Motorsazan.CMMS.Shared.Models.Output.ProductiveWorkOrder;
using Motorsazan.CMMS.Shared.Utilities;

namespace Motorsazan.CMMS.Client.Controllers
{
    public class SafetyAndHealthReportController: BaseController
    {
        public ActionResult DetailRow()
        {
            const string partialViewUrl = "~/Views/SafetyAndHealthReport/Grid/DetailRow.cshtml";
            return PartialView(partialViewUrl);
        }

        public ActionResult FilterFormWorkOrderStatus()
        {
            const string partialViewUrl =
                "~/Views/SafetyAndHealthReport/FilterForm/FilterFormWorkOrderStatusPartial.cshtml";

            var workOrderStatus = ApiList.GetWorkOrderStatusTypeList();

            var allStatus = new OutputGetWorkOrderStatusTypeList {WorkOrderStatusTypeId = 0, TypeName = "همه وضعیت ها"};

            var dataSource = Tools.PrependGetAllItemToArray(workOrderStatus, allStatus);

            return PartialView(partialViewUrl, dataSource);
        }

        public static OutputGetMaintenanceGroupList[] GetMaintenanceGroupList() =>
            ApiList.GetMaintenanceGroupList();

        public static OutputGetWorkOrderStatusTypeList[] GetWorkOrderStatusTypeList() =>
            ApiList.GetWorkOrderStatusTypeList();

        public ActionResult Grid(long workOrderTypeId, long workOrderStatusTypeId,
            DatePeriodType dateType = DatePeriodType.RecentMonth, string startShamsiDate = "",
            string endPersianDate = "")
        {
            const string partialViewUrl = "~/Views/SafetyAndHealthReport/Grid/Grid.cshtml";

            if(workOrderTypeId == -1)
            {
                return PartialView(partialViewUrl);
            }

            DateTime enStartDate;
            DateTime enEndDate;
            (enStartDate, enEndDate) = Tools.NormalizeDates(startShamsiDate, endPersianDate, dateType);

            var apiParam = new InputGetSafetyAndHealthReportByCondition
            {
                WorkOrderTypeId = workOrderTypeId,
                WorkOrderStatusTypeId = workOrderStatusTypeId,
                EndDate = enEndDate,
                StartDate = enStartDate
            };

            var dataSource = ApiList.GetSafetyAndHealthReportByCondition(apiParam);
            return PartialView(partialViewUrl, dataSource);
        }

        [AccessToFormValidation(FormCode = "036")]
        public ActionResult Index() => View();

        public ActionResult RegistrarInfoModal(InputGetWorkOrderRegistrantInfoByWorkOrderId input)
        {
            const string partialViewUrl = "~/Views/SafetyAndHealthReport/Grid/RegistrarInfoModal.cshtml";

            var workOrderRegistrantInfoDataSource = ApiList.GetWorkOrderRegistrantInfoByWorkOrderId(input);

            return PartialView(partialViewUrl, workOrderRegistrantInfoDataSource);
        }

        public ActionResult ShowActionHistory(long workOrderId)
        {
            var apiParam = new InputGetActionOrDelayListByWorkOrderId {WorkOrderId = workOrderId};
            var token = GetUserToken();
            var partialViewUrl = "~/Views/SafetyAndHealthReport/Grid/ShowActionHistoryPartial.cshtml";

            var dataSource = ApiList.GetActionOrDelayListByWorkOrderId(apiParam, token);
            return PartialView(partialViewUrl, dataSource);
        }

        public ActionResult ShowReferenceHistory(long workOrderId)
        {
            var apiParam = new InputGetWorKOrderReferralListByWorkOrderId {WorkOrderId = workOrderId};
            var token = GetUserToken();
            const string partialViewUrl = "~/Views/SafetyAndHealthReport/Grid/ShowReferenceHistoryPartial.cshtml";

            var dataSource = ApiList.GetWorKOrderReferralListByWorkOrderId(apiParam, token);
            return PartialView(partialViewUrl, dataSource);
        }

        public ActionResult WorKOrderConsumableList(long workOrderId)
        {
            var apiParam = new InputGetWorKOrderConsumableListByWorkOrderId {WorkOrderId = workOrderId};
            var token = GetUserToken();
            var partialViewUrl = "~/Views/SafetyAndHealthReport/Grid/ShowWorKOrderConsumableList.cshtml";

            var dataSource = ApiList.GetWorKOrderConsumableListByWorkOrderId(apiParam, token);
            return PartialView(partialViewUrl, dataSource);
        }
    }
}