using System;
using System.Web.Mvc;
using Motorsazan.CMMS.Client.Api;
using Motorsazan.CMMS.Client.Filters;
using Motorsazan.CMMS.Shared.Enums;
using Motorsazan.CMMS.Shared.Models.Input.Inspection;
using Motorsazan.CMMS.Shared.Models.Input.ProductiveWorkOrder;
using Motorsazan.CMMS.Shared.Models.Output.ProductiveWorkOrder;
using Motorsazan.CMMS.Shared.Utilities;

namespace Motorsazan.CMMS.Client.Controllers
{
    public class InspectionController : BaseController
    {
        private const string Partialviewaddress = "~/Views/Inspection/";

        public ActionResult AddFormGetAllMainDepartmentPartial()
        {
            var token = GetUserToken();
            const string partialViewUrl = "~/Views/Inspection/AddForm/AddFormGetAllMainDepartmentPartial.cshtml";
            var departmentList = ApiList.GetAllMainDepartment(token);

            return PartialView(partialViewUrl, departmentList);

        }

        public ActionResult AddFormGetDepartmentMachineList(int departmentId = -1)
        {
            var value = new InputGetMainMachineListBySubDepartmentId
            {
                DepartmentId = departmentId
            };
            const string partialViewUrl = "~/Views/Inspection/AddForm/AddFormGetDepartmentMachineListPartial.cshtml";
            var subDepartmentList = ApiList.GetMainMachineListBySubDepartmentId(value);

            return PartialView(partialViewUrl, subDepartmentList);
        }

        public ActionResult AddFormGetProdcutviceAllMainDepartmentPartial()
        {
            const string partialViewUrl =
                "~/Views/Inspection/AddForm/AddFormGetProdcutviceAllMainDepartmentPartial.cshtml";
            var departmentList = ApiList.GetMainDepartmentListHasMachine();

            return PartialView(partialViewUrl, departmentList);
        }

        public ActionResult AddFormGetProdcutviceSubDepartmentListByMainDepartmentIdPartial(
            InputGetSubDepartmentListByMainDepartmentId value)
        {
            const string partialViewUrl =
                "~/Views/Inspection/AddForm/AddFormGetProdcutviceSubDepartmentListByMainDepartmentIdPartial.cshtml";
            var subDepartmentList = ApiList.GetSubDepartmentListByMainDepartmentId(value);

            return PartialView(partialViewUrl, subDepartmentList);
        }
        public ActionResult AddFormGetSubDepartmentListByMainDepartmentIdPartial(
            InputGetSubDepartmentListByMainDepartmentId value)
        {
            const string partialViewUrl =
                "~/Views/Inspection/AddForm/AddFormGetSubDepartmentListByMainDepartmentIdPartial.cshtml";
            var subDepartmentList = ApiList.GetSubDepartmentListByMainDepartmentId(value);

            return PartialView(partialViewUrl, subDepartmentList);
        }

        public ActionResult AddInspection(InputAddInspection values, string preferDate)
        {
            var token = GetUserToken();
            values.UserId = GetCurrentUser().UserID;
            values.PreferDate = Tools.ConvertToLatinDate(preferDate);
            var apiResult = ApiList.AddInspection(values, token);
            return Content(apiResult);
        }

        public ActionResult AddInspectionDetailPartial(long inspectionId)
        {
            const string partialViewUrl = "~/Views/Inspection/Grid/AddInspectionDetailPartial.cshtml";
            ViewBag.InspectionId = inspectionId;
            return PartialView(partialViewUrl);
        }

        public ActionResult AddInspectionDetailToInspection(InputAddInspectionDetailToInspection values)
        {
            var apiResult = ApiList.AddInspectionDetailToInspection(values);
            return Content(apiResult);
        }

        public ActionResult ChangeToWorkOrder(InputAddInspectionWorkOrder values)
        {
            var token = GetUserToken();
            values.UserId = GetCurrentUser().UserID;
            var apiResult = ApiList.AddInspectionWorkOrder(values, token);
            return Content(apiResult);
        }

        public ActionResult DetailRow(long inspectionId = 0)
        {
            const string partialViewUrl = "~/Views/Inspection/Grid/DetailRow.cshtml";
            var apiParam = new InputGetInspectionByInspectionId
            {
                InspectionId = inspectionId
            };
            var inspectionItem = ApiList.GetInspectionByInspectionId(apiParam);

            return PartialView(partialViewUrl, inspectionItem);
        }

        public ActionResult Edit(InputEditTheInspection values, string preferDate)
        {
            var token = GetUserToken();
            values.UserId = GetCurrentUser().UserID;
            values.PreferDate = Tools.ConvertToLatinDate(preferDate);
            var apiResult = ApiList.EditTheInspection(values, token);
            return Content(apiResult);
        }

