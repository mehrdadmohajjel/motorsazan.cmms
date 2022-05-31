using System.Threading.Tasks;
using Motorsazan.CMMS.Shared.Models.Input.DetermineSalary;
using Motorsazan.CMMS.Shared.Models.Output.DetermineSalary;

namespace Motorsazan.CMMS.Client.Api
{
    public partial class ApiList
    {
        public static string EditMaintenanceGroupMemberSalaryBySalaryId(
            InputEditMaintenanceGroupMemberSalaryBySalaryId values,
            string token)
        {
            var url = $"{BaseUrl}/DetermineSalary/";
            const string methodName = nameof(EditMaintenanceGroupMemberSalaryBySalaryId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, parameters: values, token: token)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetMaintenanceGroupMemberSalaryDetailListBySalaryId[]
            GetMaintenanceGroupMemberSalaryDetailListBySalaryId(
                InputGetMaintenanceGroupMemberSalaryDetailListBySalaryId values)
        {
            var url = $"{BaseUrl}/DetermineSalary/";
            const string methodName = nameof(GetMaintenanceGroupMemberSalaryDetailListBySalaryId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetMaintenanceGroupMemberSalaryDetailListBySalaryId[]>.Post(
                        url,
                        methodName, parameters: values)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetMaintenanceGroupMemberSalaryList[] GetMaintenanceGroupMemberSalaryList()
        {
            var url = $"{BaseUrl}/DetermineSalary/";
            const string methodName = nameof(GetMaintenanceGroupMemberSalaryList);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetMaintenanceGroupMemberSalaryList[]>.Post(
                        url,
                        methodName, parameters: null)
            );

            return task.GetAwaiter().GetResult();
        }
    }
}