using Motorsazan.CMMS.Api.Business;
using Motorsazan.CMMS.Api.Filters;
using Motorsazan.CMMS.Shared.Models.Input.MachineMttrMtbfReport;
using Motorsazan.CMMS.Shared.Models.Output.MachineMttrMtbfReport;
using System.Web.Http;

namespace Motorsazan.CMMS.Api.Controllers
{
    [RoutePrefix("MachineMttrMtbfReport")]
    public class MachineMttrMtbfReportController: ApiController
    {
        private readonly BusinessManager _businessManager = new BusinessManager();

        /// <summary>
        ///     دریافت لیست گزارش ام تی تی آر / ام تی بی اف بر اساس ماشین
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetMachineMttrAndMtbfReportByCondition")]
        [HttpPost]
        public IHttpActionResult GetDepartmentMttrAndMtbfReportByCondition(
            InputGetMachineMTTRAndMTBFReportByCondition input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetMachineMTTRAndMTBFReportByCondition]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetMachineMTTRAndMTBFReportByCondition,
                        OutputGetMachineMTTRAndMTBFReportByCondition[]>(
                        storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        ///     دریافت ام تی تی آر کلی/ ام تی بی اف کلی و اِی ای کلی بر اساس شناسه ماشین
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetTotalMttrAndMtbfByMachineId")]
        [HttpPost]
        public IHttpActionResult GetTotalMttrAndMtbfByMachineId(
            InputGetTotalMttrAndMtbfByMachineId input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetTotalMTTRAndMTBFByMachineID]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetTotalMttrAndMtbfByMachineId,
                        OutputGetTotalMttrAndMtbfByMachineId[]>(
                        storedProcedureName, input);

            return Ok(result);
        }
    }
}