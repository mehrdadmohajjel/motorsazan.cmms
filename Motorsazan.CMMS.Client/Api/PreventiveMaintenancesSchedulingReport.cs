using Motorsazan.CMMS.Shared.Models.Input.PreventiveMaintenancesSchedulingReport;
using Motorsazan.CMMS.Shared.Models.Output.PreventiveMaintenancesSchedulingReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Motorsazan.CMMS.Client.Api
{
    public static partial class ApiList
    {
        public static OutputGetPreventiveMaintenanceSchedulingReportByCondition[] GetPreventiveMaintenanceSchedulingReportByCondition(InputGetPreventiveMaintenanceSchedulingReportByCondition values,
            string token)
        {
            var url = $"{BaseUrl}/PreventiveMaintenancesSchedulingReport/";
            const string methodName = nameof(GetPreventiveMaintenanceSchedulingReportByCondition);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetPreventiveMaintenanceSchedulingReportByCondition[]>.Post(
                        url,
                        methodName, parameters: values)
            );

            return task.GetAwaiter().GetResult();

        }

    }
}