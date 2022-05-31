using System.Web.Http;
using Motorsazan.CMMS.Api.Business;
using Motorsazan.CMMS.Api.Filters;
using Motorsazan.CMMS.Shared.Models.Input.DetermineSalary;
using Motorsazan.CMMS.Shared.Models.Output.DetermineSalary;

namespace Motorsazan.CMMS.Api.Controllers
{
    [RoutePrefix("DetermineSalary")]
    public class DetermineSalaryController: ApiController
    {
        private readonly BusinessManager _businessManager = new BusinessManager();

        /// <summary>
        ///     ویرایش جدول حقوق اعضای گروه تعمیراتی
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("EditMaintenanceGroupMemberSalaryBySalaryId")]
        [HttpPost]
        [JwtValidation]
        [AccessToEventValidation(EventCode = "002", FormCode = "008")]
        public IHttpActionResult EditMaintenanceGroupMemberSalaryBySalaryId(
            InputEditMaintenanceGroupMemberSalaryBySalaryId input)
        {
            const string storedProcedureName = "[CMMS].[prc_EditMaintenanceGroupMemberSalaryBySalaryID]";

            _businessManager.CallStoredProcedure(storedProcedureName, input);

            return Ok("ویرایش با موفقیت انجام شد");
        }


        /// <summary>
        ///     دریافت لیست جزئیات حقوق اعضای گروه تعمیراتی براساس شناسه جدول حقوق و دستمزد
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetMaintenanceGroupMemberSalaryDetailListBySalaryId")]
        [HttpPost]
        public IHttpActionResult GetMaintenanceGroupMemberSalaryDetailListBySalaryId(
            InputGetMaintenanceGroupMemberSalaryDetailListBySalaryId input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetMaintenanceGroupMemberSalaryDetailListBySalaryID]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetMaintenanceGroupMemberSalaryDetailListBySalaryId,
                        OutputGetMaintenanceGroupMemberSalaryDetailListBySalaryId[]>(
                        storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        ///     دریافت لیست حقوق اعضای گروه تعمیراتی
        /// </summary>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetMaintenanceGroupMemberSalaryList")]
        [HttpPost]
        public IHttpActionResult GetMaintenanceGroupMemberSalaryList()
        {
            const string storedProcedureName = "[CMMS].[prc_GetMaintenanceGroupMemberSalaryList]";

            var result =
                _businessManager
                    .CallStoredProcedure<OutputGetMaintenanceGroupMemberSalaryList[]>(
                        storedProcedureName);

            return Ok(result);
        }
    }
}