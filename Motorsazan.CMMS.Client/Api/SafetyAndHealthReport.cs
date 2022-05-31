using Motorsazan.CMMS.Shared.Models.Input.SafetyAndHealthReport;
using Motorsazan.CMMS.Shared.Models.Output.SafetyAndHealthReport;
using System.Threading.Tasks;

namespace Motorsazan.CMMS.Client.Api
{
    public static partial class ApiList
    {
        public static OutputGetSafetyAndHealthReportByCondition[] GetSafetyAndHealthReportByCondition(
            InputGetSafetyAndHealthReportByCondition values)
        {
            var url = $"{BaseUrl}/SafetyAndHealthReport/";
            const string methodName = nameof(GetSafetyAndHealthReportByCondition);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetSafetyAndHealthReportByCondition[]>.Post(
                        url,
                        methodName, parameters: values)
            );

            return task.GetAwaiter().GetResult();
        }
    }
}