        public ActionResult EditFormGetAllMainDepartmentPartial()
        {
            var token = GetUserToken();
            const string partialViewUrl = "~/Views/Inspection/EditModal/EditFormGetAllMainDepartmentPartial.cshtml";
            var departmentList = ApiList.GetAllMainDepartment(token);

            return PartialView(partialViewUrl, departmentList);
        }

        public ActionResult EditFormGetDepartmentMachineListPartial(int departmentId = -1)
        {
            var value = new InputGetMainMachineListBySubDepartmentId
            {
                DepartmentId = departmentId
            };
            const string partialViewUrl = "~/Views/Inspection/EditModal/EditFormGetDepartmentMachineListPartial.cshtml";
            var subDepartmentList = ApiList.GetMainMachineListBySubDepartmentId(value);

            return PartialView(partialViewUrl, subDepartmentList);
        }

        public ActionResult EditFormGetProdcutviceAllMainDepartmentPartial()
        {
            const string partialViewUrl =
                "~/Views/Inspection/EditModal/EditFormGetProdcutviceAllMainDepartmentPartial.cshtml";
            var departmentList = ApiList.GetMainDepartmentListHasMachine();

            return PartialView(partialViewUrl, departmentList);
        }

        public ActionResult EditFormGetProdcutviceSubDepartmentListByMainDepartmentIdPartial(
            InputGetSubDepartmentListByMainDepartmentId value)
        {
            const string partialViewUrl =
                "~/Views/Inspection/EditModal/EditFormGetProdcutviceSubDepartmentListByMainDepartmentIdPartial.cshtml";
            var subDepartmentList = ApiList.GetSubDepartmentListByMainDepartmentId(value);

            return PartialView(partialViewUrl, subDepartmentList);
        }

        public ActionResult EditFormGetSubDepartmentListByMainDepartmentIdPartial(
            InputGetSubDepartmentListByMainDepartmentId value)
        {
            const string partialViewUrl =
                "~/Views/Inspection/EditModal/EditFormGetSubDepartmentListByMainDepartmentIdPartial.cshtml";
            var subDepartmentList = ApiList.GetSubDepartmentListByMainDepartmentId(value);

            return PartialView(partialViewUrl, subDepartmentList);
        }

        public ActionResult EditGetInspectionTypeList()
        {
            const string partialViewUrl = "~/Views/Inspection/EditModal/editFormGetWorkOrderPartial.cshtml";
            var token = GetUserToken();
            var dataSource = ApiList.GetInspectionTypeList(token);
            return PartialView(partialViewUrl, dataSource);
        }

        public ActionResult EditGetMaintenanceGroupList()
        {
            const string partialViewUrl = "~/Views/Inspection/EditModal/EditFormMaintenanceGroupListCombo.cshtml";
            var machinesDataSource = ApiList.GetMaintenanceGroupList();
            return PartialView(partialViewUrl, machinesDataSource);
        }

        public ActionResult EditGetWorkOrderType()
        {
            const string partialViewUrl = "~/Views/Inspection/EditModal/editFormGetWorkOrderTypePartial.cshtml";
            return PartialView(partialViewUrl);
        }

        public ActionResult EditInspection(InputEditTheInspection values, string preferDate)
        {
            var token = GetUserToken();
            values.UserId = GetCurrentUser().UserID;
            values.PreferDate = Tools.ConvertToLatinDate(preferDate);
            var apiResult = ApiList.EditTheInspection(values, token);
            return Content(apiResult);
        }

        public ActionResult EditInspectionDetailGridModal()
        {
            const string partailViewUrl = "~/Views/Inspection/Grid/EditInspectionDetailGridModal.cshtml";
            return PartialView(partailViewUrl);
        }

        public ActionResult EditInspectionDetails(InputEditInspectionDetailByInspectionDetailId values)
        {
            var token = GetUserToken();
            var apiResult = ApiList.EditInspectionDetailByInspectionDetailId(values, token);
            return Content(apiResult);
        }

        public ActionResult EditInspectionModalPartial(InputGetInspectionByInspectionId input)
        {
            var apiResult = ApiList.GetInspectionByInspectionId(input);
            const string partailViewUrl = "~/Views/Inspection/EditModal/EditInspectionModalPartial.cshtml";
            return PartialView(partailViewUrl, apiResult);
        }

        public static OutputGetMaintenanceGroupList[] GetAllMaintenanceGroupList() =>
            ApiList.GetMaintenanceGroupList();

        public JsonResult GetInspectionByInspectionId(InputGetInspectionByInspectionId values)
        {
            var inspectionDetailDataSource = ApiList.GetInspectionByInspectionId(values);

            return inspectionDetailDataSource != null ? Json(inspectionDetailDataSource) : Json("");
        }

