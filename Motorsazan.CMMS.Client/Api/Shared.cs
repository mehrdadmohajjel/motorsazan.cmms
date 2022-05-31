using Motorsazan.CMMS.Shared.Models.Input.Shared;
using Motorsazan.CMMS.Shared.Models.Output.Shared;
using System.Threading.Tasks;

namespace Motorsazan.CMMS.Client.Api
{
    public static partial class ApiList
    {
        public static OutputGetAllFinalCodeListForUnderConstructionStocks[] GetAllFinalCodeListForUnderConstructionStocks(
            InputGetAllFinalCodeListForUnderConstructionStocks values)
        {
            const string methodName = nameof(GetAllFinalCodeListForUnderConstructionStocks);
            var url = $"{BaseUrl}/Shared/";

            var task = Task.Run(async () => await ApiConnector<OutputGetAllFinalCodeListForUnderConstructionStocks[]>.Post(url, methodName, null,
                values));

            return task.GetAwaiter().GetResult();
        }


        public static OutputGetFinalCodeRangeByRackCode[] GetFinalCodeRangeByRackCode(
            InputGetFinalCodeRangeByRackCode values)
        {
            const string methodName = nameof(GetFinalCodeRangeByRackCode);
            var url = $"{BaseUrl}/Shared/";

            var task = Task.Run(async () => await ApiConnector<OutputGetFinalCodeRangeByRackCode[]>.Post(url, methodName, null,
                values));

            return task.GetAwaiter().GetResult();
        }
    }
}