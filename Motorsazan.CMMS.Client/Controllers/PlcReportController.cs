using Motorsazan.CMMS.Client.Api;
using Motorsazan.CMMS.Client.Filters;
using Motorsazan.CMMS.Shared.Enums;
using Motorsazan.CMMS.Shared.Models.Input.PLCReport;
using Motorsazan.CMMS.Shared.Utilities;
using System.Web.Mvc;

namespace Motorsazan.CMMS.Client.Controllers
{
    public class PLCReportController: BaseController
    {
        public ActionResult AccessHourToMachineForm(InputGetPlcMachineAvailabilityByMachineId input,
            string persianStartDate, string persianEndDate, DatePeriodType datePeriodType, string machineName)
        {
            ViewData["MachineName"] = machineName;

            const string partialViewUrl = "~/Views/PLCReport/Grid/AccessHourToMachineForm.cshtml";

            var model = new {input, persianStartDate, persianEndDate, datePeriodType};

            return PartialView(partialViewUrl, model);
        }

        public ActionResult AccessHourToMachineGrid(InputGetPlcMachineAvailabilityByMachineId input,
            string persianStartDate, string persianEndDate, DatePeriodType datePeriodType)
        {
            const string partialViewUrl = "~/Views/PLCReport/Grid/AccessHourToMachineGrid.cshtml";

            (input.StartDate, input.EndDate) = Tools.NormalizeDates(persianStartDate, persianEndDate, datePeriodType);

            var apiResult = ApiList.GetPLCMachineAvailabilityByMachineId(input);

            return PartialView(partialViewUrl, apiResult);
        }

        public ActionResult ComparisonBetweenToolsAndPalletChangerChartReport(
            InputGetPlcMachineComparisonBetweenToolsAndPalletChangerChartReportByMachineId input,
            string persianStartDate, string persianEndDate, DatePeriodType datePeriodType, string machineName,
            string oldMachineCode)
        {
            const string partialViewUrl =
                "~/Views/PLCReport/ReportChart/ComparisonBetweenToolsAndPalletChangerChartReport.cshtml";

            (input.StartDate, input.EndDate) = Tools.NormalizeDates(persianStartDate, persianEndDate, datePeriodType);

            ViewData["StartDate"] = input.StartDate.Date.ToShamsiDateString();
            ViewData["EndDate"] = input.EndDate.Date.ToShamsiDateString();
            ViewData["MachineName"] = machineName;
            ViewData["OldMachineCode"] = oldMachineCode;

            var apiResult = ApiList.GetPlcMachineComparisonBetweenToolsAndPalletChangerChartReportByMachineId(input);

            return PartialView(partialViewUrl, apiResult);
        }

        public ActionResult FilterFormMachineCombo()
        {
            const string partialViewUrl = "~/Views/PLCReport/FilterForm/FilterFormMachineIdCombo.cshtml";

            var machinePlcFileList = ApiList.GetMachinePLCFileList();

            return PartialView(partialViewUrl, machinePlcFileList);
        }

        [AccessToFormValidation(FormCode = "016")]
        public ActionResult Index() => View();

        public ActionResult MachineOilPressureChartReport(InputGetPlcMachineOilPressureChartReportByMachineId input,
            string persianStartDate, string persianEndDate, DatePeriodType datePeriodType, string machineName,
            string oldMachineCode)
        {
            const string partialViewUrl = "~/Views/PLCReport/ReportChart/MachineOilPressureChartReport.cshtml";

            (input.StartDate, input.EndDate) = Tools.NormalizeDates(persianStartDate, persianEndDate, datePeriodType);

            ViewData["StartDate"] = input.StartDate.Date.ToShamsiDateString();
            ViewData["EndDate"] = input.EndDate.Date.ToShamsiDateString();
            ViewData["MachineName"] = machineName;
            ViewData["OldMachineCode"] = oldMachineCode;

            var apiResult = ApiList.GetPlcMachineOilPressureChartReportByMachineId(input);

            return PartialView(partialViewUrl, apiResult);
        }

        public ActionResult MachineOilTemperatureChartReport(
            InputGetPlcMachineOilTemperatureChartReportByMachineId input,
            string persianStartDate, string persianEndDate, DatePeriodType datePeriodType, string machineName,
            string oldMachineCode)
        {
            const string partialViewUrl = "~/Views/PLCReport/ReportChart/MachineOilTemperatureChartReport.cshtml";

            (input.StartDate, input.EndDate) = Tools.NormalizeDates(persianStartDate, persianEndDate, datePeriodType);

            ViewData["StartDate"] = input.StartDate.Date.ToShamsiDateString();
            ViewData["EndDate"] = input.EndDate.Date.ToShamsiDateString();
            ViewData["MachineName"] = machineName;
            ViewData["OldMachineCode"] = oldMachineCode;

            var apiResult = ApiList.GetPlcMachineOilTemperatureChartReportByMachineId(input);

            return PartialView(partialViewUrl, apiResult);
        }

        public ActionResult MachinePowerChangerChartReport(InputGetPlcMachinePowerChangerChartReportByMachineId input,
            string persianStartDate, string persianEndDate, DatePeriodType datePeriodType, string machineName,
            string oldMachineCode)
        {
            const string partialViewUrl = "~/Views/PLCReport/ReportChart/MachinePowerChangerChartReport.cshtml";

            (input.StartDate, input.EndDate) = Tools.NormalizeDates(persianStartDate, persianEndDate, datePeriodType);

            ViewData["StartDate"] = input.StartDate.Date.ToShamsiDateString();
            ViewData["EndDate"] = input.EndDate.Date.ToShamsiDateString();
            ViewData["MachineName"] = machineName;
            ViewData["OldMachineCode"] = oldMachineCode;

            var apiResult = ApiList.GetPlcMachinePowerChangerChartReportByMachineId(input);

            return PartialView(partialViewUrl, apiResult);
        }

