using System.Web.Http;
using Motorsazan.CMMS.Api.Business;
using Motorsazan.CMMS.Api.Filters;
using Motorsazan.CMMS.Shared.Models.Input.OCEReport;
using Motorsazan.CMMS.Shared.Models.Output.OCEReport;

namespace Motorsazan.CMMS.Api.Controllers
{
    [RoutePrefix("OCEReport")]
    public class OCEReportController: ApiController
    {
        private readonly BusinessManager _businessManager = new BusinessManager();

        /// <summary>
        ///     دریافت  گزارش او سی ای براساس شرایط
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetOCEReportByCondition")]
        [HttpPost]
        public IHttpActionResult GetOCEReportByCondition(
            InputGetOCEReportByCondition input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetOCEReportByCondition]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetOCEReportByCondition,
                        OutputGetOCEReportByCondition[]>(
                        storedProcedureName, input);

            return Ok(result);
        }
    }
}