using Motorsazan.CMMS.Client.Api;
using Motorsazan.CMMS.Shared.Enums;
using Motorsazan.CMMS.Shared.Models.Input.CostReportByMachine;
using Motorsazan.CMMS.Shared.Models.Input.Inspection;
using Motorsazan.CMMS.Shared.Models.Output.Inspection;
using Motorsazan.CMMS.Shared.Models.Output.ProductiveWorkOrder;
using Motorsazan.CMMS.Shared.Utilities;
using System.Web.Mvc;
using Motorsazan.CMMS.Client.Filters;

namespace Motorsazan.CMMS.Client.Controllers
{
    public class CostReportByMachineController: BaseController
    {
        public ActionResult FilterFormDepartmentIdCombo()
        {
            const string partialViewUrl = "~/Views/CostReportByMachine/FilterForm/FilterFormDepartmentIdCombo.cshtml";

            var departmentList = ApiList.GetMainDepartmentListHasMachine();

            return PartialView(partialViewUrl, departmentList);
        }

        public ActionResult FilterFormMachineIdCombo(InputGetMainMachineListBySubDepartmentId input)
        {
            const string partialViewUrl = "~/Views/CostReportByMachine/FilterForm/FilterFormMachineIdCombo.cshtml";

            var machineList = ApiList.GetMainMachineListBySubDepartmentId(input);

            var allSubDepartmentMachineList =
                new OutputGetMainMachineListBySubDepartmentId { MachineId = 0, MachineName = "همه" };

            var dataSource = Tools.PrependGetAllItemToArray(machineList, allSubDepartmentMachineList);

            return PartialView(partialViewUrl, dataSource);
        }

        public ActionResult FilterFormSubDepartmentIdCombo(InputGetSubDepartmentListHasMachineByMainDepartmentId input)
        {
            const string partialViewUrl =
                "~/Views/CostReportByMachine/FilterForm/FilterFormSubDepartmentIdCombo.cshtml";


            var subDepartmentList = ApiList.GetSubDepartmentListHasMachineByMainDepartmentId(input);

            return PartialView(partialViewUrl, subDepartmentList);
        }

        public static OutputGetMaintenanceGroupList[] GetAllMaintenanceGroupList() =>
            ApiList.GetMaintenanceGroupList();

        public ActionResult Grid(InputGetMainMachineCostReportByCondition input, string startDate,
            string endDate, DatePeriodType datePeriodType = DatePeriodType.CurrentDay)
        {
            const string partialViewUrl = "~/Views/CostReportByMachine/Grid/Grid.cshtml";

            (input.StartDate, input.EndDate) = Tools.NormalizeDates(startDate, endDate, datePeriodType);

            var costReportByMachineDataSource = ApiList.GetMainMachineCostReportByCondition(input);

            return PartialView(partialViewUrl, costReportByMachineDataSource);
        }

        [AccessToFormValidation(FormCode = "028")]
        public ActionResult Index() => View();
    }
}