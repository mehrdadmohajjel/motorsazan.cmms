using Motorsazan.CMMS.Api.Business;
using Motorsazan.CMMS.Api.Filters;
using Motorsazan.CMMS.Shared.Models.Input.SafetyAndHealthReport;
using Motorsazan.CMMS.Shared.Models.Output.SafetyAndHealthReport;
using System.Web.Http;

namespace Motorsazan.CMMS.Api.Controllers
{
    [RoutePrefix("SafetyAndHealthReport")]
    public class SafetyAndHealthReportController: ApiController
    {
        private readonly BusinessManager _businessManager = new BusinessManager();

        /// <summary>
        ///دریافت گزارش تاخیر در سفارشکار بر اساس شرایط 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetSafetyAndHealthReportByCondition")]
        [HttpPost]
        public IHttpActionResult GetSafetyAndHealthReportByCondition(
            InputGetSafetyAndHealthReportByCondition input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetSafetyAndHealthReportByCondition]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetSafetyAndHealthReportByCondition,
                        OutputGetSafetyAndHealthReportByCondition[]>(
                        storedProcedureName, input);

            return Ok(result);
        }


    }
}
