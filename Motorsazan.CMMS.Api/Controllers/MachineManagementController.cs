using Motorsazan.CMMS.Api.Business;
using Motorsazan.CMMS.Api.Filters;
using Motorsazan.CMMS.Shared.Models.DomainModels;
using Motorsazan.CMMS.Shared.Models.Input.MachineManagement;
using Motorsazan.CMMS.Shared.Models.Output.MachineManagement;
using System;
using System.Linq;
using System.Web.Http;

namespace Motorsazan.CMMS.Api.Controllers
{
    [RoutePrefix("MachineManagement")]
    public class MachineManagementController: ApiController
    {
        private readonly BusinessManager _businessManager = new BusinessManager();

        /// <summary>
        ///     ثبت مستندات ماشین
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("AddMachineDocument")]
        [HttpPost]
        [JwtValidation]
        [AccessToEventValidation(EventCode = "003", FormCode = "002")]
        public IHttpActionResult AddMachineDocument(InputAddMachineDocument input)
        {
            const string storedProcedureName = "[CMMS].[prc_AddMachineDocument]";

            var message = _businessManager.CallStoredProcedureAndReturnMessageIfExits(storedProcedureName, input);

            if(!string.IsNullOrEmpty(message))
            {
                return BadRequest(message);
            }

            message = "با موفقیت انجام شد";
            return Ok(message);
        }


