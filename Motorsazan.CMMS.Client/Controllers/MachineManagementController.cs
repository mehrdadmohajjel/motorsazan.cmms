using Motorsazan.CMMS.Client.Api;
using Motorsazan.CMMS.Client.Filters;
using Motorsazan.CMMS.Shared.Models.Input.Inspection;
using Motorsazan.CMMS.Shared.Models.Input.MachineManagement;
using Motorsazan.CMMS.Shared.Models.Output.Inspection;
using Motorsazan.CMMS.Shared.Utilities;
using System;
using System.Net.Mime;
using System.Web.Mvc;

namespace Motorsazan.CMMS.Client.Controllers
{
    public class MachineManagementController: BaseController
    {
        public ActionResult AddMachineElectricalInfo(InputAddMachineElectricalInfo values)
        {
            var token = GetUserToken();

            var apiResult = ApiList.AddMachineElectricalInfo(values, token);
            return Content(apiResult);
        }

        public ActionResult RemoveMachineElectricalInfoByRecordId(InputRemoveMachineElectricalInfoByRecordId input)
        {
            var token = GetUserToken();

            var apiResult = ApiList.RemoveMachineElectricalInfoByRecordId(input, token);
            return Content(apiResult);
        }

        public ActionResult RegisterOilAndLubrication(InputRegisterOilAndLubrication values)
        {
            var token = GetUserToken();

            var apiResult = ApiList.RegisterOilAndLubrication(values, token);
            return Content(apiResult);
        }

        public ActionResult RemoveMachineOilAndLubricationInfoByRecordId(
            InputRemoveMachineOilAndLubricationInfoByRecordId values)
        {
            var token = GetUserToken();

            var apiResult = ApiList.RemoveMachineOilAndLubricationInfoByRecordId(values, token);
            return Content(apiResult);
        }


        public ActionResult EditMachineBuiltInfo(InputEditMachineBuiltInfo input, string importPersianDate)
        {
            var token = GetUserToken();
            input.ImportDate = Tools.ConvertToLatinDate(importPersianDate);
            var apiResult = ApiList.EditMachineBuiltInfo(input, token);
            return Content(apiResult);
        }

        public ActionResult FilterDepartmentHasMachineCombo()
        {
            const string partialViewUrl = "~/Views/MachineManagement/FilterForm/filterDepartmentHasMachineCombo.cshtml";

            var departmentListDataSource = ApiList.GetMainDepartmentListHasMachine();


            var allDepartment = new OutputGetMainDepartmentListHasMachine {DepartmentId = 0, Title = "همه"};

            var dataSource = Tools.PrependGetAllItemToArray(departmentListDataSource, allDepartment);


            return PartialView(partialViewUrl, dataSource);
        }

        public ActionResult FilterSubDepartmentHasMachineCombo(
            InputGetSubDepartmentListHasMachineByMainDepartmentId input)
        {
            const string partialViewUrl =
                "~/Views/MachineManagement/FilterForm/FilterSubDepartmentHasMachineCombo.cshtml";

            var subDepartmentListDataSource = ApiList.GetSubDepartmentListHasMachineByMainDepartmentId(input);

            return PartialView(partialViewUrl, subDepartmentListDataSource);
        }


        public ActionResult MachineProducerTabContent(InputGetMachineBuiltInfo input)
        {
            var apiResult = ApiList.GetMachineBuiltInfo(input);
            const string partialViewUrl = "~/Views/MachineManagement/MachineProducer/AddForm.cshtml";
            return PartialView(partialViewUrl, apiResult);
        }

        public ActionResult ShowOilAndLubricationModal(long machineId)
        {
            ViewBag.MachineId = machineId;
            const string partialViewUrl = "~/Views/MachineManagement/AddOilAndLubrication/AddForm.cshtml";
            return PartialView(partialViewUrl);
        }

        [AccessToEventValidation(EventCode = "006", FormCode = "002")]

