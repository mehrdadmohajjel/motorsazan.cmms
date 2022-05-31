using System.Threading.Tasks;
using Motorsazan.CMMS.Shared.Models.Input.NetExpert;
using Motorsazan.CMMS.Shared.Models.Input.ProductiveWorkOrder;
using Motorsazan.CMMS.Shared.Models.Output.NetExpert;
using Motorsazan.CMMS.Shared.Models.Output.ProductiveWorkOrder;

namespace Motorsazan.CMMS.Client.Api
{
    public static partial class ApiList
    {
        public static string AddProductiveWorkOrder(InputAddProductiveWorkOrder values, string token)
        {
            var url = $"{BaseUrl}/ProductiveWorkOrder/";
            const string methodName = nameof(AddProductiveWorkOrder);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, parameters: values, token:token)
            );

            return task.GetAwaiter().GetResult();

        }
         
         public static string EditWorkOrderRateByCustomer(InputEditWorkOrderRateByCustomer values, string token)
         {
             var url = $"{BaseUrl}/ProductiveWorkOrder/";
             const string methodName = nameof(EditWorkOrderRateByCustomer);

             var task = Task.Run(
                 async () =>
                     await ApiConnector<string>.Post(
                         url,
                         methodName, parameters: values, token: token)
             );

             return task.GetAwaiter().GetResult();
         }


        public static string GetWorkOrderRateByWorkOrderID(InputGetWorkOrderRateByWorkOrderId values)
        {
            var url = $"{BaseUrl}/ProductiveWorkOrder/";
            const string methodName = nameof(GetWorkOrderRateByWorkOrderID);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, parameters: values)
            );

            return task.GetAwaiter().GetResult();
        }

        
        public static OutputGetStopTypeList[] GetStopTypeList()
        {
            var url = $"{BaseUrl}/ProductiveWorkOrder/";
            const string methodName = nameof(GetStopTypeList);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetStopTypeList[]>.Post(
                        url,
                        methodName, parameters: null)
            );

            return task.GetAwaiter().GetResult();
        }
        
        public static OutputGetStopTypeList[] GetStopTypeListForProductiveWorkOrder()
        {
            var url = $"{BaseUrl}/ProductiveWorkOrder/";
            const string methodName = nameof(GetStopTypeListForProductiveWorkOrder);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetStopTypeList[]>.Post(
                        url,
                        methodName, parameters: null)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetCurrentUserDepartmentList[] GetCurrentUserDepartmentList(InputGetCurrentUserDepartmentList values)
        {
            var url = $"{BaseUrl}/ProductiveWorkOrder/";
            const string methodName = nameof(GetCurrentUserDepartmentList);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetCurrentUserDepartmentList[]>.Post(
                        url,
                        methodName, parameters: values)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetDepartmentMachineList[] GetMachineListByDepartmentId(InputGetDepartmentMachineList values, string token)
        {
            var url = $"{BaseUrl}/ProductiveWorkOrder/";
            const string methodName = nameof(GetMachineListByDepartmentId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetDepartmentMachineList[]>.Post(
                        url,
                        methodName, parameters: values)
            );

            return task.GetAwaiter().GetResult();

        }

        public static OutputGetMaintenanceGroupList[] GetMaintenanceGroupList()
        {
            var url = $"{BaseUrl}/ProductiveWorkOrder/";
            const string methodName = nameof(GetMaintenanceGroupList);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetMaintenanceGroupList[]>.Post(
                        url,
                        methodName, parameters: null)
            );

            return task.GetAwaiter().GetResult();
        }


        public static OutputGetProductiveWorkOrderByCondition[] GetProductiveWorkOrderByCondition(InputGetProductiveWorkOrderByCondition values, string token)
        {
            var url = $"{BaseUrl}/ProductiveWorkOrder/";
            const string methodName = nameof(GetProductiveWorkOrderByCondition);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetProductiveWorkOrderByCondition[]>.Post(
                        url,
                        methodName, parameters: values,token: token )
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetWorkOrderStatusTypeList[] GetWorkOrderStatusTypeList()
        {
            var url = $"{BaseUrl}/ProductiveWorkOrder/";
            const string methodName = nameof(GetWorkOrderStatusTypeList);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetWorkOrderStatusTypeList[]>.Post(
                        url,
                        methodName, parameters: null)
            );

            return task.GetAwaiter().GetResult();

        }

        public static OutputGetWorkOrderByWorkOrderId GetWorkOrderByWorkOrderId(InputGetWorkOrderByWorkOrderId values,string token)
        {
            var url = $"{BaseUrl}/NetExpert/";
            const string methodName = nameof(GetWorkOrderByWorkOrderId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetWorkOrderByWorkOrderId>.Post(
                        url,
                        methodName, parameters: values, token: null)
            );

            return task.GetAwaiter().GetResult();
        }

        public static string SetWorkOrderStatusToNotConfirmFinishByCustomer(InputSetWorkOrderStatusToNotConfirmFinishByCustomer values, string token)
        {
            var url = $"{BaseUrl}/ProductiveWorkOrder/";
            const string methodName = nameof(SetWorkOrderStatusToNotConfirmFinishByCustomer);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, parameters: values, token: token)
            );

            return task.GetAwaiter().GetResult();
        }

        public static string SetWorkOrderStatusToConfirmFinishByCustomer(InputSetWorkOrderStatusToConfirmFinishByCustomer values, string token)
        {
            var url = $"{BaseUrl}/ProductiveWorkOrder/";
            const string methodName = nameof(SetWorkOrderStatusToConfirmFinishByCustomer);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, parameters: values, token: token)
            );

            return task.GetAwaiter().GetResult();
        }
        
        public static string SetWorkOrderStatusToCancelByCustomer(InputSetWorkOrderStatusToCancelByCustomer values, string token)
        {
            var url = $"{BaseUrl}/ProductiveWorkOrder/";
            const string methodName = nameof(SetWorkOrderStatusToCancelByCustomer);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, parameters: values, token: token)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetActionOrDelayListByWorkOrderId[] GetActionOrDelayListByWorkOrderId(InputGetActionOrDelayListByWorkOrderId values, string token)
        {
            var url = $"{BaseUrl}/ProductiveWorkOrder/";
            const string methodName = nameof(GetActionOrDelayListByWorkOrderId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetActionOrDelayListByWorkOrderId[]>.Post(
                        url,
                        methodName, parameters: values, token: null)
            );

            return task.GetAwaiter().GetResult();

        }

        public static OutputGetWorKOrderConsumableListByWorkOrderId[] GetWorKOrderConsumableListByWorkOrderId(InputGetWorKOrderConsumableListByWorkOrderId values, string token)
        {
            var url = $"{BaseUrl}/ProductiveWorkOrder/";
            const string methodName = nameof(GetWorKOrderConsumableListByWorkOrderId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetWorKOrderConsumableListByWorkOrderId[]>.Post(
                        url,
                        methodName, parameters: values, token: null)
            );

            return task.GetAwaiter().GetResult();

        }
        public static OutputGetWorKOrderReferralListByWorkOrderId[] GetWorKOrderReferralListByWorkOrderId(InputGetWorKOrderReferralListByWorkOrderId values, string token)
        {
            var url = $"{BaseUrl}/ProductiveWorkOrder/";
            const string methodName = nameof(GetWorKOrderReferralListByWorkOrderId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetWorKOrderReferralListByWorkOrderId[]>.Post(
                        url,
                        methodName, parameters: values, token:null)
            );

            return task.GetAwaiter().GetResult();

        }

    }
}