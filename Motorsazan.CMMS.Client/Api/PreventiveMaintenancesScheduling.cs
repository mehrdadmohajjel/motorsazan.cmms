using Motorsazan.CMMS.Shared.Models.Input.PreventiveMaintenancesScheduling;
using Motorsazan.CMMS.Shared.Models.Output.PreventiveMaintenancesScheduling;
using System.Threading.Tasks;

namespace Motorsazan.CMMS.Client.Api
{
    public static partial class ApiList
    {
        public static OutputGetPreventiveMaintenanceSchedulingListByCondition[]
            GetPreventiveMaintenanceSchedulingListByCondition(
                InputGetPreventiveMaintenanceSchedulingListByCondition values, string token)
        {
            var url = $"{BaseUrl}/PreventiveMaintenancesScheduling/";
            const string methodName = nameof(GetPreventiveMaintenanceSchedulingListByCondition);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetPreventiveMaintenanceSchedulingListByCondition[]>.Post(
                        url,
                        methodName, token, values)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetOperationItemListByPMSchedulingInfoId[] GetOperationItemListByPMSchedulingInfoId(
            InputGetOperationItemListByPMSchedulingInfoId values)
        {
            var url = $"{BaseUrl}/PreventiveMaintenancesScheduling/";
            const string methodName = nameof(GetOperationItemListByPMSchedulingInfoId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetOperationItemListByPMSchedulingInfoId[]>.Post(
                        url,
                        methodName, null, values)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetMainMachineListByMainDepartmentIdAndSubDepartmentId[]
            GetMainMachineListByMainDepartmentIdAndSubDepartmentId(
                InputGetMainMachineListByMainDepartmentIdAndSubDepartmentId values)
        {
            var url = $"{BaseUrl}/PreventiveMaintenancesScheduling/";
            const string methodName = nameof(GetMainMachineListByMainDepartmentIdAndSubDepartmentId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetMainMachineListByMainDepartmentIdAndSubDepartmentId[]>.Post(
                        url,
                        methodName, null, values)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetStartAndEndDateOfParticularWeekByNumberOfWeeksOfCurrentYear
            GetStartAndEndDateOfParticularWeekByNumberOfWeeksOfCurrentYear(
                InputGetStartAndEndDateOfParticularWeekByNumberOfWeeksOfCurrentYear values)
        {
            var url = $"{BaseUrl}/PreventiveMaintenancesScheduling/";
            const string methodName = nameof(GetStartAndEndDateOfParticularWeekByNumberOfWeeksOfCurrentYear);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetStartAndEndDateOfParticularWeekByNumberOfWeeksOfCurrentYear>.Post(
                        url,
                        methodName, null, values)
            );

            return task.GetAwaiter().GetResult();
        }

        public static string RemoveSeveralOperationItemByPMSchedulingInfoId(
            InputRemoveSeveralOperationItemByPMSchedulingInfoId values, string token)
        {
            var url = $"{BaseUrl}/PreventiveMaintenancesScheduling/";
            const string methodName = nameof(RemoveSeveralOperationItemByPMSchedulingInfoId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, parameters: values, token: token)
            );

            return task.GetAwaiter().GetResult();
        }

        public static string SetWorkOrderToSeveralPereventiveMaintenanceScheduling(
            InputSetWorkOrderToSeveralPereventiveMaintenanceScheduling values, string token)
        {
            var url = $"{BaseUrl}/PreventiveMaintenancesScheduling/";
            const string methodName = nameof(SetWorkOrderToSeveralPereventiveMaintenanceScheduling);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, parameters: values, token: token)
            );

            return task.GetAwaiter().GetResult();
        }


        public static string ResetPredectiveMaintenanceJob()
        {
            var url = $"{BaseUrl}/PreventiveMaintenancesScheduling/";
            const string methodName = nameof(ResetPredectiveMaintenanceJob);

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