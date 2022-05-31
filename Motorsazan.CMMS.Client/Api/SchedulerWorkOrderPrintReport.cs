using Motorsazan.CMMS.Shared.Models.Input.SchedulerWorkOrderPrintReport;
using Motorsazan.CMMS.Shared.Models.Output.SchedulerWorkOrderPrintReport;
using System.Threading.Tasks;

namespace Motorsazan.CMMS.Client.Api
{
    public static partial class ApiList
    {
        public static OutputGetSchedulerWorkOrderReportByCondition[] GetSchedulerWorkOrderReportByCondition(
            InputGetSchedulerWorkOrderReportByCondition values)
        {
            var url = $"{BaseUrl}/SchedulerWorkOrderPrintReport/";
            const string methodName = nameof(GetSchedulerWorkOrderReportByCondition);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetSchedulerWorkOrderReportByCondition[]>.Post(
                        url,
                        methodName, parameters: values)
            );

            return task.GetAwaiter().GetResult();
        }
        public static OutputGetVisitFormPrintByWorkOrderId GetVisitFormPrintByWorkOrderId(
            InputGetVisitFormPrintByWorkOrderId values)
        {
            var url = $"{BaseUrl}/SchedulerWorkOrderPrintReport/";
            const string methodName = nameof(GetVisitFormPrintByWorkOrderId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetVisitFormPrintByWorkOrderId>.Post(
                        url,
                        methodName, parameters: values)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetPreventiveVisitFormPrintByWorkOrderId GetPreventiveVisitFormPrintByWorkOrderId(
            InputGetPreventiveVisitFormPrintByWorkOrderId values)
        {
            var url = $"{BaseUrl}/SchedulerWorkOrderPrintReport/";
            const string methodName = nameof(GetPreventiveVisitFormPrintByWorkOrderId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetPreventiveVisitFormPrintByWorkOrderId>.Post(
                        url,
                        methodName, parameters: values)
            );

            return task.GetAwaiter().GetResult();
        }
    }

}