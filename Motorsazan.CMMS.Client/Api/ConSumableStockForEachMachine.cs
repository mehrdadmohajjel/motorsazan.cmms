using Motorsazan.CMMS.Shared.Models.Input.ConsumableStockForEachMachineByCondition;
using Motorsazan.CMMS.Shared.Models.Output.ConsumableStockForEachMachineByCondition;
using System.Threading.Tasks;

namespace Motorsazan.CMMS.Client.Api
{
    public static partial class ApiList
    {
        public static OutputGetConsumableStockForEachMachineByCondition[] GetConsumableStockForEachMachineByCondition(
    InputGetConsumableStockForEachMachineByCondition values)
        {
            var url = $"{BaseUrl}/ConsumableStockForEachMachine/";
            const string methodName = nameof(GetConsumableStockForEachMachineByCondition);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetConsumableStockForEachMachineByCondition[]>.Post(
                        url,
                        methodName, parameters: values)
            );

            return task.GetAwaiter().GetResult();
        }

    }
}