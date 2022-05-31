using Motorsazan.CMMS.Api.Business;
using Motorsazan.CMMS.Api.Filters;
using Motorsazan.CMMS.Shared.Models.Input.OperationItem;
using Motorsazan.CMMS.Shared.Models.Output.OperationItem;
using System.Web.Http;

namespace Motorsazan.CMMS.Api.Controllers
{
    [RoutePrefix("OperationItem")]
    public class OperationItemController: ApiController
    {
        private readonly BusinessManager _businessManager = new BusinessManager();

        /// <summary>
        ///     ثبت کد آیتم خرابی
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("AddFaultOperationItem")]
        [HttpPost]
        [JwtValidation]
        [AccessToEventValidation(EventCode = "002", FormCode = "004")]
        public IHttpActionResult AddFaultOperationItem(InputAddFaultOperationItem input)
        {
            const string storedProcedureName = "[CMMS].[prc_AddFaultOperationItem]";

            _businessManager.CallStoredProcedure(storedProcedureName, input);

            return Ok("با موفقیت ثبت شد");
        }


        /// <summary>
        ///     تخصیص قطعه یدکی به آیتم عملیاتی
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("AddSparePartListToOperationItem")]
        [HttpPost]
        [JwtValidation]
        [AccessToEventValidation(EventCode = "002", FormCode = "004")]
        public IHttpActionResult AddSparePartListToOperationItem(InputAddSparePartListToOperationItem input)
        {
            const string storedProcedureName = "[CMMS].[prc_AddSparePartListToOperationItem]";

            var message = _businessManager.CallStoredProcedureAndReturnMessageIfExits(storedProcedureName, input);

            if(!string.IsNullOrEmpty(message))
            {
                return BadRequest(message);
            }

            message = "با موفقیت انجام شد";
            return Ok(message);
        }


        /// <summary>
        ///     تخصیص کدهای خرابی به آیتم پیشگیرانه
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("AddOperationItemMappingToPMItem")]
        [HttpPost]
        [JwtValidation]
        [AccessToEventValidation(EventCode = "002", FormCode = "004")]
        public IHttpActionResult AddOperationItemMappingToPMItem(InputAddOperationItemMappingToPMItem input)
        {
            const string storedProcedureName = "[CMMS].[prc_AddOperationItemMappingToPMItem]";

            _businessManager.CallStoredProcedure(storedProcedureName, input);

            return Ok("با موفقیت ثبت شد");
        }

        /// <summary>
        ///     دریافت لیست تخصیصات کدهای خرابی به آیتم های پیشگیرانه به وسیله شناسه آیتم کدهای خرابی
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetOperationItemMappingToPMItemListByOperationItemId")]
        [HttpPost]
        public IHttpActionResult GetOperationItemMappingToPMItemListByOperationItemId(
            InputGetOperationItemMappingToPMItemListByOperationItemId input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetOperationItemMappingToPMItemListByOperationItemID]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetOperationItemMappingToPMItemListByOperationItemId,
                        OutputGetOperationItemMappingToPMItemListByOperationItemId[]>(
                        storedProcedureName, input);

            return Ok(result);
        }


        /// <summary>
        ///     دریافت لیست قطعات یدکی بر اساس آیتم عملیاتی
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetOperationItemSparePartListByOperationItemId")]
        [HttpPost]
        public IHttpActionResult GetOperationItemSparePartListByOperationItemId(
            InputGetOperationItemSparePartListByOperationItemId input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetOperationItemSparePartListByOperationItemId]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetOperationItemSparePartListByOperationItemId,
                        OutputGetOperationItemSparePartListByOperationItemId[]>(
                        storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        ///     دریافت لیست آیتم های عملیاتی پیشگیرانه برای یک دستگاه
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetPreventiveOperationItemListByMachineId")]
        [HttpPost]
        public IHttpActionResult GetPreventiveOperationItemListByMachineId(
            InputGetPreventiveOperationItemListByMachineId input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetPreventiveOperationItemListByMachineID]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetPreventiveOperationItemListByMachineId,
                        OutputGetPreventiveOperationItemListByMachineId[]>(
                        storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        ///     حذف تخصیص ایتم عملیاتی به پیشگیرانه
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("RemoveOperationItemMappingToPMItemByOperationItemMappingToPMItemId")]
        [HttpPost]
        [JwtValidation]
        [AccessToEventValidation(EventCode = "002", FormCode = "004")]
        public IHttpActionResult RemoveOperationItemMappingToPMItemByOperationItemMappingToPMItemId(
            InputRemoveOperationItemMappingToPMItemByOperationItemMappingToPMItemId input)
        {
            const string storedProcedureName =
                "[CMMS].[prc_RemoveOperationItemMappingToPMItemByOperationItemMappingToPMItemID]";

            _businessManager.CallStoredProcedure(storedProcedureName, input);

            return Ok("با موفقیت انجام شد");
        }


        /// <summary>
        ///     حذف قطعه یدکی از آیتم عملیاتی
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("RemoveSparePartFromOperationItem")]
        [HttpPost]
        [JwtValidation]
        [AccessToEventValidation(EventCode = "002", FormCode = "004")]
        public IHttpActionResult RemoveSparePartFromOperationItem(
            InputRemoveSparePartFromOperationItem input)
        {
            const string storedProcedureName =
                "[CMMS].[prc_RemoveSparePartFromOperationItem]";
            var message = _businessManager.CallStoredProcedureAndReturnMessageIfExits(storedProcedureName, input);

            if(!string.IsNullOrEmpty(message))
            {
                return BadRequest(message);
            }

            message = "با موفقیت انجام شد";
            return Ok(message);
        }
    }
}