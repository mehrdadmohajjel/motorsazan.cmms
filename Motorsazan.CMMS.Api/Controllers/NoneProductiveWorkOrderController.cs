using Motorsazan.CMMS.Api.Business;
using Motorsazan.CMMS.Api.Filters;
using Motorsazan.CMMS.Shared.Models.Input.NoneProductiveWorkOrder;
using Motorsazan.CMMS.Shared.Models.Output.NoneProductiveWorkOrder;
using Motorsazan.CMMS.Shared.Models.Output.ProductiveWorkOrder;
using System.Web.Http;

namespace Motorsazan.CMMS.Api.Controllers
{
    [RoutePrefix("NoneProductiveWorkOrder")]
    public class NoneProductiveWorkOrderController: ApiController
    {

        private readonly BusinessManager _businessManager = new BusinessManager();

        /// <summary>
        /// وب سرویس ثبت سفارشکار غیر تولیدی جدید
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("AddNoneProductiveWorkOrder")]
        [HttpPost]
        [JwtValidation]
        public IHttpActionResult AddNoneProductiveWorkOrder(InputAddNoneProductiveWorkOrder input)
        {
            const string storedProcedureName = "[CMMS].[prc_AddNoneProductiveWorkOrder]";

            var result =
                _businessManager.CallStoredProcedure<InputAddNoneProductiveWorkOrder, string>(storedProcedureName, input);

            return Ok("سفارشکار به شماره<b>: " + result + "  </b>با موفقیت ایجاد شد.");
        }

        /// <summary>
        /// وب سرویس دریافت لیست سفارشکارهای غیر تولیدی ثبت شده توسط فرد
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetNoneProductiveWorkOrderByCondition")]
        [HttpPost]
        public IHttpActionResult GetNoneProductiveWorkOrderByCondition(InputGetNoneProductiveWorkOrderByCondition input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetNoneProductiveWorkOrderByCondition]";

            var result =
                _businessManager.CallStoredProcedure<InputGetNoneProductiveWorkOrderByCondition, OutputGetNoneProductiveWorkOrderByCondition[]>(storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        /// دریافت لیست نوع توقف برای سفارشکار غیر تولیدی
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetStopTypeListForNoneProductiveWorkOrder")]
        [HttpPost]
        public IHttpActionResult GetStopTypeListForNoneProductiveWorkOrder()
        {
            const string storedProcedureName = "[CMMS].[prc_GetStopTypeListForNonePreventiveWorkOrder]";

            var result =
                _businessManager.CallStoredProcedure<OutputGetStopTypeList[]>(storedProcedureName);

            return Ok(result);
        }
    }
}