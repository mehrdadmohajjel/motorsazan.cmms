using Motorsazan.CMMS.Api.Business;
using Motorsazan.CMMS.Api.Filters;
using Motorsazan.CMMS.Shared.Models.Input.Inspection;
using Motorsazan.CMMS.Shared.Models.Input.PreventiveMaintenancesScheduling;
using Motorsazan.CMMS.Shared.Models.Output.PreventiveMaintenancesScheduling;
using System.Web.Http;

namespace Motorsazan.CMMS.Api.Controllers
{
    [RoutePrefix("PreventiveMaintenancesScheduling")]
    public class PreventiveMaintenancesSchedulingController: ApiController
    {
        private readonly BusinessManager _businessManager = new BusinessManager();

        /// <summary>
        ///     دریافت لیست امورها
        /// </summary>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetAllMainDepartment")]
        [HttpPost]
        public IHttpActionResult GetAllMainDepartment()
        {
            const string storedProcedureName = "[HRS].[prc_GetAllMainDepartment]";

            var result =
                _businessManager
                    .CallStoredProcedure<OutputGetAllMainDepartment[]>(
                        storedProcedureName);

            return Ok(result);
        }

        /// <summary>
        ///     دریافت لیست گروه های تعمیراتی
        /// </summary>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetMaintenanceGroupList")]
        [HttpPost]
        public IHttpActionResult GetMaintenanceGroupList()
        {
            const string storedProcedureName = "[CMMS].[prc_GetMaintenanceGroupList]";

            var result =
                _businessManager
                    .CallStoredProcedure<OutputGetMaintenanceGroupList[]>(
                        storedProcedureName);

            return Ok(result);
        }

        /// <summary>
        ///     دریافت لیست دستگاه با امور یا خط
        /// </summary>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetMainMachineListByMainDepartmentIdAndSubDepartmentId")]
        [HttpPost]
        public IHttpActionResult GetMainMachineListByMainDepartmentIdAndSubDepartmentId(
            InputGetMainMachineListByMainDepartmentIdAndSubDepartmentId input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetMainMachineListByMainDepartmentIDAndSubDepartmentID]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetMainMachineListByMainDepartmentIdAndSubDepartmentId,
                        OutputGetMainMachineListByMainDepartmentIdAndSubDepartmentId[]>(storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        ///     دریافت لیست ایتم عملیاتی بر اساس شناسه برنامه ریز پیشگیرانه
        /// </summary>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetOperationItemListByPMSchedulingInfoId")]
        [HttpPost]
        public IHttpActionResult GetOperationItemListByPMSchedulingInfoId(
            InputGetOperationItemListByPMSchedulingInfoId input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetOperationItemListByPMSchedulingInfoID]";


            var result =
                _businessManager
                    .CallStoredProcedure<InputGetOperationItemListByPMSchedulingInfoId,
                        OutputGetOperationItemListByPMSchedulingInfoId[]>(storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        ///     دریافت لیست برنامه ریزی تعمیرات پیشگیرانه بر اساس شرایط
        /// </summary>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetPreventiveMaintenanceSchedulingListByCondition")]
        [HttpPost]
        public IHttpActionResult GetPreventiveMaintenanceSchedulingListByCondition(
            InputGetPreventiveMaintenanceSchedulingListByCondition input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetPreventiveMaintenanceSchedulingListByCondition]";


            var result =
                _businessManager
                    .CallStoredProcedure<InputGetPreventiveMaintenanceSchedulingListByCondition,
                        OutputGetPreventiveMaintenanceSchedulingListByCondition[]>(storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        ///     حذف  چندین آیتم  عملیاتی براساس شناسه جدول برنامه ریز  پیشگیرانه
        /// </summary>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("RemoveSeveralOperationItemByPMSchedulingInfoID")]
        [HttpPost]
        [JwtValidation]
        [AccessToEventValidation(EventCode = "003", FormCode = "007")]
        public IHttpActionResult RemoveSeveralOperationItemByPMSchedulingInfoID(
            InputRemoveSeveralOperationItemByPMSchedulingInfoId input)
        {
            const string storedProcedureName = "[CMMS].[prc_RemoveSeveralOperationItemByPMSchedulingInfoID]";

            _businessManager
                .CallStoredProcedure(storedProcedureName, input);

            return Ok("تولید برنامه پیشگیرانه از آیتم های انتخابی غیر فعال شد");
        }

        /// <summary>
        ///     تنظیم سفارشکار برای آیتم های پیشگیرانه
        /// </summary>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("SetWorkOrderToSeveralPereventiveMaintenanceScheduling")]
        [HttpPost]
        [JwtValidation]
        [AccessToEventValidation(EventCode = "002", FormCode = "007")]
        public IHttpActionResult SetWorkOrderToSeveralPereventiveMaintenanceScheduling(
            InputSetWorkOrderToSeveralPereventiveMaintenanceScheduling input)
        {
            const string storedProcedureName = "[CMMS].[prc_SetWorkOrderToSeveralPereventiveMaintenanceScheduling]";

            var message = _businessManager.CallStoredProcedureAndReturnMessageIfExits(storedProcedureName, input);

            if(!string.IsNullOrEmpty(message))
            {
                return BadRequest(message);
            }

            message = "با موفقیت انجام شد";
            return Ok(message);
        }

        /// <summary>
        ///     دریافت لسیت زیر دپارتمان ها بر اساس شناسه دپارتمان اصلی
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetSubDepartmentListByMainDepartmentId")]
        [HttpPost]
        public IHttpActionResult GetSubDepartmentListByMainDepartmentId(
            InputGetSubDepartmentListByMainDepartmentId input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetSubDepartmentListByMainDepartmentID]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetSubDepartmentListByMainDepartmentId,
                        OutputGetSubDepartmentListByMainDepartmentId[]>(storedProcedureName, input);

            return Ok(result);
        }


        /// <summary>
        ///     دریافت تاریخ شروع و پایان یک هفته خاص در سال جاری
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetStartAndEndDateOfParticularWeekByNumberOfWeeksOfCurrentYear")]
        [HttpPost]
        public IHttpActionResult GetStartAndEndDateOfParticularWeekByNumberOfWeeksOfCurrentYear(
            InputGetStartAndEndDateOfParticularWeekByNumberOfWeeksOfCurrentYear input)
        {
            const string storedProcedureName =
                "[CMMS].[prc_GetStartAndEndDateOfParticularWeekByNumberOfWeeksOfCurrentYear]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetStartAndEndDateOfParticularWeekByNumberOfWeeksOfCurrentYear,
                        OutputGetStartAndEndDateOfParticularWeekByNumberOfWeeksOfCurrentYear>(storedProcedureName,
                        input);

            return Ok(result);
        }

        /// <summary>
        ///     ریست جاب محاسبه پیشگیرانه تعمیرات
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ResetPredectiveMaintenanceJob")]
        [RequestModelNullValidation]
        [RequestModelValidation]
        public IHttpActionResult ResetPredectiveMaintenanceJob()
        {
            const string storedProcedureName = "[CMMS].[prc_ResetPredectiveMaintenanceJob]";
            var input = new
                InputResetPredectiveMaintenanceJob {Type = 1};
            var errorMessage = _businessManager.CallStoredProcedureAndReturnMessageIfExits(storedProcedureName, input);

            return string.IsNullOrEmpty(errorMessage)
                ? Ok("با موفقیت انجام شد")
                : (IHttpActionResult)BadRequest(errorMessage);
        }
    }
}