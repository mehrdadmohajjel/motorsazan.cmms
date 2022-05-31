using Motorsazan.CMMS.Api.Business;
using Motorsazan.CMMS.Api.Filters;
using Motorsazan.CMMS.Shared.Models.Input.NetExpert;
using Motorsazan.CMMS.Shared.Models.Output.NetExpert;
using System.Web.Http;

namespace Motorsazan.CMMS.Api.Controllers
{
    [RoutePrefix("NetExpert")]
    public class NetExpertController: ApiController
    {
        private readonly BusinessManager _businessManager = new BusinessManager();

        /// <summary>
        ///     ثبت عملیات به سفارشکار
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("AddActionToWorkOrder")]
        [HttpPost]
        [JwtValidation]
        public IHttpActionResult AddActionToWorkOrder(InputAddActionToWorkOrder input)
        {
            const string storedProcedureName = "[CMMS].[prc_AddActionToWorkOrder]";

            _businessManager.CallStoredProcedure(storedProcedureName, input);

            return Ok("با موفقیت ثبت شد");
        }

        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("EditActionOrDelayListInPreventiveScheduling")]
        [HttpPost]
        [JwtValidation]
        public IHttpActionResult EditActionOrDelayListInPreventiveScheduling(
            InputEditActionOrDelayListInPreventiveScheduling input)
        {
            const string storedProcedureName = "[CMMS].[prc_EditActionOrDelayListInPreventiveScheduling]";

            var message = _businessManager.CallStoredProcedureAndReturnMessageIfExits(storedProcedureName, input);

            if(!string.IsNullOrEmpty(message))
            {
                return Ok(message);
            }

            return Ok("زمان ها با موفقیت ویرایش شد");
        }


        /// <summary>
        ///     ثبت  عملیات بر سفارشکار پیشگیرانه
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("AddPreventiveMaintenanceActionToWorkOrder")]
        [HttpPost]
        [JwtValidation]
        public IHttpActionResult AddPreventiveMaintenanceActionToWorkOrder(
            InputAddPreventiveMaintenanceActionToWorkOrder input)
        {
            const string storedProcedureName = "[CMMS].[prc_AddPreventiveMaintenanceActionToWorkOrder]";

            var message = _businessManager.CallStoredProcedureAndReturnMessageIfExits(storedProcedureName, input);

            if(!string.IsNullOrEmpty(message))
            {
                return BadRequest(message);
            }

            message = "با موفقیت انجام شد";
            return Ok(message);
        }


        /// <summary>
        ///     ثبت عملیات بازرسی روی سفارشکار
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("AddInspectionActionToWorkOrder")]
        [HttpPost]
        [JwtValidation]
        public IHttpActionResult AddInspectionActionToWorkOrder(InputAddInspectionActionToWorkOrder input)
        {
            const string storedProcedureName = "[CMMS].[prc_AddInspectionActionToWorkOrder]";

            _businessManager.CallStoredProcedure(storedProcedureName, input);

            return Ok("با موفقیت ثبت شد");
        }


        /// <summary>
        ///     ثبت وضیعت ابطال برای سفارشکار
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("AddCancelStatusToWorkOrder")]
        [HttpPost]
        [JwtValidation]
        public IHttpActionResult AddCancelStatusToWorkOrder(InputAddCancelStatusToWorkOrder input)
        {
            const string storedProcedureName = "[CMMS].[prc_AddCancelStatusToWorkOrder]";

            var message = _businessManager.CallStoredProcedureAndReturnMessageIfExits(storedProcedureName, input);

            if(!string.IsNullOrEmpty(message))
            {
                return BadRequest(message);
            }

            message = "با موفقیت انجام شد";
            return Ok(message);
        }

        /// <summary>
        ///     وب سرویس بررسی وجود آیتم عملیاتی برای ماشین
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("CheckMachineHasOperationItem")]
        [HttpPost]
        public IHttpActionResult CheckMachineHasOperationItem(InputCheckMachineHasOperationItem input)
        {
            const string storedProcedureName = "[CMMS].[prc_CheckMachineHasOperationItem]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputCheckMachineHasOperationItem, OutputCheckMachineHasOperationItem>(
                        storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        ///     همه شماره حواله های یک سفارشکار تایید شده است یا نه؟
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("CheckAllHavalehNOConfirmedByWorkOrderId")]
        [HttpPost]
        public IHttpActionResult CheckAllHavalehNOConfirmedByWorkOrderId(
            InputCheckAllHavalehNOConfirmedByWorkOrderId input)
        {
            const string storedProcedureName = "[CMMS].[prc_CheckAllHavalehNOConfirmedByWorkOrderId]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputCheckAllHavalehNOConfirmedByWorkOrderId,
                        OutputCheckAllHavalehNOConfirmedByWorkOrderId>(storedProcedureName, input);

            return Ok(result);
        }


        /// <summary>
        ///     دریافت لیست آیتم های عملیاتی بازرسی برای یک سفارشکار
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetInspectionDetailByWorkOrderId")]
        [HttpPost]
        public IHttpActionResult GetInspectionDetailByWorkOrderId(InputGetInspectionDetailByWorkOrderId input)
        {
            ;
            const string storedProcedureName = "[CMMS].[prc_GetInspectionDetailByWorkOrderId]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetInspectionDetailByWorkOrderId, OutputGetInspectionDetailByWorkOrderId
                        []>(storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        ///     ثبت اتمام نهایی روی سفارشکار
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("AddRepairFinishedStatusToWorkOrder")]
        [HttpPost]
        [JwtValidation]
        public IHttpActionResult AddRepairFinishedStatusToWorkOrder(InputAddRepairFinishedStatusToWorkOrder input)
        {
            const string storedProcedureName = "[CMMS].[prc_AddRepairFinishedStatusToWorkOrder]";

            var message = _businessManager.CallStoredProcedureAndReturnMessageIfExits(storedProcedureName, input);

            if(!string.IsNullOrEmpty(message))
            {
                return BadRequest(message);
            }

            message = "با موفقیت انجام شد";
            return Ok(message);
        }


        /// <summary>
        ///     ثبت اتمام سفارشکار
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("AddNetFinishedStatusToWorkOrder")]
        [HttpPost]
        [JwtValidation]
        public IHttpActionResult AddNetFinishedStatusToWorkOrder(InputAddNetFinishedStatusToWorkOrder input)
        {
            const string storedProcedureName = "[CMMS].[prc_AddNetFinishedStatusToWorkOrder]";

            _businessManager.CallStoredProcedure(storedProcedureName, input);

            return Ok("با موفقیت انجام شد");
        }


        /// <summary>
        ///     ثبت وضعیت منتظر قطعه به سفارشکار
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("AddStockWaitingStatusToWorkOrder")]
        [HttpPost]
        [JwtValidation]
        public IHttpActionResult AddStockWaitingStatusToWorkOrder(InputAddStockWaitingStatusToWorkOrder input)
        {
            const string storedProcedureName = "[CMMS].[prc_AddStockWaitingStatusToWorkOrder]";

            var message = _businessManager.CallStoredProcedureAndReturnMessageIfExits(storedProcedureName, input);

            if(!string.IsNullOrEmpty(message))
            {
                return BadRequest(message);
            }

            message = "دستورکار شما با قطعات انتخابی به وضعیت منتظر قطعه تبدیل شد";
            return Ok(message);
        }

        /// <summary>
        ///     اتمام انتظار قطعه
        /// </summary>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("FinishingStockWaitingForWorkOrder")]
        [HttpPost]
        [JwtValidation]
        public IHttpActionResult FinishingStockWaitingForWorkOrder(InputFinishingStockWaitingForWorkOrder input)
        {
            const string storedProcedureName = "[CMMS].[prc_FinishingStockWaitingForWorkOrder]";

            _businessManager.CallStoredProcedure(storedProcedureName, input);

            return Ok("دستورکار از حالت منتظر قطعه خارج شد");
        }


        /// <summary>
        ///     دریافت لسیت نوع تاخیر
        /// </summary>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetDelayTypeListByActivity")]
        [HttpPost]
        public IHttpActionResult GetDelayTypeListByActivity(InputGetDelayTypeListByActivity input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetDelayTypeListByActivity]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetDelayTypeListByActivity, OutputGetDelayTypeListByActivity[]>(
                        storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        ///     دریافت لیست آیتم های عملیاتی پیشگیرانه برای یک سفارشکار
        /// </summary>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetPreventiveOperationItemListByWorkOrderId")]
        [HttpPost]
        public IHttpActionResult GetPreventiveOperationItemListByWorkOrderId(
            InputGetPreventiveOperationItemListByWorkOrderId input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetPreventiveOperationItemListByWorkOrderId]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetPreventiveOperationItemListByWorkOrderId,
                        OutputGetPreventiveOperationItemListByWorkOrderId[]>(storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        ///     لیست پرسنل برای ثبت عملکرد
        /// </summary>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetEmployeeListForPerformanceRegistering")]
        [HttpPost]
        public IHttpActionResult GetEmployeeListForPerformanceRegistering()
        {
            const string storedProcedureName = "[CMMS].[prc_GetEmployeeListForPerformanceRegistering]";

            var result =
                _businessManager.CallStoredProcedure<OutputGetEmployeeListForPerformanceRegistering[]>(
                    storedProcedureName);

            return Ok(result);
        }

        /// <summary>
        ///     دریافت لیست اعضای گروه تعمیراتی
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetMaintenanceGroupMemberListByMaintenanceGroupId")]
        [HttpPost]
        public IHttpActionResult GetMaintenanceGroupMemberList(
            InputGetMaintenanceGroupMemberListByMaintenanceGroupId input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetMaintenanceGroupMemberListByMaintenanceGroupId]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetMaintenanceGroupMemberListByMaintenanceGroupId,
                        OutputGetMaintenanceGroupMemberListByMaintenanceGroupId[]>(storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        ///     وب سرویس دریافت لیست گروه های تعمیراتی بر اساس شناسه کارمند
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetMaintenanceGroupListByEId")]
        [HttpPost]
        public IHttpActionResult GetMaintenanceGroupListByEId(InputGetMaintenanceGroupListByEId input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetMaintenanceGroupListByEID]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetMaintenanceGroupListByEId, OutputGetMaintenanceGroupListByEId[]>(
                        storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        ///     دریافت لیست سفارشکارهای یک کارشناس
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetNetExpertWorkOrderListByCondition")]
        [HttpPost]
        public IHttpActionResult GetNetExpertWorkOrderListByCondition(InputGetNetExpertWorkOrderListByCondition input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetNetExpertWorkOrderListByCondition]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetNetExpertWorkOrderListByCondition,
                        OutputGetNetExpertWorkOrderListByCondition[]>(storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        ///     دریافت لسیت آیتم عملیاتی بر اساس شناسه گروه تعمیراتی
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetOperationItemListByMaintenanceGroupIdAndMachineId")]
        [HttpPost]
        public IHttpActionResult GetOperationItemListByMaintenanceGroupIdAndMachineId(
            InputGetOperationItemListByMaintenanceGroupIdAndMachineId input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetOperationItemListByMaintenanceGroupIDAndMachineID]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetOperationItemListByMaintenanceGroupIdAndMachineId,
                        OutputGetOperationItemListByMaintenanceGroupIdAndMachineId[]>(storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        ///     دریافت اطلاعات شفارشکار براساس شناسه سفارشکار
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetWorkOrderByWorkOrderId")]
        [HttpPost]
        public IHttpActionResult GetWorkOrderByWorkOrderId(InputGetWorkOrderByWorkOrderId input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetWorkOrderByWorkOrderId]";

            var result =
                _businessManager.CallStoredProcedure<InputGetWorkOrderByWorkOrderId, OutputGetWorkOrderByWorkOrderId>(
                    storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        ///     دریافت لیست اطلاعات ثبت کننده سفارشکار براساس سفارشکار
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetWorkOrderRegistrantInfoByWorkOrderId")]
        [HttpPost]
        public IHttpActionResult GetWorkOrderRegistrantInfoByWorkOrderId(
            InputGetWorkOrderRegistrantInfoByWorkOrderId input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetWorkOrderRegistrantInfoByWorkOrderId]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetWorkOrderRegistrantInfoByWorkOrderId,
                        OutputGetWorkOrderRegistrantInfoByWorkOrderId>(storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        ///     دریافت لیست خرید براساس شماره درخواست و سال درخواست
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetPurchaseByRequestNumberAndRequestYear")]
        [HttpPost]
        public IHttpActionResult GetPurchaseByRequestNumberAndRequestYear(
            InputGetPurchaseByRequestNumberAndRequestYear input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetPurchaseByRequestNumberAndRequestYear]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetPurchaseByRequestNumberAndRequestYear,
                        OutputGetPurchaseByRequestNumberAndRequestYear[]>(storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        ///     ارجاع سفارشکار به گروه تعمیراتی
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("SendWorkOrderToMaintenanceGroup")]
        [HttpPost]
        [JwtValidation]
        public IHttpActionResult SendWorkOrderToMaintenanceGroup(InputSendWorkOrderToMaintenanceGroup input)
        {
            const string storedProcedureName = "[CMMS].[prc_SendWorkOrderToMaintenanceGroup]";

            _businessManager.CallStoredProcedure(storedProcedureName, input);

            return Ok("با موفقیت انجام شد");
        }

        /// <summary>
        ///     حذف رکورد عملیات یا تاخیر
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("RemoveActionOrDelayByActionOrDelayListId")]
        [HttpPost]
        [JwtValidation]
        public IHttpActionResult RemoveActionOrDelayByActionOrDelayListId(
            InputRemoveActionOrDelayByActionOrDelayListId input)
        {
            const string storedProcedureName = "[CMMS].[prc_RemoveActionOrDelayByActionOrDelayListId]";

            _businessManager.CallStoredProcedure(storedProcedureName, input);

            return Ok("با موفقیت حذف شد");
        }

        /// <summary>
        ///     ارجاع سفارشکار به  اعضای گروه تعمیراتی
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("SendWorkOrderToMaintenanceGroupMember")]
        [HttpPost]
        [JwtValidation]
        public IHttpActionResult SendWorkOrderToMaintenanceGroupMember(InputSendWorkOrderToMaintenanceGroupMember input)
        {
            const string storedProcedureName = "[CMMS].[prc_SendWorkOrderToMaintenanceGroupMember]";

            _businessManager.CallStoredProcedure(storedProcedureName, input);

            return Ok("با موفقیت انجام شد");
        }


        /// <summary>
        ///     تنظیم شماره حواله بر اساس شناسه ارجاع سفارشکار حواله
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("SetHavaleNOListByHavalehWorkOrderIDList")]
        [HttpPost]
        [JwtValidation]
        public IHttpActionResult SetHavaleNOListByHavalehWorkOrderIDList(InputSetHavaleNOListByHavalehWorkOrderId input)
        {
            const string storedProcedureName = "[CMMS].[prc_SetHavaleNOListByHavalehWorkOrderIDList]";


            _businessManager.CallStoredProcedure(storedProcedureName, input);

            return Ok("با موفقیت ثبت انجام شد");
        }

        /// <summary>
        ///     دریافت لیست تاریخچه وضیعت های یک سفارشکار
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetWorkOrderStatusHistoryByWorkOrderId")]
        [HttpPost]
        public IHttpActionResult GetWorkOrderStatusHistoryByWorkOrderId(
            InputGetWorkOrderStatusHistoryByWorkOrderId input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetWorkOrderStatusHistoryByWorkOrderID]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetWorkOrderStatusHistoryByWorkOrderId,
                        OutputGetWorkOrderStatusHistoryByWorkOrderId[]>(storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        ///     دریافت لیست اقلام منتظر قطعه برای سفارشکار
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetStockPurchaseRequestForStockWaitingByWorkOrderId")]
        [HttpPost]
        public IHttpActionResult GetStockPurchaseRequestForStockWaitingByWorkOrderId(
            InputGetStockPurchaseRequestForStockWaitingByWorkOrderId input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetStockPurchaseRequestForStockWaitingByWorkOrderID]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetStockPurchaseRequestForStockWaitingByWorkOrderId,
                        OutputGetStockPurchaseRequestForStockWaitingByWorkOrderId[]>(storedProcedureName, input);

            return Ok(result);
        }
    }
}