        public ActionResult ShowElectricalModal(InputGetMachineInfoByMachineId input)
        {
            ViewBag.MachineId = input.MachineId;
            var dataSource = ApiList.GetMachineInfoByMachineId(input);

            const string partialViewUrl = "~/Views/MachineManagement/AddFormMachineBaseInfo/AddForm.cshtml";
            return PartialView(partialViewUrl, dataSource);
        }


        public ActionResult MachineOilTabContentGrid(InputGetMachineOilAndLubricationInfo input)
        {
            var dataSource = ApiList.GetMachineOilAndLubricationInfo(input);
            var partialUrl = "~/Views/MachineManagement/AddOilAndLubrication/MachineOilTabContentGrid.cshtml";
            return PartialView(partialUrl, dataSource);
        }

        public ActionResult MachineElectricalTabContent(InputGetMachineInfoByMachineId input)
        {
            ViewBag.MachineId = input.MachineId;
            var partialUrl = "~/Views/MachineManagement/AddFormMachineBaseInfo/MachineElectricalTabContent.cshtml";
            return PartialView(partialUrl);
        }

        public ActionResult MachineElectricalTabContentGrid(InputGetMachineElectricalInfo input)
        {
            var dataSource = ApiList.GetMachineElectricalInfo(input);
            var partialUrl = "~/Views/MachineManagement/AddFormMachineBaseInfo/MachineElectricalTabContentGrid.cshtml";
            return PartialView(partialUrl, dataSource);
        }

        public ActionResult AddFormCharacteristicCombo()
        {
            const string partialViewUrl =
                "~/Views/MachineManagement/AddFormMachineBaseInfo/AddFormCharacteristicComboPartial.cshtml";
            var source = ApiList.GetCharacteristicList();
            return PartialView(partialViewUrl, source);
        }

        public ActionResult AddFormRpmlistCombo()
        {
            const string partialViewUrl =
                "~/Views/MachineManagement/AddFormMachineBaseInfo/AddFormRpmListComboPartial.cshtml";
            var source = ApiList.GetRpmList();
            return PartialView(partialViewUrl, source);
        }

        public ActionResult AddFormVoltageListCombo()
        {
            const string partialViewUrl =
                "~/Views/MachineManagement/AddFormMachineBaseInfo/AddFormVoltageListComboPartial.cshtml";
            var source = ApiList.GetVoltageList();
            return PartialView(partialViewUrl, source);
        }

        [AccessToFormValidation(FormCode = "002")]
        public ActionResult Index() => View();


        #region StartGrid

        public ActionResult MachineManagementGrid(InputGetTopMachineListByCondition input)
        {
            const string partialViewUrl = "~/Views/MachineManagement/MachinesGrid/MachineManagementGrid.cshtml";

            var machinesDataSource = ApiList.GetTopMachineListByCondition(input);

            return PartialView(partialViewUrl, machinesDataSource);
        }

        public ActionResult MachinesGridDetailRow(long machineId, string machineName, string machineLevelName)
        {
            const string partialViewUrl = "~/Views/MachineManagement/MachinesGrid/MachinesGridDetailRow.cshtml";

            ViewData["MachineId"] = machineId;
            ViewData["MachineName"] = machineName;
            ViewData["MachineLevelName"] = machineLevelName;

            return PartialView(partialViewUrl);
        }

        #endregion StartGrid


        #region ShowSubMachineTree

        public ActionResult SubMachineTreeModal(InputGetHierarchicalMachineList input)
        {
            const string partialViewUrl = "~/Views/MachineManagement/ShowAllSubMachine/SubMachineTreeModal.cshtml";

            ViewBag.MachineTopParentId = input.MachineTopParentId;

            return PartialView(partialViewUrl);
        }

        public ActionResult SubMachineTree(InputGetHierarchicalMachineList input)
        {
            const string partialViewUrl = "~/Views/MachineManagement/ShowAllSubMachine/SubMachineTree.cshtml";

            ViewBag.Data = input.MachineTopParentId;

            var subMachineDataSource = ApiList.GetHierarchicalMachineList(input);

            return PartialView(partialViewUrl, subMachineDataSource);
        }

        #endregion ShowSubMachineTree


