using System.Web.Http;
using Motorsazan.CMMS.Api.Business;
using Motorsazan.CMMS.Api.Filters;
using Motorsazan.CMMS.Shared.Models.Input.PreventiveItemOperation;
using Motorsazan.CMMS.Shared.Models.Output.PreventiveItemOperation;

namespace Motorsazan.CMMS.Api.Controllers
{
    [RoutePrefix("PreventiveItemOperation")]
    public class PreventiveItemOperationController: ApiController
    {
        private readonly BusinessManager _businessManager = new BusinessManager();

        /// <summary>
        ///     ثبت آیتم عملیاتی پیشگیرانه
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("AddPreventiveOperationItem")]
        [HttpPost]
        [JwtValidation]
        [AccessToEventValidation(EventCode = "002", FormCode = "005")]
        public IHttpActionResult AddPreventiveOperationItem(InputAddPreventiveOperationItem input)
        {
            const string storedProcedureName = "[CMMS].[prc_AddPreventiveOperationItem]";

            _businessManager.CallStoredProcedure(storedProcedureName, input);

            return Ok("با موفقیت ثبت شد");
        }

        /// <summary>
        ///     ویرایش آیتم پیشگیرانه بر اساس شناسه آیتم عملیاتی
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("EditPreventiveOperationByOperationItemId")]
        [HttpPost]
        [JwtValidation]
        [AccessToEventValidation(EventCode = "002", FormCode = "005")]
        public IHttpActionResult EditPreventiveOperationByOperationItemId(
            InputEditPreventiveOperationByOperationItemId input)
        {
            const string storedProcedureName = "[CMMS].[prc_EditPreventiveOperationByOperationItemID]";

            _businessManager.CallStoredProcedure(storedProcedureName, input);

            return Ok("با موفقیت انجام شد");
        }

        /// <summary>
        ///     دریافت لیست شاخص اندازه گیری
        /// </summary>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetMiterMeasuringTypeListForPreventiveOperationItem")]
        [HttpPost]
        public IHttpActionResult GetMiterMeasuringTypeListForPreventiveOperationItem()
        {
            const string storedProcedureName = "[CMMS].[prc_GetMiterMeasuringTypeListForPreventiveOperationItem]";

            var result =
                _businessManager
                    .CallStoredProcedure<OutputGetMiterMeasuringTypeListForPreventiveOperationItem[]>(
                        storedProcedureName);

            return Ok(result);
        }

        /// <summary>
        ///     دریافت لیست آیتم های عملیاتی پیشگیرانه براساس شناسه آیتم های عملیاتی
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetPreventiveOperationItemByOperationItemId")]
        [HttpPost]
        public IHttpActionResult GetPreventiveOperationItemByOperationItemId(
            InputGetPreventiveOperationItemByOperationItemId input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetPreventiveOperationItemByOperationItemID]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetPreventiveOperationItemByOperationItemId,
                        OutputGetPreventiveOperationItemByOperationItemId>(
                        storedProcedureName, input);

            return Ok(result);
        }
    }
}