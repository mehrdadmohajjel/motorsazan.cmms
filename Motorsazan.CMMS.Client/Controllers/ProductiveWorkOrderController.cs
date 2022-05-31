using Motorsazan.CMMS.Client.Api;
using Motorsazan.CMMS.Client.Filters;
using Motorsazan.CMMS.Shared.Enums;
using Motorsazan.CMMS.Shared.Models.Input.NetExpert;
using Motorsazan.CMMS.Shared.Models.Input.ProductiveWorkOrder;
using Motorsazan.CMMS.Shared.Models.Output.ProductiveWorkOrder;
using Motorsazan.CMMS.Shared.Utilities;
using System;
using System.Web.Mvc;
using Motorsazan.CMMS.Shared.Models.Input.Inspection;
using Motorsazan.CMMS.Shared.Models.Output.Inspection;

namespace Motorsazan.CMMS.Client.Controllers
{
    public class ProductiveWorkOrderController: BaseController
    {
        public ActionResult AddFormGetDepartmentMachineList(int departmentId = 0)
        {
            const string partialViewUrl = "~/Views/ProductiveWorkOrder/AddForm/AddFormGetDepartmentMachineList.cshtml";
            var apiParam = new InputGetMainMachineListBySubDepartmentId { DepartmentId = departmentId};

            var dataSource = GetDepartmentMachineList(apiParam);
            return PartialView(partialViewUrl, dataSource);
        }

        [AccessToEventValidation(EventCode = "002", FormCode = "009")]
        public ActionResult AddNewProductiveWorkOrder(InputAddProductiveWorkOrder values)
        {
            var token = GetUserToken();
            values.UserId = GetCurrentUser().UserID;
            var apiResult = ApiList.AddProductiveWorkOrder(values, token);
            return Content(apiResult);
        }

        public ActionResult DetailRow(long workOrderId = 0)
        {
            const string partialViewUrl = "~/Views/ProductiveWorkOrder/Grid/DetailRow.cshtml";

            var apiParam = new InputGetWorkOrderByWorkOrderId {WorkOrderId = workOrderId};
            var token = GetUserToken();
            var workOrderItem = ApiList.GetWorkOrderByWorkOrderId(apiParam, token);

            return PartialView(partialViewUrl, workOrderItem);
        }

        public ActionResult GetMaintenanceGroupList()
        {
            const string partialViewUrl = "~/Views/ProductiveWorkOrder/AddForm/AddFormMaintenanceGroupListCombo.cshtml";
            var maintenanceGroupsDataSource = ApiList.GetMaintenanceGroupList();
            return PartialView(partialViewUrl, maintenanceGroupsDataSource);
        }

        public ActionResult GetEmployeeDepartmentList()
        {
            var userId = GetCurrentUser().UserID;
            const string partialViewUrl =
                "~/Views/ProductiveWorkOrder/AddForm/AddFormEmployeeDepartmentListCombo.cshtml";
            var apiParams = new InputGetCurrentUserDepartmentList {UserId = userId};

            var departmentsDataSource = ApiList.GetCurrentUserDepartmentList(apiParams);
            return PartialView(partialViewUrl, departmentsDataSource);
        }

        public ActionResult InitRate(long workOrderId)
        {
            var values = new InputGetWorkOrderRateByWorkOrderId {WorkOrderId = workOrderId};
            var apiResult = ApiList.GetWorkOrderRateByWorkOrderID(values);
            return Content(apiResult);
        }

        public ActionResult RatePeople(long workOrderId)
        {
            const string partialViewUrl = "~/Views/ProductiveWorkOrder/Grid/AddRatePeople.cshtml";
            ViewData["workOrderId"] = workOrderId;
            return PartialView(partialViewUrl);
        }

        public ActionResult ShowSetWorkOrderStatusToCancelByCustomer(long workOrderId)
        {
            const string partialViewUrl =
                "~/Views/ProductiveWorkOrder/Grid/ShowSetWorkOrderStatusToCancelByCustomer.cshtml";
            ViewData["workOrderId"] = workOrderId;
            return PartialView(partialViewUrl);
        }

        public ActionResult ShowSetWorkOrderStatusToNotConfirmFinishByCustomer(long workOrderId)
        {
            const string partialViewUrl =
                "~/Views/ProductiveWorkOrder/Grid/ShowSetWorkOrderStatusToNotConfirmFinishByCustomer.cshtml";
            ViewData["workOrderId"] = workOrderId;
            return PartialView(partialViewUrl);
        }

