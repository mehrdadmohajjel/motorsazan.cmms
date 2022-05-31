using Motorsazan.CMMS.Client.Api;
using Motorsazan.CMMS.Client.Filters;
using Motorsazan.CMMS.Shared.Enums;
using Motorsazan.CMMS.Shared.Models.Input.NetExpert;
using Motorsazan.CMMS.Shared.Models.Input.NoneProductiveWorkOrder;
using Motorsazan.CMMS.Shared.Models.Input.ProductiveWorkOrder;
using Motorsazan.CMMS.Shared.Models.Output.ProductiveWorkOrder;
using Motorsazan.CMMS.Shared.Utilities;
using System;
using System.Web.Mvc;

namespace Motorsazan.CMMS.Client.Controllers
{
    public class NoneProductiveWorkOrderController: BaseController
    {
        public ActionResult AddNewNoneProductiveWorkOrder(InputAddNoneProductiveWorkOrder values)
        {
            var token = GetUserToken();
            values.UserId = GetCurrentUser().UserID;
            var apiResult = ApiList.AddNoneProductiveWorkOrder(values, token);
            return Content(apiResult);
        }

        public ActionResult DetailRow(long workOrderId = 0)
        {
            const string partialViewUrl = "~/Views/NoneProductiveWorkOrder/Grid/DetailRow.cshtml";
            var apiParam = new InputGetWorkOrderByWorkOrderId {WorkOrderId = workOrderId};
            var token = GetUserToken();
            var workOrderItem = ApiList.GetWorkOrderByWorkOrderId(apiParam, token);

            return PartialView(partialViewUrl, workOrderItem);
        }

        public ActionResult DenyEndWorkOrderAndCreateNewWorkOrder(InputGetWorkOrderByWorkOrderId value)
        {
            var token = GetUserToken();
            var apiResult = ApiList.GetWorkOrderByWorkOrderId(value, token);
            var item = new InputAddProductiveWorkOrder
            {
                DepartmentId = apiResult.DepartmentId,
                MachineId = apiResult.MachineId,
                MaintenanceGroupId = apiResult.ReceiverMaintenanceGroupId,
                StopTypeId = apiResult.StopTypeId,
                UserId = apiResult.UserId,
                Description = apiResult.Description,
                HasScrap = apiResult.HasScrap,
                RelativeWorkOrderId = value.WorkOrderId
            };
            var finalApiResult = ApiList.AddProductiveWorkOrder(item, token);
            return Content(finalApiResult);
        }

        public ActionResult GetMaintenanceGroupList()
        {
            const string partialViewUrl =
                "~/Views/NoneProductiveWorkOrder/AddForm/AddFormMaintenanceGroupListCombo.cshtml";
            var maintenaGroupListnceGroupListDataSource = ApiList.GetMaintenanceGroupList();
            return PartialView(partialViewUrl, maintenaGroupListnceGroupListDataSource);
        }

        public ActionResult GetEmployeeDepartmentList()
        {
            const string partialViewUrl =
                "~/Views/NoneProductiveWorkOrder/AddForm/AddFormEmployeeDepartmentListCombo.cshtml";
            var item = new InputGetCurrentUserDepartmentList {UserId = GetCurrentUser().UserID};
            var currentUserDepartmentListDataSource = ApiList.GetCurrentUserDepartmentList(item);
            return PartialView(partialViewUrl, currentUserDepartmentListDataSource);
        }

        public ActionResult FailEndWorkOrder(InputSetWorkOrderStatusToNotConfirmFinishByCustomer value)
        {
            var token = GetUserToken();
            value.UserId = GetCurrentUser().UserID;
            var apiResult = ApiList.SetWorkOrderStatusToNotConfirmFinishByCustomer(value, token);
            return Content(apiResult);
        }

        public ActionResult GetStopTypeList()
        {
            const string partialViewUrl = "~/Views/NoneProductiveWorkOrder/AddForm/AddFormStoptypeListCombo.cshtml";
            var stopTypeDataSource = ApiList.GetStopTypeListForNoneProductiveWorkOrder();
            return PartialView(partialViewUrl, stopTypeDataSource);
        }

        public ActionResult GetNoneProductiveWorkOrderAllStatusList()
        {
            const string partailViewUrl = "~/Views/NoneProductiveWorkOrder/FilterForm/FilterFormStatusTypeCombo.cshtml";

            var dataSource = ApiList.GetWorkOrderStatusTypeList();
            var statusItem = new OutputGetWorkOrderStatusTypeList {WorkOrderStatusTypeId = 0, TypeName = "انتخاب همه"};
            var alldataSource = Tools.PrependGetAllItemToArray(dataSource, statusItem);

            return PartialView(partailViewUrl, alldataSource);
        }

        public OutputGetWorkOrderStatusTypeList[] GetWorkOrderStatusList() => ApiList.GetWorkOrderStatusTypeList();

