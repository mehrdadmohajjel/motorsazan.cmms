using DevExpress.DataProcessing;
using DevExpress.Web.Mvc;
using Motorsazan.CMMS.Client.Api;
using Motorsazan.CMMS.Client.Filters;
using Motorsazan.CMMS.Shared.Enums;
using Motorsazan.CMMS.Shared.Models.DomainModels;
using Motorsazan.CMMS.Shared.Models.Input.NetExpert;
using Motorsazan.CMMS.Shared.Models.Input.ProductiveWorkOrder;
using Motorsazan.CMMS.Shared.Models.Output.ProductiveWorkOrder;
using Motorsazan.CMMS.Shared.Utilities;
using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Motorsazan.CMMS.Client.Controllers
{
    public class NetExpertController: BaseController
    {
        public ActionResult AddActionToWorkOrder(InputAddActionToWorkOrder input, int stopDurationInMinute,
            string persianStartDate, string persianEndDate)
        {
            var inputActionOrDelayListByWorkOrderId =
                new InputGetActionOrDelayListByWorkOrderId {WorkOrderId = input.WorkOrderId};

            var actionOrDelayListByWorkOrderId = GetActionOrDelayListByWorkOrderId(inputActionOrDelayListByWorkOrderId);

            var users = input.EmployeeIdList;

            var isAnyUserThatTheirDurationInMinuteIsNotValid = false;

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("<ul style='text-align:right;'>");
            stringBuilder.AppendLine("مجموع عملکرد ثبت شده برای نفرات زیر بیش از مدت زمان توقف ماشین می باشد:");
            stringBuilder.AppendLine();

            for(var i = 0; i < users.Length; i++)
            {
                var user = users[i];
                var userName = input.EmployeeNamesList[i];

                var durationInMinuteSum = actionOrDelayListByWorkOrderId
                    .Where(a => a.EmployeeId == user.EmployeeId)
                    .Sum(a => a.DurationInMinute);

                if(durationInMinuteSum + input.DurationInMinute <= stopDurationInMinute)
                {
                    continue;
                }

                isAnyUserThatTheirDurationInMinuteIsNotValid = true;
                stringBuilder.AppendLine($"<li>{userName}</li>");
            }

            stringBuilder.AppendLine("</ul>");

            if(isAnyUserThatTheirDurationInMinuteIsNotValid)
            {
                return Json(new {Message = stringBuilder.ToString(), IsSuccess = false});
            }

            var token = GetUserToken();

            input.UserId = GetCurrentUser().UserID;

            input.StartDate = Tools.ConvertToLatinDate(persianStartDate);
            input.EndDate = Tools.ConvertToLatinDate(persianEndDate);

            var apiResult = ApiList.AddActionToWorkOrder(input, token);

            return Json(new {Message = apiResult, IsSuccess = true});
        }

        public ActionResult AddCancelStatusToWorkOrder(InputAddCancelStatusToWorkOrder input)
        {
            var token = GetUserToken();

            input.UserId = GetCurrentUser().UserID;

            var apiResult = ApiList.AddCancelStatusToWorkOrder(input, token);

            return Json(apiResult);
        }

        public ActionResult AddOperationsModal(long workOrderId)
        {
            const string partialViewUrl = "~/Views/NetExpert/MainGrid/DetailRowModals/AddOperationsModal.cshtml";

            ViewData["workOrderId"] = workOrderId;

            return PartialView(partialViewUrl);
        }

        public ActionResult AddInspectionDetailsModal(long workOrderId)
        {
            const string partialViewUrl = "~/Views/NetExpert/MainGrid/DetailRowModals/AddInspectionDetailsModal.cshtml";

            var input = new InputGetWorkOrderByWorkOrderId {WorkOrderId = workOrderId};
            var model = ApiList.GetWorkOrderByWorkOrderId(input);


            return PartialView(partialViewUrl, model);
        }

        public ActionResult AddInspectionDetailsModalGrid(InputGetActionOrDelayListByWorkOrderId input)
        {
            var token = GetUserToken();
            const string partialViewUrl =
                "~/Views/NetExpert/MainGrid/DetailRowModals/AddInspectionDetailsModalGrid.cshtml";
            var model = ApiList.GetActionOrDelayListByWorkOrderId(input, token);


            return PartialView(partialViewUrl, model);
        }

        public ActionResult AddInspectionItemsComboBox(long workOrderId)
        {
            const string partialViewUrl =
                "~/Views/NetExpert/MainGrid/DetailRowModals/AddInspectionItemsComboBox.cshtml";
            var input = new InputGetInspectionDetailByWorkOrderId {WorkOrderId = workOrderId};
            var dataSource = ApiList.GetInspectionDetailByWorkOrderId(input);

            return PartialView(partialViewUrl, dataSource);
        }

        public ActionResult AddInspectionEmployeeComboBox()
        {
            const string partialViewUrl =
                "~/Views/NetExpert/MainGrid/DetailRowModals/AddInspectionEmployeeComboBox.cshtml";

            var dataSource = ApiList.GetAllMaintenanceGroupMemberList();

            return PartialView(partialViewUrl, dataSource);
        }

        public ActionResult AddInspectionMaintenanceActionToWorkOrder(
            InputAddInspectionActionToWorkOrder input, string starFatDate, string endFaDate)
        {
            DateTime enStartDate;
            DateTime enEndDate;
            (enStartDate, enEndDate) = Tools.NormalizeDates(starFatDate, endFaDate, DatePeriodType.CustomPeriod);

            input.StartDate = enStartDate;
            input.EndDate = enEndDate;
            var token = GetUserToken();
            input.UserId = GetCurrentUser().UserID;
            var apiResult = ApiList.AddInspectionActionToWorkOrder(input, token);
            return Content(apiResult);
        }

        public ActionResult AddOperationsModalAddForm(long workOrderId)
        {
            const string partialViewUrl = "~/Views/NetExpert/MainGrid/DetailRowModals/AddOperationsModalAddForm.cshtml";

            var inputGetWorkOrderByWorkOrderId = new InputGetWorkOrderByWorkOrderId {WorkOrderId = workOrderId};

            var workOrderByWorkOrderId = ApiList.GetWorkOrderByWorkOrderId(inputGetWorkOrderByWorkOrderId);

            return PartialView(partialViewUrl, workOrderByWorkOrderId);
        }

        public ActionResult AddPreventiveMaintenanceActionToWorkOrder(
            InputAddPreventiveMaintenanceActionToWorkOrder input, string starFatDate, string endFaDate)
        {
            DateTime enStartDate;
            DateTime enEndDate;
            (enStartDate, enEndDate) = Tools.NormalizeDates(starFatDate, endFaDate, DatePeriodType.CustomPeriod);

            input.StartDate = enStartDate;
            input.EndDate = enEndDate;
            var token = GetUserToken();
            input.UserId = GetCurrentUser().UserID;
            var apiResult = ApiList.AddPreventiveMaintenanceActionToWorkOrder(input, token);
            return Content(apiResult);
        }

        public ActionResult BatchEditingUpdatePreventiveModel(InputEditActionOrDelayListInPreventiveScheduling input)
        {
            var token = GetUserToken();
            var apiResult = ApiList.EditActionOrDelayListInPreventiveScheduling(input, token);
            return Content(apiResult);
        }

        public ActionResult AddPreventiveItemsComboBox(long workOrderId)
        {
            const string partialViewUrl =
                "~/Views/NetExpert/MainGrid/DetailRowModals/AddPreventiveItemsComboBox.cshtml";
            var input = new InputGetPreventiveOperationItemListByWorkOrderId {WorkOrderId = workOrderId};
            var employeeListForPerformanceRegisteringDataSource =
                ApiList.GetPreventiveOperationItemListByWorkOrderId(input);

            return PartialView(partialViewUrl, employeeListForPerformanceRegisteringDataSource);
        }

        public ActionResult AddOperationsModalPreventiveItemsComboBox()
        {
            const string partialViewUrl =
                "~/Views/NetExpert/MainGrid/DetailRowModals/AddOperationsModalEmployeeCheckComboBox.cshtml";

            var employeeListForPerformanceRegisteringDataSource = ApiList.GetEmployeeListForPerformanceRegistering();

            return PartialView(partialViewUrl, employeeListForPerformanceRegisteringDataSource);
        }

        public ActionResult AddOperationsModalEmployeeCheckComboBox()
        {
            const string partialViewUrl =
                "~/Views/NetExpert/MainGrid/DetailRowModals/AddOperationsModalEmployeeCheckComboBox.cshtml";

            var employeeListForPerformanceRegisteringDataSource = ApiList.GetEmployeeListForPerformanceRegistering();

            return PartialView(partialViewUrl, employeeListForPerformanceRegisteringDataSource);
        }

        public ActionResult CheckMachineHasOperationItem(InputCheckMachineHasOperationItem input) =>
            Json(ApiList.CheckMachineHasOperationItem(input));

        public ActionResult WorkOrderCancelModal()
        {
            const string partialViewUrl = "~/Views/NetExpert/MainGrid/DetailRowModals/WorkOrderCancelModal.cshtml";

            return PartialView(partialViewUrl);
        }

        public ActionResult WorkOrderTempFinishWithDescriptionModal()
        {
            const string partialViewUrl =
                "~/Views/NetExpert/MainGrid/DetailRowModals/WorkOrderTempFinishWithDescriptionModal.cshtml";

            return PartialView(partialViewUrl);
        }

        public ActionResult WorkOrderTempFinishModal()
        {
            const string partialViewUrl = "~/Views/NetExpert/MainGrid/DetailRowModals/WorkOrderTempFinishModal.cshtml";

            return PartialView(partialViewUrl);
        }

        public ActionResult WorkOrderTempFinishModalMaintenanceGroupCombo()
        {
            const string partialViewUrl =
                "~/Views/NetExpert/MainGrid/DetailRowModals/WorkOrderTempFinishModalMaintenanceGroupCombo.cshtml";

            var maintenanceGroupDataSource = ApiList.GetMaintenanceGroupList();

            return PartialView(partialViewUrl, maintenanceGroupDataSource);
        }

        public ActionResult WorkOrderTempFinishModalOperationItemCombo(
            InputGetOperationItemListByMaintenanceGroupIdAndMachineId input)
        {
            const string partialViewUrl =
                "~/Views/NetExpert/MainGrid/DetailRowModals/WorkOrderTempFinishModalOperationItemCombo.cshtml";

            input.OperationItemTypeId = OperationItemType.Accidental;

            var maintenanceGroupDataSource = ApiList.GetOperationItemListByMaintenanceGroupIdAndMachineId(input);

            return PartialView(partialViewUrl, maintenanceGroupDataSource);
        }

        public ActionResult AddNetFinishedStatusToWorkOrder(InputAddNetFinishedStatusToWorkOrder input)
        {
            var token = GetUserToken();

            input.UserId = GetCurrentUser().UserID;

            var apiResult = ApiList.AddNetFinishedStatusToWorkOrder(input, token);

            return Json(apiResult);
        }

        public ActionResult StocksGrid(InputGetStockPurchaseRequestForStockWaitingByWorkOrderId input)
        {
            const string partialViewUrl = "~/Views/NetExpert/MainGrid/DetailRowModals/StocksGrid.cshtml";

            var source = ApiList.GetStockPurchaseRequestForStockWaitingByWorkOrderId(input);

            return PartialView(partialViewUrl, source);
        }

        public ActionResult AddRepairFinishedStatusToWorkOrder(InputAddRepairFinishedStatusToWorkOrder input)
        {
            var token = GetUserToken();

            input.UserId = GetCurrentUser().UserID;

            var apiResult = ApiList.AddRepairFinishedStatusToWorkOrder(input, token);

            return Json(apiResult);
        }

        public ActionResult AddOperationsModalOperationTypeCombo()
        {
            const string partialViewUrl =
                "~/Views/NetExpert/MainGrid/DetailRowModals/AddOperationsModalOperationTypeCombo.cshtml";

            return PartialView(partialViewUrl);
        }

        public ActionResult AddOperationsModalDelayReasonCombo()
        {
            const string partialViewUrl =
                "~/Views/NetExpert/MainGrid/DetailRowModals/AddOperationsModalDelayReasonCombo.cshtml";

            var inputGetDelayTypeListByActivity = new InputGetDelayTypeListByActivity {IsActivity = false};

            var delayTypeListByActivityDataSource = ApiList.GetDelayTypeListByActivity(inputGetDelayTypeListByActivity);

            return PartialView(partialViewUrl, delayTypeListByActivityDataSource);
        }

        public ActionResult StockWaitingModal()
        {
            const string partialViewUrl = "~/Views/NetExpert/MainGrid/DetailRowModals/StockWaitingModal.cshtml";

            return PartialView(partialViewUrl);
        }

        public ActionResult AddPreventiveDetailsModal(long workOrderId)
        {
            const string partialViewUrl = "~/Views/NetExpert/MainGrid/DetailRowModals/AddPreventiveDetailsModal.cshtml";

            var input = new InputGetWorkOrderByWorkOrderId {WorkOrderId = workOrderId};
            var model = ApiList.GetWorkOrderByWorkOrderId(input);


            return PartialView(partialViewUrl, model);
        }

        public ActionResult AddPreventiveEmployeeComboBox(long maintenanceGroupId)
        {
            const string partialViewUrl =
                "~/Views/NetExpert/MainGrid/DetailRowModals/AddPreventiveEmployeeComboBox.cshtml";

            var input = new InputGetMaintenanceGroupMemberListByMaintenanceGroupId
            {
                MaintenanceGroupId = maintenanceGroupId
            };
            var model = ApiList.GetMaintenanceGroupMemberListByMaintenanceGroupId(input);


            return PartialView(partialViewUrl, model);
        }

        public ActionResult StockWaitingModalGrid(InputGetPurchaseByRequestNumberAndRequestYear input)
        {
            const string partialViewUrl = "~/Views/NetExpert/MainGrid/DetailRowModals/StockWaitingModalGrid.cshtml";

            var source = ApiList.GetPurchaseByRequestNumberAndRequestYear(input);

            return PartialView(partialViewUrl, source);
        }

        public ActionResult AddStockWaitingStatusToWorkOrder(InputAddStockWaitingStatusToWorkOrder input)
        {
            input.UserId = GetCurrentUser().UserID;
            var token = GetUserToken();

            var apiResult = ApiList.AddStockWaitingStatusToWorkOrder(input, token);

            return Json(apiResult);
        }

        public ActionResult AddOperationsModalGrid(InputGetActionOrDelayListByWorkOrderId input)
        {
            const string partialViewUrl = "~/Views/NetExpert/MainGrid/DetailRowModals/AddOperationsModalGrid.cshtml";

            var actionOrDelayListDataSource = GetActionOrDelayListByWorkOrderId(input);

            return PartialView(partialViewUrl, actionOrDelayListDataSource);
        }

        public OutputGetActionOrDelayListByWorkOrderId[] GetActionOrDelayListByWorkOrderId(
            InputGetActionOrDelayListByWorkOrderId input)
        {
            var token = GetUserToken();

            var actionOrDelayListDataSource = ApiList.GetActionOrDelayListByWorkOrderId(input, token);

            return actionOrDelayListDataSource;
        }

        public ActionResult AddPreventiveDetailsModalGrid(InputGetActionOrDelayListByWorkOrderId input)
        {
            var token = GetUserToken();
            const string partialViewUrl =
                "~/Views/NetExpert/MainGrid/DetailRowModals/AddPreventiveDetailsModalGrid.cshtml";
            var model = ApiList.GetActionOrDelayListByWorkOrderId(input, token);


            return PartialView(partialViewUrl, model);
        }

        public ActionResult ReferToAnotherGroupModalGroupCombo()
        {
            const string partialViewUrl =
                "~/Views/NetExpert/MainGrid/DetailRowModals/ReferToAnotherGroupModalGroupCombo.cshtml";

            var maintenanceGroupDataSource = ApiList.GetMaintenanceGroupList();

            return PartialView(partialViewUrl, maintenanceGroupDataSource);
        }

        public ActionResult ReferToAnotherPersonModalPersonCombo(
            InputGetMaintenanceGroupMemberListByMaintenanceGroupId input)
        {
            const string partialViewUrl =
                "~/Views/NetExpert/MainGrid/DetailRowModals/ReferToAnotherPersonModalPersonCombo.cshtml";

            var maintenanceGroupMemberListDataSource = ApiList.GetMaintenanceGroupMemberListByMaintenanceGroupId(input);

            return PartialView(partialViewUrl, maintenanceGroupMemberListDataSource);
        }

        public ActionResult RemoveActionOrDelayByActionOrDelayListId(
            InputRemoveActionOrDelayByActionOrDelayListId input)
        {
            var token = GetUserToken();

            var apiResult = ApiList.RemoveActionOrDelayByActionOrDelayListId(input, token);

            return Json(apiResult);
        }

        public ActionResult FinishingStockWaitingForWorkOrder(InputFinishingStockWaitingForWorkOrder input)
        {
            var token = GetUserToken();
            input.UserId = GetCurrentUser().UserID;
            var apiResult = ApiList.FinishingStockWaitingForWorkOrder(input, token);

            return Json(apiResult);
        }

        public ActionResult FilterFormStatusCombo()
        {
            const string partialViewUrl = "~/Views/NetExpert/FilterForm/FilterFormStatusCombo.cshtml";

            var workOrderStatusTypeListDataSource = ApiList.GetWorkOrderStatusTypeList();

            var allStatusItem = new OutputGetWorkOrderStatusTypeList
            {
                WorkOrderStatusTypeId = 0, TypeName = "همه وضعیت ها"
            };

            var dataSource = Tools.PrependGetAllItemToArray(workOrderStatusTypeListDataSource, allStatusItem);

            var referredStatusType = dataSource.SingleOrDefault(status => status.TypeName == "ارجاع شده");

            ViewData["SelectedIndex"] = referredStatusType == null
                ? -1
                : dataSource.IndexOf(referredStatusType);


            return PartialView(partialViewUrl, dataSource);
        }

        public ActionResult MainGridDetailRow(InputGetActionOrDelayListByWorkOrderId input,
            WorkOrderStatusType workOrderStatusType, WorkOrderType workOrderType, long? machineId)
        {
            const string partialViewUrl = "~/Views/NetExpert/MainGrid/MainGridDetailRow.cshtml";
            var item = new InputCheckAllHavalehNOConfirmedByWorkOrderId {WorkOrderId = input.WorkOrderId};
            var token = GetUserToken();

            var workOrderActionOrDelay = ApiList.GetActionOrDelayListByWorkOrderId(input, token);
            var isAllHavalehNoConfirmedResult = ApiList.CheckAllHavalehNOConfirmedByWorkOrderId(item);

            ViewData["WorkOrderStatusType"] = workOrderStatusType;
            ViewData["WorkOrderType"] = workOrderType;
            ViewData["HasAnyActionOrDelay"] = workOrderActionOrDelay.Length > 0;
            ViewData["IsAllHavalehNOConfirmed"] = isAllHavalehNoConfirmedResult.IsAllHavalehNOConfirmed;
            ViewData["HasMachine"] = machineId != null;

            return PartialView(partialViewUrl);
        }

        public ActionResult ReferToAnotherPersonModal(InputGetWorkOrderByWorkOrderId input)
        {
            const string partialViewUrl = "~/Views/NetExpert/MainGrid/DetailRowModals/ReferToAnotherPersonModal.cshtml";

            var workOrderDataSource = ApiList.GetWorkOrderByWorkOrderId(input);

            return PartialView(partialViewUrl, workOrderDataSource);
        }

        public ActionResult ReferToAnotherGroupModal()
        {
            const string partialViewUrl = "~/Views/NetExpert/MainGrid/DetailRowModals/ReferToAnotherGroupModal.cshtml";

            return PartialView(partialViewUrl);
        }

        public ActionResult SendWorkOrderToMaintenanceGroup(InputSendWorkOrderToMaintenanceGroup input)
        {
            input.UserId = GetCurrentUser().UserID;

            var token = GetUserToken();

            var apiResult = ApiList.SendWorkOrderToMaintenanceGroup(input, token);

            return Json(apiResult);
        }

        public ActionResult SendWorkOrderToMaintenanceGroupMember(InputSendWorkOrderToMaintenanceGroupMember input)
        {
            input.UserId = GetCurrentUser().UserID;

            var token = GetUserToken();

            var apiResult = ApiList.SendWorkOrderToMaintenanceGroupMember(input, token);

            return Json(apiResult);
        }

        public ActionResult ReferralsHistoryGrid(InputGetWorKOrderReferralListByWorkOrderId input)
        {
            const string partialViewUrl = "~/Views/NetExpert/MainGrid/DetailRowModals/ReferralsHistoryGrid.cshtml";

            var token = GetUserToken();

            var workOrderReferralListDataSource = ApiList.GetWorKOrderReferralListByWorkOrderId(input, token);

            return PartialView(partialViewUrl, workOrderReferralListDataSource);
        }

        public ActionResult MainGrid(InputGetNetExpertWorkOrderListByCondition input, string persianStartDate,
            string persianEndDate, DatePeriodType datePeriodType)
        {
            const string partialViewUrl = "~/Views/NetExpert/MainGrid/MainGrid.cshtml";

            (input.StartDate, input.EndDate) = Tools.NormalizeDates(persianStartDate, persianEndDate, datePeriodType);

            input.UserId = GetCurrentUser().UserID;

            var netExpertWorkOrdersDataSource = ApiList.GetNetExpertWorkOrderListByCondition(input);

            return PartialView(partialViewUrl, netExpertWorkOrdersDataSource);
        }

        [AccessToFormValidation(FormCode = "011")]
        public ActionResult Index()
        {
            ViewData["EId"] = GetCurrentUser().EID;
            return View();
        }

        public ActionResult GetMaintenanceGroupListByEId(InputGetMaintenanceGroupListByEId input) =>
            Json(ApiList.GetMaintenanceGroupListByEId(input));

        public ActionResult RegistrarInfoModal(InputGetWorkOrderRegistrantInfoByWorkOrderId input)
        {
            const string partialViewUrl = "~/Views/NetExpert/MainGrid/DetailRowModals/RegistrarInfoModal.cshtml";

            var workOrderRegistrantInfoDataSource = ApiList.GetWorkOrderRegistrantInfoByWorkOrderId(input);

            return PartialView(partialViewUrl, workOrderRegistrantInfoDataSource);
        }

        public ActionResult PerformedOperationsGrid(InputGetActionOrDelayListByWorkOrderId input)
        {
            const string partialViewUrl = "~/Views/NetExpert/MainGrid/DetailRowModals/PerformedOperationsGrid.cshtml";

            var token = GetUserToken();

            var workOrderActionOrDelayListDataSource = ApiList.GetActionOrDelayListByWorkOrderId(input, token);

            return PartialView(partialViewUrl, workOrderActionOrDelayListDataSource);
        }

        public ActionResult ConsumingMaterialsGrid(InputGetWorKOrderConsumableListByWorkOrderId input)
        {
            const string partialViewUrl = "~/Views/NetExpert/MainGrid/DetailRowModals/ConsumingMaterialsGrid.cshtml";

            var token = GetUserToken();

            var worKOrderConsumableListDataSource = ApiList.GetWorKOrderConsumableListByWorkOrderId(input, token);

            return PartialView(partialViewUrl, worKOrderConsumableListDataSource);
        }

        public ActionResult UpdateConsumingMaterials(
            MVCxGridViewBatchUpdateValues<OutputGetWorKOrderConsumableListByWorkOrderId> input)
        {
            const string partialViewUrl = "~/Views/NetExpert/MainGrid/DetailRowModals/ConsumingMaterialsGrid.cshtml";
            var token = GetUserToken();
            long workorderId = 0;
            var inputArray = input.Update.ToArray();
            var havaleArray = new Tbl_SetHavaleNOListByHavalehWorkOrderId[inputArray.Length];
            for(var i = 0; i < inputArray.Length; i++)
            {
                havaleArray[i] = new Tbl_SetHavaleNOListByHavalehWorkOrderId
                {
                    HavaleNo = Convert.ToInt32(inputArray[i].HavaleNO),
                    HavaleWorkOrderReferralID = Convert.ToInt64(inputArray[i].HavaleWorkOrderReferralId)
                };
                workorderId = Convert.ToInt64(inputArray[0].WorkOrderId);
            }

            var item = new InputSetHavaleNOListByHavalehWorkOrderId {HavaleWorkOrderReferralIDList = havaleArray};
            _ = EditReferralNumber(item);
            var list = new InputGetWorKOrderConsumableListByWorkOrderId {WorkOrderId = workorderId};
            var worKOrderConsumableListDataSource = ApiList.GetWorKOrderConsumableListByWorkOrderId(list, token);

            return PartialView(partialViewUrl, worKOrderConsumableListDataSource);
        }

        public string EditReferralNumber(InputSetHavaleNOListByHavalehWorkOrderId input)
        {
            var token = GetUserToken();
            var result = ApiList.SetHavaleNOListByHavalehWorkOrderIDList(input, token);
            return result;
        }

        public static OutputGetWorkOrderStatusTypeList[] GetWorkOrderStatusTypeList() =>
            ApiList.GetWorkOrderStatusTypeList();

        public static OutputGetStopTypeList[] GetAllStopTypeList() =>
            ApiList.GetStopTypeList();

        public ActionResult GetWorkOrderStatusHistoryByWorkOrderId(InputGetWorkOrderStatusHistoryByWorkOrderId input)
        {
            const string partialViewUrl =
                "~/Views/NetExpert/MainGrid/DetailRowModals/WorkOrderStatusHistoryGrid.cshtml";
            var result = ApiList.GetWorkOrderStatusHistoryByWorkOrderId(input);
            return PartialView(partialViewUrl, result);
        }
    }
}