        #region AddSubMahcine

        [AccessToEventValidation(EventCode = "002", FormCode = "002")]
        public ActionResult AddSubMachine(long topMachineId)
        {
            const string partialViewUrl = "~/Views/MachineManagement/AddSubMachine/AddForm.cshtml";

            ViewData["MachineTopParentId"] = topMachineId;

            return PartialView(partialViewUrl);
        }

        public ActionResult AddSubMachineTreeList(InputGetHierarchicalMachineList input)
        {
            const string partialViewUrl = "~/Views/MachineManagement/AddSubMachine/AddSubMachineTreeList.cshtml";

            ViewBag.Data = input.MachineTopParentId;

            var subMachineTreeListDataSource = ApiList.GetHierarchicalMachineList(input);

            return PartialView(partialViewUrl, subMachineTreeListDataSource);
        }

        [AccessToEventValidation(EventCode = "002", FormCode = "002")]
        public string AddNewSubMachine(InputAddSubMachine input)
        {
            var token = GetUserToken();
            input.UserId = GetCurrentUser().UserID;

            var addResult = ApiList.AddSubMachine(input, token);
            return addResult;
        }

        #endregion AddSubMahcine


        #region AddMachineDocument

        [AccessToEventValidation(EventCode = "003", FormCode = "002")]
        public ActionResult AddMachineDocument(long topMachineId)
        {
            const string partialViewUrl = "~/Views/MachineManagement/AddDocument/AddForm.cshtml";

            ViewData["MachineTopParentId"] = topMachineId;

            return PartialView(partialViewUrl);
        }

        public ActionResult AddMachineDocumentTreeList(InputGetHierarchicalMachineList input)
        {
            const string partialViewUrl = "~/Views/MachineManagement/AddDocument/AddSubMachineTreeList.cshtml";

            ViewBag.Data = input.MachineTopParentId;

            var machineDocumentDataSource = ApiList.GetHierarchicalMachineList(input);

            return PartialView(partialViewUrl, machineDocumentDataSource);
        }

        public ActionResult AddDocumentTypeListComboPartial()
        {
            const string partialViewUrl =
                "~/Views/MachineManagement/AddDocument/AddDocumentTypeListComboPartial.cshtml";

            var machineTypeListDataSource = ApiList.GetFileTypeList();

            return PartialView(partialViewUrl, machineTypeListDataSource);
        }

        [AccessToEventValidation(EventCode = "003", FormCode = "002")]
        public ActionResult RemoveUploadedAttachments(InputRemoveUploadedAttachmentsOfMachine input)
        {
            var token = GetUserToken();

            var apiResult = ApiList.RemoveUploadedAttachmentsOfMachine(input, token);

            return Content(apiResult);
        }

        [AccessToEventValidation(EventCode = "003", FormCode = "002")]
        public string AddNewMachineDocument(InputAddMachineDocument input)
        {
            var token = GetUserToken();
            input.UserId = GetCurrentUser().UserID;

            var addResult = ApiList.AddMachineDocument(input, token);
            return addResult;
        }

        public ActionResult GetMachineDocumentListByMachineId(InputGetMachineDocumentListByMachineId input)
        {
            const string partialViewUrl = "~/Views/MachineManagement/AddDocument/MachineDocumentGrid.cshtml";

            var machineSparePartDocumentSource = ApiList.GetMachineDocumentListByMachineId(input);

            return PartialView(partialViewUrl, machineSparePartDocumentSource);
        }

        [AccessToEventValidation(EventCode = "003", FormCode = "002")]
        public string RemoveMachineDocumentByMachineDocumentId(InputRemoveMachineDocumentByMachineDocumentId input)
        {
            var token = GetUserToken();

            input.UserId = GetCurrentUser().UserID;

            var result = ApiList.RemoveMachineDocumentByMachineDocumentId(input, token);

            return result;
        }

        #endregion AddMachineDocument


        #region AddMachineLocation

