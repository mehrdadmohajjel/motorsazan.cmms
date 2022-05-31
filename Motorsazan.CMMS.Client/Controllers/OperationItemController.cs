using Motorsazan.CMMS.Client.Api;
using Motorsazan.CMMS.Client.Filters;
using Motorsazan.CMMS.Shared.Models.Input.Inspection;
using Motorsazan.CMMS.Shared.Models.Input.MachineCheckList;
using Motorsazan.CMMS.Shared.Models.Input.MachineManagement;
using Motorsazan.CMMS.Shared.Models.Input.OperationItem;
using Motorsazan.CMMS.Shared.Models.Output.ProductiveWorkOrder;
using System.Web.Mvc;

namespace Motorsazan.CMMS.Client.Controllers
{
    public class OperationItemController: BaseController
    {
        [AccessToEventValidation(EventCode = "002", FormCode = "004")]
        public ActionResult ActiveOrDeActiveOperationItem(InputActiveOrDeActiveOperationItem input)
        {
            var token = GetUserToken();

            var apiResult = ApiList.ActiveOrDeActiveOperationItem(input, token);
            return Content(apiResult);
        }

        [AccessToEventValidation(EventCode = "002", FormCode = "004")]
        public ActionResult AddFaultOperationItem(InputAddFaultOperationItem input)
        {
            var token = GetUserToken();

            var apiResult = ApiList.AddFaultOperationItem(input, token);
            return Content(apiResult);
        }

        public ActionResult AddMachineOperationDepartmentList()
        {
            const string partialViewUrl =
                "~/Views/OperationItem/AddOperationItem/AddMachineOperationDepartmentList.cshtml";

            var departmentListDataSource = ApiList.GetMainDepartmentListHasMachine();

            return PartialView(partialViewUrl, departmentListDataSource);
        }

        public ActionResult AddMachineOperationSubDepartmentList(
            InputGetSubDepartmentListHasMachineByMainDepartmentId values)
        {
            const string partialViewUrl =
                "~/Views/OperationItem/AddOperationItem/AddMachineOperationSubDepartmentList.cshtml";

            var subDepartmentListDataSource = ApiList.GetSubDepartmentListHasMachineByMainDepartmentId(values);

            return PartialView(partialViewUrl, subDepartmentListDataSource);
        }

        [AccessToEventValidation(EventCode = "002", FormCode = "004")]
        public ActionResult AddOperationItemMappingToPMItem(InputAddOperationItemMappingToPMItem input)
        {
            input.UserId = GetCurrentUser().UserID;

            var token = GetUserToken();

            var apiResult = ApiList.AddOperationItemMappingToPMItem(input, token);
            return Content(apiResult);
        }

        [AccessToEventValidation(EventCode = "002", FormCode = "004")]
        public ActionResult AddSparePartToItem(InputAddSparePartListToOperationItem input)
        {
            input.UserId = GetCurrentUser().UserID;

            var token = GetUserToken();

            var apiResult = ApiList.AddSparePartListToOperationItem(input, token);
            return Content(apiResult);
        }

        public ActionResult AddPreventiveOperationItem(long operationItemId, long machineId)
        {
            const string partialViewUrl =
                "~/Views/OperationItem/Grid/Modal/AddPreventiveOperationItem/AddPreventiveOperationItemModal.cshtml";

            ViewBag.OperationItemId = operationItemId;
            ViewBag.MachineId = machineId;

            return PartialView(partialViewUrl);
        }

        public ActionResult AddSparePartToItemCheckComboBox(
            InputGetMachineSparePartListByMachineId values)
        {
            const string partialViewUrl =
                "~/Views/OperationItem/Grid/Modal/AddSparePartToOperation/AddSparePartToItemCheckComboBox.cshtml";
            var preventiveOperationItemList = ApiList.GetMachineSparePartListByMachineId(values);

            return PartialView(partialViewUrl, preventiveOperationItemList);
        }


        public ActionResult AddSparepartToOperation(long operationItemId, long machineId)
        {
            const string partialViewUrl =
                "~/Views/OperationItem/Grid/Modal/AddSparePartToOperation/AddSparePartToOperationModal.cshtml";

            ViewBag.OperationItemId = operationItemId;
            ViewBag.MachineId = machineId;

            return PartialView(partialViewUrl);
        }

        public ActionResult GetSparePartToOperationItemGrid(
            InputGetOperationItemSparePartListByOperationItemId values)
        {
            const string partialViewUrl =
                "~/Views/OperationItem/Grid/Modal/AddSparePartToOperation/GetSparePartToOperationGetSparePartOperationItemGrid.cshtml";

            var preventiveItemGrid = ApiList.GetOperationItemSparePartListByOperationItemId(values);


            return PartialView(partialViewUrl, preventiveItemGrid);
        }


