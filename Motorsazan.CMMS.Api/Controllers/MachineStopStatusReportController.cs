using Motorsazan.CMMS.Api.Business;
using Motorsazan.CMMS.Api.Filters;
using Motorsazan.CMMS.Shared.Models.Input.MachineStopStatusReport;
using Motorsazan.CMMS.Shared.Models.Output.MachineStopStatusReport;
using System.Web.Http;

namespace Motorsazan.CMMS.Api.Controllers
{
    [RoutePrefix("MachineStopStatusReport")]
    public class MachineStopStatusReportController: ApiController
    {
        private readonly BusinessManager _businessManager = new BusinessManager();

        /// <summary>
        ///    دریافت لیست گزارش وضیعت توقف ماشین آلات 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetMachineStopStatusReportList")]
        [HttpPost]
        public IHttpActionResult GetMachineStopStatusReportList(
            )
        {
            const string storedProcedureName = "[CMMS].[prc_GetMachineStopStatusReportList]";

            var result =
                _businessManager
                    .CallStoredProcedure<
                        OutputGetMachineStopStatusReportList[]>(
                        storedProcedureName);

            return Ok(result);
        }



        /// <summary>
        ///    دریافت لیست سفارشکار بر اساس شناسه ماشین
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetWorkOrderListByMachineId")]
        [HttpPost]
        public IHttpActionResult GetWorkOrderListByMachineId(InputGetWorkOrderListByMachineId input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetWorkOrderListByMachineID]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetWorkOrderListByMachineId, OutputGetWorkOrderListByMachineId[]>(
                        storedProcedureName, input);

            return Ok(result);
        }
    }
}