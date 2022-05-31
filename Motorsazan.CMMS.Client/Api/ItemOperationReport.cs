using Motorsazan.CMMS.Shared.Models.Input.ItemOperationReport;
using Motorsazan.CMMS.Shared.Models.Output.ItemOperationReport;
using System.Threading.Tasks;

namespace Motorsazan.CMMS.Client.Api
{
    public partial class ApiList
    {
        public static OutputGetOperationItemTypeList[] GetOperationItemTypeList()
        {
            var url = $"{BaseUrl}/ItemOperationReport/";
            const string methodName = nameof(GetOperationItemTypeList);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetOperationItemTypeList[]>.Post(
                        url,
                        methodName, parameters: null)
            );

            return task.GetAwaiter().GetResult();
        }


        public static OutputGetItemOperationReportByCondtion[] GetItemOperationReportByCondtion(
            InputGetItemOperationReportByCondtion values)
        {
            var url = $"{BaseUrl}/ItemOperationReport/";
            const string methodName = nameof(GetItemOperationReportByCondtion);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetItemOperationReportByCondtion[]>.Post(
                        url,
                        methodName, parameters: values)
            );

            return task.GetAwaiter().GetResult();
        }
    }
}