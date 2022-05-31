using Motorsazan.CMMS.Api.Business;
using Motorsazan.CMMS.Api.Filters;
using Motorsazan.CMMS.Shared.Models.Input.PreventiveMaintenancesSchedulingReport;
using Motorsazan.CMMS.Shared.Models.Output.PreventiveMaintenancesSchedulingReport;
using System.Web.Http;

namespace Motorsazan.CMMS.Api.Controllers
{
    [RoutePrefix("PreventiveMaintenancesSchedulingReport")]
    public class PreventiveMaintenancesSchedulingReportController: ApiController
    {
        private readonly BusinessManager _businessManager = new BusinessManager();

        /// <summary>
        ///دریافت گزارش سفارشکار پیشگیرانه بر اساس شرایط 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetPreventiveMaintenanceSchedulingReportByCondition")]
        [HttpPost]
        public IHttpActionResult GetPreventiveMaintenanceSchedulingReportByCondition(
            InputGetPreventiveMaintenanceSchedulingReportByCondition input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetPreventiveMaintenanceSchedulingReportByCondition]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetPreventiveMaintenanceSchedulingReportByCondition,
                        OutputGetPreventiveMaintenanceSchedulingReportByCondition[]>(
                        storedProcedureName, input);

            return Ok(result);
        }


    }
}
