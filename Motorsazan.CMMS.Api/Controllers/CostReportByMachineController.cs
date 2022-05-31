using System.Web.Http;
using Motorsazan.CMMS.Api.Business;
using Motorsazan.CMMS.Api.Filters;
using Motorsazan.CMMS.Shared.Models.Input.CostReportByMachine;
using Motorsazan.CMMS.Shared.Models.Output.CostReportByMachine;

namespace Motorsazan.CMMS.Api.Controllers
{
    [RoutePrefix("CostReportByMachine")]
    public class CostReportByMachineController: ApiController
    {
        private readonly BusinessManager _businessManager = new BusinessManager();

        /// <summary>
        ///     دریافت لیست گزارش هزینه ماشین اصلی براساس شرایط
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetMainMachineCostReportByCondition")]
        [HttpPost]
        public IHttpActionResult GetMainMachineCostReportByCondition(
            InputGetMainMachineCostReportByCondition input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetMainMachineCostReportByCondition]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetMainMachineCostReportByCondition,
                        OutputGetMainMachineCostReportByCondition[]>(
                        storedProcedureName, input);

            return Ok(result);
        }
    }
}