using Motorsazan.CMMS.Client.Api;
using Motorsazan.CMMS.Client.Filters;
using Motorsazan.CMMS.Shared.Enums;
using Motorsazan.CMMS.Shared.Models.Input.ConsumableStockForEachMachineByCondition;
using Motorsazan.CMMS.Shared.Models.Input.Inspection;
using Motorsazan.CMMS.Shared.Models.Output.Inspection;
using Motorsazan.CMMS.Shared.Utilities;
using System.Web.Mvc;

namespace Motorsazan.CMMS.Client.Controllers
{
    public class ConsumableStockForEachMachineController: BaseController
    {

        public ActionResult FillResultGrid(InputGetConsumableStockForEachMachineByCondition input,
            string persianStartDate, string persianEndDate, DatePeriodType datePeriodType = DatePeriodType.ThisYear)
        {
            const string partialViewUrl = "~/Views/ConSumableStockForEachMachine/Grid/ResultGrid.cshtml";


            if(input.MachineId == -1)
            {
                return PartialView(partialViewUrl);
            }

            (input.StartDate, input.EndDate) = Tools.NormalizeDates(persianStartDate, persianEndDate, datePeriodType);

            var apiResult = ApiList.GetConsumableStockForEachMachineByCondition(input);

            return PartialView(partialViewUrl, apiResult);
        }


        public ActionResult FilterFormGetDepartmentListHasMachine()
        {
            const string partialViewUrl =
                "~/Views/ConSumableStockForEachMachine/FilterForm/FilterFormGetDepartmentListHasMachineCombo.cshtml";

            var departmentListInSelectWithMachine = ApiList.GetMainDepartmentListHasMachine();

            return PartialView(partialViewUrl, departmentListInSelectWithMachine);
        }

        public ActionResult FilterFormGetMachineList(InputGetMainMachineListBySubDepartmentId input)
        {
            const string partialViewUrl = "~/Views/ConsumableStockForEachMachine/FilterForm/FilterFormGetMachineListCombo.cshtml";

            var machineList = ApiList.GetMainMachineListBySubDepartmentId(input);
            var allmachineList =
            new OutputGetMainMachineListBySubDepartmentId { MachineId = 0, MachineName = "همه دستگاه ها" };

            var dataSource = Tools.PrependGetAllItemToArray(machineList, allmachineList);

            return PartialView(partialViewUrl, dataSource);
        }


        public ActionResult FilterFormGetSubDepartmentListHasMachineByMainDepartmentId(
            InputGetSubDepartmentListHasMachineByMainDepartmentId input)
        {
            const string partialViewUrl =
                "~/Views/ConsumableStockForEachMachine/FilterForm/FilterFormGetSubDepartmentListHasMachineCombo.cshtml";

            var subDepartmentList = ApiList.GetSubDepartmentListHasMachineByMainDepartmentId(input);

            return PartialView(partialViewUrl, subDepartmentList);
        }

        [AccessToFormValidation(FormCode = "041")]
        public ActionResult Index() => View();


    }
}