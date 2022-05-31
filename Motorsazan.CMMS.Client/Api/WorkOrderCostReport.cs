using Motorsazan.CMMS.Shared.Models.Input.WorkOrderCostReport;
using Motorsazan.CMMS.Shared.Models.Output.WorkOrderCostReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Motorsazan.CMMS.Client.Api
{
    public static partial class ApiList
    {
        public static OutputGetWorkOrderCostReportByCondition[] GetWorkOrderCostReportByCondition(
    InputGetWorkOrderCostReportByCondition values)
        {
            var url = $"{BaseUrl}/WorkOrderCostReport/";
            const string methodName = nameof(GetWorkOrderCostReportByCondition);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetWorkOrderCostReportByCondition[]>.Post(
                        url,
                        methodName, parameters: values)
            );

            return task.GetAwaiter().GetResult();
        }

    }
}