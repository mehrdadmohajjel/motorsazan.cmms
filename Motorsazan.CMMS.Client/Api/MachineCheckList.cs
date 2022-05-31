using System.Threading.Tasks;
using Motorsazan.CMMS.Shared.Models.Input.MachineCheckList;
using Motorsazan.CMMS.Shared.Models.Output.MachineCheckList;

namespace Motorsazan.CMMS.Client.Api
{
    public static partial class ApiList
    {
        public static string ActiveOrDeActiveOperationItem(InputActiveOrDeActiveOperationItem values, string token)
        {
            var url = $"{BaseUrl}/MachineCheckList/";
            const string methodName = nameof(ActiveOrDeActiveOperationItem);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, parameters: values, token: token)
            );

            return task.GetAwaiter().GetResult();
        }

        public static string AddCheckListOperationItem(InputAddCheckListOperationItem values, string token)
        {
            var url = $"{BaseUrl}/MachineCheckList/";
            const string methodName = nameof(AddCheckListOperationItem);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, parameters: values, token: token)
            );

            return task.GetAwaiter().GetResult();
        }

        public static string CopyOperationItemForOtherMachine(InputCopyOperationItemForOtherMachine values,
            string token)
        {
            var url = $"{BaseUrl}/MachineCheckList/";
            const string methodName = nameof(CopyOperationItemForOtherMachine);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, parameters: values, token: token)
            );

            return task.GetAwaiter().GetResult();
        }

        public static string CopySeveralOperationItemToOtherMachine(InputCopySeveralOperationItemToOtherMachine values,
            string token)
        {
            var url = $"{BaseUrl}/MachineCheckList/";
            const string methodName = nameof(CopySeveralOperationItemToOtherMachine);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, parameters: values, token: token)
            );

            return task.GetAwaiter().GetResult();
        }

        public static string EditOperationItemByOperationItemId(InputEditOperationItemByOperationItemId values,
            string token)
        {
            var url = $"{BaseUrl}/MachineCheckList/";
            const string methodName = nameof(EditOperationItemByOperationItemId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, parameters: values, token: token)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetHierarchicalMachineListByDepartmentId[] GetHierarchicalMachineListByDepartmentId(
            InputGetHierarchicalMachineListByDepartmentId values)
        {
            var url = $"{BaseUrl}/MachineCheckList/";
            const string methodName = nameof(GetHierarchicalMachineListByDepartmentId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetHierarchicalMachineListByDepartmentId[]>.Post(
                        url,
                        methodName, parameters: values)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetOperationItemListByMachineId[] GetOperationItemListByMachineId(
            InputGetOperationItemListByMachineId values)
        {
            var url = $"{BaseUrl}/MachineCheckList/";
            const string methodName = nameof(GetOperationItemListByMachineId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetOperationItemListByMachineId[]>.Post(
                        url,
                        methodName, parameters: values)
            );

            return task.GetAwaiter().GetResult();
        }


        public static OutputGetSerialForOperationItem GetSerialForOperationItem(InputGetSerialForOperationItem values)
        {
            var url = $"{BaseUrl}/MachineCheckList/";
            const string methodName = nameof(GetSerialForOperationItem);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetSerialForOperationItem>.Post(
                        url,
                        methodName, parameters: values)
            );

            return task.GetAwaiter().GetResult();
        }

        public static string MoveOperationItemFromCurrentMachineToNewMachine(
            InputMoveOperationItemFromCurrentMachineToNewMachine values, string token)
        {
            var url = $"{BaseUrl}/MachineCheckList/";
            const string methodName = nameof(MoveOperationItemFromCurrentMachineToNewMachine);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, parameters: values, token: token)
            );

            return task.GetAwaiter().GetResult();
        }
    }
}