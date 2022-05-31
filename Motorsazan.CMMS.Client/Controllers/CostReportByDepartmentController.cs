using System.Web.Mvc;
using Motorsazan.CMMS.Client.Api;
using Motorsazan.CMMS.Client.Filters;
using Motorsazan.CMMS.Shared.Enums;
using Motorsazan.CMMS.Shared.Models.Input.CostReportByDepartment;
using Motorsazan.CMMS.Shared.Models.Output.ProductiveWorkOrder;
using Motorsazan.CMMS.Shared.Utilities;
using OutputGetAllMainDepartment = Motorsazan.CMMS.Shared.Models.Output.MachineManagement.OutputGetAllMainDepartment;

namespace Motorsazan.CMMS.Client.Controllers
{
    public class CostReportByDepartmentController: BaseController
    {
        public ActionResult FilterFormDepartmentIdCombo()
        {
            const string partialViewUrl =
                "~/Views/CostReportByDepartment/FilterForm/FilterFormDepartmentIdCombo.cshtml";

            var departmentList = ApiList.GetAllMainDepartment();

            var allDepartment = new OutputGetAllMainDepartment { DepartmentId = 0, Title = "همه" };
            
            var dataSource = Tools.PrependGetAllItemToArray(departmentList, allDepartment);

            return PartialView(partialViewUrl, dataSource);
        }

        public static OutputGetMaintenanceGroupList[] GetAllMaintenanceGroupList() =>
            ApiList.GetMaintenanceGroupList();

        public ActionResult Grid(InputGetDepartmentCostReportByCondition input, string startDate,
            string endDate, DatePeriodType datePeriodType = DatePeriodType.CurrentDay)
        {
            const string partialViewUrl = "~/Views/CostReportByDepartment/Grid/Grid.cshtml";

            (input.StartDate, input.EndDate) = Tools.NormalizeDates(startDate, endDate, datePeriodType);

            var costReportByDepartmentDataSource = ApiList.GetDepartmentCostReportByCondition(input);

            return PartialView(partialViewUrl, costReportByDepartmentDataSource);
        }

        [AccessToFormValidation(FormCode = "029")]
        public ActionResult Index() => View();
    }
}