        [AccessToEventValidation(EventCode = "004", FormCode = "002")]
        public ActionResult AddLocation(long topMachineId)
        {
            const string partialViewUrl = "~/Views/MachineManagement/AddLocation/AddMachineToLocation.cshtml";

            ViewData["MachineTopParentId"] = topMachineId;

            return PartialView(partialViewUrl);
        }

        public ActionResult FilterDepartmentCombo()
        {
            const string partialViewUrl = "~/Views/MachineManagement/AddLocation/DepartmentCombo.cshtml";

            var departmentListDataSource = ApiList.GetAllMainDepartment();

            return PartialView(partialViewUrl, departmentListDataSource);
        }

        public ActionResult FilterSubDepartmentCombo(InputGetSubDepartmentListByMainDepartmentId input)
        {
            const string partialViewUrl = "~/Views/MachineManagement/AddLocation/SubDepartmentCombo.cshtml";

            var subDepartmentListDataSource = ApiList.GetSubDepartmentListByMainDepartmentId(input);

            return PartialView(partialViewUrl, subDepartmentListDataSource);
        }

        public ActionResult AddEmployeeCombo(InputGetEmployeeListOfDepartment input)
        {
            const string partialViewUrl = "~/Views/MachineManagement/AddLocation/AddEmployeeCombo.cshtml";

            var dataSource = ApiList.GetEmployeeListOfDepartment(input);

            return PartialView(partialViewUrl, dataSource);
        }

        [AccessToEventValidation(EventCode = "004", FormCode = "002")]
        public string AssignMachineToEmployee(InputAssignMachineToEmployee input)
        {
            var token = GetUserToken();

            var addResult = ApiList.AssignMachineToEmployee(input, token);

            return addResult;
        }

        public ActionResult MachineLocationTable(InputGetMachineLocationByMachineId input)
        {
            const string partialViewUrl = "~/Views/MachineManagement/AddLocation/MachineLocationTable.cshtml";

            ViewData["MachineId"] = input.MachineId;

            var machineLocationList = ApiList.GetMachineLocationByMachineId(input);

            return PartialView(partialViewUrl, machineLocationList);
        }

        [AccessToEventValidation(EventCode = "004", FormCode = "002")]
        public ActionResult RemoveAssignMachineToEmployee(InputRemoveAssignMachineToEmployee values)
        {
            var token = GetUserToken();

            var apiResult = ApiList.RemoveAssignMachineToEmployee(values, token);

            return Content(apiResult);
        }

        #endregion AddMachineLocation


        #region AddSparePart

        [AccessToEventValidation(EventCode = "005", FormCode = "002")]

        public ActionResult AddFormMachineSparePart(long topMachineId)
        {
            const string partialViewUrl = "~/Views/MachineManagement/AddMachineSparePart/AddForm.cshtml";

            ViewData["MachineTopParentId"] = topMachineId;

            return PartialView(partialViewUrl);
        }

        public ActionResult AddFormGetStoreListForSparePart()
        {
            const string partialViewUrl =
                "~/Views/MachineManagement/AddMachineSparePart/AddFormStoreListForSparePartCombo.cshtml";

            var dataSource = ApiList.GetStoreListForSparePart();

            return PartialView(partialViewUrl, dataSource);
        }

        public ActionResult AddSparePartSubMachineTreeList(InputGetHierarchicalMachineList input)
        {
            const string partialViewUrl =
                "~/Views/MachineManagement/AddMachineSparePart/AddFormSparePartSubMachineTreeList.cshtml";

            ViewBag.Data = input.MachineTopParentId;

            var machineDocumentDataSource = ApiList.GetHierarchicalMachineList(input);

            return PartialView(partialViewUrl, machineDocumentDataSource);
        }

        public ActionResult SundryModalStockComboByRack61()
        {
            const string partialUrl = "~/Views/Shared/MachineManagementSparePart/SundryModalStockComboByRack61.cshtml";

            ViewData["RackCode"] = 61;
            ViewData["ControllerName"] = "MachineManagement";

            return PartialView(partialUrl);
        }

