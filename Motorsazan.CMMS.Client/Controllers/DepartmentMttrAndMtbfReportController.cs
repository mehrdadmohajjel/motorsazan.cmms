using Motorsazan.CMMS.Client.Api;
using Motorsazan.CMMS.Shared.Enums;
using Motorsazan.CMMS.Shared.Models.Input.DepartmentMttrAndMtbfReport;
using Motorsazan.CMMS.Shared.Models.Input.NetExpert;
using Motorsazan.CMMS.Shared.Models.Input.ProductiveWorkOrder;
using Motorsazan.CMMS.Shared.Models.Output.MachineManagement;
using Motorsazan.CMMS.Shared.Models.Output.ProductiveWorkOrder;
using Motorsazan.CMMS.Shared.Utilities;
using System.Web.Mvc;
using Motorsazan.CMMS.Client.Filters;

namespace Motorsazan.CMMS.Client.Controllers
{
    public class DepartmentMttrAndMtbfReportController: BaseController
    {
        public ActionResult DetailRow()
        {
            const string partialViewUrl = "~/Views/DepartmentMttrAndMtbfReport/Grid/DetailRow.cshtml";

            return PartialView(partialViewUrl);
        }

        public ActionResult WorkOrderGridDetailRow()
        {
            const string partialViewUrl =
                "~/Views/DepartmentMttrAndMtbfReport/WorkOrderDetail/WorkOrderDetaillRow.cshtml";

            return PartialView(partialViewUrl);
        }

        public ActionResult FilterFormDepartmentIdCombo()
        {
            const string partialViewUrl =
                "~/Views/DepartmentMttrAndMtbfReport/FilterForm/FilterFormDepartmentIdCombo.cshtml";

            var departmentList = ApiList.GetAllMainDepartment();

            var allDepartment = new OutputGetAllMainDepartment { DepartmentId = 0, Title = "همه" };

            var dataSource = Tools.PrependGetAllItemToArray(departmentList, allDepartment);

            return PartialView(partialViewUrl, dataSource);
        }

        public static OutputGetMaintenanceGroupList[] GetMaintenanceGroupList() =>
            ApiList.GetMaintenanceGroupList();

        public ActionResult Grid(InputGetMTTRAndMTBFReportByCondition input, string startDate,
            string endDate, DatePeriodType datePeriodType)
        {
            var token = GetUserToken();
            const string partialViewUrl = "~/Views/DepartmentMttrAndMtbfReport/Grid/Grid.cshtml";

            if(input.DepartmentId == -1)
            {
                return PartialView(partialViewUrl);
            }

            (input.StartDate, input.EndDate) = Tools.NormalizeDates(startDate, endDate, datePeriodType);

            var dataSource = ApiList.GetDepartmentMttrAndMtbfReportByCondition(input, token);

            return PartialView(partialViewUrl, dataSource);
        }

        [AccessToFormValidation(FormCode = "017")]
        public ActionResult Index() => View();

        public ActionResult ShowMttrAndMtbfPerformanceCostReportParentPartial(
            InputGetMTTRAndMTBFPerformanceCostReportByMachineId input, string startDate,
            string endDate, DatePeriodType datePeriodType)
        {
            const string partialViewUrl =
                "~/Views/DepartmentMttrAndMtbfReport/DetailRow/ShowMTTRAndMTBFPerformanceCostReportParentPartial.cshtml";

            ViewData["StartDate"] = startDate;
            ViewData["EndDate"] = endDate;
            ViewData["DatePeriodType"] = datePeriodType;

            return PartialView(partialViewUrl, input);
        }

        public ActionResult ShowMttrAndMtbfPerformanceCostReportPartial(InputGetMTTRAndMTBFPerformanceCostReportByMachineId input, string startDate,
            string endDate, DatePeriodType datePeriodType)
        {
            const string partialViewUrl =
                "~/Views/DepartmentMttrAndMtbfReport/DetailRow/ShowMTTRAndMTBFPerformanceCostReportPartial.cshtml";

            (input.StartDate, input.EndDate) = Tools.NormalizeDates(startDate, endDate, datePeriodType);

            var dataSource = ApiList.GetMttrAndMtbfPerformanceCostReportByMachineId(input);

            return PartialView(partialViewUrl, dataSource);
        }

        public ActionResult ShowMttrAndMtbfPerformanceReportParentPartial(
            InputGetMTTRAndMTBFPerformanceReportByMachineId input, string startDate,
            string endDate, DatePeriodType datePeriodType)
        {
            const string partialViewUrl =
                "~/Views/DepartmentMttrAndMtbfReport/DetailRow/ShowMTTRAndMTBFPerformanceReportParentPartial.cshtml";

            ViewData["StartDate"] = startDate;
            ViewData["EndDate"] = endDate;
            ViewData["DatePeriodType"] = datePeriodType;

            return PartialView(partialViewUrl, input);
        }

