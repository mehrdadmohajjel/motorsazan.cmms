using Motorsazan.CMMS.Api.Business;
using Motorsazan.CMMS.Api.Filters;
using Motorsazan.CMMS.Shared.Models.Input.WorkOrderCostReport;
using Motorsazan.CMMS.Shared.Models.Output.WorkOrderCostReport;
using System.Web.Http;

namespace Motorsazan.CMMS.Api.Controllers
{
    [RoutePrefix("WorkOrderCostReport")]
    public class WorkOrderCostReportController: ApiController
    {
        private readonly BusinessManager _businessManager = new BusinessManager();

        /// <summary>
        ///     دریافت لیست گزارش هزینه هر سفارشکار براساس شرایط
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetWorkOrderCostReportByCondition")]
        [HttpPost]
        public IHttpActionResult GetWorkOrderCostReportByCondition(
            InputGetWorkOrderCostReportByCondition input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetWorkOrderCostReportByCondition]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetWorkOrderCostReportByCondition,
                        OutputGetWorkOrderCostReportByCondition[]>(
                        storedProcedureName, input);

            return Ok(result);
        }
    }
}