        public ActionResult SundryModalStockComboByRack62()
        {
            const string partialUrl = "~/Views/Shared/MachineManagementSparePart/SundryModalStockComboByRack62.cshtml";

            ViewData["RackCode"] = 62;
            ViewData["ControllerName"] = "MachineManagement";

            return PartialView(partialUrl);
        }

        public ActionResult SundryModalStockComboByRack73()
        {
            const string partialUrl = "~/Views/Shared/MachineManagementSparePart/SundryModalStockComboByRack73.cshtml";

            ViewData["RackCode"] = 73;
            ViewData["ControllerName"] = "MachineManagement";

            return PartialView(partialUrl);
        }

        public ActionResult SundryModalStockComboByRackCode(int rackCode = 73)
        {
            var partialUrl = "~/Views/Shared/MachineManagementSparePart/SundryModalStockComboByRack" + rackCode +
                             ".cshtml";

            ViewData["RackCode"] = rackCode;
            ViewData["ControllerName"] = "MachineManagement";

            return PartialView(partialUrl);
        }

        [AccessToEventValidation(EventCode = "005", FormCode = "002")]
        public string AddMachineSparePart(InputAddMachineSparePart input, string persianDate)
        {
            var token = GetUserToken();
            input.UserId = GetCurrentUser().UserID;

            input.UsedDate = Tools.ConvertToLatinDate(persianDate);

            var addResult = ApiList.AddMachineSparePart(input, token);
            return addResult;
        }

        public ActionResult GetMachineSparePartList(InputGetMachineSparePartListByMachineId input)
        {
            const string partialViewUrl = "~/Views/MachineManagement/AddMachineSparePart/MachineSparePartGrid.cshtml";

            ViewBag.Data = input.MachineId;

            var subMachineDataSource = ApiList.GetMachineSparePartListByMachineId(input);

            return PartialView(partialViewUrl, subMachineDataSource);
        }

        [AccessToEventValidation(EventCode = "005", FormCode = "002")]
        public string RemoveMachineSparePartByMachineSparePartId(InputRemoveMachineSparePartByMachineSparePartId input)
        {
            var token = GetUserToken();

            var result = ApiList.RemoveMachineSparePartByMachineSparePartId(input, token);

            return result;
        }

        #endregion AddSparePart


        #region AddSparePartUploader

        public ActionResult AddDocumentToSparePart(long topMachineId, long machineId, long machineSparePartId)
        {
            const string partialViewUrl =
                "~/Views/MachineManagement/AddMachineSparePart/AddFormDocumentToSparePartModal.cshtml";

            ViewData["MachineTopParentId"] = topMachineId;
            ViewData["MachineId"] = machineId;
            ViewData["MachineSparePartId"] = machineSparePartId;

            return PartialView(partialViewUrl);
        }

        [AccessToEventValidation(EventCode = "005", FormCode = "002")]
        public string AddMachineSparePartDocument(InputAddMachineSparePartDocument input)
        {
            var token = GetUserToken();
            input.UserId = GetCurrentUser().UserID;

            var addResult = ApiList.AddMachineSparePartDocument(input, token);
            return addResult;
        }

        public ActionResult GetMachineSparePartDocumentList(
            InputGetMachineSparePartDocumentListByMachineSparePartId input)
        {
            const string partialViewUrl =
                "~/Views/MachineManagement/AddMachineSparePart/MachineSparePartDocumentGrid.cshtml";

            var machineSparePartDocumentSource = ApiList.GetMachineSparePartDocumentListByMachineSparePartId(input);

            return PartialView(partialViewUrl, machineSparePartDocumentSource);
        }

        [AccessToEventValidation(EventCode = "005", FormCode = "002")]
        public ActionResult RemoveMachineSparePartDocumentByMachineSparePartDocumentId(
            InputRemoveMachineSparePartDocumentByMachineSparePartDocumentId input)
        {
            var token = GetUserToken();

            input.UserId = GetCurrentUser().UserID;

            var apiResult = ApiList.RemoveMachineSparePartDocumentByMachineSparePartDocumentId(input, token);

            return Content(apiResult);
        }

        #endregion
    }
}