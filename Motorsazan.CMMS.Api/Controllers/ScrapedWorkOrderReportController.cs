using Motorsazan.CMMS.Api.Business;
using Motorsazan.CMMS.Api.Filters;
using Motorsazan.CMMS.Shared.Models.Input.ScrapedWorkOrderReport;
using Motorsazan.CMMS.Shared.Models.Output.ScrapedWorkOrderReport;
using System.Web.Http;

namespace Motorsazan.CMMS.Api.Controllers
{
    [RoutePrefix("ScrapedWorkOrderReport")]
    public class ScrapedWorkOrderReportController: ApiController
    {
        private readonly BusinessManager _businessManager = new BusinessManager();

        /// <summary>
        ///گزارش سفارشکارهای دارای اسکرپ براساس شرایط 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetScrapedWorkOrderReportByCondition")]
        [HttpPost]
        public IHttpActionResult GetScrapedWorkOrderReportByCondition(
            InputGetScrapedWorkOrderReportByCondition input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetScrapedWorkOrderReportByCondition]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetScrapedWorkOrderReportByCondition,
                        OutputGetScrapedWorkOrderReportByCondition[]>(
                        storedProcedureName, input);

            return Ok(result);
        }


    }
}
