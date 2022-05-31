using System.Threading.Tasks;
using Motorsazan.CMMS.Shared.Models.Input.MaintenanceGroupPerformanceReport;
using Motorsazan.CMMS.Shared.Models.Output.MaintenanceGroupPerformanceReport;

namespace Motorsazan.CMMS.Client.Api
{
    public static partial class ApiList
    {
        public static OutputGetMaintenanceGroupPerformanceReportByCondition[]
            GetMaintenanceGroupPerformanceReportByCondition(
                InputGetMaintenanceGroupPerformanceReportByCondition values)
        {
            var url = $"{BaseUrl}/MaintenanceGroupPerformanceReport/";
            const string methodName = nameof(GetMaintenanceGroupPerformanceReportByCondition);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetMaintenanceGroupPerformanceReportByCondition[]>.Post(
                        url,
                        methodName, parameters: values)
            );

            return task.GetAwaiter().GetResult();
        }
    }
}