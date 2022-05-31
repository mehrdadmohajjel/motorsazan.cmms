using System.Threading.Tasks;
using Motorsazan.CMMS.Shared.Models.Input.OEEReport;
using Motorsazan.CMMS.Shared.Models.Output.OEEReport;

namespace Motorsazan.CMMS.Client.Api
{
    public static partial class ApiList
    {
        public static OutputGetOEEReportByCondition[] GetDepartmentOEEReportByCondition(
            InputGetDepartmentOEEReportByCondition values)
        {
            var url = $"{BaseUrl}/OEEReport/";
            const string methodName = nameof(GetDepartmentOEEReportByCondition);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetOEEReportByCondition[]>.Post(
                        url,
                        methodName, parameters: values)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetOEEReportByCondition[] GetMachineOEEReportByCondition(
            InputGetMachineOEEReportByCondition values)
        {
            var url = $"{BaseUrl}/OEEReport/";
            const string methodName = nameof(GetMachineOEEReportByCondition);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetOEEReportByCondition[]>.Post(
                        url,
                        methodName, parameters: values)
            );

            return task.GetAwaiter().GetResult();
        }


        public static OutputGetOEEReportByCondition[] GetOEEReportByCondition(
            InputGetOEEReportByCondition values)
        {
            var url = $"{BaseUrl}/OEEReport/";
            const string methodName = nameof(GetOEEReportByCondition);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetOEEReportByCondition[]>.Post(
                        url,
                        methodName, parameters: values)
            );

            return task.GetAwaiter().GetResult();
        }
    }
}