using System.Web.Http;
using Motorsazan.CMMS.Api.Business;
using Motorsazan.CMMS.Api.Filters;
using Motorsazan.CMMS.Shared.Models.Input.WarehouseDailyReports;
using Motorsazan.CMMS.Shared.Models.Output.WarehouseDailyReports;

namespace Motorsazan.CMMS.Api.Controllers
{
    [RoutePrefix("WarehouseDailyReports")]
    public class WarehouseDailyReportsController: ApiController
    {
        private readonly BusinessManager _businessManager = new BusinessManager();

        /// <summary>
        ///     دریافت گزارش روزانه انبار
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetDailyWearhouseReportByCondition")]
        [HttpPost]
        public IHttpActionResult GetDailyWearhouseReportByCondition(
            InputGetDailyWearhouseReportByCondition input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetDailyWearhouseReportByCondition]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetDailyWearhouseReportByCondition,
                        OutputGetDailyWearhouseReportByCondition[]>(
                        storedProcedureName, input);

            return Ok(result);
        }
    }
}