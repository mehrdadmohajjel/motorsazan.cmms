using Motorsazan.CMMS.Api.Business;
using Motorsazan.CMMS.Api.Filters;
using Motorsazan.CMMS.Shared.Models.Input.SparePartReport;
using Motorsazan.CMMS.Shared.Models.Output.SparePartReport;
using System.Web.Http;

namespace Motorsazan.CMMS.Api.Controllers
{
    [RoutePrefix("SparePartReport")]
    public class SparePartReportController: ApiController
    {
        private readonly BusinessManager _businessManager = new BusinessManager();

        /// <summary>
        ///    دریافت لیست قطعات یدکی بر اساس شناسه ماشین
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetMachineSparePartReportByMachineId")]
        [HttpPost]
        public IHttpActionResult GetMachineSparePartReportByMachineId(
            InputGetMachineSparePartReportByMachineId input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetMachineSparePartReportByMachineId]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetMachineSparePartReportByMachineId,
                        OutputGetMachineSparePartReportByMachineId[]>(
                        storedProcedureName, input);

            return Ok(result);
        }
    }
}