        public ActionResult ShowMttrAndMtbfPerformanceReportPartial(
            InputGetMTTRAndMTBFPerformanceReportByMachineId input, string startDate,
            string endDate, DatePeriodType datePeriodType)
        {
            const string partialViewUrl =
                "~/Views/DepartmentMttrAndMtbfReport/DetailRow/ShowMTTRAndMTBFPerformanceReportPartial.cshtml";
            (input.StartDate, input.EndDate) = Tools.NormalizeDates(startDate, endDate, datePeriodType);
            var dataSource = ApiList.GetMttrAndMtbfPerformanceReportByMachineId(input);

            return PartialView(partialViewUrl, dataSource);
        }

        public ActionResult ShowMttrAndMtbfWearhouseParentReport(
            InputGetMTTRAndMTBFWearhouseReportByMachineId input, string startDate, string endDate, DatePeriodType datePeriodType)
        {
            const string partialViewUrl =
                "~/Views/DepartmentMttrAndMtbfReport/DetailRow/ShowMTTRAndMTBFWearhouseParentReport.cshtml";

            ViewData["StartDate"] = startDate;
            ViewData["EndDate"] = endDate;
            ViewData["DatePeriodType"] = datePeriodType;

            return PartialView(partialViewUrl, input);
        }

        public ActionResult ShowMttrAndMtbfWearhouseReport(
            InputGetMTTRAndMTBFWearhouseReportByMachineId input, string startDate, string endDate, DatePeriodType datePeriodType)
        {
            const string partialViewUrl =
                "~/Views/DepartmentMttrAndMtbfReport/DetailRow/ShowMTTRAndMTBFWearhouseReport.cshtml";
            (input.StartDate, input.EndDate) = Tools.NormalizeDates(startDate, endDate, datePeriodType);
            var dataSource = ApiList.GetMttrAndMtbfWearhouseReportByMachineId(input);

            return PartialView(partialViewUrl, dataSource);
        }

        public ActionResult ShowMttrAndMtbfMachineWorkOrderListParentPartial(
            InputGetMTTRAndMTBFMachineWorkOrderListByMachineId input, string startDate,
            string endDate, DatePeriodType datePeriodType)
        {
            const string partialViewUrl =
                "~/Views/DepartmentMttrAndMtbfReport/DetailRow/ShowMTTRAndMTBFMachineWorkOrderListParentPartial.cshtml";

            ViewData["StartDate"] = startDate;
            ViewData["EndDate"] = endDate;
            ViewData["DatePeriodType"] = datePeriodType;

            return PartialView(partialViewUrl, input);
        }

        public ActionResult ShowMttrAndMtbfMachineWorkOrderListPartial(
            InputGetMTTRAndMTBFMachineWorkOrderListByMachineId input, string startDate,
            string endDate, DatePeriodType datePeriodType)
        {
            const string partialViewUrl =
                "~/Views/DepartmentMttrAndMtbfReport/DetailRow/ShowMTTRAndMTBFMachineWorkOrderListPartial.cshtml";
            (input.StartDate, input.EndDate) = Tools.NormalizeDates(startDate, endDate, datePeriodType);
            var dataSource = ApiList.GetMttrAndMtbfMachineWorkOrderListByMachineId(input);

            return PartialView(partialViewUrl, dataSource);
        }

        public ActionResult ShowMttrChartReportByMachineIdPartial(InputGetMTTRChartReportByMachineId input,
            string startDate,
            string endDate, DatePeriodType datePeriodType)
        {
            const string partialViewUrl =
                "~/Views/DepartmentMttrAndMtbfReport/DetailRow/ShowMttrChartReportByMachineIdPartial.cshtml";
            (input.StartDate, input.EndDate) = Tools.NormalizeDates(startDate, endDate, datePeriodType);

            var mttrChartReportDataSource = ApiList.GetMttrChartReportByMachineId(input);
            return PartialView(partialViewUrl, mttrChartReportDataSource);
        }

        public ActionResult ShowMtbfChartReportByMachineIdPartial(InputGetMTBFChartReportByMachineId input,
            string startDate,
            string endDate, DatePeriodType datePeriodType)
        {
            const string partialViewUrl =
                "~/Views/DepartmentMttrAndMtbfReport/DetailRow/ShowMtbfChartReportByMachineIdPartial.cshtml";
            (input.StartDate, input.EndDate) = Tools.NormalizeDates(startDate, endDate, datePeriodType);

            var mttrChartReportDataSource = ApiList.GetMtbfChartReportByMachineId(input);
            return PartialView(partialViewUrl, mttrChartReportDataSource);
        }

