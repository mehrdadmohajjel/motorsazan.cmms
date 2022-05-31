using System.Web.Mvc;
using Motorsazan.CMMS.Client.Api;
using Motorsazan.CMMS.Client.Filters;
using Motorsazan.CMMS.Shared.Enums;
using Motorsazan.CMMS.Shared.Models.Input.Inspection;
using Motorsazan.CMMS.Shared.Models.Input.PerformanceReportByMachine;
using Motorsazan.CMMS.Shared.Models.Output.Inspection;
using Motorsazan.CMMS.Shared.Models.Output.ProductiveWorkOrder;
using Motorsazan.CMMS.Shared.Utilities;

namespace Motorsazan.CMMS.Client.Controllers
{
    public class PerformanceReportByMachineController: BaseController
    {
        public ActionResult FilterFormDepartmentIdCombo()
        {
            const string partialViewUrl =
                "~/Views/PerformanceReportByMachine/FilterForm/FilterFormDepartmentIdCombo.cshtml";

            var getDepartmentList = ApiList.GetMainDepartmentListHasMachine();

            return PartialView(partialViewUrl, getDepartmentList);
        }

        public ActionResult FilterFormMachineIdCombo(InputGetMainMachineListBySubDepartmentId input)
        {
            const string partialViewUrl =
                "~/Views/PerformanceReportByMachine/FilterForm/FilterFormMachineIdCombo.cshtml";

            var getMachineList = ApiList.GetMainMachineListBySubDepartmentId(input);

            var allMachine = new OutputGetMainMachineListBySubDepartmentId { MachineId = 0, MachineName = "همه"};

            var dataSource = Tools.PrependGetAllItemToArray(getMachineList, allMachine);

            return PartialView(partialViewUrl, dataSource);
        }

        public ActionResult FilterFormSubDepartmentIdCombo(InputGetSubDepartmentListHasMachineByMainDepartmentId input)
        {
            const string partialViewUrl =
                "~/Views/PerformanceReportByMachine/FilterForm/FilterFormSubDepartmentIdCombo.cshtml";

            var getSubDepartmentList = ApiList.GetSubDepartmentListHasMachineByMainDepartmentId(input);

            return PartialView(partialViewUrl, getSubDepartmentList);
        }

        public static OutputGetMaintenanceGroupList[] GetAllMaintenanceGroupList() =>
            ApiList.GetMaintenanceGroupList();

        public ActionResult Grid(InputGetMainMachinePerformanceReportByCondition input, string startDate,
            string endDate, DatePeriodType datePeriodType)
        {
            const string partialViewUrl = "~/Views/PerformanceReportByMachine/Grid/Grid.cshtml";

            if(input.DepartmentId == -1)
            {
                return PartialView(partialViewUrl);
            }


            (input.StartDate, input.EndDate) = Tools.NormalizeDates(startDate, endDate, datePeriodType);

            var performanceReportByMachineListDataSource = ApiList.GetMainMachinePerformanceReportByCondition(input);

            return PartialView(partialViewUrl, performanceReportByMachineListDataSource);
        }

        [AccessToFormValidation(FormCode = "032")]
        public ActionResult Index() => View();
    }
}