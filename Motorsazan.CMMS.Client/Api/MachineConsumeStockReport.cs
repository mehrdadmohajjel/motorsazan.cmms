using Motorsazan.CMMS.Shared.Models.Input.MachineConsumeStockReport;
using Motorsazan.CMMS.Shared.Models.Output.MachineConsumeStockReport;
using System.Threading.Tasks;

namespace Motorsazan.CMMS.Client.Api
{
    public static partial class ApiList
    {
        public static OutputGetStockFromHavaleWorkOrderReferral[] GetStockFromHavaleWorkOrderReferral()
        {
            var url = $"{BaseUrl}/MachineConsumeStockReport/";
            const string methodName = nameof(GetStockFromHavaleWorkOrderReferral);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetStockFromHavaleWorkOrderReferral[]>.Post(
                        url,
                        methodName, parameters: null, token: null)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetMachineConsumeStockReportByCondition[] GetMachineConsumeStockReportByCondition(
    InputGetMachineConsumeStockReportByCondition values)
        {
            var url = $"{BaseUrl}/MachineConsumeStockReport/";
            const string methodName = nameof(GetMachineConsumeStockReportByCondition);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetMachineConsumeStockReportByCondition[]>.Post(
                        url,
                        methodName, parameters: values)
            );

            return task.GetAwaiter().GetResult();
        }
    }
}