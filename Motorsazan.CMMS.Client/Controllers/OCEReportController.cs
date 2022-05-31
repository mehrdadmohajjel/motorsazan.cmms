using System.Web.Mvc;
using Motorsazan.CMMS.Client.Api;
using Motorsazan.CMMS.Client.Filters;
using Motorsazan.CMMS.Shared.Enums;
using Motorsazan.CMMS.Shared.Models.Input.NetExpert;
using Motorsazan.CMMS.Shared.Models.Input.OCEReport;
using Motorsazan.CMMS.Shared.Models.Output.NetExpert;
using Motorsazan.CMMS.Shared.Models.Output.ProductiveWorkOrder;
using Motorsazan.CMMS.Shared.Utilities;

namespace Motorsazan.CMMS.Client.Controllers
{
    public class OCEReportController: BaseController
    {
        public ActionResult FilterFormGetMaintenanceGroupList()
        {
            const string partialViewUrl =
                "~/Views/OCEReport/FilterForm/FilterFormGetMaintenanceGroupListCombo.cshtml";

            var maintenanceGroupList = ApiList.GetMaintenanceGroupList();

            var allMaintenanceGroupList =
                new OutputGetMaintenanceGroupList { MaintenanceGroupId = 0, MaintenanceGroupName = "همه" };

            var dataSource = Tools.PrependGetAllItemToArray(maintenanceGroupList, allMaintenanceGroupList);

            return PartialView(partialViewUrl, dataSource);
        }

        public ActionResult FilterFormGetMaintenanceGroupMemberList(InputGetMaintenanceGroupMemberListByMaintenanceGroupId input)
        {
            const string partialViewUrl =
                "~/Views/OCEReport/FilterForm/FilterFormGetMaintenanceGroupMemberListCombo.cshtml";

            var maintenanceGroupMemberList = ApiList.GetMaintenanceGroupMemberListByMaintenanceGroupId(input);
            var allMaintenanceGroupMemberList = new OutputGetMaintenanceGroupMemberListByMaintenanceGroupId { EmployeeId = 0, Name = "همه" };

            var dataSource = Tools.PrependGetAllItemToArray(maintenanceGroupMemberList, allMaintenanceGroupMemberList);

            return PartialView(partialViewUrl, dataSource);
        }

        public static OutputGetMaintenanceGroupList[] GetWorkOrderMaintenanceGroupList() =>
            ApiList.GetMaintenanceGroupList();

        [AccessToFormValidation(FormCode = "024")]
        public ActionResult Index() => View();

        public ActionResult ResultGrid(InputGetOCEReportByCondition input, string persianStartDate,
            string persianEndDate, DatePeriodType datePeriodType)
        {
            const string partialViewUrl = "~/Views/OCEReport/Grid/ResultGrid.cshtml";

            if(input.MaintenanceGroupId == -1)
            {
                return PartialView(partialViewUrl);
            }

            (input.StartDate, input.EndDate) = Tools.NormalizeDates(persianStartDate, persianEndDate, datePeriodType);

            var dataSource = ApiList.GetOCEReportByCondition(input);

            return PartialView(partialViewUrl, dataSource);
        }
    }
}