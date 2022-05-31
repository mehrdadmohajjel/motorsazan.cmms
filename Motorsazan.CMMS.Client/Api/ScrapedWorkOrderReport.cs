using Motorsazan.CMMS.Shared.Models.Input.ScrapedWorkOrderReport;
using Motorsazan.CMMS.Shared.Models.Output.ScrapedWorkOrderReport;
using System.Threading.Tasks;

namespace Motorsazan.CMMS.Client.Api
{
    public static partial class ApiList
    {
        public static OutputGetScrapedWorkOrderReportByCondition[] GetScrapedWorkOrderReportByCondition(
            InputGetScrapedWorkOrderReportByCondition values)
        {
            var url = $"{BaseUrl}/ScrapedWorkOrderReport/";
            const string methodName = nameof(GetScrapedWorkOrderReportByCondition);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetScrapedWorkOrderReportByCondition[]>.Post(
                        url,
                        methodName, parameters: values)
            );

            return task.GetAwaiter().GetResult();
        }
    }
}