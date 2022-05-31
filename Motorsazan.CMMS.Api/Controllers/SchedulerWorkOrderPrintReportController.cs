using Motorsazan.CMMS.Api.Business;
using Motorsazan.CMMS.Api.Filters;
using Motorsazan.CMMS.Shared.Models.Input.SchedulerWorkOrderPrintReport;
using Motorsazan.CMMS.Shared.Models.Output.SchedulerWorkOrderPrintReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Motorsazan.CMMS.Api.Controllers
{
    [RoutePrefix("SchedulerWorkOrderPrintReport")]
    public class SchedulerWorkOrderPrintReportController : ApiController
    {
        private readonly BusinessManager _businessManager = new BusinessManager();

        /// <summary>
        ///دریافت گزارش برنامه ریز - چاپ سفارشکار بر اساس شرایط 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetSchedulerWorkOrderReportByCondition")]
        [HttpPost]
        public IHttpActionResult GetSchedulerWorkOrderReportByCondition(
            InputGetSchedulerWorkOrderReportByCondition input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetSchedulerWorkOrderReportByCondition]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetSchedulerWorkOrderReportByCondition,
                        OutputGetSchedulerWorkOrderReportByCondition[]>(
                        storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        ///دریافت پرینت فرم مراجعه بر اساس شناسه سفارشکار 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetVisitFormPrintByWorkOrderId")]
        [HttpPost]
        public IHttpActionResult GetVisitFormPrintByWorkOrderId(
            InputGetVisitFormPrintByWorkOrderId input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetVisitFormPrintByWorkOrderID]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetVisitFormPrintByWorkOrderId,
                        OutputGetVisitFormPrintByWorkOrderId>(
                        storedProcedureName, input);

            return Ok(result);
        }


        /// <summary>
        ///دریافت پرینت فرم مراجعه بر اساس شناسه سفارشکار 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetPreventiveVisitFormPrintByWorkOrderId")]
        [HttpPost]
        public IHttpActionResult GetPreventiveVisitFormPrintByWorkOrderId(
            InputGetPreventiveVisitFormPrintByWorkOrderId input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetPreventiveVisitFormPrintByWorkOrderID]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetPreventiveVisitFormPrintByWorkOrderId,
                        OutputGetPreventiveVisitFormPrintByWorkOrderId>(
                        storedProcedureName, input);

            return Ok(result);
        }
    }
}