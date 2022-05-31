using System.Web.Mvc;
using Motorsazan.CMMS.Client.Api;
using Motorsazan.CMMS.Client.Filters;
using Motorsazan.CMMS.Shared.Enums;
using Motorsazan.CMMS.Shared.Models.Input.Inspection;
using Motorsazan.CMMS.Shared.Models.Input.MainMachineOilServiceReport;
using Motorsazan.CMMS.Shared.Models.Output.Inspection;
using Motorsazan.CMMS.Shared.Utilities;

namespace Motorsazan.CMMS.Client.Controllers
{
    public class MainMachineOilServiceReportController: BaseController
    {
        public ActionResult FilterFormDepartmentIdCombo()
        {
            const string partialViewUrl =
                "~/Views/MainMachineOilServiceReport/FilterForm/FilterFormDepartmentIdCombo.cshtml";

            var departmentList = ApiList.GetMainDepartmentListHasMachine();

            return PartialView(partialViewUrl, departmentList);
        }

        public ActionResult FilterFormMachineIdCombo(InputGetMainMachineListBySubDepartmentId input)
        {
            const string partialViewUrl =
                "~/Views/MainMachineOilServiceReport/FilterForm/FilterFormMachineIdCombo.cshtml";

            var machineList = ApiList.GetMainMachineListBySubDepartmentId(input);

            var allSubDepartmentMachineList =
                new OutputGetMainMachineListBySubDepartmentId { MachineId = 0, MachineName = "همه"};

            var dataSource = Tools.PrependGetAllItemToArray(machineList, allSubDepartmentMachineList);

            return PartialView(partialViewUrl, dataSource);
        }

        public ActionResult FilterFormSubDepartmentIdCombo(InputGetSubDepartmentListHasMachineByMainDepartmentId input)
        {
            const string partialViewUrl =
                "~/Views/MainMachineOilServiceReport/FilterForm/FilterFormSubDepartmentIdCombo.cshtml";


            var subDepartmentList = ApiList.GetSubDepartmentListHasMachineByMainDepartmentId(input);

            return PartialView(partialViewUrl, subDepartmentList);
        }

        public ActionResult Grid(InputGetMainMachineOilServiceReportByCondition input, string startDate,
            string endDate, DatePeriodType datePeriodType)
        {
            const string partialViewUrl = "~/Views/MainMachineOilServiceReport/Grid/Grid.cshtml";

            (input.StartDate, input.EndDate) = Tools.NormalizeDates(startDate, endDate, datePeriodType);

            if(input.DepartmentId == -1)
            {
                return PartialView(partialViewUrl);
            }

            var mainMachineOilServiceReportListDataSource = ApiList.GetMainMachineOilServiceReportByCondition(input);

            return PartialView(partialViewUrl, mainMachineOilServiceReportListDataSource);
        }

        [AccessToFormValidation(FormCode = "026")]
        public ActionResult Index() => View();
    }
}