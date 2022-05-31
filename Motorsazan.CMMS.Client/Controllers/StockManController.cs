using System.Web.Mvc;
using Motorsazan.CMMS.Client.Api;
using Motorsazan.CMMS.Client.Filters;
using Motorsazan.CMMS.Shared.Enums;
using Motorsazan.CMMS.Shared.Models.Input.ProductiveWorkOrder;
using Motorsazan.CMMS.Shared.Models.Input.StockMan;
using Motorsazan.CMMS.Shared.Models.Output.ProductiveWorkOrder;
using Motorsazan.CMMS.Shared.Utilities;


namespace Motorsazan.CMMS.Client.Controllers
{
    public class StockManController : BaseController
    {
        [AccessToFormValidation(FormCode = "014")]
        public ActionResult AddingRegisterConsumable(InputGetAddingConsumable input)
        {
            var token = GetUserToken();
            var userId = GetCurrentUser().UserID;
            input.UserId = userId;
            var result = ApiList.AddingConsumable(input, token);
            return Content(result);
        }

        public ActionResult HavaleWorkOrderReferralGrid(InputGetHavaleWorkOrderReferral input)
        {
            const string partialViewUrl = "~/Views/StockMan/RegisterConsumables/AddedRegisterConsumableGrid.cshtml";
            var token = GetUserToken();
            var result = ApiList.GetHavaleWorkOrderReferralByWorkOrderReferralId(input, token);
            return PartialView(partialViewUrl, result);
        }

        public ActionResult AddingRegisterConsumableForm(long workOrderSerial, string referralNumber, string employeeId)
        {
            const string partialViewUrl = "~/Views/StockMan/RegisterConsumables/AddingFormRegisterConsumable.cshtml";
            ViewData["workOrderSerial"] = workOrderSerial;
            ViewData["referralNumber"] = referralNumber;
            ViewData["EmployeeId"] = employeeId;
            return PartialView(partialViewUrl);
        }

        public string ConfirmConsumablesManagement(InputGetConfirmConsumablesManagement input)
        {
            var token = GetUserToken();
            var userId = GetCurrentUser().UserID;
            input.UserId = userId;
            var result = ApiList.ConfirmConsumablesManagement(input, token);
            return result;
        }

        public ActionResult ConsumablesManagementModal(InputGetConsumablesManagementByWorkOrderId input)
        {
            const string partialViewUrl = "~/Views/StockMan/ConsumablesManagement/ConsumablesManagementModal.cshtml";
            return PartialView(partialViewUrl, input);
        }

        public ActionResult ConsumablesManagementGrid(InputGetWorKOrderConsumableListByWorkOrderId input)
        {
            const string partialViewUrl = "~/Views/StockMan/ConsumablesManagement/ConsumablesManagementGrid.cshtml";
            var token = GetUserToken();
            var result = ApiList.GetWorKOrderConsumableListByWorkOrderId(input, token);
            return PartialView(partialViewUrl, result);
        }

        public string DeleteConsumablesManagement(InputGetDeleteConsumablesManagement input)
        {
            var token = GetUserToken();
            var result = ApiList.DeleteConsumablesManagement(input, token);
            return result;
        }

        public ActionResult EditConsumablesManagement()
        {
            const string partialViewUrl = "~/Views/StockMan/ConsumablesManagement/ConsumablesManagementEdit.cshtml";
            return PartialView(partialViewUrl);
        }

        public string EditReferralNumber(InputGetEditConsumablesManagement input)
        {
            var token = GetUserToken();
            var userId = GetCurrentUser().UserID;
            input.UserId = userId;
            var result = ApiList.UpdateConsumablesManagement(input, token);
            return result;
        }

        public static OutputGetMaintenanceGroupList[] GetAllMaintenanceGroupList() =>
            ApiList.GetMaintenanceGroupList();

        [AccessToFormValidation(FormCode = "014")]
        public ActionResult Index() => View();

        public ActionResult ReferralHistoryObservationGrid(InputGetWorKOrderReferralListByWorkOrderId input)
        {
            const string partialViewUrl = "~/Views/StockMan/ReferralHistoryObservation/ReferralHistoryObservationGrid.cshtml";
            var token = GetUserToken();
            var result = ApiList.GetWorKOrderReferralListByWorkOrderId(input, token);
            return PartialView(partialViewUrl, result);
        }

