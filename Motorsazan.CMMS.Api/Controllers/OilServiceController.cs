using System.Web.Http;
using Motorsazan.CMMS.Api.Business;
using Motorsazan.CMMS.Api.Filters;
using Motorsazan.CMMS.Shared.Models.Input.Inspection;
using Motorsazan.CMMS.Shared.Models.Input.OilService;
using Motorsazan.CMMS.Shared.Models.Output.Inspection;
using Motorsazan.CMMS.Shared.Models.Output.OilService;

namespace Motorsazan.CMMS.Api.Controllers
{
    [RoutePrefix("OilService")]
    public class OilServiceController: ApiController
    {
        private readonly BusinessManager _businessManager = new BusinessManager();

        /// <summary>
        ///     ثبت سرویس روغنکاری
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("AddOilService")]
        [HttpPost]
        [JwtValidation]
        public IHttpActionResult AddOilService(InputAddOilService input)
        {
            const string storedProcedureName = "[CMMS].[prc_AddOilService]";

            _businessManager.CallStoredProcedure(storedProcedureName, input);

            return Ok("با موفقیت انجام شد");
        }



        /// <summary>
        ///     دریافت لیست مواد
        /// </summary>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetMaterials")]
        [HttpPost]
        public IHttpActionResult GetMaterials()
        {
            const string storedProcedureName = "[CMMS].[prc_GetMaterials]";

            var result =
                _businessManager
                    .CallStoredProcedure<OutputGetMaterials[]>(
                        storedProcedureName);

            return Ok(result);
        }

        /// <summary>
        ///     دریافت لیست واحد اندازه گیری
        /// </summary>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetMeasurementUnitList")]
        [HttpPost]
        public IHttpActionResult GetMeasurementUnitList()
        {
            const string storedProcedureName = "[CMMS].[prc_GetMeasurementUnitList]";

            var result =
                _businessManager
                    .CallStoredProcedure<OutputGetMeasurementUnitList[]>(
                        storedProcedureName);

            return Ok(result);
        }

        /// <summary>
        ///     دریافت لیست سرویس روغنکاری
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetOilServiceListByCondition")]
        [HttpPost]
        public IHttpActionResult GetOilServiceListByCondition(InputGetOilServiceListByCondition input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetOilServiceListByCondition]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetOilServiceListByCondition, OutputGetOilServiceListByCondition[]>(
                        storedProcedureName, input);

            return Ok(result);
        }

        /// <summary>
        ///     دریافت لسیت محل روغنکاری
        /// </summary>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetOilServicePlaceList")]
        [HttpPost]
        public IHttpActionResult GetOilServicePlaceList()
        {
            const string storedProcedureName = "[CMMS].[prc_GetOilServicePlaceList]";

            var result =
                _businessManager
                    .CallStoredProcedure<OutputGetOilServicePlaceList[]>(
                        storedProcedureName);

            return Ok(result);
        }

        /// <summary>
        ///     دریافت لست اعضای گروه عملیاتی سرویس
        /// </summary>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetServiceMaintenanceGroupMemberList")]
        [HttpPost]
        public IHttpActionResult GetServiceMaintenanceGroupMemberList()
        {
            const string storedProcedureName = "[CMMS].[prc_GetServiceMaintenanceGroupMemberList]";

            var result =
                _businessManager
                    .CallStoredProcedure<OutputGetServiceMaintenanceGroupMemberList[]>(
                        storedProcedureName);

            return Ok(result);
        }

        /// <summary>
        ///     حذف بازرسی
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("RemoveOilService")]
        [HttpPost]
        [JwtValidation]
        public IHttpActionResult RemoveOilService(InputRemoveOilService input)
        {
            const string storedProcedureName = "[CMMS].[prc_RemoveOilService]";

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