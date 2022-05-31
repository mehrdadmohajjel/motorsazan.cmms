using Motorsazan.CMMS.Shared.Models.Input.PLCReport;
using Motorsazan.CMMS.Shared.Models.Output.PLCReport;
using System.Threading.Tasks;

namespace Motorsazan.CMMS.Client.Api
{
    public static partial class ApiList
    {
        public static OutputChangeJobStatus ChangeJobStatusAsync(InputChangeJobStatus input)
        {
            const string url = "http://erp-server:2019/WebUI/default.aspx/";
            const string methodName = "ChangeJobStatus";

            var task = Task.Run(async () =>
                await ApiConnector<OutputChangeJobStatus>.Post(url, methodName, parameters: input));

            return task.GetAwaiter().GetResult();
        }


        public static OutputGetMachinePLCFileList[] GetMachinePLCFileList()
        {
            var url = $"{BaseUrl}/PLCReport/";
            const string methodName = nameof(GetMachinePLCFileList);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetMachinePLCFileList[]>.Post(
                        url,
                        methodName, parameters: null)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetPlcMachineAvailabilityByMachineId[] GetPLCMachineAvailabilityByMachineId(
            InputGetPlcMachineAvailabilityByMachineId values)
        {
            var url = $"{BaseUrl}/PLCReport/";
            const string methodName = nameof(GetPLCMachineAvailabilityByMachineId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetPlcMachineAvailabilityByMachineId[]>.Post(
                        url,
                        methodName, parameters: values)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetPlcMachineComparisonBetweenToolsAndPalletChangerChartReportByMachineId[]
            GetPlcMachineComparisonBetweenToolsAndPalletChangerChartReportByMachineId(
                InputGetPlcMachineComparisonBetweenToolsAndPalletChangerChartReportByMachineId values)
        {
            var url = $"{BaseUrl}/PLCReport/";
            const string methodName = nameof(GetPlcMachineComparisonBetweenToolsAndPalletChangerChartReportByMachineId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetPlcMachineComparisonBetweenToolsAndPalletChangerChartReportByMachineId[]
                    >.Post(
                        url,
                        methodName, parameters: values)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetPlcMachineOilPressureChartReportByMachineId[]
            GetPlcMachineOilPressureChartReportByMachineId(
                InputGetPlcMachineOilPressureChartReportByMachineId values)
        {
            var url = $"{BaseUrl}/PLCReport/";
            const string methodName = nameof(GetPlcMachineOilPressureChartReportByMachineId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetPlcMachineOilPressureChartReportByMachineId[]>.Post(
                        url,
                        methodName, parameters: values)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetPlcMachineOilTemperatureChartReportByMachineId[]
            GetPlcMachineOilTemperatureChartReportByMachineId(
                InputGetPlcMachineOilTemperatureChartReportByMachineId values)
        {
            var url = $"{BaseUrl}/PLCReport/";
            const string methodName = nameof(GetPlcMachineOilTemperatureChartReportByMachineId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetPlcMachineOilTemperatureChartReportByMachineId[]>.Post(
                        url,
                        methodName, parameters: values)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetPlcMachinePalletChangerCountChartReportByMachineId[]
            GetPlcMachinePalletChangerCountChartReportByMachineId(
                InputGetPlcMachinePalletChangerCountChartReportByMachineId values)
        {
            var url = $"{BaseUrl}/PLCReport/";
            const string methodName = nameof(GetPlcMachinePalletChangerCountChartReportByMachineId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetPlcMachinePalletChangerCountChartReportByMachineId[]>.Post(
                        url,
                        methodName, parameters: values)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetPlcMachinePowerChangerChartReportByMachineId[]
            GetPlcMachinePowerChangerChartReportByMachineId(
                InputGetPlcMachinePowerChangerChartReportByMachineId values)
        {
            var url = $"{BaseUrl}/PLCReport/";
            const string methodName = nameof(GetPlcMachinePowerChangerChartReportByMachineId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetPlcMachinePowerChangerChartReportByMachineId[]>.Post(
                        url,
                        methodName, parameters: values)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetPlcMachineSpindlePerformanceReportByMachineId[]
            GetPlcMachineSpindlePerformanceReportByMachineId(
                InputGetPlcMachineSpindlePerformanceReportByMachineId values)
        {
            var url = $"{BaseUrl}/PLCReport/";
            const string methodName = nameof(GetPlcMachineSpindlePerformanceReportByMachineId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetPlcMachineSpindlePerformanceReportByMachineId[]>.Post(
                        url,
                        methodName, parameters: values)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetPlcMachineToolsChangerChartReportByMachineId[]
            GetPlcMachineToolsChangerChartReportByMachineId(
                InputGetPlcMachineToolsChangerChartReportByMachineId values)
        {
            var url = $"{BaseUrl}/PLCReport/";
            const string methodName = nameof(GetPlcMachineToolsChangerChartReportByMachineId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetPlcMachineToolsChangerChartReportByMachineId[]>.Post(
                        url,
                        methodName, parameters: values)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetToolsChangerDetailOnSpindle[]
            GetToolsChangerDetailOnSpindle(
                InputGetToolsChangerDetailOnSpindle values)
        {
            var url = $"{BaseUrl}/PLCReport/";
            const string methodName = nameof(GetToolsChangerDetailOnSpindle);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetToolsChangerDetailOnSpindle[]>.Post(
                        url,
                        methodName, parameters: values)
            );

            return task.GetAwaiter().GetResult();
        }

        public static string ResetMachinePlcFileDataTransferJob(
        )
        {
            var url = $"{BaseUrl}/PLCReport/";
            const string methodName = nameof(ResetMachinePlcFileDataTransferJob);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, parameters: null)
            );

            return task.GetAwaiter().GetResult();
        }
    }
}