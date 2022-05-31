using Motorsazan.CMMS.Api.Business;
using Motorsazan.CMMS.Api.Filters;
using Motorsazan.CMMS.Shared.Models.Input.ProductiveWorkOrder;
using Motorsazan.CMMS.Shared.Models.Output.ProductiveWorkOrder;
using System.Web.Http;

namespace Motorsazan.CMMS.Api.Controllers
{
    [RoutePrefix("ProductiveWorkOrder")]
    public class ProductiveWorkOrderController: ApiController
    {
        private readonly BusinessManager _businessManager = new BusinessManager();

        /// <summary>
        /// وب سرویس افزودن یک سفارشکار جدید
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("AddProductiveWorkOrder")]
        [HttpPost]
        [JwtValidation]
        [AccessToEventValidation(EventCode = "002", FormCode = "009")]
        public IHttpActionResult AddProductiveWorkOrder(InputAddProductiveWorkOrder input)
        {
            const string storedProcedureName = "[CMMS].[prc_AddProductiveWorkOrder]";

            var result =
                _businessManager.CallStoredProcedure<InputAddProductiveWorkOrder,string>(storedProcedureName, input);

            return Ok("سفارشکار به شماره<b>: "+ result +"  </b>با موفقیت ایجاد شد.");
        }
        
