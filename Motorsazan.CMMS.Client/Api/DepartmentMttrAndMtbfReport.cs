using Motorsazan.CMMS.Shared.Models.Input.DepartmentMttrAndMtbfReport;
using Motorsazan.CMMS.Shared.Models.Output.DepartmentMttrAndMtbfReport;
using System.Threading.Tasks;

namespace Motorsazan.CMMS.Client.Api
{
    public static partial class ApiList
    {
        public static OutputGetDepartmentMttrAndMtbfReportByCondition[] GetDepartmentMttrAndMtbfReportByCondition(
            InputGetMTTRAndMTBFReportByCondition values, string token)
        {
            var url = $"{BaseUrl}/DepartmentMttrAndMtbfReport/";
            const string methodName = nameof(GetDepartmentMttrAndMtbfReportByCondition);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetDepartmentMttrAndMtbfReportByCondition[]>.Post(
                        url,
                        methodName, token, values)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetMTTRAndMTBFPerformanceCostReportByMachineId[]
            GetMttrAndMtbfPerformanceCostReportByMachineId(InputGetMTTRAndMTBFPerformanceCostReportByMachineId values)
        {
            var url = $"{BaseUrl}/DepartmentMttrAndMtbfReport/";
            const string methodName = nameof(GetMttrAndMtbfPerformanceCostReportByMachineId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetMTTRAndMTBFPerformanceCostReportByMachineId[]>.Post(
                        url,
                        methodName, null, values)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetMTTRAndMTBFPerformanceReportByMachineId[] GetMttrAndMtbfPerformanceReportByMachineId(
            InputGetMTTRAndMTBFPerformanceReportByMachineId values)
        {
            var url = $"{BaseUrl}/DepartmentMttrAndMtbfReport/";
            const string methodName = nameof(GetMttrAndMtbfPerformanceReportByMachineId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetMTTRAndMTBFPerformanceReportByMachineId[]>.Post(
                        url,
                        methodName, null, values)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetMTTRAndMTBFWearhouseReportByMachineId[] GetMttrAndMtbfWearhouseReportByMachineId(
            InputGetMTTRAndMTBFWearhouseReportByMachineId values)
        {
            var url = $"{BaseUrl}/DepartmentMttrAndMtbfReport/";
            const string methodName = nameof(GetMttrAndMtbfWearhouseReportByMachineId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetMTTRAndMTBFWearhouseReportByMachineId[]>.Post(
                        url,
                        methodName, null, values)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetMTTRAndMTBFMachineWorkOrderListByMachineId[]
            GetMttrAndMtbfMachineWorkOrderListByMachineId(InputGetMTTRAndMTBFMachineWorkOrderListByMachineId values)
        {
            var url = $"{BaseUrl}/DepartmentMttrAndMtbfReport/";
            const string methodName = nameof(GetMttrAndMtbfMachineWorkOrderListByMachineId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetMTTRAndMTBFMachineWorkOrderListByMachineId[]>.Post(
                        url,
                        methodName, null, values)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetMTTRChartReportByMachineId[]
            GetMttrChartReportByMachineId(InputGetMTTRChartReportByMachineId values)
        {
            var url = $"{BaseUrl}/DepartmentMttrAndMtbfReport/";
            const string methodName = nameof(GetMttrChartReportByMachineId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetMTTRChartReportByMachineId[]>.Post(
                        url,
                        methodName, null, values)
            );

            return task.GetAwaiter().GetResult();
        }


        public static OutputGetMTBFChartReportByMachineId[]
            GetMtbfChartReportByMachineId(InputGetMTBFChartReportByMachineId values)
        {
            var url = $"{BaseUrl}/DepartmentMttrAndMtbfReport/";
            const string methodName = nameof(GetMtbfChartReportByMachineId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetMTBFChartReportByMachineId[]>.Post(
                        url,
                        methodName, null, values)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetOeeChartReportOnMTTRAndMTBFByMachineId[]
            GetOeeChartReportOnMTTRAndMTBFByMachineId(InputGetOeeChartReportOnMTTRAndMTBFByMachineId values)
        {
            var url = $"{BaseUrl}/DepartmentMttrAndMtbfReport/";
            const string methodName = nameof(GetOeeChartReportOnMTTRAndMTBFByMachineId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetOeeChartReportOnMTTRAndMTBFByMachineId[]>.Post(
                        url,
                        methodName, null, values)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetOEEPrintReportOnMTTRAndMTBFByMachineId
            GetOEEPrintReportOnMTTRAndMTBFByMachineId(InputGetOEEPrintReportOnMTTRAndMTBFByMachineId values)
        {
            var url = $"{BaseUrl}/DepartmentMttrAndMtbfReport/";
            const string methodName = nameof(GetOEEPrintReportOnMTTRAndMTBFByMachineId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetOEEPrintReportOnMTTRAndMTBFByMachineId>.Post(
                        url,
                        methodName, null, values)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetMTTRAndMTBFFaultCodeCountReportByMachineId[]
            GetMttrAndMtbfFaultCodeCountReportByMachineId(InputGetMTTRAndMTBFFaultCodeCountReportByMachineId values)
        {
            var url = $"{BaseUrl}/DepartmentMttrAndMtbfReport/";
            const string methodName = nameof(GetMttrAndMtbfFaultCodeCountReportByMachineId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetMTTRAndMTBFFaultCodeCountReportByMachineId[]>.Post(
                        url,
                        methodName, null, values)
            );

            return task.GetAwaiter().GetResult();
        }
    }
}