        public JsonResult GetInspectionDetailByInspectionDetailId(InputGetInspectionDetailByInspectionDetailId values)
        {
            var inspectionDetailDataSource = ApiList.GetInspectionDetailByInspectionDetailId(values);

            return inspectionDetailDataSource != null ? Json(inspectionDetailDataSource) : Json("");
        }

        public ActionResult GetInspectionTypeList()
        {
            const string partialViewUrl = "~/Views/Inspection/AddForm/AddFormGetWorkOrderPartial.cshtml";
            var token = GetUserToken();
            var dataSource = ApiList.GetInspectionTypeList(token);
            return PartialView(partialViewUrl, dataSource);
        }

        public ActionResult GetMaintenanceGroupList()
        {
            const string partialViewUrl = "~/Views/Inspection/AddForm/AddFormMaintenanceGroupListCombo.cshtml";
            var maintenanceGroupListDataSource = ApiList.GetMaintenanceGroupList();
            return PartialView(partialViewUrl, maintenanceGroupListDataSource);
        }

        public ActionResult GetWorkOrderType()
        {
            const string partialViewUrl = "~/Views/Inspection/AddForm/AddFormGetWorkOrderTypePartial.cshtml";
            return PartialView(partialViewUrl);
        }

        [AccessToFormValidation(FormCode = "012")]
        public ActionResult Index() => View();

        public ActionResult InspectionDetailGrid(long? inspectionId)
        {
            const string partialViewUrl = "~/Views/Inspection/Grid/InspectionDetailGrid.cshtml";

            var apiParam = new InputGetInspectionDetailsByInspectionId
            {
                InspectionId = inspectionId.Value
            };
            var dataSource = ApiList.GetInspectionDetailsByInspectionId(apiParam);
            return PartialView(partialViewUrl, dataSource);
        }

        public ActionResult InspectionGird(DatePeriodType dateType = DatePeriodType.RecentMonth, string startDate = "",
            string endDate = "")
        {
            const string partialViewUrl = "~/Views/Inspection/Grid/InspectionGrid.cshtml";
            DateTime enStartDate;
            DateTime enEndDate;
            (enStartDate, enEndDate) = Tools.NormalizeDates(startDate, endDate, dateType);

            var apiParam = new InputGetInspectionListByCondition { EndDate = enEndDate, StartDate = enStartDate };
            var token = GetUserToken();
            var dataSource = ApiList.GetInspectionListByCondition(apiParam, token);
            return PartialView(partialViewUrl, dataSource);
        }

        public ActionResult LoadedItInspection(InputGetInspectionByInspectionId values)
        {
            const string partialViewUrl = "~/Views/Inspection/EditModal/EditInspectionModalPartial.cshtml";
            var inspectionDetailDataSource = ApiList.GetInspectionByInspectionId(values);

            return PartialView(partialViewUrl, inspectionDetailDataSource);
        }

        public ActionResult RemoveInspection(InputRemoveInspection values)
        {
            var token = GetUserToken();
            var apiResult = ApiList.RemoveInspection(values, token);
            return Content(apiResult);
        }

        public ActionResult RemoveInspectionDeatilsByInspectionDetailId(
            InputRemoveInspectionDeatilsByInspectionDetailId values)
        {
            var token = GetUserToken();
            var apiResult = ApiList.RemoveInspectionDeatilsByInspectionDetailId(values, token);
            return Content(apiResult);
        }

        public ActionResult ShowActionHistory(InputGetActionOrDelayListByWorkOrderId input)
        {
            var token = GetUserToken();

            const string partailViewUrl = "~/Views/Inspection/Grid/ShowActionHistoryPartial.cshtml";
            var dataSource = ApiList.GetActionOrDelayListByWorkOrderId(input, token);
            return PartialView(partailViewUrl, dataSource);
        }

        public ActionResult ShowReferenceHistory(InputGetWorKOrderReferralListByWorkOrderId value)
        {
            var token = GetUserToken();
            const string partailViewUrl = "~/Views/Inspection/Grid/ShowReferenceHistoryPartial.cshtml";

            var dataSource = ApiList.GetWorKOrderReferralListByWorkOrderId(value, token);
            return PartialView(partailViewUrl, dataSource);
        }

        public ActionResult ShowUsedTools(long workOrderId)
        {
            var apiParam = new InputGetWorKOrderConsumableListByWorkOrderId { WorkOrderId = workOrderId };
            var token = GetUserToken();
            const string partailViewUrl = "~/Views/Inspection/Grid/ShowUsedToolsPartial.cshtml";

            var dataSource = ApiList.GetWorKOrderConsumableListByWorkOrderId(apiParam, token);
            return PartialView(partailViewUrl, dataSource);
        }
    }
}