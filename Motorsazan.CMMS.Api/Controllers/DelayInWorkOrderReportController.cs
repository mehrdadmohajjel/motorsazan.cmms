using Motorsazan.CMMS.Api.Business;
using Motorsazan.CMMS.Api.Filters;
using Motorsazan.CMMS.Shared.Models.Input.DelayInWorkOrderReport;
using Motorsazan.CMMS.Shared.Models.Output.DelayInWorkOrderReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Motorsazan.CMMS.Api.Controllers
{
    [RoutePrefix("DelayInWorkOrderReport")]
    public class DelayInWorkOrderReportController : ApiController
    {
        private readonly BusinessManager _businessManager = new BusinessManager();

        /// <summary>
        ///دریافت گزارش تاخیر در سفارشکار بر اساس شرایط 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetDelayInWorkOrderReportByCondition")]
        [HttpPost]
        public IHttpActionResult GetDelayInWorkOrderReportByCondition(
            InputGetDelayInWorkOrderReportByCondition input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetDelayInWorkOrderReportByCondition]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetDelayInWorkOrderReportByCondition,
                        OutputGetDelayInWorkOrderReportByCondition[]>(
                        storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        ///دریافت گزارش تاخیر در سفارشکار بر اساس شرایط 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetWorkOrderByDelayTypeId")]
        [HttpPost]
        public IHttpActionResult GetWorkOrderByDelayTypeId(
            InputGetWorkOrderByDelayTypeId input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetWorkOrderByDelayTypeId]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetWorkOrderByDelayTypeId,
                        OutputGetWorkOrderByDelayTypeId[]>(
                        storedProcedureName, input);

            return Ok(result);
        }

    }
}
