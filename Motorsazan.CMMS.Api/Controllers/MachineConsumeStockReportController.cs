using Motorsazan.CMMS.Api.Business;
using Motorsazan.CMMS.Api.Filters;
using Motorsazan.CMMS.Shared.Models.Input.MachineConsumeStockReport;
using Motorsazan.CMMS.Shared.Models.Output.MachineConsumeStockReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Motorsazan.CMMS.Api.Controllers
{
    [RoutePrefix("MachineConsumeStockReport")]
    public class MachineConsumeStockReportController : ApiController
    {
        private readonly BusinessManager _businessManager = new BusinessManager();

        /// <summary>
        ///دریافت لیست ماشین هایی که قطعات مصرف شده دارند
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetMachineConsumeStockReportByCondition")]
        [HttpPost]
        public IHttpActionResult GetMachineConsumeStockReportByCondition(
            InputGetMachineConsumeStockReportByCondition input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetMachineConsumeStockReportByCondition]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetMachineConsumeStockReportByCondition,
                        OutputGetMachineConsumeStockReportByCondition[]>(
                        storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        ///دریافت لیست قطعات مجوجود  در جدول حواله(دریافت قطعات مصرف شده)
        /// </summary>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetStockFromHavaleWorkOrderReferral")]
        [HttpPost]
        public IHttpActionResult GetStockFromHavaleWorkOrderReferral()
        {
            const string storedProcedureName = "[CMMS].[prc_GetStockFromHavaleWorkOrderReferral]";

            var result =
                _businessManager.CallStoredProcedure<OutputGetStockFromHavaleWorkOrderReferral[]>(storedProcedureName);

            return Ok(result);
        }
    }
}
