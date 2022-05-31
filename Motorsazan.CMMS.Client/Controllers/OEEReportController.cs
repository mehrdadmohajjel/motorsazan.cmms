using System.Web.Mvc;
using Motorsazan.CMMS.Client.Api;
using Motorsazan.CMMS.Client.Filters;
using Motorsazan.CMMS.Shared.Enums;
using Motorsazan.CMMS.Shared.Models.Input.Inspection;
using Motorsazan.CMMS.Shared.Models.Input.OEEReport;
using Motorsazan.CMMS.Shared.Utilities;

namespace Motorsazan.CMMS.Client.Controllers
{
    public class OEEReportController: BaseController
    {
        public ActionResult FillResultGrid(InputGetOEEReportByCondition input,
            string persianStartDate, string persianEndDate, DatePeriodType datePeriodType)
        {
            const string partialViewUrl = "~/Views/OEEReport/Grid/ResultGrid.cshtml";

            ViewBag.Date = "FillResultGrid";

            if(datePeriodType == (DatePeriodType) (-1))
            {
                return PartialView(partialViewUrl);
            }

            (input.StartDate, input.EndDate) = Tools.NormalizeDates(persianStartDate, persianEndDate, datePeriodType);

            var apiResult = ApiList.GetOEEReportByCondition(input);

            return PartialView(partialViewUrl, apiResult);
        }

        public ActionResult FillResultGridWithDepartment(InputGetDepartmentOEEReportByCondition input,
            string persianStartDate, string persianEndDate, DatePeriodType datePeriodType)
        {
            const string partialViewUrl = "~/Views/OEEReport/Grid/ResultGrid.cshtml";

            ViewBag.Date = "FillResultGridWithDepartment";

            if(input.DepartmentId == -1)
            {
                return PartialView(partialViewUrl);
            }

            (input.StartDate, input.EndDate) = Tools.NormalizeDates(persianStartDate, persianEndDate, datePeriodType);

            var apiResult = ApiList.GetDepartmentOEEReportByCondition(input);

            return PartialView(partialViewUrl, apiResult);
        }

        public ActionResult FillResultGridWithMachine(InputGetMachineOEEReportByCondition input,
            string persianStartDate, string persianEndDate, DatePeriodType datePeriodType)
        {
            const string partialViewUrl = "~/Views/OEEReport/Grid/ResultGrid.cshtml";

            ViewBag.Date = "FillResultGridWithMachine";

            if(input.MachineId == -1)
            {
                return PartialView(partialViewUrl);
            }

            (input.StartDate, input.EndDate) = Tools.NormalizeDates(persianStartDate, persianEndDate, datePeriodType);

            var apiResult = ApiList.GetMachineOEEReportByCondition(input);

            return PartialView(partialViewUrl, apiResult);
        }

        public ActionResult FilterFormGetAllMainDepartmentList()
        {
            var token = GetUserToken();

            const string partialViewUrl = "~/Views/OEEReport/FilterForm/FilterFormGetAllMainDepartmentListCombo.cshtml";

            var departmentListInSelectWithDepartment = ApiList.GetAllMainDepartment(token);

            return PartialView(partialViewUrl, departmentListInSelectWithDepartment);
        }

        public ActionResult FilterFormGetDepartmentListHasMachine()
        {
            const string partialViewUrl =
                "~/Views/OEEReport/FilterForm/FilterFormGetDepartmentListHasMachineCombo.cshtml";

            var departmentListInSelectWithMachine = ApiList.GetMainDepartmentListHasMachine();

            return PartialView(partialViewUrl, departmentListInSelectWithMachine);
        }

        public ActionResult FilterFormGetMachineList(InputGetMainMachineListBySubDepartmentId input)
        {
            const string partialViewUrl = "~/Views/OEEReport/FilterForm/FilterFormGetMachineListCombo.cshtml";

            var machineList = ApiList.GetMainMachineListBySubDepartmentId(input);

            return PartialView(partialViewUrl, machineList);
        }

        public ActionResult FilterFormGetSubDepartmentListByMainDepartment(
            InputGetSubDepartmentListByMainDepartmentId input)
        {
            const string partialViewUrl =
                "~/Views/OEEReport/FilterForm/FilterFormGetSubDepartmentListByMainDepartmentCombo.cshtml";

            var subDepartmentList = ApiList.GetSubDepartmentListByMainDepartmentId(input);

            return PartialView(partialViewUrl, subDepartmentList);
        }

        public ActionResult FilterFormGetSubDepartmentListHasMachineByMainDepartmentId(
            InputGetSubDepartmentListHasMachineByMainDepartmentId input)
        {
            const string partialViewUrl =
                "~/Views/OEEReport/FilterForm/FilterFormGetSubDepartmentListHasMachineCombo.cshtml";

            var subDepartmentList = ApiList.GetSubDepartmentListHasMachineByMainDepartmentId(input);

            return PartialView(partialViewUrl, subDepartmentList);
        }

        [AccessToFormValidation(FormCode = "025")]
        public ActionResult Index() => View();

        public ActionResult InitResultGrid()
        {
            const string partialViewUrl = "~/Views/OEEReport/Grid/ResultGrid.cshtml";

            ViewBag.Date = "FillResultGridWithMachine";

            return PartialView(partialViewUrl);
        }

        public ActionResult ShowHelpModal()
        {
            const string partialUrl = "~/Views/OEEReport/ShowForm/ShowHelpForm.cshtml";

            return PartialView(partialUrl);
        }
    }
}