using System.Web.Http;
using Motorsazan.CMMS.Api.Business;
using Motorsazan.CMMS.Api.Filters;
using Motorsazan.CMMS.Shared.Models.Input.EmployeeCostReport;
using Motorsazan.CMMS.Shared.Models.Output.EmployeeCostReport;

namespace Motorsazan.CMMS.Api.Controllers
{
    [RoutePrefix("EmployeeCostReport")]
    public class EmployeeCostReportController: ApiController
    {
        private readonly BusinessManager _businessManager = new BusinessManager();

        /// <summary>
        ///     دریافت لیست گزارش هزینه پرسنل براساس شرایط
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetEmployeeCostReportByCondition")]
        [HttpPost]
        public IHttpActionResult GetEmployeeCostReportByCondition(
            InputGetEmployeeCostReportByCondition input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetEmployeeCostReportByCondition]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetEmployeeCostReportByCondition,
                        OutputGetEmployeeCostReportByCondition[]>(
                        storedProcedureName, input);

            return Ok(result);
        }
    }
}