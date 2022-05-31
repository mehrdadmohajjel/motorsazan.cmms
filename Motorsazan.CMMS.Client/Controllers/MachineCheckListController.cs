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
    public class MachineCheckListController: BaseController
    {
        [AccessToEventValidation(EventCode = "002", FormCode = "006")]
        public ActionResult ActiveOrDeActiveOperationItem(InputActiveOrDeActiveOperationItem input)
        {
            var token = GetUserToken();

            var apiResult = ApiList.ActiveOrDeActiveOperationItem(input, token);
            return Content(apiResult);
        }

        [AccessToEventValidation(EventCode = "002", FormCode = "006")]
        public ActionResult AddCheckListOperationItem(InputAddCheckListOperationItem input)
        {
            var token = GetUserToken();

            var apiResult = ApiList.AddCheckListOperationItem(input, token);
            return Content(apiResult);
        }

        public ActionResult AddMachineOperationDepartmentList()
        {
            const string partialViewUrl =
                "~/Views/MachineCheckList/AddCheckList/AddMachineOperationDepartmentList.cshtml";

            var departmentListDataSource = ApiList.GetMainDepartmentListHasMachine();

            return PartialView(partialViewUrl, departmentListDataSource);
        }

        public ActionResult AddMachineOperationSubDepartmentList(
            InputGetSubDepartmentListHasMachineByMainDepartmentId values)
        {
            const string partialViewUrl =
                "~/Views/MachineCheckList/AddCheckList/AddMachineOperationSubDepartmentList.cshtml";

            var subDepartmentListDataSource = ApiList.GetSubDepartmentListHasMachineByMainDepartmentId(values);

            return PartialView(partialViewUrl, subDepartmentListDataSource);
        }

        public ActionResult AddSerialForOperationItem(InputGetSerialForOperationItem input)
        {
            const string partialViewUrl = "~/Views/MachineCheckList/AddCheckList/AddSerialForOperationItemLabel.cshtml";

            input.OperationTypeItemId = 3;

            var serialForOperationItem = ApiList.GetSerialForOperationItem(input);

            var final = serialForOperationItem.SerialOperationItem.ToString("000");

            return PartialView(partialViewUrl, final);
        }

        public ActionResult AddSubMachineCheckListTreeList(InputGetHierarchicalMachineListByDepartmentId input)
        {
            const string partialViewUrl = "~/Views/MachineCheckList/AddCheckList/AddSubMachineCheckListTreeList.cshtml";
            ViewBag.Data = input.DepartmentId;

            var subMachineDataSource = ApiList.GetHierarchicalMachineListByDepartmentId(input);

            return PartialView(partialViewUrl, subMachineDataSource);
        }


        public ActionResult CopyOneOperationItemToOtherMachine(
            InputCopyOneOperationItemToOtherMachine input)
        {
            const string partialViewUrl =
                "~/Views/MachineCheckList/Grid/Modal/CopyOneOperationItem/CopyOneOperationItemToOtherMachineModal.cshtml";

            ViewData["OperationItemId"] = input.OperationItemId;
            ViewData["CurrentMachineId"] = input.CurrentMachineId;

            return PartialView(partialViewUrl);
        }

        [AccessToEventValidation(EventCode = "002", FormCode = "006")]
        public ActionResult CopyOperationItemForOtherMachine(InputCopyOperationItemForOtherMachine input)
        {
            var token = GetUserToken();

            var apiResult = ApiList.CopyOperationItemForOtherMachine(input, token);
            return Content(apiResult);
        }

        public ActionResult CopySeveralOperationItemModal()
        {
            const string partialViewUrl =
                "~/Views/MachineCheckList/Grid/Modal/CopySeveralOperationItem/CopySeveralOperationItemToOtherMachineModal.cshtml";

            return PartialView(partialViewUrl);
        }

        [AccessToEventValidation(EventCode = "002", FormCode = "006")]
        public ActionResult CopySeveralOperationItemToOtherMachine(InputCopySeveralOperationItemToOtherMachine input)
        {
            var token = GetUserToken();

            var apiResult = ApiList.CopySeveralOperationItemToOtherMachine(input, token);
            return Content(apiResult);
        }

        public ActionResult CopySeveralOperationItemToOtherMachineModalDepartmentList()
        {
            const string partialViewUrl =
                "~/Views/MachineCheckList/Grid/Modal/CopySeveralOperationItem/CopySeveralOperationItemDepartmentList.cshtml";

            var departmentListDataSource = ApiList.GetMainDepartmentListHasMachine();

            return PartialView(partialViewUrl, departmentListDataSource);
        }

        public ActionResult CopySeveralOperationItemToOtherMachineModalSubDepartmentList(
            InputGetSubDepartmentListHasMachineByMainDepartmentId values)
        {
            const string partialViewUrl =
                "~/Views/MachineCheckList/Grid/Modal/CopySeveralOperationItem/CopySeveralOperationItemSubDepartmentList.cshtml";

            var subDepartmentListDataSource = ApiList.GetSubDepartmentListHasMachineByMainDepartmentId(values);

            return PartialView(partialViewUrl, subDepartmentListDataSource);
        }

        public ActionResult CopySeveralOperationItemToOtherMachineModalTreeList(
            InputGetHierarchicalMachineListByDepartmentId input)
        {
            const string partialViewUrl =
                "~/Views/MachineCheckList/Grid/Modal/CopySeveralOperationItem/CopySeveralOperationItemSubMachineTreeList.cshtml";
            ViewBag.Data = input.DepartmentId;

            var subMachineDataSource = ApiList.GetHierarchicalMachineListByDepartmentId(input);

            return PartialView(partialViewUrl, subMachineDataSource);
        }

        public ActionResult DeActiveOperationItem()
        {
            const string partialViewUrl =
                "~/Views/MachineCheckList/Grid/Modal/DeActiveOperationItem/DeActiveOperationItemModal.cshtml";

            return PartialView(partialViewUrl);
        }

        [AccessToEventValidation(EventCode = "002", FormCode = "006")]
        public ActionResult EditOperationItemByOperationItemId(InputEditOperationItemByOperationItemId input)
        {
            var token = GetUserToken();

            var apiResult = ApiList.EditOperationItemByOperationItemId(input, token);
            return Content(apiResult);
        }

        public ActionResult EditOperationItemModal()
        {
            const string partialViewUrl =
                "~/Views/MachineCheckList/Grid/Modal/EditOperationItem/EditOperationItemModal.cshtml";

            return PartialView(partialViewUrl);
        }

        public ActionResult GetMaintenanceGroupList()
        {
            const string partialViewUrl =
                "~/Views/MachineCheckList/AddCheckList/AddFormMaintenanceGroupListCombo.cshtml";
            var machinesDataSource = ApiList.GetMaintenanceGroupList();
            return PartialView(partialViewUrl, machinesDataSource);
        }

        public static OutputGetMaintenanceGroupList[] GetWorkOrderMaintenanceGroupList() =>
            ApiList.GetMaintenanceGroupList();

        [AccessToFormValidation(FormCode = "006")]
        public ActionResult Index() => View();

        public ActionResult MachineCheckListGrid(InputGetOperationItemListByMachineId input)
        {
            const string partialViewUrl = "~/Views/MachineCheckList/Grid/MachineCheckListGrid.cshtml";

            //-- CheckList = 3, Fault = 2, Preventive = 1
            input.OperationItemTypeId = 3;

            var dataSource = ApiList.GetOperationItemListByMachineId(input);

            return PartialView(partialViewUrl, dataSource);
        }

        public ActionResult MachineCheckListGridDetailRow(long operationItemId, bool isActive)
        {
            const string partialViewUrl = "~/Views/MachineCheckList/Grid/MachineCheckListGridDetailRow.cshtml";

            ViewBag.OperationItemId = operationItemId;
            ViewBag.IsActive = isActive;

            return PartialView(partialViewUrl);
        }

        [AccessToEventValidation(EventCode = "002", FormCode = "006")]
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
                "~/Views/MachineCheckList/Grid/Modal/MoveOperationItemToOtherMachine/MoveOperationItemToOtherMachineModal.cshtml";

            ViewData["OperationItemId"] = input.OperationItemId;


            return PartialView(partialViewUrl);
        }

        public ActionResult MoveOperationItemToOtherMachineModalDepartmentList()
        {
            const string partialViewUrl =
                "~/Views/MachineCheckList/Grid/Modal/MoveOperationItemToOtherMachine/MoveOperationItemDepartmentList.cshtml";

            var departmentListDataSource = ApiList.GetMainDepartmentListHasMachine();

            return PartialView(partialViewUrl, departmentListDataSource);
        }

        public ActionResult MoveOperationItemToOtherMachineModalSubDepartmentList(
            InputGetSubDepartmentListHasMachineByMainDepartmentId values)
        {
            const string partialViewUrl =
                "~/Views/MachineCheckList/Grid/Modal/MoveOperationItemToOtherMachine/MoveOperationItemSubDepartmentList.cshtml";

            var subDepartmentListDataSource = ApiList.GetSubDepartmentListHasMachineByMainDepartmentId(values);

            return PartialView(partialViewUrl, subDepartmentListDataSource);
        }

        public ActionResult MoveOperationItemToOtherMachineModalTreeList(
            InputGetHierarchicalMachineListByDepartmentId input)
        {
            const string partialViewUrl =
                "~/Views/MachineCheckList/Grid/Modal/MoveOperationItemToOtherMachine/MoveOperationItemSubMachineTreeList.cshtml";
            ViewBag.Data = input.DepartmentId;

            var subMachineDataSource = ApiList.GetHierarchicalMachineListByDepartmentId(input);

            return PartialView(partialViewUrl, subMachineDataSource);
        }

        public ActionResult AddSparePartToItemCheckComboBox(
            InputGetMachineSparePartListByMachineId values)
        {
            const string partialViewUrl =
                "~/Views/MachineCheckList/Grid/Modal/AddSparePartToOperation/AddSparePartToItemCheckComboBox.cshtml";
            var preventiveOperationItemList = ApiList.GetMachineSparePartListByMachineId(values);

            return PartialView(partialViewUrl, preventiveOperationItemList);
        }

        [AccessToEventValidation(EventCode = "002", FormCode = "005")]
        public ActionResult AddSparePartToItem(InputAddSparePartListToOperationItem input)
        {
            input.UserId = GetCurrentUser().UserID;

            var token = GetUserToken();

            var apiResult = ApiList.AddSparePartListToOperationItem(input, token);
            return Content(apiResult);
        }

        public ActionResult AddSparepartToOperation(long operationItemId, long machineId)
        {
            const string partialViewUrl =
                "~/Views/MachineCheckList/Grid/Modal/AddSparePartToOperation/AddSparePartToOperationModal.cshtml";

            ViewBag.OperationItemId = operationItemId;
            ViewBag.MachineId = machineId;

            return PartialView(partialViewUrl);
        }

        public ActionResult GetSparePartToOperationItemGrid(
            InputGetOperationItemSparePartListByOperationItemId values)
        {
            const string partialViewUrl =
                "~/Views/MachineCheckList/Grid/Modal/AddSparePartToOperation/GetSparePartToOperationGetSparePartOperationItemGrid.cshtml";

            var preventiveItemGrid = ApiList.GetOperationItemSparePartListByOperationItemId(values);


            return PartialView(partialViewUrl, preventiveItemGrid);
        }

        [AccessToEventValidation(EventCode = "002", FormCode = "005")]
        public string RemoveSparePartFromOperationItem(
            InputRemoveSparePartFromOperationItem input)
        {
            var token = GetUserToken();

            var result = ApiList.RemoveSparePartFromOperationItem(input, token);
            return result;
        }
    }
}