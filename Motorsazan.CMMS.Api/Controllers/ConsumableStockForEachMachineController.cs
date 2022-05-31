using Motorsazan.CMMS.Api.Business;
using Motorsazan.CMMS.Api.Filters;
using Motorsazan.CMMS.Shared.Models.Input.ConsumableStockForEachMachineByCondition;
using Motorsazan.CMMS.Shared.Models.Output.ConsumableStockForEachMachineByCondition;
using System.Web.Http;

namespace Motorsazan.CMMS.Api.Controllers
{
    [RoutePrefix("ConsumableStockForEachMachine")]
    public class ConsumableStockForEachMachineController: ApiController
    {
        private readonly BusinessManager _businessManager = new BusinessManager();

        /// <summary>
        ///    دریافت گزارش لیست قطعات مصرفی به تفکیک هر ماشین
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequestModelNullValidation]
        [RequestModelValidation]
        [Route("GetConsumableStockForEachMachineByCondition")]
        [HttpPost]
        public IHttpActionResult GetConsumableStockForEachMachineByCondition(
            InputGetConsumableStockForEachMachineByCondition input)
        {
            const string storedProcedureName = "[CMMS].[prc_GetConSumableStockForEachMachineByCondition]";

            var result =
                _businessManager
                    .CallStoredProcedure<InputGetConsumableStockForEachMachineByCondition,
                        OutputGetConsumableStockForEachMachineByCondition[]>(
                        storedProcedureName, input);

            return Ok(result);
        }
    }
}