        /// <summary>
        ///     حذف اطلاعات مشخصات زوغن ها و روانکارهای ماشی
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("RemoveMachineOilAndLubricationInfoByRecordId")]
        [HttpPost]
        [JwtValidation]
        public IHttpActionResult RemoveMachineOilAndLubricationInfoByRecordId(
            InputRemoveMachineOilAndLubricationInfoByRecordId input)
        {
            const string storedProcedureName = "[CMMS].[prc_RemoveMachineOilAndLubricationInfoByRecordID]";

            var message = _businessManager.CallStoredProcedureAndReturnMessageIfExits(storedProcedureName, input);

            if(!string.IsNullOrEmpty(message))
            {
                return BadRequest(message);
            }

            message = "با موفقیت انجام شد";
            return Ok(message);
        }


        /// <summary>
        ///     ثبت اطلاعات الکتریکی ماشین
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("AddMachineElectricalInfo")]
        [HttpPost]
        [JwtValidation]
        [AccessToEventValidation(EventCode = "006", FormCode = "002")]
        public IHttpActionResult AddMachineElectricalInfo(InputAddMachineElectricalInfo input)
        {
            const string storedProcedureName = "[CMMS].[prc_AddMachineElectricalInfo]";

            var message = _businessManager.CallStoredProcedureAndReturnMessageIfExits(storedProcedureName, input);

            if(!string.IsNullOrEmpty(message))
            {
                return BadRequest(message);
            }

            message = "با موفقیت انجام شد";
            return Ok(message);
        }

        /// <summary>
        ///     ثبت مستندات ماشین
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("RegisterOilAndLubrication")]
        [HttpPost]
        [JwtValidation]
        public IHttpActionResult RegisterOilAndLubrication(InputRegisterOilAndLubrication input)
        {
            const string storedProcedureName = "[CMMS].[prc_AddMachineOilAndlubricationInfo]";

            var message = _businessManager.CallStoredProcedureAndReturnMessageIfExits(storedProcedureName, input);

            if(!string.IsNullOrEmpty(message))
            {
                return BadRequest(message);
            }

            message = "با موفقیت انجام شد";
            return Ok(message);
        }


        /// <summary>
        ///     ثبت قطعات یدکی به ماشین
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("AddMachineSparePart")]
        [HttpPost]
        [JwtValidation]
        [AccessToEventValidation(EventCode = "005", FormCode = "002")]
        public IHttpActionResult AddMachineSparePart(InputAddMachineSparePart input)
        {
            const string storedProcedureName = "[CMMS].[prc_AddMachineSparePart]";

            var message = _businessManager.CallStoredProcedureAndReturnMessageIfExits(storedProcedureName, input);

            if(!string.IsNullOrEmpty(message))
            {
                return BadRequest(message);
            }

            message = "با موفقیت انجام شد";
            return Ok(message);
        }

        /// <summary>
        ///     ذخیره تغییرات در مشخصات ساخت دستگاه
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("EditMachineBuiltInfo")]
        [HttpPost]
        [JwtValidation]
        public IHttpActionResult EditMachineBuiltInfo(InputEditMachineBuiltInfo input)
        {
            const string storedProcedureName = "[CMMS].[prc_EditMachineBuiltInfo]";

            var message = _businessManager.CallStoredProcedureAndReturnMessageIfExits(storedProcedureName, input);

            if(!string.IsNullOrEmpty(message))
            {
                return BadRequest(message);
            }

            message = "با موفقیت انجام شد";
            return Ok(message);
        }


        /// <summary>
        ///     حذف اطلاعات الکترونیکی دستگاه
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("RemoveMachineElectricalInfoByRecordId")]
        [HttpPost]
        [JwtValidation]
        public IHttpActionResult RemoveMachineElectricalInfoByRecordId(InputRemoveMachineElectricalInfoByRecordId input)
        {
            const string storedProcedureName = "[CMMS].[prc_RemoveMachineElectricalInfoByRecordId]";

            var message = _businessManager.CallStoredProcedureAndReturnMessageIfExits(storedProcedureName, input);

            if(!string.IsNullOrEmpty(message))
            {
                return BadRequest(message);
            }

            message = "با موفقیت انجام شد";
            return Ok(message);
        }

        /// <summary>
        ///     ثبت قطعات یدکی ماشین
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("AddMachineSparePartDocument")]
        [HttpPost]
        [JwtValidation]
        [AccessToEventValidation(EventCode = "005", FormCode = "002")]
        public IHttpActionResult AddMachineSparePartDocument(InputAddMachineSparePartDocument input)
        {
            const string storedProcedureName = "[CMMS].[prc_AddMachineSparePartDocument]";

            var message = _businessManager.CallStoredProcedureAndReturnMessageIfExits(storedProcedureName, input);

            if(!string.IsNullOrEmpty(message))
            {
                return BadRequest(message);
            }

            message = "با موفقیت انجام شد";
            return Ok(message);
        }

        /// <summary>
        ///     ثبت زیر ماشین
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("AddSubMachine")]
        [HttpPost]
        [JwtValidation]
        [AccessToEventValidation(EventCode = "002", FormCode = "002")]
        public IHttpActionResult AddSubMachine(InputAddSubMachine input)
        {
            const string storedProcedureName = "[CMMS].[prc_AddSubMachine]";

            var message = _businessManager.CallStoredProcedureAndReturnMessageIfExits(storedProcedureName, input);

            if(!string.IsNullOrEmpty(message))
            {
                return BadRequest(message);
            }

            message = "با موفقیت انجام شد";
            return Ok(message);
        }

        /// <summary>
        ///     تخصیص ماشین به فرد
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("AssignMachineToEmployee")]
        [HttpPost]
        [JwtValidation]
        [AccessToEventValidation(EventCode = "004", FormCode = "002")]
        public IHttpActionResult AssignMachineToEmployee(InputAssignMachineToEmployee input)
        {
            const string storedProcedureName = "[CMMS].[prc_AssignMachineToEmployee]";

            var message = _businessManager.CallStoredProcedureAndReturnMessageIfExits(storedProcedureName, input);

            if(!string.IsNullOrEmpty(message))
            {
                return BadRequest(message);
            }

            message = "با موفقیت انجام شد";
            return Ok(message);
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
                _businessManager.CallStoredProcedure<OutputGetAllMainDepartment[]>(storedProcedureName);

            return Ok(result);
        }

        /// <summary>
        ///     دریافت لیست مشخصه ها
        /// </summary>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetCharacteristicList")]
        [HttpPost]
        public IHttpActionResult GetCharacteristicList()
        {
            const string storedProcedureName = "[CMMS].[prc_GetCharacteristicList]";

            var result =
                _businessManager.CallStoredProcedure<OutputGetCharacteristicList[]>(storedProcedureName);

            return Ok(result);
        }

        /// <summary>
        ///     دریافت لیست rpp
        /// </summary>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetRpmList")]
        [HttpPost]
        public IHttpActionResult GetRpmList()
        {
            const string storedProcedureName = "[CMMS].[prc_GetRpmList]";

            var result =
                _businessManager.CallStoredProcedure<OutputGetRpmList[]>(storedProcedureName);

            return Ok(result);
        }

        /// <summary>
        ///     دریافت لیست ولتاژ
        /// </summary>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetVoltageList")]
        [HttpPost]
        public IHttpActionResult GetVoltageList()
        {
            const string storedProcedureName = "[CMMS].[prc_GetVoltageList]";

            var result =
                _businessManager.CallStoredProcedure<OutputGetVoltageList[]>(storedProcedureName);

            return Ok(result);
        }


        /// <summary>
        ///     دریافت لیست پرسنل یک دپارتمان
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetEmployeeListOfDepartment")]
        [HttpPost]
        public IHttpActionResult GetEmployeeListOfDepartment(InputGetEmployeeListOfDepartment input)
        {
            const string storedProcedureName = "[HRS].[prc_GetEmployeeListOfDepartment]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetEmployeeListOfDepartment, OutputGetEmployeeListOfDepartment[]>(
                        storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        ///     دریافت مشخصات  یک دستگاه
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetMachineInfoByMachineId")]
        [HttpPost]
        public IHttpActionResult GetMachineInfoByMachineId(InputGetMachineInfoByMachineId input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetMachineInfoByMachineId]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetMachineInfoByMachineId, OutputGetMachineInfoByMachineId>(
                        storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        ///     دریافت مشخصات  ساخت یک دستگاه
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetMachineBuiltInfo")]
        [HttpPost]
        public IHttpActionResult GetMachineBuiltInfo(InputGetMachineBuiltInfo input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetMachineBuiltInfo]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetMachineBuiltInfo, OutputGetMachineBuiltInfo>(
                        storedProcedureName, input);

            return Ok(result);
        }


        /// <summary>
        ///     دریافت مشخصات  یک دستگاه
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetMachineElectricalInfo")]
        [HttpPost]
        public IHttpActionResult GetMachineElectricalInfo(InputGetMachineElectricalInfo input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetMachineElectricalInfo]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetMachineElectricalInfo, OutputGetMachineElectricalInfo[]>(
                        storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        ///     دریافت مشخصات  روغن های استفاده شده برای دستگاه
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetMachineOilAndLubricationInfo")]
        [HttpPost]
        public IHttpActionResult GetMachineElectricalInfo(InputGetMachineOilAndLubricationInfo input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetMachineOilAndLubricationInfo]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetMachineOilAndLubricationInfo, OutputGetMachineOilAndLubricationInfo[]>(
                        storedProcedureName, input);

            return Ok(result);
        }


        /// <summary>
        ///     دریافت لیست نوع فایل ماشین
        /// </summary>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetFileTypeList")]
        [HttpPost]
        public IHttpActionResult GetFileTypeList()
        {
            const string storedProcedureName = "[CMMS].[prc_GetFileTypeList]";

            var result =
                _businessManager.CallStoredProcedure<OutputGetFileTypeList[]>(storedProcedureName);

            return Ok(result);
        }

        /// <summary>
        ///     دریافت لیست ماشین به صورت درختی
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetHierarchicalMachineList")]
        [HttpPost]
        public IHttpActionResult GetHierarchicalMachineList(InputGetHierarchicalMachineList input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetHierarchicalMachineListByTopMachineID]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetHierarchicalMachineList, OutputGetHierarchicalMachineList[]>(
                        storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        ///     دریافت لیست مستندات ماشین بر اساس شناسه ماشین
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetMachineDocumentListByMachineId")]
        [HttpPost]
        public IHttpActionResult GetMachineDocumentListByMachineId(InputGetMachineDocumentListByMachineId input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetMachineDocumentListByMachineID]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetMachineDocumentListByMachineId,
                        OutputGetMachineDocumentListByMachineId[]>(
                        storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        ///     دریافت لسیت محل ماشین بر اساس شناسه ماشین اصلی
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetMachineLocationByMachineId")]
        [HttpPost]
        public IHttpActionResult GetMachineLocationByMachineId(InputGetMachineLocationByMachineId input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetMachineLocationByMachineID]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetMachineLocationByMachineId, OutputGetMachineLocationByMachineId[]>(
                        storedProcedureName, input);

            return Ok(result);
        }

        //------------------------------------------

        /// <summary>
        ///     دریافت لیست مستندات قطعات یدکی بر اساس شناسه قطعات یدکی
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetMachineSparePartDocumentListByMachineSparePartId")]
        [HttpPost]
        public IHttpActionResult GetMachineSparePartDocumentListByMachineSparePartId(
            InputGetMachineSparePartDocumentListByMachineSparePartId input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetMachineSparePartDocumentListByMachineSparePartID]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetMachineSparePartDocumentListByMachineSparePartId,
                        OutputGetMachineSparePartDocumentListByMachineSparePartId[]>(
                        storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        ///     دریافت لیست قطعات یدکی بر اساس شناسه ماشین
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetMachineSparePartListByMachineId")]
        [HttpPost]
        public IHttpActionResult GetMachineSparePartListByMachineId(InputGetMachineSparePartListByMachineId input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetMachineSparePartListByMachineID]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetMachineSparePartListByMachineId,
                        OutputGetMachineSparePartListByMachineId[]>(
                        storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        ///     دریافت لیست کالا بر اساس کد انبار در فرم قطعات یدکی
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetStockListByStoreCodeForSparePart")]
        [RequestModelNullValidation]
        [RequestModelValidation]
        public IHttpActionResult GetStockListByStoreCodeForSparePart(InputGetStockListByStoreCodeForSparePart input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetStockListByStoreCodeForSparePart]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetStockListByStoreCodeForSparePart,
                        OutputGetStockListByStoreCodeForSparePart[]>(storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        ///     دریافت لیست انبار برای افزودن قطعات یدکی برای دستگاه ها در سیستم نت
        /// </summary>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetStoreListForSparePart")]
        [HttpPost]
        public IHttpActionResult GetStoreListForSparePart()
        {
            const string storedProcedureName = "[CMMS].[prc_GetStoreListForSparePart]";

            var result =
                _businessManager.CallStoredProcedure<OutputGetStoreListForSparePart[]>(storedProcedureName);

            return Ok(result);
        }

        /// <summary>
        ///     دریافت لیست ماشین ها
        /// </summary>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetTopMachineListByCondition")]
        [HttpPost]
        public IHttpActionResult GetTopMachineListByCondition(InputGetTopMachineListByCondition input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetTopMachineListByCondition]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetTopMachineListByCondition, OutputGetTopMachineListByCondition[]>(
                        storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        ///     حذف فرد از دستگاه
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("RemoveAssignMachineToEmployee")]
        [HttpPost]
        [JwtValidation]
        [AccessToEventValidation(EventCode = "004", FormCode = "002")]
        public IHttpActionResult RemoveAssignMachineToEmployee(InputRemoveAssignMachineToEmployee input)
        {
            const string storedProcedureName = "[CMMS].[prc_RemoveAssignMachineToEmployee]";

            _businessManager.CallStoredProcedure(storedProcedureName, input);

            return Ok("با موفقیت انجام شد");
        }

        /// <summary>
        ///     حذف آپلود ضمیمه های ماشین
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("RemoveMachineDocumentByMachineDocumentId")]
        [HttpPost]
        [JwtValidation]
        [AccessToEventValidation(EventCode = "003", FormCode = "002")]
        public IHttpActionResult RemoveMachineDocumentByMachineDocumentId(
            InputRemoveMachineDocumentByMachineDocumentId input)
        {
            const string storedProcedureName = "[CMMS].[prc_RemoveMachineDocumentByMachineDocumentID]";

            _businessManager.CallStoredProcedure(storedProcedureName, input);

            return Ok("با موفقیت انجام شد");
        }

        /// <summary>
        ///     حذف قطعات یدکی ماشین بر اساس شناسه جدول قطعات یدکی ماشین
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("RemoveMachineSparePartByMachineSparePartId")]
        [HttpPost]
        [JwtValidation]
        [AccessToEventValidation(EventCode = "005", FormCode = "002")]
        public IHttpActionResult RemoveMachineSparePartByMachineSparePartId(
            InputRemoveMachineSparePartByMachineSparePartId input)
        {
            const string storedProcedureName = "[CMMS].[prc_RemoveMachineSparePartByMachineSparePartID]";

            _businessManager.CallStoredProcedure(storedProcedureName, input);

            return Ok("با موفقیت انجام شد");
        }

        /// <summary>
        ///     حذف آپلود ضمیمه های قطعات یدکی ماشین
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("RemoveMachineSparePartDocumentByMachineSparePartDocumentId")]
        [HttpPost]
        [JwtValidation]
        [AccessToEventValidation(EventCode = "005", FormCode = "002")]
        public IHttpActionResult RemoveMachineSparePartDocumentByMachineSparePartDocumentId(
            InputRemoveMachineSparePartDocumentByMachineSparePartDocumentId input)
        {
            const string storedProcedureName =
                "[CMMS].[prc_RemoveMachineSparePartDocumentByMachineSparePartDocumentID]";

            _businessManager.CallStoredProcedure(storedProcedureName, input);

            return Ok("با موفقیت انجام شد");
        }
    }
}