using System.Web.Http;
using Motorsazan.CMMS.Api.Business;
using Motorsazan.CMMS.Api.Filters;
using Motorsazan.CMMS.Shared.Models.Input.OEEReport;
using Motorsazan.CMMS.Shared.Models.Output.OEEReport;

namespace Motorsazan.CMMS.Api.Controllers
{
    [RoutePrefix("OEEReport")]
    public class OEEReportController: ApiController
    {
        private readonly BusinessManager _businessManager = new BusinessManager();

        /// <summary>
        ///     دریافت گزارش او او ای بر اساس شناسه دپارتمان
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetDepartmentOEEReportByCondition")]
        [HttpPost]
        public IHttpActionResult GetDepartmentOEEReportByCondition(
            InputGetDepartmentOEEReportByCondition input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetDepartmentOEEReportByCondition]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetDepartmentOEEReportByCondition,
                        OutputGetOEEReportByCondition[]>(
                        storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        ///     دریافت گزارش او او ای بر اساس شناسه ماشین
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetMachineOEEReportByCondition")]
        [HttpPost]
        public IHttpActionResult GetMachineOEEReportByCondition(
            InputGetMachineOEEReportByCondition input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetMachineOEEReportByCondition]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetMachineOEEReportByCondition,
                        OutputGetOEEReportByCondition[]>(
                        storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        ///     دریافت گزارش او او ای بر اساس شرایط
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetOEEReportByCondition")]
        [HttpPost]
        public IHttpActionResult GetOEEReportByCondition(
            InputGetOEEReportByCondition input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetOEEReportByCondition]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetOEEReportByCondition,
                        OutputGetOEEReportByCondition[]>(
                        storedProcedureName, input);

            return Ok(result);
        }
    }
}