using Motorsazan.CMMS.Shared.Models.Input.MachineMttrMtbfReport;
using Motorsazan.CMMS.Shared.Models.Output.MachineMttrMtbfReport;
using System.Threading.Tasks;

namespace Motorsazan.CMMS.Client.Api
{
    public static partial class ApiList
    {
        public static OutputGetMachineMTTRAndMTBFReportByCondition[] GetMachineMttrAndMtbfReportByCondition(
            InputGetMachineMTTRAndMTBFReportByCondition values, string token)
        {
            var url = $"{BaseUrl}/MachineMttrMtbfReport/";
            const string methodName = nameof(GetMachineMttrAndMtbfReportByCondition);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetMachineMTTRAndMTBFReportByCondition[]>.Post(
                        url,
                        methodName, token, values)
            );

            return task.GetAwaiter().GetResult();
        }
        public static OutputGetTotalMttrAndMtbfByMachineId[] GetTotalMttrAndMtbfByMachineId(
            InputGetTotalMttrAndMtbfByMachineId values)
        {
            var url = $"{BaseUrl}/MachineMttrMtbfReport/";
            const string methodName = nameof(GetTotalMttrAndMtbfByMachineId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetTotalMttrAndMtbfByMachineId[]>.Post(
                        url,
                        methodName, token:null, values)
            );

            return task.GetAwaiter().GetResult();
        }
        
    }
}