using System.Threading.Tasks;
using Motorsazan.CMMS.Shared.Models.Input.EmployeeCostReport;
using Motorsazan.CMMS.Shared.Models.Output.EmployeeCostReport;

namespace Motorsazan.CMMS.Client.Api
{
    public static partial class ApiList
    {
        public static OutputGetEmployeeCostReportByCondition[] GetEmployeeCostReportByCondition(
            InputGetEmployeeCostReportByCondition values)
        {
            var url = $"{BaseUrl}/EmployeeCostReport/";
            const string methodName = nameof(GetEmployeeCostReportByCondition);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetEmployeeCostReportByCondition[]>.Post(
                        url,
                        methodName, parameters: values)
            );

            return task.GetAwaiter().GetResult();
        }
    }
}