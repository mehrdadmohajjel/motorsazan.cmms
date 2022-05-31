using System.Web.Http;
using Motorsazan.CMMS.Api.Business;
using Motorsazan.CMMS.Api.Filters;
using Motorsazan.CMMS.Shared.Models.Input.PerformanceReportByDepartment;
using Motorsazan.CMMS.Shared.Models.Output.PerformanceReportByDepartment;

namespace Motorsazan.CMMS.Api.Controllers
{
    [RoutePrefix("PerformanceReportByDepartment")]
    public class PerformanceReportByDepartmentController: ApiController
    {
        private readonly BusinessManager _businessManager = new BusinessManager();

        /// <summary>
        ///     دریافت لیست گزارش عملکرد به تفکیک دپارتمان براساس شرایط
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetDepartmentPerformanceReportByCondition")]
        [HttpPost]
        public IHttpActionResult GetDepartmentPerformanceReportByCondition(
            InputGetDepartmentPerformanceReportByCondition input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetDepartmentPerformanceReportByCondition]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetDepartmentPerformanceReportByCondition,
                        OutputGetDepartmentPerformanceReportByCondition[]>(
                        storedProcedureName, input);

            return Ok(result);
        }
    }
}