using Motorsazan.CMMS.Api.Business;
using Motorsazan.CMMS.Api.Filters;
using Motorsazan.CMMS.Shared.Models.Input.DepartmentMttrAndMtbfReport;
using Motorsazan.CMMS.Shared.Models.Output.DepartmentMttrAndMtbfReport;
using System.Web.Http;

namespace Motorsazan.CMMS.Api.Controllers
{
    [RoutePrefix("DepartmentMttrAndMtbfReport")]
    public class DepartmentMttrAndMtbfReportController: ApiController
    {
        private readonly BusinessManager _businessManager = new BusinessManager();

        /// <summary>
        ///     دریافت لیست گزارش ام تی تی آر / ام تی بی اف بر اساس امور
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetDepartmentMttrAndMtbfReportByCondition")]
        [HttpPost]
        public IHttpActionResult GetDepartmentMttrAndMtbfReportByCondition(
            InputGetMTTRAndMTBFReportByCondition input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetDepartmentMTTRAndMTBFReportByCondition]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetMTTRAndMTBFReportByCondition,
                        OutputGetDepartmentMttrAndMtbfReportByCondition[]>(
                        storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        ///     دریافت لیست گزارش  هزینه کارکرد ام تی تی آر / ام تی بی اف بر اساس شناسه ماشین
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetMttrAndMtbfPerformanceCostReportByMachineId")]
        [HttpPost]
        public IHttpActionResult GetMttrAndMtbfPerformanceCostReportByMachineId(
            InputGetMTTRAndMTBFPerformanceCostReportByMachineId input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetMTTRAndMTBFPerformanceCostReportByMachineID]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetMTTRAndMTBFPerformanceCostReportByMachineId,
                        OutputGetMTTRAndMTBFPerformanceCostReportByMachineId[]>(
                        storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        ///     دریافت لیست گزارش عملکرد ام تی تی آر / ام تی بی اف بر اساس شناسه ماشین
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetMttrAndMtbfPerformanceReportByMachineId")]
        [HttpPost]
        public IHttpActionResult GetMttrAndMtbfPerformanceReportByMachineId(
            InputGetMTTRAndMTBFPerformanceReportByMachineId input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetMTTRAndMTBFPerformanceReportByMachineID]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetMTTRAndMTBFPerformanceReportByMachineId,
                        OutputGetMTTRAndMTBFPerformanceReportByMachineId[]>(
                        storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        ///     دریافت لیست گزارش انبار ام تی تی آر / ام تی بی اف بر اساس شناسه ماشین
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetMttrAndMtbfWearhouseReportByMachineId")]
        [HttpPost]
        public IHttpActionResult GetMttrAndMtbfWearhouseReportByMachineId(
            InputGetMTTRAndMTBFWearhouseReportByMachineId input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetMTTRAndMTBFWearhouseReportByMachineID]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetMTTRAndMTBFWearhouseReportByMachineId,
                        OutputGetMTTRAndMTBFWearhouseReportByMachineId[]>(
                        storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        ///     دریافت لیست سفارشکارهای دستگاه ام تی تی آر / ام تی بی اف بر اساس شناسه ماشین
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetMttrAndMtbfMachineWorkOrderListByMachineId")]
        [HttpPost]
        public IHttpActionResult GetMttrAndMtbfMachineWorkOrderListByMachineId(
            InputGetMTTRAndMTBFMachineWorkOrderListByMachineId input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetMttrAndMtbfMachineWorkOrderListByMachineId]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetMTTRAndMTBFMachineWorkOrderListByMachineId,
                        OutputGetMTTRAndMTBFMachineWorkOrderListByMachineId[]>(
                        storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        ///     دریافت  گزارش نمودار ام تی تی آر بر اساس شناسه ماشین
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetMttrChartReportByMachineId")]
        [HttpPost]
        public IHttpActionResult GetMttrChartReportByMachineId(
            InputGetMTTRChartReportByMachineId input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetMTTRChartReportByMachineID]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetMTTRChartReportByMachineId,
                        OutputGetMTTRChartReportByMachineId[]>(
                        storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        ///     دریافت گزارش نمودار ام تی بی اف بر اساس شناسه ماشین
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetMtbfChartReportByMachineId")]
        [HttpPost]
        public IHttpActionResult GetMtbfChartReportByMachineId(
            InputGetMTBFChartReportByMachineId input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetMTBFChartReportByMachineID]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetMTBFChartReportByMachineId,
                        OutputGetMTBFChartReportByMachineId[]>(
                        storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        ///     دریافت  گزارش او ای ای بر اساس شناسه ماشین
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetOeeChartReportOnMTTRAndMTBFByMachineId")]
        [HttpPost]
        public IHttpActionResult GetOeeChartReportOnMTTRAndMTBFByMachineId(
            InputGetOeeChartReportOnMTTRAndMTBFByMachineId input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetOEEChartReportOnMTTRAndMTBFByMachineID]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetOeeChartReportOnMTTRAndMTBFByMachineId,
                        OutputGetOeeChartReportOnMTTRAndMTBFByMachineId[]>(
                        storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        ///     دریافت گزارش پرینت او ای ای روی ام تی تی آر / ام تی بی اف بر اساس شناسه ماشین
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetOEEPrintReportOnMTTRAndMTBFByMachineId")]
        [HttpPost]
        public IHttpActionResult GetOEEPrintReportOnMTTRAndMTBFByMachineId(
            InputGetOEEPrintReportOnMTTRAndMTBFByMachineId input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetOEEPrintReportOnMTTRAndMTBFByMachineID]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetOEEPrintReportOnMTTRAndMTBFByMachineId,
                        OutputGetOEEPrintReportOnMTTRAndMTBFByMachineId>(
                        storedProcedureName, input);

            return Ok(result);
        }


        /// <summary>
        ///     دریافت لیست گزارش تعداد کد خطا ام تی تی آر / ام تی بی اف بر اساس شناسه ماشین
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetMttrAndMtbfFaultCodeCountReportByMachineId")]
        [HttpPost]
        public IHttpActionResult GetMttrAndMtbfFaultCodeCountReportByMachineId(
            InputGetMTTRAndMTBFFaultCodeCountReportByMachineId input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetMTTRAndMTBFFaultCodeCountReportByMachineID]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetMTTRAndMTBFFaultCodeCountReportByMachineId,
                        OutputGetMTTRAndMTBFFaultCodeCountReportByMachineId[]>(
                        storedProcedureName, input);

            return Ok(result);
        }
    }
}