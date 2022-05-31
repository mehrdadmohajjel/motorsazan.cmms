using System.Web.Http;
using Motorsazan.CMMS.Api.Business;
using Motorsazan.CMMS.Api.Filters;
using Motorsazan.CMMS.Shared.Models.Input.CostReportByDepartment;
using Motorsazan.CMMS.Shared.Models.Output.CostReportByDepartment;

namespace Motorsazan.CMMS.Api.Controllers
{
    [RoutePrefix("CostReportByDepartment")]
    public class CostReportByDepartmentController: ApiController
    {
        private readonly BusinessManager _businessManager = new BusinessManager();

        /// <summary>
        ///     دریافت لیست گزارش هزینه دپارتمان براساس شرایط
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetDepartmentCostReportByCondition")]
        [HttpPost]
        public IHttpActionResult GetDepartmentCostReportByCondition(
            InputGetDepartmentCostReportByCondition input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetDepartmentCostReportByCondition]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetDepartmentCostReportByCondition,
                        OutputGetDepartmentCostReportByCondition[]>(
                        storedProcedureName, input);

            return Ok(result);
        }
    }
}