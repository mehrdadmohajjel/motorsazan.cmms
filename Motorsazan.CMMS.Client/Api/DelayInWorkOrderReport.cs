using Motorsazan.CMMS.Shared.Models.Input.DelayInWorkOrderReport;
using Motorsazan.CMMS.Shared.Models.Output.DelayInWorkOrderReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Motorsazan.CMMS.Client.Api
{
    public static partial class ApiList
    {
        public static OutputGetDelayInWorkOrderReportByCondition[] GetDelayInWorkOrderReportByCondition(
            InputGetDelayInWorkOrderReportByCondition values)
        {
            var url = $"{BaseUrl}/DelayInWorkOrderReport/";
            const string methodName = nameof(GetDelayInWorkOrderReportByCondition);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetDelayInWorkOrderReportByCondition[]>.Post(
                        url,
                        methodName, parameters: values)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetWorkOrderByDelayTypeId[] GetWorkOrderByDelayTypeId(
    InputGetWorkOrderByDelayTypeId values)
        {
            var url = $"{BaseUrl}/DelayInWorkOrderReport/";
            const string methodName = nameof(GetWorkOrderByDelayTypeId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetWorkOrderByDelayTypeId[]>.Post(
                        url,
                        methodName, parameters: values)
            );

            return task.GetAwaiter().GetResult();
        }

    }


}