        public ActionResult AddPreventiveOperationItemListByMachineIdCheckComboBox(
            InputGetPreventiveOperationItemListByMachineId values)
        {
            const string partialViewUrl =
                "~/Views/OperationItem/Grid/Modal/AddPreventiveOperationItem/AddPreventiveOperationItemCheckComboBox.cshtml";
            var preventiveOperationItemList = ApiList.GetPreventiveOperationItemListByMachineId(values);

            return PartialView(partialViewUrl, preventiveOperationItemList);
        }

        public ActionResult AddSerialForOperationItem(InputGetSerialForOperationItem input)
        {
            const string partialViewUrl =
                "~/Views/OperationItem/AddOperationItem/AddSerialForOperationItemLabel.cshtml";

            input.OperationTypeItemId = 2;

            var serialForOperationItem = ApiList.GetSerialForOperationItem(input);

            var final = serialForOperationItem.SerialOperationItem.ToString("000");

            return PartialView(partialViewUrl, final);
        }

        public ActionResult AddSubMachineOperationItemTreeList(InputGetHierarchicalMachineListByDepartmentId input)
        {
            const string partialViewUrl =
                "~/Views/OperationItem/AddOperationItem/AddSubMachineOperationItemTreeList.cshtml";
            ViewBag.Data = input.DepartmentId;

            var subMachineDataSource = ApiList.GetHierarchicalMachineListByDepartmentId(input);

            return PartialView(partialViewUrl, subMachineDataSource);
        }

        public ActionResult CopyOneOperationItemToOtherMachine()
        {
            const string partialViewUrl =
                "~/Views/OperationItem/Grid/Modal/CopyOneOperationItem/CopyOneOperationItemToOtherMachineModal.cshtml";

            return PartialView(partialViewUrl);
        }

        [AccessToEventValidation(EventCode = "002", FormCode = "004")]
        public ActionResult CopyOperationItemForOtherMachine(InputCopyOperationItemForOtherMachine input)
        {
            var token = GetUserToken();

            var apiResult = ApiList.CopyOperationItemForOtherMachine(input, token);
            return Content(apiResult);
        }

        public ActionResult CopySeveralOperationItemModal()
        {
            const string partialViewUrl =
                "~/Views/OperationItem/Grid/Modal/CopySeveralOperationItem/CopySeveralOperationItemToOtherMachineModal.cshtml";

            return PartialView(partialViewUrl);
        }

        [AccessToEventValidation(EventCode = "002", FormCode = "004")]
        public ActionResult CopySeveralOperationItemToOtherMachine(InputCopySeveralOperationItemToOtherMachine input)
        {
            var token = GetUserToken();

            var apiResult = ApiList.CopySeveralOperationItemToOtherMachine(input, token);
            return Content(apiResult);
        }

        public ActionResult CopySeveralOperationItemToOtherMachineModalDepartmentList()
        {
            const string partialViewUrl =
                "~/Views/OperationItem/Grid/Modal/CopySeveralOperationItem/CopySeveralOperationItemDepartmentList.cshtml";

            var departmentListDataSource = ApiList.GetMainDepartmentListHasMachine();

            return PartialView(partialViewUrl, departmentListDataSource);
        }


        public ActionResult CopySeveralOperationItemToOtherMachineModalSubDepartmentList(
            InputGetSubDepartmentListHasMachineByMainDepartmentId values)
        {
            const string partialViewUrl =
                "~/Views/OperationItem/Grid/Modal/CopySeveralOperationItem/CopySeveralOperationItemSubDepartmentList.cshtml";

            var subDepartmentListDataSource = ApiList.GetSubDepartmentListHasMachineByMainDepartmentId(values);

            return PartialView(partialViewUrl, subDepartmentListDataSource);
        }

        public ActionResult CopySeveralOperationItemToOtherMachineModalTreeList(
            InputGetHierarchicalMachineListByDepartmentId input)
        {
            const string partialViewUrl =
                "~/Views/OperationItem/Grid/Modal/CopySeveralOperationItem/CopySeveralOperationItemSubMachineTreeList.cshtml";
            ViewBag.Data = input.DepartmentId;

            var subMachineDataSource = ApiList.GetHierarchicalMachineListByDepartmentId(input);

            return PartialView(partialViewUrl, subMachineDataSource);
        }

        public ActionResult DeActiveOperationItem()
        {
            const string partialViewUrl =
                "~/Views/OperationItem/Grid/Modal/DeActiveOperationItem/DeActiveOperationItemModal.cshtml";

            return PartialView(partialViewUrl);
        }

        [AccessToEventValidation(EventCode = "002", FormCode = "004")]
        public ActionResult EditOperationItemByOperationItemId(InputEditOperationItemByOperationItemId input)
        {
            var token = GetUserToken();

            var apiResult = ApiList.EditOperationItemByOperationItemId(input, token);
            return Content(apiResult);
        }

        public ActionResult EditOperationItemModal()
        {
            const string partialViewUrl =
                "~/Views/OperationItem/Grid/Modal/EditOperationItem/EditOperationItemModal.cshtml";

            return PartialView(partialViewUrl);
        }

