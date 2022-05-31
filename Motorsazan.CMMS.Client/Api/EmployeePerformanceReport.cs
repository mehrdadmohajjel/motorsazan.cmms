using System.Threading.Tasks;
using Motorsazan.CMMS.Shared.Models.Input.EmployeePerformanceReport;
using Motorsazan.CMMS.Shared.Models.Output.EmployeePerformanceReport;

namespace Motorsazan.CMMS.Client.Api
{
    public static partial class ApiList
    {
        public static OutputGetAllMaintenanceGroupMemberList[] GetAllMaintenanceGroupMemberList()
        {
            var url = $"{BaseUrl}/EmployeePerformanceReport/";
            const string methodName = nameof(GetAllMaintenanceGroupMemberList);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetAllMaintenanceGroupMemberList[]>.Post(
                        url,
                        methodName, parameters: null)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetEmployeePerformanceReportByCondition[] GetEmployeePerformanceReportByCondition(
            InputGetEmployeePerformanceReportByCondition values)
        {
            var url = $"{BaseUrl}/EmployeePerformanceReport/";
            const string methodName = nameof(GetEmployeePerformanceReportByCondition);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetEmployeePerformanceReportByCondition[]>.Post(
                        url,
                        methodName, parameters: values)
            );

            return task.GetAwaiter().GetResult();
        }
    }
}