        public ActionResult GetStopTypeList()
        {
            const string partialViewUrl = "~/Views/ProductiveWorkOrder/AddForm/AddFormStoptypeListCombo.cshtml";
            var stopTypeDataSource = ApiList.GetStopTypeListForProductiveWorkOrder();
            return PartialView(partialViewUrl, stopTypeDataSource);
        }

        public ActionResult GetProductiveWorkOrderAllStatusList()
        {
            const string partailViewUrl = "~/Views/ProductiveWorkOrder/FilterForm/FilterFormStatusTypeCombo.cshtml";

            var dataSource = ApiList.GetWorkOrderStatusTypeList();
            var statusItem = new OutputGetWorkOrderStatusTypeList {WorkOrderStatusTypeId = 0, TypeName = "انتخاب همه"};
            var alldataSource = Tools.PrependGetAllItemToArray(dataSource, statusItem);
            return PartialView(partailViewUrl, alldataSource);
        }

        public OutputGetMainMachineListBySubDepartmentId[] GetDepartmentMachineList(InputGetMainMachineListBySubDepartmentId values)
        {
            if(values.DepartmentId == 0)
            {
                return new OutputGetMainMachineListBySubDepartmentId[0];
            }

            return ApiList.GetMainMachineListBySubDepartmentId(values);
        }

        [AccessToFormValidation(FormCode = "009")]
        public ActionResult Index() => View();

        public ActionResult ProductiveWorkOrderGird(string startShamsiDate, string endPersianDate,
            long workOrderStatusTypeId = 0, DatePeriodType dateType = DatePeriodType.RecentMonth)
        {
            const string partialViewUrl = "~/Views/ProductiveWorkOrder/Grid/ProductiveWorkOrderGrid.cshtml";
            DateTime enStartDate;
            DateTime enEndDate;
            (enStartDate, enEndDate) = Tools.NormalizeDates(startShamsiDate, endPersianDate, dateType);

            var apiParam = new InputGetProductiveWorkOrderByCondition
            {
                EndDate = enEndDate,
                StartDate = enStartDate,
                UserID = GetCurrentUser().UserID,
                WorkOrderStatusTypeID = workOrderStatusTypeId
            };
            var token = GetUserToken();
            var dataSource = ApiList.GetProductiveWorkOrderByCondition(apiParam, token);
            return PartialView(partialViewUrl, dataSource);
        }

        public ActionResult RemoveWorkOrder(InputSetWorkOrderStatusToCancelByCustomer value)
        {
            var token = GetUserToken();
            value.UserId = GetCurrentUser().UserID;
            var apiResult = ApiList.SetWorkOrderStatusToCancelByCustomer(value, token);
            return Content(apiResult);
        }

        public ActionResult ProveEndWorkOrder(InputSetWorkOrderStatusToConfirmFinishByCustomer value)
        {
            var token = GetUserToken();
            value.UserId = GetCurrentUser().UserID;
            var apiResult = ApiList.SetWorkOrderStatusToConfirmFinishByCustomer(value, token);
            return Content(apiResult);
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

        public ActionResult FailEndWorkOrder(InputSetWorkOrderStatusToNotConfirmFinishByCustomer value)
        {
            var token = GetUserToken();
            value.UserId = GetCurrentUser().UserID;
            var apiResult = ApiList.SetWorkOrderStatusToNotConfirmFinishByCustomer(value, token);
            return Content(apiResult);
        }

        public ActionResult ShowActionHistory(long workOrderId)
        {
            var apiParam = new InputGetActionOrDelayListByWorkOrderId {WorkOrderId = workOrderId};
            var token = GetUserToken();
            var partailViewUrl = "~/Views/ProductiveWorkOrder/Grid/ShowActionHistoryPartial.cshtml";

            var dataSource = ApiList.GetActionOrDelayListByWorkOrderId(apiParam, token);
            return PartialView(partailViewUrl, dataSource);
        }

        public ActionResult WorKOrderConsumableList(long workOrderId)
        {
            var apiParam = new InputGetWorKOrderConsumableListByWorkOrderId {WorkOrderId = workOrderId};
            var token = GetUserToken();
            var partailViewUrl = "~/Views/ProductiveWorkOrder/Grid/ShowWorKOrderConsumableList.cshtml";

            var dataSource = ApiList.GetWorKOrderConsumableListByWorkOrderId(apiParam, token);
            return PartialView(partailViewUrl, dataSource);
        }

        public ActionResult ShowReferenceHistory(long workOrderId)
        {
            var apiParam = new InputGetWorKOrderReferralListByWorkOrderId {WorkOrderId = workOrderId};
            var token = GetUserToken();
            const string partailViewUrl = "~/Views/ProductiveWorkOrder/Grid/ShowReferenceHistoryPartial.cshtml";

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