using System.Web.Http;
using Motorsazan.CMMS.Api.Business;
using Motorsazan.CMMS.Api.Filters;
using Motorsazan.CMMS.Shared.Models.Input.MaintenanceGroup;
using Motorsazan.CMMS.Shared.Models.Output.MaintenanceGroup;

namespace Motorsazan.CMMS.Api.Controllers
{
    [RoutePrefix("MaintenanceGroup")]
    public class MaintenanceGroupController: ApiController
    {
        private readonly BusinessManager _businessManager = new BusinessManager();


        /// <summary>
        ///     تخصیص کاربر به گروه تعمیراتی
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("AssignEmployeeToMaintenanceGroup")]
        [HttpPost]
        [JwtValidation]
        [AccessToEventValidation(EventCode = "002", FormCode = "003")]
        public IHttpActionResult AssignEmployeeToMaintenanceGroup(InputAddEmployeeToMaintenanceGroup input)
        {
            const string storedProcedureName = "[CMMS].[prc_AssignEmployeeToMaintenanceGroup]";

            var message = _businessManager.CallStoredProcedureAndReturnMessageIfExits(storedProcedureName, input);

            if(!string.IsNullOrEmpty(message))
            {
                return BadRequest(message);
            }

            message = "با موفقیت انجام شد";
            return Ok(message);
        }

        /// <summary>
        ///     دریافت لیست همه کاربران
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetAllEmployeeList")]
        [HttpPost]
        [JwtValidation]
        public IHttpActionResult GetAllEmployeeList()
        {
            const string storedProcedureName = "[HRS].[prc_GetAllEmployeeList]";

            var result = _businessManager.CallStoredProcedure<OutputGetAllEmployeeList[]>(storedProcedureName);

            return Ok(result);
        }

        /// <summary>
        ///     حذف عضو اعضای گروه تعمیراتی بر اساس شناسه جدول اعضای گروه تعمیراتی
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("RemoveMaintenanceGroupMemberFromMaintenanceGroup")]
        [HttpPost]
        [JwtValidation]
        [AccessToEventValidation(EventCode = "002", FormCode = "003")]
        public IHttpActionResult RemoveMaintenanceGroupMemberFromMaintenanceGroup(
            InputRemoveMaintenanceGroupMemberFromMaintenanceGroup input)
        {
            const string storedProcedureName = "[CMMS].[prc_RemoveMaintenanceGroupMemberByMaintenanceGroupMemberID]";

            var message = _businessManager.CallStoredProcedureAndReturnMessageIfExits(storedProcedureName, input);

            if(!string.IsNullOrEmpty(message))
            {
                return BadRequest(message);
            }

            message = "با موفقیت انجام شد";
            return Ok(message);
        }

        /// <summary>
        ///     تنظیم سرپرست روی گروه تعمیراتی
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("SetSuperviserToMaintenanceGroupMember")]
        [HttpPost]
        [JwtValidation]
        [AccessToEventValidation(EventCode = "002", FormCode = "003")]
        public IHttpActionResult SetSuperviserToMaintenanceGroupMember(InputGetSuperviserToMaintenanceGroupMember input)
        {
            const string storedProcedureName = "[CMMS].[prc_SetSuperviserToMaintenanceGroupMember]";

            _businessManager.CallStoredProcedure(storedProcedureName, input);

            return Ok("سرپرست با موفقیت تغییر کرد");
        }
    }
}