using System.Threading.Tasks;
using Motorsazan.CMMS.Shared.Models.Input.OCEReport;
using Motorsazan.CMMS.Shared.Models.Output.OCEReport;

namespace Motorsazan.CMMS.Client.Api
{
    public static partial class ApiList
    {
        public static OutputGetOCEReportByCondition[] GetOCEReportByCondition(
            InputGetOCEReportByCondition values)
        {
            var url = $"{BaseUrl}/OCEReport/";
            const string methodName = nameof(GetOCEReportByCondition);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetOCEReportByCondition[]>.Post(
                        url,
                        methodName, parameters: values)
            );

            return task.GetAwaiter().GetResult();
        }
    }
}