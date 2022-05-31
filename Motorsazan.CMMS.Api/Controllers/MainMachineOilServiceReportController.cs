using System.Web.Http;
using Motorsazan.CMMS.Api.Business;
using Motorsazan.CMMS.Api.Filters;
using Motorsazan.CMMS.Shared.Models.Input.MainMachineOilServiceReport;
using Motorsazan.CMMS.Shared.Models.Output.MainMachineOilServiceReport;

namespace Motorsazan.CMMS.Api.Controllers
{
    [RoutePrefix("MainMachineOilServiceReport")]
    public class MainMachineOilServiceReportController: ApiController
    {
        private readonly BusinessManager _businessManager = new BusinessManager();

        /// <summary>
        ///     دریافت لیست گزارش روغن مصرفی دستگاه های اصلی براساس شرایط
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetMainMachineOilServiceReportByCondition")]
        [HttpPost]
        public IHttpActionResult GetMainMachineOilServiceReportByCondition(
            InputGetMainMachineOilServiceReportByCondition input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetMainMachineOilServiceReportByCondition]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetMainMachineOilServiceReportByCondition,
                        OutputGetMainMachineOilServiceReportByCondition[]>(
                        storedProcedureName, input);

            return Ok(result);
        }
    }
}