        public ActionResult PalletChangeCountPLCReportChart(
            InputGetPlcMachinePalletChangerCountChartReportByMachineId input,
            string persianStartDate, string persianEndDate, DatePeriodType datePeriodType, string machineName,
            string oldMachineCode)
        {
            const string partialViewUrl = "~/Views/PLCReport/ReportChart/PalletChangeCountPLCReportChart.cshtml";

            (input.StartDate, input.EndDate) = Tools.NormalizeDates(persianStartDate, persianEndDate, datePeriodType);

            ViewData["StartDate"] = input.StartDate.Date.ToShamsiDateString();
            ViewData["EndDate"] = input.EndDate.Date.ToShamsiDateString();
            ViewData["MachineName"] = machineName;
            ViewData["OldMachineCode"] = oldMachineCode;

            var apiResult = ApiList.GetPlcMachinePalletChangerCountChartReportByMachineId(input);

            return PartialView(partialViewUrl, apiResult);
        }

        public ActionResult PLCMachineSpindlePerformanceReportDetailForm(InputGetToolsChangerDetailOnSpindle input,
            string machineName, string oldMachineCode)
        {
            ViewData["MachineName"] = machineName;
            ViewData["OldMachineCode"] = oldMachineCode;
            ViewData["PalletChanger"] = input.PalletChanger;
            ViewData["CreateDate"] = input.CreateDate;
            ViewData["MachinePLCFileDataId"] = input.MachinePLCFileDataId;

            const string partialViewUrl = "~/Views/PLCReport/Grid/PLCMachineSpindlePerformanceReportDetailForm.cshtml";

            var model = new {input};

            return PartialView(partialViewUrl, model);
        }

        public ActionResult PLCMachineSpindlePerformanceReportDetailGrid(InputGetToolsChangerDetailOnSpindle input)
        {
            const string partialViewUrl = "~/Views/PLCReport/Grid/PLCMachineSpindlePerformanceReportDetailGrid.cshtml";

            var apiResult = ApiList.GetToolsChangerDetailOnSpindle(input);

            return PartialView(partialViewUrl, apiResult);
        }


        public ActionResult PLCMachineSpindlePerformanceReportForm(
            InputGetPlcMachineSpindlePerformanceReportByMachineId input,
            string persianStartDate, string persianEndDate, DatePeriodType datePeriodType, string machineName,
            string oldMachineCode)
        {
            ViewData["MachineName"] = machineName;
            ViewData["OldMachineCode"] = oldMachineCode;

            const string partialViewUrl = "~/Views/PLCReport/Grid/PLCMachineSpindlePerformanceReportForm.cshtml";

            var model = new {input, persianStartDate, persianEndDate, datePeriodType};

            return PartialView(partialViewUrl, model);
        }

        public ActionResult PLCMachineSpindlePerformanceReportGrid(
            InputGetPlcMachineSpindlePerformanceReportByMachineId input,
            string persianStartDate, string persianEndDate, DatePeriodType datePeriodType)
        {
            const string partialViewUrl = "~/Views/PLCReport/Grid/PLCMachineSpindlePerformanceReportGrid.cshtml";

            if(input.MachineId == -1)
            {
                return PartialView(partialViewUrl);
            }

            (input.StartDate, input.EndDate) = Tools.NormalizeDates(persianStartDate, persianEndDate, datePeriodType);

            var apiResult = ApiList.GetPlcMachineSpindlePerformanceReportByMachineId(input);

            return PartialView(partialViewUrl, apiResult);
        }

        public ActionResult ToolsChangeRatePLCReportChart(InputGetPlcMachineToolsChangerChartReportByMachineId input,
            string persianStartDate, string persianEndDate, DatePeriodType datePeriodType, string machineName,
            string oldMachineCode)
        {
            const string partialViewUrl = "~/Views/PLCReport/ReportChart/ToolsChangeRatePLCReportChart.cshtml";

            (input.StartDate, input.EndDate) = Tools.NormalizeDates(persianStartDate, persianEndDate, datePeriodType);

            ViewData["StartDate"] = input.StartDate.Date.ToShamsiDateString();
            ViewData["EndDate"] = input.EndDate.Date.ToShamsiDateString();
            ViewData["MachineName"] = machineName;
            ViewData["OldMachineCode"] = oldMachineCode;

            var apiResult = ApiList.GetPlcMachineToolsChangerChartReportByMachineId(input);

            return PartialView(partialViewUrl, apiResult);
        }


        public ActionResult ResetMachinePlcFileDataTransferJob()

        {
            var apiResult = ApiList.ResetMachinePlcFileDataTransferJob();
            return Content(apiResult);
        }


        public ActionResult ChangeJobStatus(InputChangeJobStatus input)

        {
            //  var DbChangeResult = ApiList.ResetMachinePlcFileDataTransferJob();


            var apiResult = ApiList.ChangeJobStatusAsync(input);

            if(apiResult.JobStatus.ToLower() == "started" || apiResult.JobStatus.ToLower() == "stopped")
            {
                apiResult.IsSuccess = true;
                apiResult.JobStatus = "واکشی اطلاعات با موفقیت انجام شد";
            }

            return Json(apiResult);
        }
    }
}