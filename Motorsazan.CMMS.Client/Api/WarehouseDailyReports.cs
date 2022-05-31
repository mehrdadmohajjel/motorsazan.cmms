using System.Threading.Tasks;
using Motorsazan.CMMS.Shared.Models.Input.WarehouseDailyReports;
using Motorsazan.CMMS.Shared.Models.Output.WarehouseDailyReports;

namespace Motorsazan.CMMS.Client.Api
{
    public static partial class ApiList
    {
        public static OutputGetDailyWearhouseReportByCondition[] GetDailyWearhouseReportByCondition(
            InputGetDailyWearhouseReportByCondition values)
        {
            var url = $"{BaseUrl}/WarehouseDailyReports/";
            const string methodName = nameof(GetDailyWearhouseReportByCondition);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetDailyWearhouseReportByCondition[]>.Post(
                        url,
                        methodName, parameters: values)
            );

            return task.GetAwaiter().GetResult();
        }
    }
}