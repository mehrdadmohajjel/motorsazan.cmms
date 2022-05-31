using Motorsazan.CMMS.Client.Api;
using Motorsazan.CMMS.Shared.Enums;
using Motorsazan.CMMS.Shared.Models.Input.Inspection;
using Motorsazan.CMMS.Shared.Models.Input.PreventiveMaintenancesSchedulingReport;
using Motorsazan.CMMS.Shared.Models.Output.Inspection;
using Motorsazan.CMMS.Shared.Models.Output.ProductiveWorkOrder;
using Motorsazan.CMMS.Shared.Utilities;
using System.Web.Mvc;

namespace Motorsazan.CMMS.Client.Controllers
{
    public class PreventiveMaintenancesSchedulingReportController: BaseController
    {
        public ActionResult FilterFormMainDepartmentCombo()
        {
            const string partialViewUrl =
                "~/Views/PreventiveMaintenancesSchedulingReport/FilterForm/FilterFormMainDepartmentCombo.cshtml";
            var token = GetUserToken();
            var departmentList = ApiList.GetAllMainDepartment(token);


            return PartialView(partialViewUrl, departmentList);
        }

        public ActionResult FilterFormSubDepartmentCombo(InputGetSubDepartmentListHasMachineByMainDepartmentId value)
        {
            const string partialViewUrl =
                "~/Views/PreventiveMaintenancesSchedulingReport/FilterForm/FilterFormSubDepartmentCombo.cshtml";
            var subDepartmentList = ApiList.GetSubDepartmentListHasMachineByMainDepartmentId(value);

            var allsubDepartmentList =
                new OutputGetSubDepartmentListHasMachineByMainDepartmentId { DepartmentId = 0, Title = "همه خطوط" };
            var dataSource = Tools.PrependGetAllItemToArray(subDepartmentList, allsubDepartmentList);

            return PartialView(partialViewUrl, dataSource);
        }

        public ActionResult FilterFormMaintenanceGroupCombo()
        {
            const string partialViewUrl =
                "~/Views/PreventiveMaintenancesSchedulingReport/FilterForm/FilterFormMaintenanceGroupCombo.cshtml";


            var maintenanceGroupList = ApiList.GetMaintenanceGroupList();

            var allMaintenanceGroupList =
                new OutputGetMaintenanceGroupList {MaintenanceGroupId = 0, MaintenanceGroupName = "همه گروه ها"};

            var dataSource = Tools.PrependGetAllItemToArray(maintenanceGroupList, allMaintenanceGroupList);

            return PartialView(partialViewUrl, dataSource);
        }

        public OutputGetSubDepartmentListByMainDepartmentId[] GetSubDepartmentList(
            InputGetSubDepartmentListByMainDepartmentId values) =>
            ApiList.GetSubDepartmentListByMainDepartmentId(values);

        public static OutputGetMaintenanceGroupList[] GetWorkOrderMaintenanceGroupList() =>
            ApiList.GetMaintenanceGroupList();

        public ActionResult Grid(InputGetPreventiveMaintenanceSchedulingReportByCondition input,
            string startShamsiDate, string endShamsiDate, DatePeriodType dateType = DatePeriodType.CurrentDay)
        {
            const string partialViewUrl = "~/Views/PreventiveMaintenancesSchedulingReport/Grid/Grid.cshtml";

            var token = GetUserToken();

            (input.StartDate, input.EndDate) = Tools.NormalizeDates(startShamsiDate, endShamsiDate, dateType);

            var dataSourc = ApiList.GetPreventiveMaintenanceSchedulingReportByCondition(input, token);

            return PartialView(partialViewUrl, dataSourc);
        }

        public ActionResult Index() => View();
    }
}