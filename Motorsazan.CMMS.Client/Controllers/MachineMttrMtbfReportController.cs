using System.Web.Mvc;
using Motorsazan.CMMS.Client.Api;
using Motorsazan.CMMS.Client.Filters;
using Motorsazan.CMMS.Shared.Enums;
using Motorsazan.CMMS.Shared.Models.Input.Inspection;
using Motorsazan.CMMS.Shared.Models.Input.MachineMttrMtbfReport;
using Motorsazan.CMMS.Shared.Models.Output.ProductiveWorkOrder;
using Motorsazan.CMMS.Shared.Utilities;

namespace Motorsazan.CMMS.Client.Controllers
{
    public class MachineMttrMtbfReportController: BaseController
    {
        public ActionResult DetailGrid(InputGetTotalMttrAndMtbfByMachineId input, string persianStartDate,
            string persianEndDate, DatePeriodType datePeriodType)
        {
            const string partialViewUrl = "~/Views/MachineMttrMtbfReport/Grid/DetailGrid.cshtml";

            (input.StartDate, input.EndDate) = Tools.NormalizeDates(persianStartDate, persianEndDate, datePeriodType);

            var dataSource = ApiList.GetTotalMttrAndMtbfByMachineId(input);

            return PartialView(partialViewUrl, dataSource);
        }

        public ActionResult FilterFormDepartmentIdCombo()
        {
            const string partialViewUrl =
                "~/Views/MachineMttrMtbfReport/FilterForm/FilterFormDepartmentIdCombo.cshtml";

            var getDepartmentList = ApiList.GetMainDepartmentListHasMachine();

            return PartialView(partialViewUrl, getDepartmentList);
        }

        public ActionResult FilterFormMachineIdCombo(InputGetMainMachineListBySubDepartmentId input)
        {
            const string partialViewUrl =
                "~/Views/MachineMttrMtbfReport/FilterForm/FilterFormMachineIdCombo.cshtml";

            var dataSource = ApiList.GetMainMachineListBySubDepartmentId(input);

            return PartialView(partialViewUrl, dataSource);
        }

        public ActionResult FilterFormSubDepartmentIdCombo(InputGetSubDepartmentListHasMachineByMainDepartmentId input)
        {
            const string partialViewUrl =
                "~/Views/MachineMttrMtbfReport/FilterForm/FilterFormSubDepartmentIdCombo.cshtml";

            var getSubDepartmentList = ApiList.GetSubDepartmentListHasMachineByMainDepartmentId(input);

            return PartialView(partialViewUrl, getSubDepartmentList);
        }

        public static OutputGetMaintenanceGroupList[] GetAllMaintenanceGroupList() =>
            ApiList.GetMaintenanceGroupList();

        public ActionResult Grid(InputGetMachineMTTRAndMTBFReportByCondition input, string persianStartDate,
            string persianEndDate, DatePeriodType datePeriodType)
        {
            var token = GetUserToken();
            const string partialViewUrl = "~/Views/MachineMttrMtbfReport/Grid/Grid.cshtml";

            if(input.MachineId == -1)
            {
                return PartialView(partialViewUrl);
            }

            (input.StartDate, input.EndDate) = Tools.NormalizeDates(persianStartDate, persianEndDate, datePeriodType);

            var dataSource = ApiList.GetMachineMttrAndMtbfReportByCondition(input, token);

            return PartialView(partialViewUrl, dataSource);
        }

        [AccessToFormValidation(FormCode = "018")]
        public ActionResult Index() => View();
    }
}