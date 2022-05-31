using Motorsazan.CMMS.Api.Business;
using Motorsazan.CMMS.Api.Filters;
using Motorsazan.CMMS.Shared.Models.Input.Inspection;
using Motorsazan.CMMS.Shared.Models.Output.Inspection;
using System.Web.Http;
using Motorsazan.CMMS.Shared.Models.Input.PerformanceReportByDepartment;
using Motorsazan.CMMS.Shared.Models.Output.PerformanceReportByDepartment;

namespace Motorsazan.CMMS.Api.Controllers
{
    [RoutePrefix("Inspection")]
    public class InspectionController: ApiController
    {
        private readonly BusinessManager _businessManager = new BusinessManager();

        /// <summary>
        /// دریافت لیست ماشین های یک دپارتمان
        /// </summary>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetMainMachineListBySubDepartmentId")]
        [HttpPost]
        public IHttpActionResult GetMainMachineListBySubDepartmentId(InputGetMainMachineListBySubDepartmentId input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetMainMachineListBySubDepartmentID]";

            var result =
                _businessManager.CallStoredProcedure<InputGetMainMachineListBySubDepartmentId, OutputGetMainMachineListBySubDepartmentId[]>(storedProcedureName, input);

            return Ok(result);
        }

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
                        OutputGetSubDepartmentListByMainDepartmentId[]>(
                        storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        /// ثبت بازرسی
        /// </summary>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("AddInspection")]
        [HttpPost]
        [JwtValidation]
        public IHttpActionResult AddInspection(InputAddInspection input)
        {
            const string storedProcedureName = "[CMMS].[prc_AddInspection]";

            _businessManager.CallStoredProcedure<InputAddInspection>(storedProcedureName, input);

            return Ok("سفارشکار بازرسی شما با موفقیت ثبت شد");
        }

        /// <summary>
        /// ثبت جزییات بازرسی برای یک فیلد بازرسی
        /// </summary>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("AddInspectionDetailToInspection")]
        [HttpPost]
        public IHttpActionResult AddInspectionDetailToInspection(InputAddInspectionDetailToInspection input)
        {
            const string storedProcedureName = "[CMMS].[prc_AddInspectionDetailToInspection]";

            _businessManager.CallStoredProcedure<InputAddInspectionDetailToInspection>(storedProcedureName, input);

            return Ok("جزئیات بازرسی با موفقیت ثبت شد");
        }


        /// <summary>
        /// تبدیل فرم بازرسی به سفارشکار
        /// </summary>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("AddInspectionWorkOrder")]
        [HttpPost]
        [JwtValidation]
        public IHttpActionResult AddInspectionWorkOrder(InputAddInspectionWorkOrder input)
        {
            const string storedProcedureName = "[CMMS].[prc_AddInspectionWorkOrder]";

            _businessManager.CallStoredProcedure<InputAddInspectionWorkOrder>(storedProcedureName, input);

            return Ok("سفارشکار بازرسی شما به سفارشکار تغییر یافت");
        }

        /// <summary>
        /// ویرایش بازرسی
        /// </summary>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("EditTheInspection")]
        [HttpPost]
        [JwtValidation]
        public IHttpActionResult EditTheInspection(InputEditTheInspection input)
        {
            const string storedProcedureName = "[CMMS].[prc_EditInspectionByInspectionID]";

            _businessManager.CallStoredProcedure<InputEditTheInspection>(storedProcedureName, input);

            return Ok("سفارشکار بازرسی شما با موفقیت ویرایش یافت");
        }

        /// <summary>
        /// ویرایش جزییات بازرسی بر اساس شناسه بازرسی
        /// </summary>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("EditInspectionDetailByInspectionDetailId")]
        [HttpPost]
        [JwtValidation]
        public IHttpActionResult EditInspectionDetailByInspectionDetailId(InputEditInspectionDetailByInspectionDetailId input)
        {
            const string storedProcedureName = "[CMMS].[prc_EditInspectionDetailByInspectionDetailID]";

            _businessManager.CallStoredProcedure<InputEditInspectionDetailByInspectionDetailId>(storedProcedureName, input);

            return Ok("جزئیات سفارشکار بازرسی شما با موفقیت ویرایش یافت");
        }

        /// <summary>
        /// دریافت لسیت نوع بازرسی
        /// </summary>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetInspectionTypeList")]
        [HttpPost]
        public IHttpActionResult GetInspectionTypeList()
        {
            const string storedProcedureName = "[CMMS].[prc_GetInspectionTypeList]";

            var result =
                _businessManager.CallStoredProcedure<OutputGetInspectionTypeList[]>(storedProcedureName);

            return Ok(result);
        }

        /// <summary>
        /// دریافت لسیت دپارتمان های اصلی دارای ماشین
        /// </summary>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetInspectionByInspectionId")]
        [HttpPost]
        public IHttpActionResult GetInspectionByInspectionId(InputGetInspectionByInspectionId input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetInspectionByInspectionID]";

            var result =
                _businessManager.CallStoredProcedure<InputGetInspectionByInspectionId, OutputGetInspectionByInspectionId>(storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        /// دریافت جزییات بازرسی براساس شناسه جزییات بازرسی
        /// </summary>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetInspectionDetailByInspectionDetailId")]
        [HttpPost]
        public IHttpActionResult GetInspectionDetailByInspectionDetailId(InputGetInspectionDetailByInspectionDetailId input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetInspectionDetailByInspectionDetailID]";

            var result =
                _businessManager.CallStoredProcedure<InputGetInspectionDetailByInspectionDetailId, OutputGetInspectionDetailByInspectionDetailId>(storedProcedureName, input);

            return Ok(result);
        }


        /// <summary>
        /// دریافت لسیت دپارتمان های اصلی دارای ماشین
        /// </summary>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetMainDepartmentListHasMachine")]
        [HttpPost]
        public IHttpActionResult GetMainDepartmentListHasMachine()
        {
            const string storedProcedureName = "[CMMS].[prc_GetMainDepartmentListHasMachine]";

            var result =
                _businessManager.CallStoredProcedure<OutputGetMainDepartmentListHasMachine[]>(storedProcedureName);

            return Ok(result);
        }

        /// <summary>
        /// دریافت لسیت دپارتمان های اصلی دارای ماشین
        /// </summary>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetSubDepartmentListHasMachineByMainDepartmentId")]
        [HttpPost]
        public IHttpActionResult GetSubDepartmentListHasMachineByMainDepartmentId(InputGetSubDepartmentListHasMachineByMainDepartmentId input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetSubDepartmentListHasMachineByMainDepartmentID]";

            var result =
                _businessManager.CallStoredProcedure<InputGetSubDepartmentListHasMachineByMainDepartmentId, OutputGetSubDepartmentListHasMachineByMainDepartmentId[]>(storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        /// دریافت لیست ماشین های اصلی یک دپارتمان
        /// </summary>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetMainMachineListByDepartmentId")]
        [HttpPost]
        public IHttpActionResult GetMainMachineListByDepartmentId(InputGetMainMachineListByDepartmentId input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetMainMachineListByDepartmentID]";

            var result =
                _businessManager.CallStoredProcedure<InputGetMainMachineListByDepartmentId, OutputGetMainMachineListByDepartmentId[]>(storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        /// دریافت  لیست بازرسی
        /// </summary>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetInspectionListByCondition")]
        [HttpPost]
        public IHttpActionResult GetInspectionListByCondition(InputGetInspectionListByCondition input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetInspectionListByCondition]";

            var result =
                _businessManager.CallStoredProcedure<InputGetInspectionListByCondition, OutputGetInspectionListByCondition[]>(storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        /// دریافت  لیست بازرسی
        /// </summary>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetInspectionDetailsByInspectionId")]
        [HttpPost]
        public IHttpActionResult GetInspectionDetailsByInspectionId(InputGetInspectionDetailsByInspectionId input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetInspectionDetailsByInspectionID]";

            var result =
                _businessManager.CallStoredProcedure<InputGetInspectionDetailsByInspectionId, OutputGetInspectionDetailsByInspectionId[]>(storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        /// ویرایش بازرسی
        /// </summary>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("RemoveInspection")]
        [HttpPost]
        [JwtValidation]
        public IHttpActionResult RemoveInspection(InputRemoveInspection input)
        {
            const string storedProcedureName = "[CMMS].[prc_RemoveInspection]";

            _businessManager.CallStoredProcedure<InputRemoveInspection>(storedProcedureName, input);

            return Ok("سفارشکار بازرسی شما ابطال شد");
        }

        /// <summary>
        /// حذف جزییات بازرسی  
        /// </summary>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("RemoveInspectionDeatilsByInspectionDetailId")]
        [HttpPost]
        [JwtValidation]
        public IHttpActionResult RemoveInspectionDeatilsByInspectionDetailId(InputRemoveInspectionDeatilsByInspectionDetailId input)
        {
            const string storedProcedureName = "[CMMS].[prc_RemoveInspectionDeatilsByInspectionDetailID]";

            _businessManager.CallStoredProcedure<InputRemoveInspectionDeatilsByInspectionDetailId>(storedProcedureName, input);

            return Ok("جزییات بازرسی حذف شد");
        }
    }
}
