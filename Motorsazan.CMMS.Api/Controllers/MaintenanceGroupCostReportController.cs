using System.Web.Http;
using Motorsazan.CMMS.Api.Business;
using Motorsazan.CMMS.Api.Filters;
using Motorsazan.CMMS.Shared.Models.Input.MaintenanceGroupCostReport;
using Motorsazan.CMMS.Shared.Models.Output.MaintenanceGroupCostReport;

namespace Motorsazan.CMMS.Api.Controllers
{
    [RoutePrefix("MaintenanceGroupCostReport")]
    public class MaintenanceGroupCostReportController: ApiController
    {
        private readonly BusinessManager _businessManager = new BusinessManager();

        /// <summary>
        ///     دریافت لیست گزارش عملکرد به تفکیک دپارتمان براساس شرایط
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetMaintenanceGroupCostReportByCondition")]
        [HttpPost]
        public IHttpActionResult GetMaintenanceGroupCostReportByCondition(
            InputGetMaintenanceGroupCostReportByCondition input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetMaintenanceGroupCostReportByCondition]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetMaintenanceGroupCostReportByCondition,
                        OutputGetMaintenanceGroupCostReportByCondition[]>(
                        storedProcedureName, input);

            return Ok(result);
        }
    }
}