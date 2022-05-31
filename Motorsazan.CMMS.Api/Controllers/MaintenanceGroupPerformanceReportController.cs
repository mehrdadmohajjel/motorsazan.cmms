using System.Web.Http;
using Motorsazan.CMMS.Api.Business;
using Motorsazan.CMMS.Api.Filters;
using Motorsazan.CMMS.Shared.Models.Input.MaintenanceGroupPerformanceReport;
using Motorsazan.CMMS.Shared.Models.Output.MaintenanceGroupPerformanceReport;

namespace Motorsazan.CMMS.Api.Controllers
{
    [RoutePrefix("MaintenanceGroupPerformanceReport")]
    public class MaintenanceGroupPerformanceReportController: ApiController
    {
        private readonly BusinessManager _businessManager = new BusinessManager();

        /// <summary>
        ///     دریافت لیست گزارش عملکرد گروه های تعمیراتی براساس شرایط
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetMaintenanceGroupPerformanceReportByCondition")]
        [HttpPost]
        public IHttpActionResult GetMaintenanceGroupPerformanceReportByCondition(
            InputGetMaintenanceGroupPerformanceReportByCondition input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetMaintenanceGroupPerformanceReportByCondition]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetMaintenanceGroupPerformanceReportByCondition,
                        OutputGetMaintenanceGroupPerformanceReportByCondition[]>(
                        storedProcedureName, input);

            return Ok(result);
        }
    }
}