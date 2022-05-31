using System.Web.Http;
using Motorsazan.CMMS.Api.Business;
using Motorsazan.CMMS.Api.Filters;
using Motorsazan.CMMS.Shared.Models.Input.PerformanceReportByMachine;
using Motorsazan.CMMS.Shared.Models.Output.PerformanceReportByMachine;

namespace Motorsazan.CMMS.Api.Controllers
{
    [RoutePrefix("PerformanceReportByMachine")]
    public class PerformanceReportByMachineController: ApiController
    {
        private readonly BusinessManager _businessManager = new BusinessManager();

        /// <summary>
        ///     دریافت لیست گزارش عملکرد به تفکیک ماشین اصلی براساس شرایط
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetMainMachinePerformanceReportByCondition")]
        [HttpPost]
        public IHttpActionResult GetMainMachinePerformanceReportByCondition(
            InputGetMainMachinePerformanceReportByCondition input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetMainMachinePerformanceReportByCondition]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetMainMachinePerformanceReportByCondition,
                        OutputGetMainMachinePerformanceReportByCondition[]>(
                        storedProcedureName, input);

            return Ok(result);
        }
    }
}