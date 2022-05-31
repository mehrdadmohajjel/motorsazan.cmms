using System.Web.Mvc;
using Motorsazan.CMMS.Client.Api;
using Motorsazan.CMMS.Shared.Enums;
using Motorsazan.CMMS.Shared.Models.Input.DepartmentMttrAndMtbfReport;
using Motorsazan.CMMS.Shared.Models.Input.MaintenanceGroupMttrAndMtbfReport;
using Motorsazan.CMMS.Shared.Models.Input.NetExpert;
using Motorsazan.CMMS.Shared.Models.Input.ProductiveWorkOrder;
using Motorsazan.CMMS.Shared.Models.Output.Inspection;
using Motorsazan.CMMS.Shared.Models.Output.ProductiveWorkOrder;
using Motorsazan.CMMS.Shared.Utilities;

namespace Motorsazan.CMMS.Client.Controllers
{
    public class MaintenanceGroupMttrAndMtbfReportController: BaseController
    {
        public ActionResult DetailRow()
        {
            const string partialViewUrl = "~/Views/MaintenanceGroupMttrAndMtbfReport/Grid/DetailRow.cshtml";

            return PartialView(partialViewUrl);
        }

        public ActionResult FilterFormDepartmentIdCombo()
        {
            const string partialViewUrl =
                "~/Views/MaintenanceGroupMttrAndMtbfReport/FilterForm/FilterFormDepartmentIdCombo.cshtml";

            var departmentList = ApiList.GetMainDepartmentListHasMachine();

            var allDepartment = new OutputGetMainDepartmentListHasMachine {DepartmentId = 0, Title = "همه"};

            var dataSource = Tools.PrependGetAllItemToArray(departmentList, allDepartment);

            return PartialView(partialViewUrl, dataSource);
        }

        public ActionResult FilterFormMaintenanceGroupIdCombo()
        {
            const string partialViewUrl =
                "~/Views/MaintenanceGroupMttrAndMtbfReport/FilterForm/FilterFormMaintenanceGroupCombo.cshtml";

            var getMaintenanceGroupList = GetMaintenanceGroupList();

            return PartialView(partialViewUrl, getMaintenanceGroupList);
        }

        public static OutputGetMaintenanceGroupList[] GetMaintenanceGroupList() =>
            ApiList.GetMaintenanceGroupList();

        public ActionResult Grid(InputGetMaintenanceGroupMttrAndMtbfReportByCondition input, string persianStartDate,
            string persianEndDate, DatePeriodType datePeriodType)
        {
            const string partialViewUrl = "~/Views/MaintenanceGroupMttrAndMtbfReport/Grid/Grid.cshtml";

            if(input.DepartmentId == -1)
            {
                return PartialView(partialViewUrl);
            }

            var token = GetUserToken();

            (input.StartDate, input.EndDate) = Tools.NormalizeDates(persianStartDate, persianEndDate, datePeriodType);

            var dataSource = ApiList.GetMaintenanceGroupMttrAndMtbfReportByCondition(input, token);

            return PartialView(partialViewUrl, dataSource);
        }

        public ActionResult Index() => View();

        public ActionResult ShowGetMttrAndMtbfFaultCodeCountReportPartial(
            InputGetMTTRAndMTBFFaultCodeCountReportByMachineId input, string startDate,
            string endDate, DatePeriodType datePeriodType)
        {
            const string partialViewUrl =
                "~/Views/MaintenanceGroupMttrAndMtbfReport/DetailRow/ShowGetMTTRAndMTBFFaultCodeCountReportPartial.cshtml";

            (input.StartDate, input.EndDate) = Tools.NormalizeDates(startDate, endDate, datePeriodType);
            var reportDataSource = ApiList.GetMttrAndMtbfFaultCodeCountReportByMachineId(input);
            return PartialView(partialViewUrl, reportDataSource);
        }

        public ActionResult ShowMtbfChartReportByMachineIdPartial(InputGetMTBFChartReportByMachineId input,
            string startDate,
            string endDate, DatePeriodType datePeriodType)
        {
            const string partialViewUrl =
                "~/Views/MaintenanceGroupMttrAndMtbfReport/DetailRow/ShowMtbfChartReportByMachineIdPartial.cshtml";
            (input.StartDate, input.EndDate) = Tools.NormalizeDates(startDate, endDate, datePeriodType);

            var mttrChartReportDataSource = ApiList.GetMtbfChartReportByMachineId(input);
            return PartialView(partialViewUrl, mttrChartReportDataSource);
        }

        public ActionResult ShowMttrAndMtbfMachineWorkOrderListPartial(
            InputGetMTTRAndMTBFMachineWorkOrderListByMachineId input, string startDate,
            string endDate, DatePeriodType datePeriodType)
        {
            const string partialViewUrl =
                "~/Views/MaintenanceGroupMttrAndMtbfReport/DetailRow/ShowMTTRAndMTBFMachineWorkOrderListPartial.cshtml";
            (input.StartDate, input.EndDate) = Tools.NormalizeDates(startDate, endDate, datePeriodType);
            var dataSource = ApiList.GetMttrAndMtbfMachineWorkOrderListByMachineId(input);

            return PartialView(partialViewUrl, dataSource);
        }

        public ActionResult ShowMttrAndMtbfPerformanceCostReportPartial(
            InputGetMTTRAndMTBFPerformanceCostReportByMachineId input, string startDate,
            string endDate, DatePeriodType datePeriodType)
        {
            const string partialViewUrl =
                "~/Views/MaintenanceGroupMttrAndMtbfReport/DetailRow/ShowMTTRAndMTBFPerformanceCostReportPartial.cshtml";
            (input.StartDate, input.EndDate) = Tools.NormalizeDates(startDate, endDate, datePeriodType);
            var dataSource = ApiList.GetMttrAndMtbfPerformanceCostReportByMachineId(input);

            return PartialView(partialViewUrl, dataSource);
        }

