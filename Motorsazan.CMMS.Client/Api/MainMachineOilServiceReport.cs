using System.Threading.Tasks;
using Motorsazan.CMMS.Shared.Models.Input.MainMachineOilServiceReport;
using Motorsazan.CMMS.Shared.Models.Output.MainMachineOilServiceReport;

namespace Motorsazan.CMMS.Client.Api
{
    public static partial class ApiList
    {
        public static OutputGetMainMachineOilServiceReportByCondition[] GetMainMachineOilServiceReportByCondition(
            InputGetMainMachineOilServiceReportByCondition values)
        {
            var url = $"{BaseUrl}/MainMachineOilServiceReport/";
            const string methodName = nameof(GetMainMachineOilServiceReportByCondition);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetMainMachineOilServiceReportByCondition[]>.Post(
                        url,
                        methodName, parameters: values)
            );

            return task.GetAwaiter().GetResult();
        }
    }
}