        public ActionResult NoneProductiveWorkOrderGird(string startShamsiDate, string endPersianDate,
            long workOrderStatusTypeId = 0, DatePeriodType dateType = DatePeriodType.RecentMonth)
        {
            const string partialViewUrl = "~/Views/NoneProductiveWorkOrder/Grid/NoneProductiveWorkOrderGrid.cshtml";

            if(workOrderStatusTypeId == -1)
            {
                return PartialView(partialViewUrl);
            }

            DateTime enStartDate;
            DateTime enEndDate;
            (enStartDate, enEndDate) = Tools.NormalizeDates(startShamsiDate, endPersianDate, dateType);

            var apiParam = new InputGetNoneProductiveWorkOrderByCondition
            {
                EndDate = enEndDate,
                StartDate = enStartDate,
                UserId = GetCurrentUser().UserID,
                WorkOrderStatusTypeId = workOrderStatusTypeId
            };
            var token = GetUserToken();

            var dataSource = ApiList.GetNoneProductiveWorkOrderByCondition(apiParam, token);
            return PartialView(partialViewUrl, dataSource);
        }

        public ActionResult Index() => View();

        public ActionResult ProveEndWorkOrder(InputSetWorkOrderStatusToConfirmFinishByCustomer value)
        {
            var token = GetUserToken();
            value.UserId = GetCurrentUser().UserID;
            var apiResult = ApiList.SetWorkOrderStatusToConfirmFinishByCustomer(value, token);
            return Content(apiResult);
        }

        public ActionResult InitRate(long workOrderId)
        {
            var values = new InputGetWorkOrderRateByWorkOrderId {WorkOrderId = workOrderId};
            var apiResult = ApiList.GetWorkOrderRateByWorkOrderID(values);
            return Content(apiResult);
        }

        public ActionResult RatePeople(long workOrderId)
        {
            const string partialViewUrl = "~/Views/NoneProductiveWorkOrder/Grid/AddRatePeople.cshtml";
            ViewData["workOrderId"] = workOrderId;
            return PartialView(partialViewUrl);
        }

        public ActionResult RemoveWorkOrder(InputSetWorkOrderStatusToCancelByCustomer value)
        {
            var token = GetUserToken();
            value.UserId = GetCurrentUser().UserID;
            var apiResult = ApiList.SetWorkOrderStatusToCancelByCustomer(value, token);
            return Content(apiResult);
        }

        public ActionResult ShowActionHistory(long workOrderId)
        {
            const string partailViewUrl = "~/Views/NoneProductiveWorkOrder/Grid/ShowActionHistoryPartial.cshtml";

            var apiParam = new InputGetActionOrDelayListByWorkOrderId {WorkOrderId = workOrderId};
            var token = GetUserToken();

            var dataSource = ApiList.GetActionOrDelayListByWorkOrderId(apiParam, token);
            return PartialView(partailViewUrl, dataSource);
        }

        public ActionResult ShowSetWorkOrderStatusToCancelByCustomer(long workOrderId)
        {
            const string partialViewUrl =
                "~/Views/NoneProductiveWorkOrder/Grid/ShowSetWorkOrderStatusToCancelByCustomer.cshtml";
            ViewData["workOrderId"] = workOrderId;
            return PartialView(partialViewUrl);
        }

        public ActionResult ShowSetWorkOrderStatusToNotConfirmFinishByCustomer(long workOrderId)
        {
            const string partialViewUrl =
                "~/Views/NoneProductiveWorkOrder/Grid/ShowSetWorkOrderStatusToNotConfirmFinishByCustomer.cshtml";
            ViewData["workOrderId"] = workOrderId;
            return PartialView(partialViewUrl);
        }

        public ActionResult GetWorKOrderConsumableListByWorkOrderId(long workOrderId)
        {
            const string partailViewUrl = "~/Views/NoneProductiveWorkOrder/Grid/ShowWorKOrderConsumableList.cshtml";

            var apiParam = new InputGetWorKOrderConsumableListByWorkOrderId {WorkOrderId = workOrderId};
            var token = GetUserToken();

            var dataSource = ApiList.GetWorKOrderConsumableListByWorkOrderId(apiParam, token);
            return PartialView(partailViewUrl, dataSource);
        }

        public ActionResult ShowReferenceHistory(long workOrderId)
        {
            const string partailViewUrl = "~/Views/NoneProductiveWorkOrder/Grid/ShowReferenceHistoryPartial.cshtml";
            var apiParam = new InputGetWorKOrderReferralListByWorkOrderId {WorkOrderId = workOrderId};
            var token = GetUserToken();

            var dataSource = ApiList.GetWorKOrderReferralListByWorkOrderId(apiParam, token);
            return PartialView(partailViewUrl, dataSource);
        }

        public ActionResult UpdateRating(InputEditWorkOrderRateByCustomer values)
        {
            var token = GetUserToken();

            var apiResult = ApiList.EditWorkOrderRateByCustomer(values, token);
            return Content(apiResult);
        }
    }
}