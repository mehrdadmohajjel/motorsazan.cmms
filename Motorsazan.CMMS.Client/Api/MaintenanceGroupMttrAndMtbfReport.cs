using Motorsazan.CMMS.Shared.Models.Input.MaintenanceGroupMttrAndMtbfReport;
using Motorsazan.CMMS.Shared.Models.Output.MaintenanceGroupMttrAndMtbfReport;
using System.Threading.Tasks;

namespace Motorsazan.CMMS.Client.Api
{
    public static partial class ApiList
    {
        public static OutputGetMaintenanceGroupMttrAndMtbfReportByCondition[] GetMaintenanceGroupMttrAndMtbfReportByCondition(
            InputGetMaintenanceGroupMttrAndMtbfReportByCondition values, string token)
        {
            var url = $"{BaseUrl}/MaintenanceGroupMttrAndMtbfReport/";
            const string methodName = nameof(GetMaintenanceGroupMttrAndMtbfReportByCondition);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetMaintenanceGroupMttrAndMtbfReportByCondition[]>.Post(
                        url,
                        methodName, token, values)
            );

            return task.GetAwaiter().GetResult();
        }
    }
}