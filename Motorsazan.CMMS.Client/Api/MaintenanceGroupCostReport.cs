using System.Threading.Tasks;
using Motorsazan.CMMS.Shared.Models.Input.MaintenanceGroupCostReport;
using Motorsazan.CMMS.Shared.Models.Output.MaintenanceGroupCostReport;

namespace Motorsazan.CMMS.Client.Api
{
    public static partial class ApiList
    {
        public static OutputGetMaintenanceGroupCostReportByCondition[] GetMaintenanceGroupCostReportByCondition(
            InputGetMaintenanceGroupCostReportByCondition values)
        {
            var url = $"{BaseUrl}/MaintenanceGroupCostReport/";
            const string methodName = nameof(GetMaintenanceGroupCostReportByCondition);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetMaintenanceGroupCostReportByCondition[]>.Post(
                        url,
                        methodName, parameters: values)
            );

            return task.GetAwaiter().GetResult();
        }
    }
}