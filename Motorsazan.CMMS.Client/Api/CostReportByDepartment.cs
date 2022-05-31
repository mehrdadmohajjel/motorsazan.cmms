using System.Threading.Tasks;
using Motorsazan.CMMS.Shared.Models.Input.CostReportByDepartment;
using Motorsazan.CMMS.Shared.Models.Output.CostReportByDepartment;

namespace Motorsazan.CMMS.Client.Api
{
    public static partial class ApiList
    {
        public static OutputGetDepartmentCostReportByCondition[] GetDepartmentCostReportByCondition(
            InputGetDepartmentCostReportByCondition values)
        {
            var url = $"{BaseUrl}/CostReportByDepartment/";
            const string methodName = nameof(GetDepartmentCostReportByCondition);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetDepartmentCostReportByCondition[]>.Post(
                        url,
                        methodName, parameters: values)
            );

            return task.GetAwaiter().GetResult();
        }
    }
}