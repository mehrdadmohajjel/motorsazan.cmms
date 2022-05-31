using Motorsazan.CMMS.Api.Business;
using Motorsazan.CMMS.Api.Filters;
using Motorsazan.CMMS.Shared.Models.Input.PLCReport;
using Motorsazan.CMMS.Shared.Models.Output.PLCReport;
using System.Web.Http;

namespace Motorsazan.CMMS.Api.Controllers
{
    [RoutePrefix("PLCReport")]
    public class PLCReportController: ApiController
    {
        private readonly BusinessManager _businessManager = new BusinessManager();

        /// <summary>
        ///     دریافت لیست ماشین های پی ال سی
        /// </summary>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetMachinePLCFileList")]
        [HttpPost]
        public IHttpActionResult GetMachinePLCFileList()
        {
            const string storedProcedureName = "[CMMS].[prc_GetMachinePLCFileList]";

            var result =
                _businessManager
                    .CallStoredProcedure<OutputGetMachinePLCFileList[]>(
                        storedProcedureName);

            return Ok(result);
        }

        /// <summary>
        ///     دریافت گزارش اطلاعات دسترسی دستگاه پی ال سی
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetPLCMachineAvailabilityByMachineId")]
        [HttpPost]
        public IHttpActionResult GetPLCMachineAvailabilityByMachineId(InputGetPlcMachineAvailabilityByMachineId input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetPlcMachineAvailabilityByMachineID]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetPlcMachineAvailabilityByMachineId,
                        OutputGetPlcMachineAvailabilityByMachineId[]>(
                        storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        ///     دریافت گزارش مقایسه تعویض پالت و ابزار دستگاه پی ال سی بر اساس شناسه ماشین
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetPlcMachineComparisonBetweenToolsAndPalletChangerChartReportByMachineId")]
        [HttpPost]
        public IHttpActionResult GetPlcMachineComparisonBetweenToolsAndPalletChangerChartReportByMachineId(
            InputGetPlcMachineComparisonBetweenToolsAndPalletChangerChartReportByMachineId input)
        {
            const string storedProcedureName =
                "[CMMS].[prc_GetPlcMachineComparisonBetweenToolsAndPalletChangerChartReportByMachineID]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetPlcMachineComparisonBetweenToolsAndPalletChangerChartReportByMachineId,
                        OutputGetPlcMachineComparisonBetweenToolsAndPalletChangerChartReportByMachineId[]>(
                        storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        ///     دریافت گزارش تغییر فشار دستگاه پی ال سی بر اساس شناسه ماشین
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetPlcMachineOilPressureChartReportByMachineId")]
        [HttpPost]
        public IHttpActionResult GetPLCMachineOilPressureChartReportByMachineId(
            InputGetPlcMachineOilPressureChartReportByMachineId input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetPLCMachineOilPressureChartReportByMachineID]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetPlcMachineOilPressureChartReportByMachineId,
                        OutputGetPlcMachineOilPressureChartReportByMachineId[]>(
                        storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        ///     دریافت گزارش تغییر دمای روغن دستگاه پی ال سی بر اساس شناسه ماشین
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetPlcMachineOilTemperatureChartReportByMachineId")]
        [HttpPost]
        public IHttpActionResult GetPlcMachineOilTemperatureChartReportByMachineId(
            InputGetPlcMachineOilTemperatureChartReportByMachineId input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetPlcMachineOilTemperatureChartReportByMachineID]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetPlcMachineOilTemperatureChartReportByMachineId,
                        OutputGetPlcMachineOilTemperatureChartReportByMachineId[]>(
                        storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        ///     دریافت گزارش تعویض پالت دستگاه پی ال سی بر اساس شناسه ماشین
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetPlcMachinePalletChangerCountChartReportByMachineId")]
        [HttpPost]
        public IHttpActionResult GetPlcMachinePalletChangerCountChartReportByMachineId(
            InputGetPlcMachinePalletChangerCountChartReportByMachineId input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetPlcMachinePalletChangerCountChartReportByMachineID]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetPlcMachinePalletChangerCountChartReportByMachineId,
                        OutputGetPlcMachinePalletChangerCountChartReportByMachineId[]>(
                        storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        ///     دریافت گزارش جریان مصرفی دستگاه پی ال سی بر اساس شناسه ماشین
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetPlcMachinePowerChangerChartReportByMachineId")]
        [HttpPost]
        public IHttpActionResult GetPlcMachinePowerChangerChartReportByMachineId(
            InputGetPlcMachinePowerChangerChartReportByMachineId input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetPlcMachinePowerChangerChartReportByMachineID]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetPlcMachinePowerChangerChartReportByMachineId,
                        OutputGetPlcMachinePowerChangerChartReportByMachineId[]>(
                        storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        ///     دریافت گزارش عملکرد اسپیندل دستگاه پی ال سی بر اساس شناسه ماشین
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetPlcMachineSpindlePerformanceReportByMachineId")]
        [HttpPost]
        public IHttpActionResult GetPLCMachineSpindlePerformanceReportByMachineId(
            InputGetPlcMachineSpindlePerformanceReportByMachineId input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetPLCMachineSpindlePerformanceReportByMachineID]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetPlcMachineSpindlePerformanceReportByMachineId,
                        OutputGetPlcMachineSpindlePerformanceReportByMachineId[]>(
                        storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        ///     دریافت گزارش تعویض ابزار دستگاه پی ال سی بر اساس شناسه ماشین
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetPlcMachineToolsChangerChartReportByMachineId")]
        [HttpPost]
        public IHttpActionResult GetPlcMachineToolsChangerChartReportByMachineId(
            InputGetPlcMachineToolsChangerChartReportByMachineId input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetPlcMachineToolsChangerChartReportByMachineID]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetPlcMachineToolsChangerChartReportByMachineId,
                        OutputGetPlcMachineToolsChangerChartReportByMachineId[]>(
                        storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        ///     دریافت گزارش عملکرد اسپیندل دستگاه پی ال سی بر اساس شناسه ماشین
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetToolsChangerDetailOnSpindle")]
        [HttpPost]
        public IHttpActionResult GetToolsChangerDetailOnSpindle(
            InputGetToolsChangerDetailOnSpindle input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetToolsChangerDetailOnSpindle]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetToolsChangerDetailOnSpindle,
                        OutputGetToolsChangerDetailOnSpindle[]>(
                        storedProcedureName, input);

            return Ok(result);
        }


        /// <summary>
        ///     ثبت جاب های مربوط به انتقال دیتای پی ال سی ها
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ResetMachinePlcFileDataTransferJob")]
        [RequestModelNullValidation]
        [RequestModelValidation]
        public IHttpActionResult ResetMachinePlcFileDataTransferJob()
        {
            const string storedProcedureName = "[CMMS].[prc_ResetMachinePlcFileDataTransferJob]";
            var input = new
                InputResetMachinePlcFileDataTransferJob {Type = 1};
            var errorMessage = _businessManager.CallStoredProcedureAndReturnMessageIfExits(storedProcedureName, input);

            return string.IsNullOrEmpty(errorMessage)
                ? Ok("با موفقیت انجام شد")
                : (IHttpActionResult)BadRequest(errorMessage);
        }
    }
}