        public ActionResult RegisterConsumables(InputGetWorKOrderReferralListByWorkOrderId input)
        {
            const string partialViewUrl = "~/Views/StockMan/RegisterConsumables/RegisterConsumablesGrid.cshtml";
            var token = GetUserToken();
            var result = ApiList.GetWorKOrderReferralListByWorkOrderId(input, token);
            return PartialView(partialViewUrl, result);
        }

        public ActionResult RegistrarProfileObservation(InputGetRegistrarProfileObservation input)
        {
            const string partialViewUrl =
                "~/Views/StockMan/RegistrarProfileObservation/RegistrarProfileObservation.cshtml";
            var result = ApiList.GetRegistrarInfoByWorkOrderId(input);
            return PartialView(partialViewUrl, result);
        }

        public ActionResult StockManGrid(InputGetStockManListByCondition input, string persianStartDate,
            string persianEndDate, DatePeriodType dateTypeId = DatePeriodType.CurrentDay)
        {
            const string partialViewUrl = "~/Views/StockMan/StockManGrid/StockManGrid.cshtml";

            if (input.WorkOrderStatus == -1)
            {
                return PartialView(partialViewUrl);
            }

            var token = GetUserToken();

            (input.StartDate, input.EndDate) = Tools.NormalizeDates(persianStartDate, persianEndDate, dateTypeId);

            var apiResult = ApiList.GetStockManListByCondition(input, token);

            return PartialView(partialViewUrl, apiResult);
        }

        public ActionResult StockManGridDetailRow(long workOrderId)
        {

            const string partialViewUrl = "~/Views/StockMan/StockManGrid/StockManGridDetailRow.cshtml";
            return PartialView(partialViewUrl, workOrderId);
        }

        public ActionResult WorkOrderPerformedOperationGrid(InputGetActionOrDelayListByWorkOrderId input)
        {
            const string partialViewUrl =
                "~/Views/StockMan/WorkOrderPerformedOperation/WorkOrderPerformedOperationGrid.cshtml";
            var token = GetUserToken();
            var result = ApiList.GetActionOrDelayListByWorkOrderId(input, token);
            return PartialView(partialViewUrl, result);
        }


        public ActionResult SundryModalStockComboByRack11()
        {
            const string partialUrl = "~/Views/Shared/StockMan/SundryModalStockComboByRack11.cshtml";

            ViewData["RackCode"] = 11;
            ViewData["ControllerName"] = "StockMan";

            return PartialView(partialUrl);
        }

        public ActionResult SundryModalStockComboByRack12()
        {
            const string partialUrl = "~/Views/Shared/StockMan/SundryModalStockComboByRack12.cshtml";

            ViewData["RackCode"] = 12;
            ViewData["ControllerName"] = "StockMan";

            return PartialView(partialUrl);
        }

        public ActionResult SundryModalStockComboByRack14()
        {
            const string partialUrl = "~/Views/Shared/StockMan/SundryModalStockComboByRack14.cshtml";

            ViewData["RackCode"] = 14;
            ViewData["ControllerName"] = "StockMan";

            return PartialView(partialUrl);
        }


        public ActionResult SundryModalStockComboByRack61()
        {
            const string partialUrl = "~/Views/Shared/StockMan/SundryModalStockComboByRack61.cshtml";

            ViewData["RackCode"] = 61;
            ViewData["ControllerName"] = "StockMan";

            return PartialView(partialUrl);
        }

        public ActionResult SundryModalStockComboByRack62()
        {
            const string partialUrl = "~/Views/Shared/StockMan/SundryModalStockComboByRack62.cshtml";

            ViewData["RackCode"] = 62;
            ViewData["ControllerName"] = "StockMan";

            return PartialView(partialUrl);
        }

        public ActionResult SundryModalStockComboByRack73()
        {
            const string partialUrl = "~/Views/Shared/StockMan/SundryModalStockComboByRack73.cshtml";

            ViewData["RackCode"] = 73;
            ViewData["ControllerName"] = "StockMan";

            return PartialView(partialUrl);
        }

        public ActionResult SundryModalStockComboByRackCode(int rackCode = 61)
        {
            var partialUrl = "~/Views/Shared/StockMan/SundryModalStockComboByRack" + rackCode + ".cshtml";

            ViewData["RackCode"] = rackCode;
            ViewData["ControllerName"] = "StockMan";

            return PartialView(partialUrl);
        }


        public ActionResult RegisterConsumablesStoreCombo()
        {
            const string partialViewUrl = "~/Views/StockMan/RegisterConsumables/RegisterConsumablesStoreCombo.cshtml";

            var dataSource = ApiList.GetStoreListForSparePart();

            return PartialView(partialViewUrl, dataSource);
        }

    }
}