using System.Threading.Tasks;
using Motorsazan.CMMS.Shared.Models.Input.MaintenanceGroup;
using Motorsazan.CMMS.Shared.Models.Output.MaintenanceGroup;

namespace Motorsazan.CMMS.Client.Api
{
    public static partial class ApiList
    {
        public static string AssignEmployeeToMaintenanceGroup(InputAddEmployeeToMaintenanceGroup input, string token)
        {
            var url = $"{BaseUrl}/MaintenanceGroup/";
            const string methodName = nameof(AssignEmployeeToMaintenanceGroup);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(url, methodName, token, input)
            );

            return task.GetAwaiter().GetResult();
        }


        public static OutputGetAllEmployeeList[] GetAllEmployeeList(string token)
        {
            var url = $"{BaseUrl}/MaintenanceGroup/";
            const string methodName = nameof(GetAllEmployeeList);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetAllEmployeeList[]>.Post(
                        url,
                        methodName, token)
            );

            return task.GetAwaiter().GetResult();
        }


        public static string RemoveMaintenanceGroupMemberFromMaintenanceGroup(
            InputRemoveMaintenanceGroupMemberFromMaintenanceGroup input, string token)
        {
            var url = $"{BaseUrl}/MaintenanceGroup/";
            const string methodName = nameof(RemoveMaintenanceGroupMemberFromMaintenanceGroup);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(url, methodName, token, input)
            );

            return task.GetAwaiter().GetResult();
        }

        public static string SetSuperviserToMaintenanceGroupMember(InputGetSuperviserToMaintenanceGroupMember input,
            string token)
        {
            var url = $"{BaseUrl}/MaintenanceGroup/";
            const string methodName = nameof(SetSuperviserToMaintenanceGroupMember);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(url, methodName, token, input)
            );

            return task.GetAwaiter().GetResult();
        }
    }
}