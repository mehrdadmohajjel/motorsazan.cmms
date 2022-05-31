using System.Threading.Tasks;
using Motorsazan.CMMS.Shared.Models.Input.StockMan;
using Motorsazan.CMMS.Shared.Models.Output.StockMan;


namespace Motorsazan.CMMS.Client.Api
{
    public static partial class ApiList
    {
        public static string ConfirmConsumablesManagement(InputGetConfirmConsumablesManagement input, string token)
        {
            var url = $"{BaseUrl}/StockMan/";
            const string methodName = nameof(ConfirmConsumablesManagement);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, parameters: input, token: token)
            );

            return task.GetAwaiter().GetResult();
        }

        public static string DeleteConsumablesManagement(InputGetDeleteConsumablesManagement values, string token)
        {
            var url = $"{BaseUrl}/StockMan/";
            const string methodName = nameof(DeleteConsumablesManagement);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, parameters: values, token: token)
            );

            return task.GetAwaiter().GetResult();
        }

        public static string AddingConsumable(InputGetAddingConsumable values, string token)
        {

            var url = $"{BaseUrl}/StockMan/";
            const string methodName = nameof(AddingConsumable);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, parameters: values, token: token)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetRegistrarProfileObservation GetRegistrarInfoByWorkOrderId(InputGetRegistrarProfileObservation values)
        {
            var url = $"{BaseUrl}/StockMan/";
            const string methodName = nameof(GetRegistrarInfoByWorkOrderId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetRegistrarProfileObservation>.Post(
                        url,
                        methodName, parameters: values)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetStockManListByCondition[] GetStockManListByCondition(InputGetStockManListByCondition values, string token)
        {

            var url = $"{BaseUrl}/StockMan/";
            const string methodName = nameof(GetStockManListByCondition);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetStockManListByCondition[]>.Post(
                        url,
                        methodName, parameters: values, token: token)
            );

            return task.GetAwaiter().GetResult();
        }

        public static string UpdateConsumablesManagement(InputGetEditConsumablesManagement values, string token)
        {
            var url = $"{BaseUrl}/StockMan/";
            const string methodName = nameof(UpdateConsumablesManagement);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, parameters: values, token: token)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetHavaleWorkOrderReferral[] GetHavaleWorkOrderReferralByWorkOrderReferralId(InputGetHavaleWorkOrderReferral values, string token)
        {
            var url = $"{BaseUrl}/StockMan/";
            const string methodName = nameof(GetHavaleWorkOrderReferralByWorkOrderReferralId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetHavaleWorkOrderReferral[]>.Post(
                        url,
                        methodName, parameters: values, token: null)
            );

            return task.GetAwaiter().GetResult();
        }


    }

}
