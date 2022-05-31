using Motorsazan.CMMS.Shared.Models.Input.Inspection;
using Motorsazan.CMMS.Shared.Models.Output.Inspection;

using System.Threading.Tasks;

namespace Motorsazan.CMMS.Client.Api
{
    public static partial class ApiList
    {
        public static OutputGetMainMachineListBySubDepartmentId[] GetMainMachineListBySubDepartmentId(InputGetMainMachineListBySubDepartmentId input)
        {
            var url = $"{BaseUrl}/Inspection/";
            const string methodName = nameof(GetMainMachineListBySubDepartmentId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetMainMachineListBySubDepartmentId[]>.Post(
                        url,
                        methodName, parameters: input, token: null)
            );

            return task.GetAwaiter().GetResult();

        }

        public static string AddInspectionDetailToInspection(InputAddInspectionDetailToInspection values)
        {
            var url = $"{BaseUrl}/Inspection/";
            const string methodName = nameof(AddInspectionDetailToInspection);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, parameters: values, token: null)
            );

            return task.GetAwaiter().GetResult();

        }

        public static string AddInspection(InputAddInspection values, string token)
        {
            var url = $"{BaseUrl}/Inspection/";
            const string methodName = nameof(AddInspection);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, parameters: values, token: token)
            );

            return task.GetAwaiter().GetResult();

        }

        public static string AddInspectionWorkOrder(InputAddInspectionWorkOrder values, string token)
        {
            var url = $"{BaseUrl}/Inspection/";
            const string methodName = nameof(AddInspectionWorkOrder);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, parameters: values, token: token)
            );

            return task.GetAwaiter().GetResult();

        }

        public static string EditTheInspection(InputEditTheInspection values, string token)
        {
            var url = $"{BaseUrl}/Inspection/";
            const string methodName = nameof(EditTheInspection);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, parameters: values, token: token)
            );

            return task.GetAwaiter().GetResult();

        }
        
        public static string EditInspectionDetailByInspectionDetailId(InputEditInspectionDetailByInspectionDetailId values, string token)
        {
            var url = $"{BaseUrl}/Inspection/";
            const string methodName = nameof(EditInspectionDetailByInspectionDetailId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, parameters: values, token: token)
            );

            return task.GetAwaiter().GetResult();

        }

        public static OutputGetInspectionByInspectionId GetInspectionByInspectionId(InputGetInspectionByInspectionId input)
        {
            var url = $"{BaseUrl}/Inspection/";
            const string methodName = nameof(GetInspectionByInspectionId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetInspectionByInspectionId>.Post(
                        url,
                        methodName, parameters: input, token: null)
            );

            return task.GetAwaiter().GetResult();

        }

        
       public static OutputGetInspectionDetailByInspectionDetailId GetInspectionDetailByInspectionDetailId(InputGetInspectionDetailByInspectionDetailId input)
        {
            var url = $"{BaseUrl}/Inspection/";
            const string methodName = nameof(GetInspectionDetailByInspectionDetailId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetInspectionDetailByInspectionDetailId>.Post(
                        url,
                        methodName, parameters: input, token: null)
            );

            return task.GetAwaiter().GetResult();

        }


        public static OutputGetInspectionDetailsByInspectionId[] GetInspectionDetailsByInspectionId(InputGetInspectionDetailsByInspectionId input)
        {
            var url = $"{BaseUrl}/Inspection/";
            const string methodName = nameof(GetInspectionDetailsByInspectionId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetInspectionDetailsByInspectionId[]>.Post(
                        url,
                        methodName, parameters: input, token: null)
            );

            return task.GetAwaiter().GetResult();

        }


        public static OutputGetMainDepartmentListHasMachine[] GetMainDepartmentListHasMachine()
        {
            var url = $"{BaseUrl}/Inspection/";
            const string methodName = nameof(GetMainDepartmentListHasMachine);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetMainDepartmentListHasMachine[]>.Post(
                        url,
                        methodName, parameters: null, token: null)
            );

            return task.GetAwaiter().GetResult();

        }

        public static OutputGetAllMainDepartment[] GetAllMainDepartment(string token)
        {
            var url = $"{BaseUrl}/Inspection/";
            const string methodName = nameof(GetAllMainDepartment);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetAllMainDepartment[]>.Post(
                        url,
                        methodName, parameters: null, token: token)
            );

            return task.GetAwaiter().GetResult();

        }

        public static OutputGetSubDepartmentListByMainDepartmentId[] GetSubDepartmentListByMainDepartmentId(InputGetSubDepartmentListByMainDepartmentId values)
        {
            var url = $"{BaseUrl}/Inspection/";
            const string methodName = nameof(GetSubDepartmentListByMainDepartmentId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetSubDepartmentListByMainDepartmentId[]>.Post(
                        url,
                        methodName, parameters: values, token: null)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetSubDepartmentListHasMachineByMainDepartmentId[]
            GetSubDepartmentListHasMachineByMainDepartmentId(
                InputGetSubDepartmentListHasMachineByMainDepartmentId values)
        {
            var url = $"{BaseUrl}/Inspection/";
            const string methodName = nameof(GetSubDepartmentListHasMachineByMainDepartmentId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetSubDepartmentListHasMachineByMainDepartmentId[]>.Post(
                        url,
                        methodName, parameters: values)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetInspectionTypeList[] GetInspectionTypeList(string token)
        {
            var url = $"{BaseUrl}/Inspection/";
            const string methodName = nameof(GetInspectionTypeList);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetInspectionTypeList[]>.Post(
                        url,
                        methodName, parameters: null, token: token)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetMainMachineListByDepartmentId[] GetMainMachineListByDepartmentId(
            InputGetMainMachineListByDepartmentId values)
        {
            var url = $"{BaseUrl}/Inspection/";
            const string methodName = nameof(GetMainMachineListByDepartmentId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetMainMachineListByDepartmentId[]>.Post(
                        url,
                        methodName, parameters: values, token: null)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetInspectionListByCondition[] GetInspectionListByCondition(InputGetInspectionListByCondition values, string token)
        {
            var url = $"{BaseUrl}/Inspection/";
            const string methodName = nameof(GetInspectionListByCondition);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetInspectionListByCondition[]>.Post(
                        url,
                        methodName, parameters: values, token: token)
            );

            return task.GetAwaiter().GetResult();

        }


        public static string RemoveInspection(InputRemoveInspection values, string token)
        {
            var url = $"{BaseUrl}/Inspection/";
            const string methodName = nameof(RemoveInspection);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, parameters: values, token: token)
            );

            return task.GetAwaiter().GetResult();


        }

        public static string RemoveInspectionDeatilsByInspectionDetailId(InputRemoveInspectionDeatilsByInspectionDetailId values, string token)
        {
            var url = $"{BaseUrl}/Inspection/";
            const string methodName = nameof(RemoveInspectionDeatilsByInspectionDetailId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, parameters: values, token: token)
            );

            return task.GetAwaiter().GetResult();


        }

        


    }
}