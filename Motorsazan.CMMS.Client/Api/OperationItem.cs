using Motorsazan.CMMS.Shared.Models.Input.OperationItem;
using Motorsazan.CMMS.Shared.Models.Output.OperationItem;
using System.Threading.Tasks;

namespace Motorsazan.CMMS.Client.Api
{
    public static partial class ApiList
    {
        public static string AddFaultOperationItem(InputAddFaultOperationItem values, string token)
        {
            var url = $"{BaseUrl}/OperationItem/";
            const string methodName = nameof(AddFaultOperationItem);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, parameters: values, token: token)
            );

            return task.GetAwaiter().GetResult();
        }

        public static string AddOperationItemMappingToPMItem(InputAddOperationItemMappingToPMItem values, string token)
        {
            var url = $"{BaseUrl}/OperationItem/";
            const string methodName = nameof(AddOperationItemMappingToPMItem);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, parameters: values, token: token)
            );

            return task.GetAwaiter().GetResult();
        }

        public static string AddSparePartListToOperationItem(InputAddSparePartListToOperationItem values, string token)
        {
            var url = $"{BaseUrl}/OperationItem/";
            const string methodName = nameof(AddSparePartListToOperationItem);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, parameters: values, token: token)
            );

            return task.GetAwaiter().GetResult();
        }


        public static OutputGetOperationItemMappingToPMItemListByOperationItemId[]
            GetOperationItemMappingToPMItemListByOperationItemId(
                InputGetOperationItemMappingToPMItemListByOperationItemId values)
        {
            var url = $"{BaseUrl}/OperationItem/";
            const string methodName = nameof(GetOperationItemMappingToPMItemListByOperationItemId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetOperationItemMappingToPMItemListByOperationItemId[]>.Post(
                        url,
                        methodName, parameters: values)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetPreventiveOperationItemListByMachineId[] GetPreventiveOperationItemListByMachineId(
            InputGetPreventiveOperationItemListByMachineId values)
        {
            var url = $"{BaseUrl}/OperationItem/";
            const string methodName = nameof(GetPreventiveOperationItemListByMachineId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetPreventiveOperationItemListByMachineId[]>.Post(
                        url,
                        methodName, parameters: values)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetOperationItemSparePartListByOperationItemId[]
            GetOperationItemSparePartListByOperationItemId(
                InputGetOperationItemSparePartListByOperationItemId values)
        {
            var url = $"{BaseUrl}/OperationItem/";
            const string methodName = nameof(GetOperationItemSparePartListByOperationItemId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetOperationItemSparePartListByOperationItemId[]>.Post(
                        url,
                        methodName, parameters: values)
            );

            return task.GetAwaiter().GetResult();
        }


        public static string RemoveOperationItemMappingToPMItemByOperationItemMappingToPMItemId(
            InputRemoveOperationItemMappingToPMItemByOperationItemMappingToPMItemId values, string token)
        {
            var url = $"{BaseUrl}/OperationItem/";
            const string methodName = nameof(RemoveOperationItemMappingToPMItemByOperationItemMappingToPMItemId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, parameters: values, token: token)
            );

            return task.GetAwaiter().GetResult();
        }


        public static string RemoveSparePartFromOperationItem(
            InputRemoveSparePartFromOperationItem values, string token)
        {
            var url = $"{BaseUrl}/OperationItem/";
            const string methodName = nameof(RemoveSparePartFromOperationItem);

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