using System.Threading.Tasks;
using Motorsazan.CMMS.Shared.Models.Input.NoneProductiveWorkOrder;
using Motorsazan.CMMS.Shared.Models.Output.NoneProductiveWorkOrder;
using Motorsazan.CMMS.Shared.Models.Output.ProductiveWorkOrder;

namespace Motorsazan.CMMS.Client.Api
{
    public static partial class ApiList
    {
        public static string AddNoneProductiveWorkOrder(InputAddNoneProductiveWorkOrder values, string token)
        {
            var url = $"{BaseUrl}/NoneProductiveWorkOrder/";
            const string methodName = nameof(AddNoneProductiveWorkOrder);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, parameters: values, token: token)
            );

            return task.GetAwaiter().GetResult();

        }

        public static OutputGetNoneProductiveWorkOrderByCondition[] GetNoneProductiveWorkOrderByCondition(InputGetNoneProductiveWorkOrderByCondition values, string token)
        {
            var url = $"{BaseUrl}/NoneProductiveWorkOrder/";
            const string methodName = nameof(GetNoneProductiveWorkOrderByCondition);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetNoneProductiveWorkOrderByCondition[]>.Post(
                        url,
                        methodName, parameters: values, token: token)
            );

            return task.GetAwaiter().GetResult();

        }
        
        public static OutputGetStopTypeList[] GetStopTypeListForNoneProductiveWorkOrder()
        {
            var url = $"{BaseUrl}/NoneProductiveWorkOrder/";
            const string methodName = nameof(GetStopTypeListForNoneProductiveWorkOrder);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetStopTypeList[]>.Post(
                        url,
                        methodName, parameters: null)
            );

            return task.GetAwaiter().GetResult();
        }

    }
}