        public ActionResult GetMaintenanceGroupList()
        {
            const string partialViewUrl =
                "~/Views/OperationItem/AddOperationItem/AddFormMaintenanceGroupListCombo.cshtml";
            var machinesDataSource = ApiList.GetMaintenanceGroupList();
            return PartialView(partialViewUrl, machinesDataSource);
        }

        public static OutputGetMaintenanceGroupList[] GetWorkOrderMaintenanceGroupList() =>
            ApiList.GetMaintenanceGroupList();

        [AccessToFormValidation(FormCode = "004")]
        public ActionResult Index() => View();

        public ActionResult MappingToPreventiveOperationItemGrid(
            InputGetOperationItemMappingToPMItemListByOperationItemId values)
        {
            const string partialViewUrl =
                "~/Views/OperationItem/Grid/Modal/AddPreventiveOperationItem/MappingToPreventiveOperationItemGrid.cshtml";

            var preventiveItemGrid = ApiList.GetOperationItemMappingToPMItemListByOperationItemId(values);


            return PartialView(partialViewUrl, preventiveItemGrid);
        }

        [AccessToEventValidation(EventCode = "002", FormCode = "004")]
        public ActionResult MoveOperationItemFromCurrentMachineToNewMachine(
            InputMoveOperationItemFromCurrentMachineToNewMachine input)
        {
            var token = GetUserToken();

            var apiResult = ApiList.MoveOperationItemFromCurrentMachineToNewMachine(input, token);
            return Content(apiResult);
        }

        public ActionResult MoveOperationItemToOtherMachine(InputMoveOperationItemFromCurrentMachineToNewMachine input)
        {
            const string partialViewUrl =
                "~/Views/OperationItem/Grid/Modal/MoveOperationItemToOtherMachine/MoveOperationItemToOtherMachineModal.cshtml";

            ViewData["OperationItemId"] = input.OperationItemId;


            return PartialView(partialViewUrl);
        }

        public ActionResult MoveOperationItemToOtherMachineModalDepartmentList()
        {
            const string partialViewUrl =
                "~/Views/OperationItem/Grid/Modal/MoveOperationItemToOtherMachine/MoveOperationItemDepartmentList.cshtml";

            var departmentListDataSource = ApiList.GetMainDepartmentListHasMachine();

            return PartialView(partialViewUrl, departmentListDataSource);
        }

        public ActionResult MoveOperationItemToOtherMachineModalSubDepartmentList(
            InputGetSubDepartmentListHasMachineByMainDepartmentId values)
        {
            const string partialViewUrl =
                "~/Views/OperationItem/Grid/Modal/MoveOperationItemToOtherMachine/MoveOperationItemSubDepartmentList.cshtml";

            var subDepartmentListDataSource = ApiList.GetSubDepartmentListHasMachineByMainDepartmentId(values);

            return PartialView(partialViewUrl, subDepartmentListDataSource);
        }

        public ActionResult MoveOperationItemToOtherMachineModalTreeList(
            InputGetHierarchicalMachineListByDepartmentId input)
        {
            const string partialViewUrl =
                "~/Views/OperationItem/Grid/Modal/MoveOperationItemToOtherMachine/MoveOperationItemSubMachineTreeList.cshtml";
            ViewBag.Data = input.DepartmentId;

            var subMachineDataSource = ApiList.GetHierarchicalMachineListByDepartmentId(input);

            return PartialView(partialViewUrl, subMachineDataSource);
        }

        public ActionResult OperationItemGrid(InputGetOperationItemListByMachineId input)
        {
            const string partialViewUrl = "~/Views/OperationItem/Grid/OperationItemGrid.cshtml";

            //-- CheckList = 3, Fault = 2, Preventive = 1
            input.OperationItemTypeId = 2;

            var dataSource = ApiList.GetOperationItemListByMachineId(input);

            return PartialView(partialViewUrl, dataSource);
        }

        public ActionResult OperationItemGridDetailRow(long operationItemId, bool isActive)
        {
            const string partialViewUrl = "~/Views/OperationItem/Grid/OperationItemGridDetailRow.cshtml";

            ViewBag.OperationItemId = operationItemId;
            ViewBag.IsActive = isActive;

            return PartialView(partialViewUrl);
        }

        [AccessToEventValidation(EventCode = "002", FormCode = "004")]
        public string RemoveOperationItemMappingToPmItemByOperationItemMappingToPmItemId(
            InputRemoveOperationItemMappingToPMItemByOperationItemMappingToPMItemId input)
        {
            var token = GetUserToken();

            var result = ApiList.RemoveOperationItemMappingToPMItemByOperationItemMappingToPMItemId(input, token);
            return result;
        }

        [AccessToEventValidation(EventCode = "002", FormCode = "004")]
        public string RemoveSparePartFromOperationItem(
            InputRemoveSparePartFromOperationItem input)
        {
            var token = GetUserToken();

            var result = ApiList.RemoveSparePartFromOperationItem(input, token);
            return result;
        }
    }
}