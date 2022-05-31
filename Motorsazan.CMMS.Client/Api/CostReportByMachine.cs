using System.Threading.Tasks;
using Motorsazan.CMMS.Shared.Models.Input.CostReportByMachine;
using Motorsazan.CMMS.Shared.Models.Output.CostReportByMachine;

namespace Motorsazan.CMMS.Client.Api
{
    public static partial class ApiList
    {
        public static OutputGetMainMachineCostReportByCondition[] GetMainMachineCostReportByCondition(
            InputGetMainMachineCostReportByCondition values)
        {
            var url = $"{BaseUrl}/CostReportByMachine/";
            const string methodName = nameof(GetMainMachineCostReportByCondition);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetMainMachineCostReportByCondition[]>.Post(
                        url,
                        methodName, parameters: values)
            );

            return task.GetAwaiter().GetResult();
        }
    }
}