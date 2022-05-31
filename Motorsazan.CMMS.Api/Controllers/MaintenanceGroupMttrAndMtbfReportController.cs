using Motorsazan.CMMS.Api.Business;
using Motorsazan.CMMS.Api.Filters;
using Motorsazan.CMMS.Shared.Models.Input.MaintenanceGroupMttrAndMtbfReport;
using Motorsazan.CMMS.Shared.Models.Output.MaintenanceGroupMttrAndMtbfReport;
using System.Web.Http;

namespace Motorsazan.CMMS.Api.Controllers
{
    [RoutePrefix("MaintenanceGroupMttrAndMtbfReport")]
    public class MaintenanceGroupMttrAndMtbfReportController: ApiController
    {
        private readonly BusinessManager _businessManager = new BusinessManager();

        /// <summary>
        ///     دریافت لیست گزارش ام تی تی آر / ام تی بی اف بر اساس امور و گروه تعمیراتی
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetMaintenanceGroupMttrAndMtbfReportByCondition")]
        [HttpPost]
        public IHttpActionResult GetMaintenanceGroupMttrAndMtbfReportByCondition(
            InputGetMaintenanceGroupMttrAndMtbfReportByCondition input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetMaintenanceGroupMTTRAndMTBFReportByCondition]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetMaintenanceGroupMttrAndMtbfReportByCondition,
                        OutputGetMaintenanceGroupMttrAndMtbfReportByCondition[]>(
                        storedProcedureName, input);

            return Ok(result);
        }

    }
}