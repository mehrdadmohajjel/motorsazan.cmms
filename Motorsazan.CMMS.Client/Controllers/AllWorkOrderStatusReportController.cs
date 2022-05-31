using Motorsazan.CMMS.Client.Api;
using Motorsazan.CMMS.Shared.Enums;
using Motorsazan.CMMS.Shared.Models.Input.AllWorkOrderStatusReport;
using Motorsazan.CMMS.Shared.Models.Input.CostReportByDepartment;
using Motorsazan.CMMS.Shared.Models.Output.CostReportByDepartment;
using Motorsazan.CMMS.Shared.Models.Output.ProductiveWorkOrder;
using Motorsazan.CMMS.Shared.Utilities;
using System.Web.Mvc;

namespace Motorsazan.CMMS.Client.Controllers
{
    public class AllWorkOrderStatusReportController: BaseController
    {
        public ActionResult FillResultGrid(InputAllWorkOrderStatusReportListByCondition input,
            string persianStartDate, string persianEndDate, DatePeriodType datePeriodType)
        {
            const string partialViewUrl = "~/Views/AllWorkOrderStatusReport/Grid/ResultGrid.cshtml";
            var token = GetUserToken();
            (input.StartDate, input.EndDate) =
                Tools.NormalizeDates(persianStartDate, persianEndDate, datePeriodType);

            var apiResult = ApiList.GetAllWorkOrderStatusReportListByCondition(input, token);

            return PartialView(partialViewUrl, apiResult);
        }


        public ActionResult FilterFormGetDepartmentListCombo()
        {
            const string partialViewUrl =
                "~/Views/AllWorkOrderStatusReport/FilterForm/FilterFormDepartmentListCombo.cshtml";

            var userId = GetCurrentUser().UserID;
            var token = GetUserToken();
            var apiParam = new InputGetProductionLineListByUserId { UserID = userId };

            var departmentList = GetDepartmentList(apiParam, token);

            var allDepartment = new OutputGetDepartmentList { DepartmentId = 0, ShowName = "همه امور" };

            var dataSource = Tools.PrependGetAllItemToArray(departmentList, allDepartment);

            return PartialView(partialViewUrl, dataSource);
        }

        public ActionResult FilterFormGetWorkOrderAllStatusList()
        {
            const string partailViewUrl =
                "~/Views/AllWorkOrderStatusReport/FilterForm/FilterFormGetWorkOrderAllStatusList.cshtml";

            var dataSource = GetWorkOrderStatusList();
            return PartialView(partailViewUrl, dataSource);
        }

        public ActionResult FilterFormMaintenanceGroupCombo()
        {
            const string partialViewUrl =
                "~/Views/AllWorkOrderStatusReport/FilterForm/FilterFormMaintenanceGroupListCombo.cshtml";
            var getMaintenanceGroupList = GetWorkOrderMaintenanceGroupList();
            var allMaintenanceGroup = new OutputGetMaintenanceGroupList
            {
                MaintenanceGroupId = 0,
                MaintenanceGroupName = "همه موارد"
            };

            var dataSource = Tools.PrependGetAllItemToArray(getMaintenanceGroupList, allMaintenanceGroup);

            return PartialView(partialViewUrl, dataSource);
        }

        public OutputGetDepartmentList[] GetDepartmentList(InputGetProductionLineListByUserId values, string token) =>
            ApiList.GetDepartmentList(values, token);

        public ActionResult GetStopTypeList()
        {
            const string partialViewUrl =
                "~/Views/AllWorkOrderStatusReport/FilterForm/FilterFormStopTypeListCombo.cshtml";
            var getStopTypeList = GetWorkOrderStopTypeList();

            var allStopTypeList = new OutputGetStopTypeList { StopTypeId = 0, StopTypeTitle = "همه موارد"};

            var dataSource = Tools.PrependGetAllItemToArray(getStopTypeList, allStopTypeList);
            return PartialView(partialViewUrl, dataSource);
        }

        public static OutputGetMaintenanceGroupList[] GetWorkOrderMaintenanceGroupList() =>
            ApiList.GetMaintenanceGroupList();

        public OutputGetWorkOrderStatusTypeList[] GetWorkOrderStatusList() => ApiList.GetWorkOrderStatusTypeList();

        public static OutputGetStopTypeList[] GetWorkOrderStopTypeList() => ApiList.GetStopTypeList();

        public ActionResult Index() => View();
    }
}