        public ActionResult ShowOeeChartReportOnMTTRAndMTBFByMachineIdPartial(
            InputGetOeeChartReportOnMTTRAndMTBFByMachineId input, string startDate,
            string endDate, DatePeriodType datePeriodType)
        {
            const string partialViewUrl =
                "~/Views/DepartmentMttrAndMtbfReport/DetailRow/ShowOeeChartReportOnMTTRAndMTBFByMachineIdPartial.cshtml";
            (input.StartDate, input.EndDate) = Tools.NormalizeDates(startDate, endDate, datePeriodType);

            var mttrChartReportDataSource = ApiList.GetOeeChartReportOnMTTRAndMTBFByMachineId(input);
            return PartialView(partialViewUrl, mttrChartReportDataSource);
        }

        public ActionResult ShowGetMttrAndMtbfFaultCodeCountReportParentPartial(
            InputGetMTTRAndMTBFFaultCodeCountReportByMachineId input, string startDate,
            string endDate, DatePeriodType datePeriodType)
        {
            const string partialViewUrl = "~/Views/DepartmentMttrAndMtbfReport/DetailRow/ShowGetMTTRAndMTBFFaultCodeCountReportParentPartial.cshtml";

            ViewData["StartDate"] = startDate;
            ViewData["EndDate"] = endDate;
            ViewData["DatePeriodType"] = datePeriodType;

            return PartialView(partialViewUrl, input);
        }

        public ActionResult ShowGetMttrAndMtbfFaultCodeCountReportPartial(
            InputGetMTTRAndMTBFFaultCodeCountReportByMachineId input, string startDate,
            string endDate, DatePeriodType datePeriodType)
        {
            const string partialViewUrl = "~/Views/DepartmentMttrAndMtbfReport/DetailRow/ShowGetMTTRAndMTBFFaultCodeCountReportPartial.cshtml";

            (input.StartDate, input.EndDate) = Tools.NormalizeDates(startDate, endDate, datePeriodType);
            var reportDataSource = ApiList.GetMttrAndMtbfFaultCodeCountReportByMachineId(input);
            return PartialView(partialViewUrl, reportDataSource);
        }

        #region workOrderDetailRow

        public ActionResult RegistrarInfoModal(InputGetWorkOrderRegistrantInfoByWorkOrderId input)
        {
            const string partialViewUrl =
                "~/Views/DepartmentMttrAndMtbfReport/WorkOrderDetail/RegistrarInfoModal.cshtml";

            var workOrderRegistrantInfoDataSource = ApiList.GetWorkOrderRegistrantInfoByWorkOrderId(input);

            return PartialView(partialViewUrl, workOrderRegistrantInfoDataSource);
        }

        public ActionResult PerformedOperationsGrid(InputGetActionOrDelayListByWorkOrderId input)
        {
            const string partialViewUrl =
                "~/Views/DepartmentMttrAndMtbfReport/WorkOrderDetail/PerformedOperationsGrid.cshtml";

            var token = GetUserToken();

            var workOrderActionOrDelayListDataSource = ApiList.GetActionOrDelayListByWorkOrderId(input, token);

            return PartialView(partialViewUrl, workOrderActionOrDelayListDataSource);
        }

        public ActionResult ConsumingMaterialsGrid(InputGetWorKOrderConsumableListByWorkOrderId input)
        {
            const string partialViewUrl =
                "~/Views/DepartmentMttrAndMtbfReport/WorkOrderDetail/ConsumingMaterialsGrid.cshtml";

            var token = GetUserToken();

            var worKOrderConsumableListDataSource = ApiList.GetWorKOrderConsumableListByWorkOrderId(input, token);

            return PartialView(partialViewUrl, worKOrderConsumableListDataSource);
        }

        public ActionResult ReferralsHistoryGrid(InputGetWorKOrderReferralListByWorkOrderId input)
        {
            const string partialViewUrl =
                "~/Views/DepartmentMttrAndMtbfReport/WorkOrderDetail/ReferralsHistoryGrid.cshtml";

            var token = GetUserToken();

            var workOrderReferralListDataSource = ApiList.GetWorKOrderReferralListByWorkOrderId(input, token);

            return PartialView(partialViewUrl, workOrderReferralListDataSource);
        }

        #endregion
    }
}