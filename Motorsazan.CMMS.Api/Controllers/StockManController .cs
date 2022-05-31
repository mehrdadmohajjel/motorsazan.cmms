using Motorsazan.CMMS.Api.Business;
using Motorsazan.CMMS.Api.Filters;
using System.Web.Http;
using Motorsazan.CMMS.Shared.Models.Input.StockMan;
using Motorsazan.CMMS.Shared.Models.Output.StockMan;


namespace Motorsazan.CMMS.Api.Controllers
{
    [RoutePrefix("StockMan")]
    public class StockManController: ApiController
    {
        private readonly BusinessManager _businessManager = new BusinessManager();


        /// <summary>
        /// دریافت لیست سفارشکارهای انباردار نت براساس انواع وضیعت های مختلف
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetStockManListByCondition")]
        [HttpPost]
        public IHttpActionResult GetStockManListByCondition(InputGetStockManListByCondition input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetStockManWorkOrderByCondition]";

            var result =
                _businessManager.CallStoredProcedure<InputGetStockManListByCondition, OutputGetStockManListByCondition[]>(storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        /// دریافت لیست اطلاعات ثبت کننده سفارشکار براساس سفارشکار
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetRegistrarInfoByWorkOrderId")]
        [HttpPost]
        public IHttpActionResult GetRegistrarInfoByWorkOrderId(InputGetRegistrarProfileObservation input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetWorkOrderRegistrantInfoByWorkOrderId]";

            var result =
                _businessManager.CallStoredProcedure<InputGetRegistrarProfileObservation, OutputGetRegistrarProfileObservation>(storedProcedureName, input);

            return Ok(result);
        }


        /// <summary>
        /// تنظیم شماره حواله بر اساس شناسه ارجاع سفارشکار حواله
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("UpdateConsumablesManagement")]
        [HttpPost]
        [JwtValidation]
        public IHttpActionResult UpdateConsumablesManagement(InputGetEditConsumablesManagement input)
        {
            const string storedProcedureName = "[CMMS].[prc_SetHavaleNOByHavalehWorkOrderID]";

           var message = _businessManager.CallStoredProcedureAndReturnMessageIfExits(storedProcedureName, input);

           if(!string.IsNullOrEmpty(message))
           {
               return BadRequest(message);
           }

           message = " ویرایش با موفقیت انجام شد";
           return Ok(message);
        }

        /// <summary>
        /// تنظیم تاریخ تایید حواله بر اساس شناسه ارجاع سفارشکار حواله
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("ConfirmConsumablesManagement")]
        [HttpPost]
        [JwtValidation]
        public IHttpActionResult ConfirmConsumablesManagement(InputGetConfirmConsumablesManagement input)
        {
            const string storedProcedureName = "[CMMS].[prc_SetConfirmDateByHavalehWorkOrderID]";

            var message = _businessManager.CallStoredProcedureAndReturnMessageIfExits(storedProcedureName, input);

            if(!string.IsNullOrEmpty(message))
            {
                return BadRequest(message);
            }

            message = "تایید با موفقیت انجام شد";
            return Ok(message);
        }

        /// <summary>
        /// حذف مواد مصرفی بر اساس شناسه حواله ارجاع سفارشکار
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("DeleteConsumablesManagement")]
        [HttpPost]
        [JwtValidation]
        public IHttpActionResult DeleteConsumablesManagement(InputGetConfirmConsumablesManagement input)
        {
            const string storedProcedureName = "[CMMS].[prc_RemoveConsumableByHavaleWorkOrderReferralID]";

            var message = _businessManager.CallStoredProcedureAndReturnMessageIfExits(storedProcedureName, input);

            if(!string.IsNullOrEmpty(message))
            {
                return BadRequest(message);
            }

            message = "حذف با موفقیت انجام شد";
            return Ok(message);
        }

        /// <summary>
        /// ثبت مواد توسط انباردار نت
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("AddingConsumable")]
        [HttpPost]
        [JwtValidation]
        public IHttpActionResult AddingConsumable(InputGetAddingConsumable input)
        {
            const string storedProcedureName = "[CMMS].[prc_AddHavaleWorkOrderReferral]";

            _businessManager.CallStoredProcedure<InputGetAddingConsumable>(storedProcedureName, input);

            return Ok("مواد مصرفی با موفقیت ثبت شد");
        }


        /// <summary>
        ///	دریافت لیست ارجاع سفارش کار حواله بر اساس شناسه ارجاع شفارشکار
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetHavaleWorkOrderReferralByWorkOrderReferralId")]
        [HttpPost]
        public IHttpActionResult GetHavaleWorkOrderReferralByWorkOrderReferralId(InputGetHavaleWorkOrderReferral input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetHavaleWorkOrderReferralByWorkOrderReferralID]";

            var result = 
                _businessManager.CallStoredProcedure<InputGetHavaleWorkOrderReferral, OutputGetHavaleWorkOrderReferral[]>(storedProcedureName, input);

            return Ok(result);
        }

    }
}
