using System.Threading.Tasks;
using Motorsazan.CMMS.Shared.Models.Input.PerformanceReportByMachine;
using Motorsazan.CMMS.Shared.Models.Output.PerformanceReportByMachine;

namespace Motorsazan.CMMS.Client.Api
{
    public static partial class ApiList
    {
        public static OutputGetMainMachinePerformanceReportByCondition[] GetMainMachinePerformanceReportByCondition(
            InputGetMainMachinePerformanceReportByCondition values)
        {
            var url = $"{BaseUrl}/PerformanceReportByMachine/";
            const string methodName = nameof(GetMainMachinePerformanceReportByCondition);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetMainMachinePerformanceReportByCondition[]>.Post(
                        url,
                        methodName, parameters: values)
            );

            return task.GetAwaiter().GetResult();
        }
    }
}