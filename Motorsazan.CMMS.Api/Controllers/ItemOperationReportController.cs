using Motorsazan.CMMS.Api.Business;
using Motorsazan.CMMS.Api.Filters;
using Motorsazan.CMMS.Shared.Models.Input.ItemOperationReport;
using Motorsazan.CMMS.Shared.Models.Output.ItemOperationReport;
using System.Web.Http;

namespace Motorsazan.CMMS.Api.Controllers
{
    [RoutePrefix("ItemOperationReport")]
    public class ItemOperationReportController: ApiController
    {
        private readonly BusinessManager _businessManager = new BusinessManager();

        /// <summary>
        ///     دریافت لسیت نوع آیتم های عملیاتی
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Route("GetOperationItemTypeList")]
        [HttpPost]
        public IHttpActionResult GetOperationItemTypeList()
        {
            const string storedProcedureName = "[CMMS].[prc_GetOperationItemTypeList]";


            var result = _businessManager.CallStoredProcedure<OutputGetOperationItemTypeList[]>(storedProcedureName);

            return Ok(result);
        }

        /// <summary>
        ///     دریافت  گزارش او سی ای براساس شرایط
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetItemOperationReportByCondtion")]
        [HttpPost]
        public IHttpActionResult GetItemOperationReportByCondtion(
            InputGetItemOperationReportByCondtion input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetItemOperationReportByCondtion]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetItemOperationReportByCondtion,
                        OutputGetItemOperationReportByCondtion[]>(
                        storedProcedureName, input);

            return Ok(result);
        }
    }
}