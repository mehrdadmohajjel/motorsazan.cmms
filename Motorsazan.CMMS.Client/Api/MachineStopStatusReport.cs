using Motorsazan.CMMS.Shared.Models.Input.MachineStopStatusReport;
using Motorsazan.CMMS.Shared.Models.Output.MachineStopStatusReport;
using System.Threading.Tasks;

namespace Motorsazan.CMMS.Client.Api
{
    public partial class ApiList
    {
        public static OutputGetMachineStopStatusReportList[] GetMachineStopStatusReportList()
        {
            var url = $"{BaseUrl}/MachineStopStatusReport/";
            const string methodName = nameof(GetMachineStopStatusReportList);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetMachineStopStatusReportList[]>.Post(
                        url,
                        methodName, token: null, parameters: null)
            );

            return task.GetAwaiter().GetResult();

        }

        public static OutputGetWorkOrderListByMachineId[] GetWorkOrderListByMachineId(InputGetWorkOrderListByMachineId values)
        {

            var url = $"{BaseUrl}/MachineStopStatusReport/";
            const string methodName = nameof(GetWorkOrderListByMachineId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetWorkOrderListByMachineId[]>.Post(
                        url,
                        methodName, parameters: values)
            );

            return task.GetAwaiter().GetResult();

        }

    }
}