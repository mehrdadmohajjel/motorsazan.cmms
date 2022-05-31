using System.Web.Http;
using Motorsazan.CMMS.Api.Business;
using Motorsazan.CMMS.Api.Filters;
using Motorsazan.CMMS.Shared.Models.Input.MachineCheckList;
using Motorsazan.CMMS.Shared.Models.Output.MachineCheckList;

namespace Motorsazan.CMMS.Api.Controllers
{
    [RoutePrefix("MachineCheckList")]
    public class MachineCheckListController: ApiController
    {
        private readonly BusinessManager _businessManager = new BusinessManager();

        /// <summary>
        ///     فعال یا غیرفعال کردن آیتم عملیاتی
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("ActiveOrDeActiveOperationItem")]
        [HttpPost]
        [JwtValidation]
        public IHttpActionResult ActiveOrDeActiveOperationItem(InputActiveOrDeActiveOperationItem input)
        {
            const string storedProcedureName = "[CMMS].[prc_ActiveOrDeActiveOperationItem]";

            _businessManager.CallStoredProcedure(storedProcedureName, input);

            return Ok("عملیات با موفقیت انجام شد");
        }

        /// <summary>
        ///     ثبت آیتم عملیاتی چک لیست
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("AddCheckListOperationItem")]
        [HttpPost]
        [JwtValidation]
        [AccessToEventValidation(EventCode = "002", FormCode = "006")]
        public IHttpActionResult AddCheckListOperationItem(InputAddCheckListOperationItem input)
        {
            const string storedProcedureName = "[CMMS].[prc_AddCheckListOperationItem]";

            _businessManager.CallStoredProcedure(storedProcedureName, input);

            return Ok("با موفقیت ثبت شد");
        }

        /// <summary>
        ///     کپی آیتم عملیاتی به دستگاه دیگر
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("CopyOperationItemForOtherMachine")]
        [HttpPost]
        [JwtValidation]
        public IHttpActionResult CopyOperationItemForOtherMachine(InputCopyOperationItemForOtherMachine input)
        {
            const string storedProcedureName = "[CMMS].[prc_CopyOperationItemForOtherMachine]";

            _businessManager.CallStoredProcedure(storedProcedureName, input);

            return Ok("با موفقیت ثبت شد");
        }

        /// <summary>
        ///     کپی چندین آیتم عملیاتی به دستگاه دیگر
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("CopySeveralOperationItemToOtherMachine")]
        [HttpPost]
        [JwtValidation]
        public IHttpActionResult CopySeveralOperationItemToOtherMachine(
            InputCopySeveralOperationItemToOtherMachine input)
        {
            const string storedProcedureName = "[CMMS].[prc_CopySeveralOperationItemToOtherMachine]";

            _businessManager.CallStoredProcedure(storedProcedureName, input);

            return Ok("با موفقیت ثبت شد");
        }

        /// <summary>
        ///     ویرایش آیتم عملیاتی
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("EditOperationItemByOperationItemId")]
        [HttpPost]
        [JwtValidation]
        public IHttpActionResult EditOperationItemByOperationItemId(InputEditOperationItemByOperationItemId input)
        {
            const string storedProcedureName = "[CMMS].[prc_EditOperationItemByOperationItemID]";

            _businessManager.CallStoredProcedure(storedProcedureName, input);

            return Ok("ویرایش با موفقیت انجام شد");
        }

        /// <summary>
        ///     دریافت لیست ماشین های یک دپارتمان به صورت درختی
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetHierarchicalMachineListByDepartmentId")]
        [HttpPost]
        public IHttpActionResult GetHierarchicalMachineListByDepartmentId(
            InputGetHierarchicalMachineListByDepartmentId input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetHierarchicalMachineListByDepartmentID]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetHierarchicalMachineListByDepartmentId,
                        OutputGetHierarchicalMachineListByDepartmentId[]>(
                        storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        ///     دریافت لسیت ایتم عملیاتی بر اساس ماشین
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetOperationItemListByMachineId")]
        [HttpPost]
        public IHttpActionResult GetOperationItemListByMachineId(InputGetOperationItemListByMachineId input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetOperationItemListByMachineID]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetOperationItemListByMachineId, OutputGetOperationItemListByMachineId[]>(
                        storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        ///     دریافت سریال برای آیتم های عملیاتی
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetSerialForOperationItem")]
        [HttpPost]
        public IHttpActionResult GetSerialForOperationItem(InputGetSerialForOperationItem input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetSerialForOperationItem]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetSerialForOperationItem, OutputGetSerialForOperationItem>(
                        storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        ///     انتقال آیتم عملیاتی از ماشین جاری به ماشین جدید
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("MoveOperationItemFromCurrentMachineToNewMachine")]
        [HttpPost]
        [JwtValidation]
        public IHttpActionResult MoveOperationItemFromCurrentMachineToNewMachine(
            InputMoveOperationItemFromCurrentMachineToNewMachine input)
        {
            const string storedProcedureName = "[CMMS].[prc_MoveOperationItemFromCurrentMachineToNewMachine]";

            _businessManager.CallStoredProcedure(storedProcedureName, input);

            return Ok("با موفقیت منتقل شد");
        }
    }
}