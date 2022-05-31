using System.Web.Http;
using Motorsazan.CMMS.Api.Business;
using Motorsazan.CMMS.Api.Filters;
using Motorsazan.CMMS.Shared.Models.Input.EmployeePerformanceReport;
using Motorsazan.CMMS.Shared.Models.Output.EmployeePerformanceReport;

namespace Motorsazan.CMMS.Api.Controllers
{
    [RoutePrefix("EmployeePerformanceReport")]
    public class EmployeePerformanceReportController: ApiController
    {
        private readonly BusinessManager _businessManager = new BusinessManager();

        /// <summary>
        ///     دریافت لیست همه اعضای گروه تعمیراتی
        /// </summary>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetAllMaintenanceGroupMemberList")]
        [HttpPost]
        public IHttpActionResult GetAllMaintenanceGroupMemberList()
        {
            const string storedProcedureName = "[CMMS].[prc_GetAllMaintenanceGroupMemberList]";

            var result =
                _businessManager
                    .CallStoredProcedure<OutputGetAllMaintenanceGroupMemberList[]>(
                        storedProcedureName);

            return Ok(result);
        }

        /// <summary>
        ///     دریافت لیست گزارش عملکرد پرسنل براساس شرایط
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetEmployeePerformanceReportByCondition")]
        [HttpPost]
        public IHttpActionResult GetEmployeePerformanceReportByCondition(
            InputGetEmployeePerformanceReportByCondition input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetEmployeePerformanceReportByCondition]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetEmployeePerformanceReportByCondition,
                        OutputGetEmployeePerformanceReportByCondition[]>(
                        storedProcedureName, input);

            return Ok(result);
        }
    }
}