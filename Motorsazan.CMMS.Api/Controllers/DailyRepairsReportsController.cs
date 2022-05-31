using System.Web.Http;
using Motorsazan.CMMS.Api.Business;
using Motorsazan.CMMS.Api.Filters;
using Motorsazan.CMMS.Shared.Models.Input.DailyRepairsReports;
using Motorsazan.CMMS.Shared.Models.Output.DailyRepairsReports;

namespace Motorsazan.CMMS.Api.Controllers
{
    [RoutePrefix("DailyRepairsReports")]
    public class DailyRepairsReportsController: ApiController
    {
        private readonly BusinessManager _businessManager = new BusinessManager();

        /// <summary>
        ///     دریافت لیست گزارش روزانه تعمیرات براساس شرایط
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetMaintenanceDailyReportByCondition")]
        [HttpPost]
        public IHttpActionResult GetMaintenanceDailyReportByCondition(
            InputGetMaintenanceDailyReportByCondition input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetMaintenanceDailyReportByCondition]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetMaintenanceDailyReportByCondition,
                        OutputGetMaintenanceDailyReportByCondition[]>(
                        storedProcedureName, input);

            return Ok(result);
        }
    }
}