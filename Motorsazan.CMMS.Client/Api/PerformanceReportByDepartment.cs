using System.Threading.Tasks;
using Motorsazan.CMMS.Shared.Models.Input.PerformanceReportByDepartment;
using Motorsazan.CMMS.Shared.Models.Output.PerformanceReportByDepartment;

namespace Motorsazan.CMMS.Client.Api
{
    public static partial class ApiList
    {
        public static OutputGetDepartmentPerformanceReportByCondition[] GetDepartmentPerformanceReportByCondition(
            InputGetDepartmentPerformanceReportByCondition values)
        {
            var url = $"{BaseUrl}/PerformanceReportByDepartment/";
            const string methodName = nameof(GetDepartmentPerformanceReportByCondition);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetDepartmentPerformanceReportByCondition[]>.Post(
                        url,
                        methodName, parameters: values)
            );

            return task.GetAwaiter().GetResult();
        }
    }
}