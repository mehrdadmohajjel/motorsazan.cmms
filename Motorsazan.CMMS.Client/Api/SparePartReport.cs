using Motorsazan.CMMS.Shared.Models.Input.SparePartReport;
using Motorsazan.CMMS.Shared.Models.Output.SparePartReport;
using System.Threading.Tasks;

namespace Motorsazan.CMMS.Client.Api
{
    public static partial class ApiList
    {
        public static OutputGetMachineSparePartReportByMachineId[] GetMachineSparePartReportByMachineId(
    InputGetMachineSparePartReportByMachineId values)
        {
            var url = $"{BaseUrl}/SparePartReport/";
            const string methodName = nameof(GetMachineSparePartReportByMachineId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetMachineSparePartReportByMachineId[]>.Post(
                        url,
                        methodName, parameters: values)
            );

            return task.GetAwaiter().GetResult();
        }
    }
}