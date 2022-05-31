using System.Web.Http;
using Motorsazan.CMMS.Api.Business;
using Motorsazan.CMMS.Api.Filters;
using Motorsazan.CMMS.Shared.Models.Input.Machine;
using Motorsazan.CMMS.Shared.Models.Output.Machine;

namespace Motorsazan.CMMS.Api.Controllers
{
    [RoutePrefix("Machine")]
    public class MachineController: ApiController
    {
        private readonly BusinessManager _businessManager = new BusinessManager();

        /// <summary>
        ///     وب سرویس افزودن دستگاه جدید
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("AddTopMachine")]
        [HttpPost]
        [JwtValidation]
        [AccessToEventValidation(EventCode = "002", FormCode = "001")]
        public IHttpActionResult AddTopMachine(InputAddTopMachine input)
        {
            const string storedProcedureName = "[CMMS].[prc_AddTopMachine]";

            _businessManager.CallStoredProcedure(storedProcedureName, input);

            return Ok("ماشین جدید با موفقیت ثبت شد");
        }

        /// <summary>
        ///     دریافت لسیت نوع کنترل
        /// </summary>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetControlSystemTypeList")]
        [HttpPost]
        public IHttpActionResult GetControlSystemTypeList()
        {
            const string storedProcedureName = "[CMMS].[prc_GetControlSystemTypeList]";

            var result =
                _businessManager
                    .CallStoredProcedure<OutputGetControlSystemTypeList[]>(
                        storedProcedureName);

            return Ok(result);
        }

        /// <summary>
        ///     دریافت لسیت سطح دستگاه
        /// </summary>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetMachineLevelList")]
        [HttpPost]
        public IHttpActionResult GetMachineLevelList()
        {
            const string storedProcedureName = "[CMMS].[prc_GetMachineLevelList]";

            var result =
                _businessManager
                    .CallStoredProcedure<OutputGetMachineLevelList[]>(
                        storedProcedureName);

            return Ok(result);
        }

        /// <summary>
        ///     دریافت لسیت نوع دستگاه
        /// </summary>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetMachineTypeList")]
        [HttpPost]
        public IHttpActionResult GetMachineTypeList()
        {
            const string storedProcedureName = "[CMMS].[prc_GetMachineTypeList]";

            var result =
                _businessManager
                    .CallStoredProcedure<OutputGetMachineTypeList[]>(
                        storedProcedureName);

            return Ok(result);
        }
    }
}