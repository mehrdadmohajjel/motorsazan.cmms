using System.Web.Http;
using Motorsazan.CMMS.Api.Business;
using Motorsazan.CMMS.Api.Filters;
using Motorsazan.CMMS.Shared.Models.Input.Shared;
using Motorsazan.CMMS.Shared.Models.Output.Shared;

namespace Motorsazan.CMMS.Api.Controllers
{
    [RoutePrefix("Shared")]
    public class SharedController: ApiController
    {
        private readonly BusinessManager _businessManager = new BusinessManager();

        /// <summary>
        //لیست قطعات کلاس 73
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetClass73AllFinalCodeListByRackCodeGroup")]
        [RequestModelNullValidation]
        [RequestModelValidation]
        public IHttpActionResult GetClass73AllFinalCodeListByRackCodeGroup(InputGetClass73AllFinalCodeListByRackCodeGroup input)
        {
            const string storedProcedureName = "[Codding].[prc_GetClass73AllFinalCodeListByRackCodeGroup]";

            var result =
                _businessManager.CallStoredProcedure<InputGetClass73AllFinalCodeListByRackCodeGroup, OutputGetClass73AllFinalCodeListByRackCodeGroup>(storedProcedureName, input);

            return Ok(result);

        }


        /// <summary>
        ///  انتخاب قطعه
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetFinalCodeRangeByRackCode")]
        [RequestModelNullValidation]
        [RequestModelValidation]
        public IHttpActionResult GetFinalCodeRangeByRackCode(InputGetFinalCodeRangeByRackCode input)
        {
            const string storedProcedureName = "[Codding].[prc_GetSpecialClassAllFinalCodeListByRackCodeGroup]";

            var result =
                _businessManager.CallStoredProcedure<InputGetFinalCodeRangeByRackCode, OutputGetFinalCodeRangeByRackCode[]>(storedProcedureName, input);

            return Ok(result);

        }
    }
}