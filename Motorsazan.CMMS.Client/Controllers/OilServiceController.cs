using System.Web.Mvc;
using Motorsazan.CMMS.Client.Api;
using Motorsazan.CMMS.Client.Filters;
using Motorsazan.CMMS.Shared.Enums;
using Motorsazan.CMMS.Shared.Models.Input.Inspection;
using Motorsazan.CMMS.Shared.Models.Input.OilService;
using Motorsazan.CMMS.Shared.Models.Output.OilService;
using Motorsazan.CMMS.Shared.Utilities;

namespace Motorsazan.CMMS.Client.Controllers
{
    public class OilServiceController: BaseController
    {
        public ActionResult AddFormDepartmentMachineList(InputGetMainMachineListBySubDepartmentId input)
        {
            const string partialViewUrl = "~/Views/OilService/AddForm/AddFormMachineListPartial.cshtml";
            ViewBag.Data = input.DepartmentId;

            var machineList = ApiList.GetMainMachineListBySubDepartmentId(input);
            return PartialView(partialViewUrl, machineList);
        }

        public ActionResult AddFormGetDepartmentList()
        {
            const string partialViewUrl = "~/Views/OilService/AddForm/AddFormGetDepartmentListPartial.cshtml";
            var mainDepartmentList = ApiList.GetMainDepartmentListHasMachine();

            return PartialView(partialViewUrl, mainDepartmentList);
        }

        public ActionResult AddFormGetOilPlaceList()
        {
            const string partialViewUrl = "~/Views/OilService/AddForm/AddFormGetOilPlacePartial.cshtml";
            var oilServicePlaceList = ApiList.GetOilServicePlaceList();

            return PartialView(partialViewUrl, oilServicePlaceList);
        }

        public ActionResult AddFormGetSubDepartmentList(InputGetSubDepartmentListHasMachineByMainDepartmentId value)
        {
            const string partialViewUrl = "~/Views/OilService/AddForm/AddFormGetSubDepartmentListPartial.cshtml";
            var subDepartmentList = ApiList.GetSubDepartmentListHasMachineByMainDepartmentId(value);

            return PartialView(partialViewUrl, subDepartmentList);
        }

        public ActionResult AddFormMeasurementUnitList()
        {
            const string partialViewUrl = "~/Views/OilService/AddForm/AddFormMeasurementUnitListPartial.cshtml";
            var measurementUnitList = ApiList.GetMeasurementUnitList();
            return PartialView(partialViewUrl, measurementUnitList);
        }

        public ActionResult AddFormOilList()
        {
            const string partialViewUrl = "~/Views/OilService/AddForm/AddFormOilListPartial.cshtml";
            var oilList = GetGetMaterialsList();
            return PartialView(partialViewUrl, oilList);
        }

        [AccessToFormValidation(FormCode = "013")]
        public ActionResult AddNewOilService(InputAddOilService input, string persianPreferDate)
        {
            input.PreferDate = Tools.ConvertToLatinDate(persianPreferDate);

            input.UserId = GetCurrentUser().UserID;

            var token = GetUserToken();

            var apiResult = ApiList.AddOilService(input, token);
            return Content(apiResult);
        }

        public ActionResult AddOperationsModalEmployeeCheckComboBox()
        {
            const string partialViewUrl = "~/Views/OilService/AddForm/AddOperationsModalEmployeeCheckComboBox.cshtml";
            var employeeList = ApiList.GetServiceMaintenanceGroupMemberList();

            return PartialView(partialViewUrl, employeeList);
        }

        public OutputGetMaterials[] GetGetMaterialsList() => ApiList.GetMaterials();

        [AccessToFormValidation(FormCode = "013")]
        public ActionResult Index() => View();

        public ActionResult OilServiceGird(InputGetOilServiceListByCondition input,
            string persianStartDate, string persianEndDate, DatePeriodType datePeriodType = DatePeriodType.RecentWeek)
        {
            const string partialViewUrl = "~/Views/OilService/Grid/OilServiceGird.cshtml";

            (input.StartDate, input.EndDate) =
                Tools.NormalizeDates(persianStartDate, persianEndDate, datePeriodType);

            var apiResult = ApiList.GetOilServiceListByCondition(input);

            return PartialView(partialViewUrl, apiResult);
        }

        [AccessToFormValidation(FormCode = "013")]
        public string RemoveOilService(InputRemoveOilService input)
        {
            input.UserId = GetCurrentUser().UserID;

            var token = GetUserToken();

            var result = ApiList.RemoveOilService(input, token);
            return result;
        }
    }
}