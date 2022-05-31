using System.Threading.Tasks;
using Motorsazan.CMMS.Shared.Models.Input.DailyRepairsReports;
using Motorsazan.CMMS.Shared.Models.Output.DailyRepairsReports;

namespace Motorsazan.CMMS.Client.Api
{
    public static partial class ApiList
    {
        public static OutputGetMaintenanceDailyReportByCondition[] GetMaintenanceDailyReportByCondition(
            InputGetMaintenanceDailyReportByCondition values)
        {
            var url = $"{BaseUrl}/DailyRepairsReports/";
            const string methodName = nameof(GetMaintenanceDailyReportByCondition);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetMaintenanceDailyReportByCondition[]>.Post(
                        url,
                        methodName, parameters: values)
            );

            return task.GetAwaiter().GetResult();
        }
    }
}