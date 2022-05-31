using System.Threading.Tasks;
using Motorsazan.CMMS.Shared.Models.Input.PreventiveItemOperation;
using Motorsazan.CMMS.Shared.Models.Output.PreventiveItemOperation;

namespace Motorsazan.CMMS.Client.Api
{
    public static partial class ApiList
    {
        public static string AddPreventiveOperationItem(InputAddPreventiveOperationItem values, string token)
        {
            var url = $"{BaseUrl}/PreventiveItemOperation/";
            const string methodName = nameof(AddPreventiveOperationItem);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, parameters: values, token: token)
            );

            return task.GetAwaiter().GetResult();
        }

        public static string EditPreventiveOperationByOperationItemId(
            InputEditPreventiveOperationByOperationItemId values, string token)
        {
            var url = $"{BaseUrl}/PreventiveItemOperation/";
            const string methodName = nameof(EditPreventiveOperationByOperationItemId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, parameters: values, token: token)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetMiterMeasuringTypeListForPreventiveOperationItem[]
            GetMiterMeasuringTypeListForPreventiveOperationItem()
        {
            var url = $"{BaseUrl}/PreventiveItemOperation/";
            const string methodName = nameof(GetMiterMeasuringTypeListForPreventiveOperationItem);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetMiterMeasuringTypeListForPreventiveOperationItem[]>.Post(
                        url,
                        methodName, parameters: null)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetPreventiveOperationItemByOperationItemId GetPreventiveOperationItemByOperationItemId(
            InputGetPreventiveOperationItemByOperationItemId values)
        {
            var url = $"{BaseUrl}/PreventiveItemOperation/";
            const string methodName = nameof(GetPreventiveOperationItemByOperationItemId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetPreventiveOperationItemByOperationItemId>.Post(
                        url,
                        methodName, parameters: values)
            );

            return task.GetAwaiter().GetResult();
        }
    }
}