        public ActionResult ShowMttrAndMtbfPerformanceReportPartial(
            InputGetMTTRAndMTBFPerformanceReportByMachineId input, string startDate,
            string endDate, DatePeriodType datePeriodType)
        {
            const string partialViewUrl =
                "~/Views/MaintenanceGroupMttrAndMtbfReport/DetailRow/ShowMTTRAndMTBFPerformanceReportPartial.cshtml";
            (input.StartDate, input.EndDate) = Tools.NormalizeDates(startDate, endDate, datePeriodType);
            var dataSource = ApiList.GetMttrAndMtbfPerformanceReportByMachineId(input);

            return PartialView(partialViewUrl, dataSource);
        }

        public ActionResult ShowMttrAndMtbfWearhouseReport(
            InputGetMTTRAndMTBFWearhouseReportByMachineId input, string startDate,
            string endDate, DatePeriodType datePeriodType)
        {
            const string partialViewUrl =
                "~/Views/MaintenanceGroupMttrAndMtbfReport/DetailRow/ShowMTTRAndMTBFWearhouseReport.cshtml";
            (input.StartDate, input.EndDate) = Tools.NormalizeDates(startDate, endDate, datePeriodType);
            var dataSource = ApiList.GetMttrAndMtbfWearhouseReportByMachineId(input);

            return PartialView(partialViewUrl, dataSource);
        }

        public ActionResult ShowMttrChartReportByMachineIdPartial(InputGetMTTRChartReportByMachineId input,
            string startDate,
            string endDate, DatePeriodType datePeriodType)
        {
            const string partialViewUrl =
                "~/Views/MaintenanceGroupMttrAndMtbfReport/DetailRow/ShowMttrChartReportByMachineIdPartial.cshtml";
            (input.StartDate, input.EndDate) = Tools.NormalizeDates(startDate, endDate, datePeriodType);

            var mttrChartReportDataSource = ApiList.GetMttrChartReportByMachineId(input);
            return PartialView(partialViewUrl, mttrChartReportDataSource);
        }

        public ActionResult ShowOeeChartReportOnMTTRAndMTBFByMachineIdPartial(
            InputGetOeeChartReportOnMTTRAndMTBFByMachineId input, string startDate,
            string endDate, DatePeriodType datePeriodType)
        {
            const string partialViewUrl =
                "~/Views/MaintenanceGroupMttrAndMtbfReport/DetailRow/ShowOeeChartReportOnMTTRAndMTBFByMachineIdPartial.cshtml";
            (input.StartDate, input.EndDate) = Tools.NormalizeDates(startDate, endDate, datePeriodType);

            var mttrChartReportDataSource = ApiList.GetOeeChartReportOnMTTRAndMTBFByMachineId(input);
            return PartialView(partialViewUrl, mttrChartReportDataSource);
        }

        public ActionResult WorkOrderGridDetailRow()
        {
            const string partialViewUrl =
                "~/Views/MaintenanceGroupMttrAndMtbfReport/WorkOrderDetail/WorkOrderDetailRow.cshtml";

            return PartialView(partialViewUrl);
        }

        #region workOrderDetailRow

        public ActionResult RegistrarInfoModal(InputGetWorkOrderRegistrantInfoByWorkOrderId input)
        {
            const string partialViewUrl =
                "~/Views/MaintenanceGroupMttrAndMtbfReport/WorkOrderDetail/RegistrarInfoModal.cshtml";

            var workOrderRegistrantInfoDataSource = ApiList.GetWorkOrderRegistrantInfoByWorkOrderId(input);

            return PartialView(partialViewUrl, workOrderRegistrantInfoDataSource);
        }

        public ActionResult PerformedOperationsGrid(InputGetActionOrDelayListByWorkOrderId input)
        {
            const string partialViewUrl =
                "~/Views/MaintenanceGroupMttrAndMtbfReport/WorkOrderDetail/PerformedOperationsGrid.cshtml";

            var token = GetUserToken();

            var workOrderActionOrDelayListDataSource = ApiList.GetActionOrDelayListByWorkOrderId(input, token);

            return PartialView(partialViewUrl, workOrderActionOrDelayListDataSource);
        }

        public ActionResult ConsumingMaterialsGrid(InputGetWorKOrderConsumableListByWorkOrderId input)
        {
            const string partialViewUrl =
                "~/Views/MaintenanceGroupMttrAndMtbfReport/WorkOrderDetail/ConsumingMaterialsGrid.cshtml";

            var token = GetUserToken();

            var worKOrderConsumableListDataSource = ApiList.GetWorKOrderConsumableListByWorkOrderId(input, token);

            return PartialView(partialViewUrl, worKOrderConsumableListDataSource);
        }

        public ActionResult ReferralsHistoryGrid(InputGetWorKOrderReferralListByWorkOrderId input)
        {
            const string partialViewUrl =
                "~/Views/MaintenanceGroupMttrAndMtbfReport/WorkOrderDetail/ReferralsHistoryGrid.cshtml";

            var token = GetUserToken();

            var workOrderReferralListDataSource = ApiList.GetWorKOrderReferralListByWorkOrderId(input, token);

            return PartialView(partialViewUrl, workOrderReferralListDataSource);
        }

        #endregion
    }
}