        /// <summary>
        /// وب سرویس دریافت لیست عملیات های یک سفارشکار
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("EditWorkOrderRateByCustomer")]
        [HttpPost]
        [JwtValidation]
        public IHttpActionResult EditWorkOrderRateByCustomer(InputEditWorkOrderRateByCustomer input)
        {
            const string storedProcedureName = "[CMMS].[prc_EditWorkOrderRateByCustomer]";

            _businessManager.CallStoredProcedure<InputEditWorkOrderRateByCustomer>(storedProcedureName, input);

            return Ok("امتیاز شما با موفقیت ثبت شد.");
        }

        /// <summary>
        /// وب سرویس دریافت لیست عملیات های یک سفارشکار
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetActionOrDelayListByWorkOrderId")]
        [HttpPost]
        public IHttpActionResult GetActionOrDelayListByWorkOrderId(InputGetActionOrDelayListByWorkOrderId input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetActionOrDelayListByWorkOrderID]";

            var result =
                _businessManager.CallStoredProcedure<InputGetActionOrDelayListByWorkOrderId, OutputGetActionOrDelayListByWorkOrderId[]>(storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        /// وب سرویس بدست آوردن امتیاز سفارشکار توسط شناسه سفارشکار 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetWorkOrderRateByWorkOrderID")]
        [HttpPost]
        public IHttpActionResult GetWorkOrderRateByWorkOrderId(InputGetWorkOrderRateByWorkOrderId input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetWorkOrderRateByWorkOrderID]";

            var result =
                _businessManager.CallStoredProcedure<InputGetWorkOrderRateByWorkOrderId,string >(storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        /// وب سرویس دریافت لیست دپارتمان های یک پرسنل
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetCurrentUserDepartmentList")]
        [HttpPost]
        public IHttpActionResult GetCurrentUserDepartmentList(InputGetCurrentUserDepartmentList input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetCurrentUserDepartmentList]";

            var result =
                _businessManager.CallStoredProcedure<InputGetCurrentUserDepartmentList, OutputGetCurrentUserDepartmentList[]>(storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        /// وب سرویس دریافت لیست ماشین های یک امور
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetMachineListByDepartmentId")]
        [HttpPost]
        public IHttpActionResult GetMachineListByDepartmentId(InputGetMachineListByDepartmentId input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetMainMachineListByDepartmentID]";

            var result =
                _businessManager.CallStoredProcedure<InputGetMachineListByDepartmentId, OutputGetMachineListByDepartmentId[]>(storedProcedureName, input);

            return Ok(result);
        }



        /// <summary>
        /// وب سرویس دریافت لیست گروه های تعمیراتی
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetMaintenanceGroupList")]
        [HttpPost]
        public IHttpActionResult GetMaintenanceGroupList()
        {
            const string storedProcedureName = "[CMMS].[prc_GetMaintenanceGroupList]";

            var result =
                _businessManager.CallStoredProcedure<OutputGetMaintenanceGroupList[]>(storedProcedureName);

            return Ok(result);
        }

        /// <summary>
        /// وب سرویس دریافت لیست سفارشکارهای تولیدی ثبت شده توسط فرد
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetProductiveWorkOrderByCondition")]
        [HttpPost]
        public IHttpActionResult GetProductiveWorkOrderByCondition(InputGetProductiveWorkOrderByCondition input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetProductiveWorkOrderByCondition]";

            var result =
                _businessManager.CallStoredProcedure<InputGetProductiveWorkOrderByCondition, OutputGetProductiveWorkOrderByCondition[]>(storedProcedureName, input);

            return Ok(result);
        }


        /// <summary>
        /// وب سرویس دریافت لیست مواد مصرفی سفارشکار
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetWorKOrderConsumableListByWorkOrderId")]
        [HttpPost]
        public IHttpActionResult GetWorKOrderConsumableListByWorkOrderId(InputGetWorKOrderConsumableListByWorkOrderId input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetWorKOrderConsumableListByWorkOrderId]";

            var result =
                _businessManager.CallStoredProcedure<InputGetWorKOrderConsumableListByWorkOrderId, OutputGetWorKOrderConsumableListByWorkOrderId[]>(storedProcedureName, input);

            return Ok(result);
        }


        /// <summary>
        /// وب سرویس دریافت لیست انواع توقف ها
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetStopTypeList")]
        [HttpPost]
        public IHttpActionResult GetStopTypeList()
        {
            const string storedProcedureName = "[CMMS].[prc_GetStopTypeList]";

            var result =
                _businessManager.CallStoredProcedure<OutputGetStopTypeList[]>(storedProcedureName);

            return Ok(result);
        }

        /// <summary>
        /// دریافت لیست نوع توقف برای سفارشکار تولیدی
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetStopTypeListForProductiveWorkOrder")]
        [HttpPost]
        public IHttpActionResult GetStopTypeListForProductiveWorkOrder()
        {
            const string storedProcedureName = "[CMMS].[prc_GetStopTypeListForProductiveWorkOrder]";

            var result =
                _businessManager.CallStoredProcedure<OutputGetStopTypeList[]>(storedProcedureName);

            return Ok(result);
        }

        /// <summary>
        /// وب سرویس دریافت لیست ارجاعات سفارشکار
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetWorKOrderReferralListByWorkOrderId")]
        [HttpPost]
        public IHttpActionResult GetWorKOrderReferralListByWorkOrderId(InputGetWorKOrderReferralListByWorkOrderId input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetWorKOrderReferralListByWorkOrderId]";

            var result =
                _businessManager.CallStoredProcedure<InputGetWorKOrderReferralListByWorkOrderId, OutputGetWorKOrderReferralListByWorkOrderId[]>(storedProcedureName, input);

            return Ok(result);
        }
        

        /// <summary>
        /// وب سرویس دریافت لیست وضعیت های سفارشکار تعمیراتی
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetWorkOrderStatusTypeList")]
        [HttpPost]
        public IHttpActionResult GetWorkOrderStatusTypeList()
        {
            const string storedProcedureName = "[CMMS].[prc_GetWorkOrderStatusTypeList]";

            var result =
                _businessManager.CallStoredProcedure<OutputGetWorkOrderStatusTypeList[]>(storedProcedureName);

            return Ok(result);
        }

        /// <summary>
        /// وب سرویس ابطال سفارشکار
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("SetWorkOrderStatusToCancelByCustomer")]
        [HttpPost]
        [JwtValidation]
        public IHttpActionResult SetWorkOrderStatusToCancelByCustomer(InputSetWorkOrderStatusToCancelByCustomer input)
        {
            const string storedProcedureName = "[CMMS].[prc_SetWorkOrderStatusToCancelByCustomer]";
            
                _businessManager.CallStoredProcedure<InputSetWorkOrderStatusToCancelByCustomer>(storedProcedureName,input);
                return Ok("سفارشکار ایجادی شما ابطال گردید");
        }

        /// <summary>
        /// وب سرویس تائید اتمام سفارشکار
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("SetWorkOrderStatusToConfirmFinishByCustomer")]
        [HttpPost]
        [JwtValidation]
        public IHttpActionResult SetWorkOrderStatusToConfirmFinishByCustomer(InputSetWorkOrderStatusToConfirmFinishByCustomer input)
        {
            const string storedProcedureName = "[CMMS].[prc_SetWorkOrderStatusToConfirmFinishByCustomer]";

            _businessManager.CallStoredProcedure<InputSetWorkOrderStatusToConfirmFinishByCustomer>(storedProcedureName, input);
            return Ok("اتمام سفارشکار تائید گردید");
        }
        
        /// <summary>
        /// وب سرویس تغییر وضیعت سفارشکار به عدم تایید اتمام توسط مشتری
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("SetWorkOrderStatusToNotConfirmFinishByCustomer")]
        [HttpPost]
        [JwtValidation]
        public IHttpActionResult SetWorkOrderStatusToNotConfirmFinishByCustomer(InputSetWorkOrderStatusToNotConfirmFinishByCustomer input)
        {
            const string storedProcedureName = "[CMMS].[prc_SetWorkOrderStatusToNotConfirmFinishByCustomer]";

            _businessManager.CallStoredProcedure<InputSetWorkOrderStatusToNotConfirmFinishByCustomer>(storedProcedureName, input);
            return Ok("تعمیر از طرف شما تائید نگردید");
        }

    }
}
