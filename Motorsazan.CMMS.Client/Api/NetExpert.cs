using Motorsazan.CMMS.Shared.Models.Input.NetExpert;
using Motorsazan.CMMS.Shared.Models.Output.NetExpert;
using System.Threading.Tasks;

namespace Motorsazan.CMMS.Client.Api
{
    public static partial class ApiList
    {
        public static string AddActionToWorkOrder(InputAddActionToWorkOrder input, string token)
        {
            var url = $"{BaseUrl}/NetExpert/";
            const string methodName = nameof(AddActionToWorkOrder);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, token, input)
            );

            return task.GetAwaiter().GetResult();
        }

        public static string AddCancelStatusToWorkOrder(InputAddCancelStatusToWorkOrder input, string token)
        {
            var url = $"{BaseUrl}/NetExpert/";
            const string methodName = nameof(AddCancelStatusToWorkOrder);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, token, input)
            );

            return task.GetAwaiter().GetResult();
        }

        public static string AddNetFinishedStatusToWorkOrder(InputAddNetFinishedStatusToWorkOrder input, string token)
        {
            var url = $"{BaseUrl}/NetExpert/";
            const string methodName = nameof(AddNetFinishedStatusToWorkOrder);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, token, input)
            );

            return task.GetAwaiter().GetResult();
        }

        public static string AddRepairFinishedStatusToWorkOrder(InputAddRepairFinishedStatusToWorkOrder input,
            string token)
        {
            var url = $"{BaseUrl}/NetExpert/";
            const string methodName = nameof(AddRepairFinishedStatusToWorkOrder);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, token, input)
            );

            return task.GetAwaiter().GetResult();
        }

        public static string AddStockWaitingStatusToWorkOrder(InputAddStockWaitingStatusToWorkOrder input, string token)
        {
            var url = $"{BaseUrl}/NetExpert/";
            const string methodName = nameof(AddStockWaitingStatusToWorkOrder);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, token, input)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputCheckAllHavalehNOConfirmedByWorkOrderId CheckAllHavalehNOConfirmedByWorkOrderId(
            InputCheckAllHavalehNOConfirmedByWorkOrderId input)
        {
            var url = $"{BaseUrl}/NetExpert/";
            const string methodName = nameof(CheckAllHavalehNOConfirmedByWorkOrderId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputCheckAllHavalehNOConfirmedByWorkOrderId>.Post(url, methodName,
                        parameters: input)
            );

            return task.GetAwaiter().GetResult();
        }


        public static OutputCheckMachineHasOperationItem CheckMachineHasOperationItem(
            InputCheckMachineHasOperationItem input)
        {
            var url = $"{BaseUrl}/NetExpert/";
            const string methodName = nameof(CheckMachineHasOperationItem);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputCheckMachineHasOperationItem>.Post(url, methodName, parameters: input)
            );

            return task.GetAwaiter().GetResult();
        }

        public static string FinishingStockWaitingForWorkOrder(InputFinishingStockWaitingForWorkOrder input,
            string token)
        {
            var url = $"{BaseUrl}/NetExpert/";
            const string methodName = nameof(FinishingStockWaitingForWorkOrder);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, token, input)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetDelayTypeListByActivity[] GetDelayTypeListByActivity(
            InputGetDelayTypeListByActivity input)
        {
            var url = $"{BaseUrl}/NetExpert/";
            const string methodName = nameof(GetDelayTypeListByActivity);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetDelayTypeListByActivity[]>.Post(
                        url,
                        methodName, parameters: input)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetEmployeeListForPerformanceRegistering[] GetEmployeeListForPerformanceRegistering()
        {
            var url = $"{BaseUrl}/NetExpert/";
            const string methodName = nameof(GetEmployeeListForPerformanceRegistering);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetEmployeeListForPerformanceRegistering[]>.Post(
                        url,
                        methodName)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetMaintenanceGroupMemberListByMaintenanceGroupId[]
            GetMaintenanceGroupMemberListByMaintenanceGroupId(
                InputGetMaintenanceGroupMemberListByMaintenanceGroupId input)
        {
            var url = $"{BaseUrl}/NetExpert/";
            const string methodName = nameof(GetMaintenanceGroupMemberListByMaintenanceGroupId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetMaintenanceGroupMemberListByMaintenanceGroupId[]>.Post(
                        url,
                        methodName, parameters: input)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetInspectionDetailByWorkOrderId[] GetInspectionDetailByWorkOrderId(
            InputGetInspectionDetailByWorkOrderId input)
        {
            var url = $"{BaseUrl}/NetExpert/";
            const string methodName = nameof(GetInspectionDetailByWorkOrderId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetInspectionDetailByWorkOrderId[]>.Post(
                        url,
                        methodName, parameters: input)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetNetExpertWorkOrderListByCondition[] GetNetExpertWorkOrderListByCondition(
            InputGetNetExpertWorkOrderListByCondition input)
        {
            var url = $"{BaseUrl}/NetExpert/";
            const string methodName = nameof(GetNetExpertWorkOrderListByCondition);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetNetExpertWorkOrderListByCondition[]>.Post(
                        url,
                        methodName, parameters: input)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetOperationItemListByMaintenanceGroupIdAndMachineId[]
            GetOperationItemListByMaintenanceGroupIdAndMachineId(
                InputGetOperationItemListByMaintenanceGroupIdAndMachineId input)
        {
            var url = $"{BaseUrl}/NetExpert/";
            const string methodName = nameof(GetOperationItemListByMaintenanceGroupIdAndMachineId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetOperationItemListByMaintenanceGroupIdAndMachineId[]>.Post(
                        url,
                        methodName, parameters: input)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetPreventiveOperationItemListByWorkOrderId[] GetPreventiveOperationItemListByWorkOrderId(
            InputGetPreventiveOperationItemListByWorkOrderId input)
        {
            var url = $"{BaseUrl}/NetExpert/";
            const string methodName = nameof(GetPreventiveOperationItemListByWorkOrderId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetPreventiveOperationItemListByWorkOrderId[]>.Post(
                        url,
                        methodName, parameters: input)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetWorkOrderByWorkOrderId GetWorkOrderByWorkOrderId(InputGetWorkOrderByWorkOrderId input)
        {
            var url = $"{BaseUrl}/NetExpert/";
            const string methodName = nameof(GetWorkOrderByWorkOrderId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetWorkOrderByWorkOrderId>.Post(
                        url,
                        methodName, parameters: input)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetMaintenanceGroupListByEId[] GetMaintenanceGroupListByEId(
            InputGetMaintenanceGroupListByEId input)
        {
            var url = $"{BaseUrl}/NetExpert/";
            const string methodName = nameof(GetMaintenanceGroupListByEId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetMaintenanceGroupListByEId[]>.Post(
                        url,
                        methodName, parameters: input)
            );

            return task.GetAwaiter().GetResult();
        }

        public static string AddInspectionActionToWorkOrder(InputAddInspectionActionToWorkOrder input, string token)
        {
            var url = $"{BaseUrl}/NetExpert/";
            const string methodName = nameof(AddInspectionActionToWorkOrder);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, token, input)
            );

            return task.GetAwaiter().GetResult();
        }

        public static string AddPreventiveMaintenanceActionToWorkOrder(
            InputAddPreventiveMaintenanceActionToWorkOrder input, string token)
        {
            var url = $"{BaseUrl}/NetExpert/";
            const string methodName = nameof(AddPreventiveMaintenanceActionToWorkOrder);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, token, input)
            );

            return task.GetAwaiter().GetResult();
        }

        public static string EditActionOrDelayListInPreventiveScheduling(
            InputEditActionOrDelayListInPreventiveScheduling values,
            string token)
        {
            var url = $"{BaseUrl}/NetExpert/";
            const string methodName = nameof(EditActionOrDelayListInPreventiveScheduling);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, parameters: values, token: token)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetPurchaseByRequestNumberAndRequestYear[] GetPurchaseByRequestNumberAndRequestYear(
            InputGetPurchaseByRequestNumberAndRequestYear input)
        {
            var url = $"{BaseUrl}/NetExpert/";
            const string methodName = nameof(GetPurchaseByRequestNumberAndRequestYear);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetPurchaseByRequestNumberAndRequestYear[]>.Post(
                        url,
                        methodName, parameters: input)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetWorkOrderRegistrantInfoByWorkOrderId GetWorkOrderRegistrantInfoByWorkOrderId(
            InputGetWorkOrderRegistrantInfoByWorkOrderId input)
        {
            var url = $"{BaseUrl}/NetExpert/";
            const string methodName = nameof(GetWorkOrderRegistrantInfoByWorkOrderId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetWorkOrderRegistrantInfoByWorkOrderId>.Post(
                        url,
                        methodName, parameters: input)
            );

            return task.GetAwaiter().GetResult();
        }


        public static string RemoveActionOrDelayByActionOrDelayListId(
            InputRemoveActionOrDelayByActionOrDelayListId input, string token)
        {
            var url = $"{BaseUrl}/NetExpert/";
            const string methodName = nameof(RemoveActionOrDelayByActionOrDelayListId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, token, input)
            );

            return task.GetAwaiter().GetResult();
        }

        public static string SendWorkOrderToMaintenanceGroup(InputSendWorkOrderToMaintenanceGroup input, string token)
        {
            var url = $"{BaseUrl}/NetExpert/";
            const string methodName = nameof(SendWorkOrderToMaintenanceGroup);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, token, input)
            );

            return task.GetAwaiter().GetResult();
        }

        public static string SendWorkOrderToMaintenanceGroupMember(InputSendWorkOrderToMaintenanceGroupMember input,
            string token)
        {
            var url = $"{BaseUrl}/NetExpert/";
            const string methodName = nameof(SendWorkOrderToMaintenanceGroupMember);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, token, input)
            );

            return task.GetAwaiter().GetResult();
        }


        public static string SetHavaleNOListByHavalehWorkOrderIDList(InputSetHavaleNOListByHavalehWorkOrderId values,
            string token)
        {
            var url = $"{BaseUrl}/NetExpert/";
            const string methodName = nameof(SetHavaleNOListByHavalehWorkOrderIDList);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, parameters: values, token: token)
            );

            return task.GetAwaiter().GetResult();
        }


        public static OutputGetWorkOrderStatusHistoryByWorkOrderId[] GetWorkOrderStatusHistoryByWorkOrderId(
            InputGetWorkOrderStatusHistoryByWorkOrderId input)
        {
            var url = $"{BaseUrl}/NetExpert/";
            const string methodName = nameof(GetWorkOrderStatusHistoryByWorkOrderId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetWorkOrderStatusHistoryByWorkOrderId[]>.Post(
                        url,
                        methodName, parameters: input)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetStockPurchaseRequestForStockWaitingByWorkOrderId[] GetStockPurchaseRequestForStockWaitingByWorkOrderId(
            InputGetStockPurchaseRequestForStockWaitingByWorkOrderId input)
        {
            var url = $"{BaseUrl}/NetExpert/";
            const string methodName = nameof(GetStockPurchaseRequestForStockWaitingByWorkOrderId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetStockPurchaseRequestForStockWaitingByWorkOrderId[]>.Post(
                        url,
                        methodName, parameters: input)
            );

            return task.GetAwaiter().GetResult();
        }
    }
}