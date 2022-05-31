using Motorsazan.CMMS.Shared.Models.Input.MachineManagement;
using Motorsazan.CMMS.Shared.Models.Output.MachineManagement;
using System.Threading.Tasks;

namespace Motorsazan.CMMS.Client.Api
{
    public static partial class ApiList
    {
        public static string AddMachineDocument(InputAddMachineDocument values, string token)
        {
            var url = $"{BaseUrl}/MachineManagement/";
            const string methodName = nameof(AddMachineDocument);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, parameters: values, token: token)
            );

            return task.GetAwaiter().GetResult();
        }

        public static string AddMachineElectricalInfo(InputAddMachineElectricalInfo values, string token)
        {
            var url = $"{BaseUrl}/MachineManagement/";
            const string methodName = nameof(AddMachineElectricalInfo);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, parameters: values, token: token)
            );

            return task.GetAwaiter().GetResult();
        }


        public static string EditMachineBuiltInfo(InputEditMachineBuiltInfo values,
            string token)
        {
            var url = $"{BaseUrl}/MachineManagement/";
            const string methodName = nameof(EditMachineBuiltInfo);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, parameters: values, token: token)
            );

            return task.GetAwaiter().GetResult();
        }

        public static string RemoveMachineElectricalInfoByRecordId(InputRemoveMachineElectricalInfoByRecordId values,
            string token)
        {
            var url = $"{BaseUrl}/MachineManagement/";
            const string methodName = nameof(RemoveMachineElectricalInfoByRecordId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, parameters: values, token: token)
            );

            return task.GetAwaiter().GetResult();
        }

        public static string RemoveMachineOilAndLubricationInfoByRecordId(
            InputRemoveMachineOilAndLubricationInfoByRecordId values,
            string token)
        {
            var url = $"{BaseUrl}/MachineManagement/";
            const string methodName = nameof(RemoveMachineOilAndLubricationInfoByRecordId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, parameters: values, token: token)
            );

            return task.GetAwaiter().GetResult();
        }


        public static string RegisterOilAndLubrication(InputRegisterOilAndLubrication values,
            string token)
        {
            var url = $"{BaseUrl}/MachineManagement/";
            const string methodName = nameof(RegisterOilAndLubrication);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, parameters: values, token: token)
            );

            return task.GetAwaiter().GetResult();
        }


        public static string AddMachineSparePart(InputAddMachineSparePart values, string token)
        {
            var url = $"{BaseUrl}/MachineManagement/";
            const string methodName = nameof(AddMachineSparePart);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, parameters: values, token: token)
            );

            return task.GetAwaiter().GetResult();
        }

        public static string AddMachineSparePartDocument(InputAddMachineSparePartDocument values, string token)
        {
            var url = $"{BaseUrl}/MachineManagement/";
            const string methodName = nameof(AddMachineSparePartDocument);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, parameters: values, token: token)
            );

            return task.GetAwaiter().GetResult();
        }

        public static string AddSubMachine(InputAddSubMachine values, string token)
        {
            var url = $"{BaseUrl}/MachineManagement/";
            const string methodName = nameof(AddSubMachine);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, parameters: values, token: token)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetMachineInfoByMachineId GetMachineInfoByMachineId(
            InputGetMachineInfoByMachineId input)
        {
            var url = $"{BaseUrl}/MachineManagement/";
            const string methodName = nameof(GetMachineInfoByMachineId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetMachineInfoByMachineId>.Post(
                        url,
                        methodName, parameters: input, token: null)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetMachineBuiltInfo GetMachineBuiltInfo(
            InputGetMachineBuiltInfo input)
        {
            var url = $"{BaseUrl}/MachineManagement/";
            const string methodName = nameof(GetMachineBuiltInfo);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetMachineBuiltInfo>.Post(
                        url,
                        methodName, parameters: input, token: null)
            );

            return task.GetAwaiter().GetResult();
        }


        public static OutputGetMachineElectricalInfo[] GetMachineElectricalInfo(
            InputGetMachineElectricalInfo input)
        {
            var url = $"{BaseUrl}/MachineManagement/";
            const string methodName = nameof(GetMachineElectricalInfo);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetMachineElectricalInfo[]>.Post(
                        url,
                        methodName, parameters: input, token: null)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetMachineOilAndLubricationInfo[] GetMachineOilAndLubricationInfo(
            InputGetMachineOilAndLubricationInfo input)
        {
            var url = $"{BaseUrl}/MachineManagement/";
            const string methodName = nameof(GetMachineOilAndLubricationInfo);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetMachineOilAndLubricationInfo[]>.Post(
                        url,
                        methodName, parameters: input, token: null)
            );

            return task.GetAwaiter().GetResult();
        }


        public static string AssignMachineToEmployee(InputAssignMachineToEmployee values, string token)
        {
            var url = $"{BaseUrl}/MachineManagement/";
            const string methodName = nameof(AssignMachineToEmployee);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, parameters: values, token: token)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetCharacteristicList[] GetCharacteristicList()
        {
            var url = $"{BaseUrl}/MachineManagement/";
            const string methodName = nameof(GetCharacteristicList);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetCharacteristicList[]>.Post(
                        url,
                        methodName, parameters: null)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetRpmList[] GetRpmList()
        {
            var url = $"{BaseUrl}/MachineManagement/";
            const string methodName = nameof(GetRpmList);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetRpmList[]>.Post(
                        url,
                        methodName, parameters: null)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetVoltageList[] GetVoltageList()
        {
            var url = $"{BaseUrl}/MachineManagement/";
            const string methodName = nameof(GetVoltageList);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetVoltageList[]>.Post(
                        url,
                        methodName, parameters: null)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetAllMainDepartment[] GetAllMainDepartment()
        {
            var url = $"{BaseUrl}/MachineManagement/";
            const string methodName = nameof(GetAllMainDepartment);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetAllMainDepartment[]>.Post(
                        url,
                        methodName, parameters: null)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetEmployeeListOfDepartment[] GetEmployeeListOfDepartment(
            InputGetEmployeeListOfDepartment values)
        {
            var url = $"{BaseUrl}/MachineManagement/";
            const string methodName = nameof(GetEmployeeListOfDepartment);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetEmployeeListOfDepartment[]>.Post(
                        url,
                        methodName, parameters: values)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetFileTypeList[] GetFileTypeList()
        {
            var url = $"{BaseUrl}/MachineManagement/";
            const string methodName = nameof(GetFileTypeList);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetFileTypeList[]>.Post(
                        url,
                        methodName, parameters: null)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetHierarchicalMachineList[] GetHierarchicalMachineList(
            InputGetHierarchicalMachineList values)
        {
            var url = $"{BaseUrl}/MachineManagement/";
            const string methodName = nameof(GetHierarchicalMachineList);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetHierarchicalMachineList[]>.Post(
                        url,
                        methodName, parameters: values)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetMachineDocumentListByMachineId[] GetMachineDocumentListByMachineId(
            InputGetMachineDocumentListByMachineId values)
        {
            var url = $"{BaseUrl}/MachineManagement/";
            const string methodName = nameof(GetMachineDocumentListByMachineId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetMachineDocumentListByMachineId[]>.Post(
                        url,
                        methodName, parameters: values)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetMachineLocationByMachineId[] GetMachineLocationByMachineId(
            InputGetMachineLocationByMachineId values)
        {
            var url = $"{BaseUrl}/MachineManagement/";
            const string methodName = nameof(GetMachineLocationByMachineId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetMachineLocationByMachineId[]>.Post(
                        url,
                        methodName, parameters: values)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetMachineSparePartDocumentListByMachineSparePartId[]
            GetMachineSparePartDocumentListByMachineSparePartId(
                InputGetMachineSparePartDocumentListByMachineSparePartId values)
        {
            var url = $"{BaseUrl}/MachineManagement/";
            const string methodName = nameof(GetMachineSparePartDocumentListByMachineSparePartId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetMachineSparePartDocumentListByMachineSparePartId[]>.Post(
                        url,
                        methodName, parameters: values)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetMachineSparePartListByMachineId[] GetMachineSparePartListByMachineId(
            InputGetMachineSparePartListByMachineId values)
        {
            var url = $"{BaseUrl}/MachineManagement/";
            const string methodName = nameof(GetMachineSparePartListByMachineId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetMachineSparePartListByMachineId[]>.Post(
                        url,
                        methodName, parameters: values)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetStockListByStoreCodeForSparePart[] GetStockListByStoreCodeForSparePart(
            InputGetStockListByStoreCodeForSparePart values)
        {
            const string methodName = nameof(GetStockListByStoreCodeForSparePart);
            var url = $"{BaseUrl}/MachineManagement/";

            var task = Task.Run(async () => await ApiConnector<OutputGetStockListByStoreCodeForSparePart[]>.Post(url,
                methodName, null,
                values));

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetStoreListForSparePart[] GetStoreListForSparePart()
        {
            var url = $"{BaseUrl}/MachineManagement/";
            const string methodName = nameof(GetStoreListForSparePart);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetStoreListForSparePart[]>.Post(
                        url,
                        methodName, parameters: null)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetTopMachineListByCondition[] GetTopMachineListByCondition(
            InputGetTopMachineListByCondition input)
        {
            var url = $"{BaseUrl}/MachineManagement/";
            const string methodName = nameof(GetTopMachineListByCondition);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetTopMachineListByCondition[]>.Post(
                        url,
                        methodName, parameters: input)
            );

            return task.GetAwaiter().GetResult();
        }

        public static string RemoveAssignMachineToEmployee(InputRemoveAssignMachineToEmployee values, string token)
        {
            var url = $"{BaseUrl}/MachineManagement/";
            const string methodName = nameof(RemoveAssignMachineToEmployee);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, parameters: values, token: token)
            );

            return task.GetAwaiter().GetResult();
        }

        public static string RemoveMachineDocumentByMachineDocumentId(
            InputRemoveMachineDocumentByMachineDocumentId input, string token)
        {
            var url = $"{BaseUrl}/MachineManagement/";
            const string methodName = nameof(RemoveMachineDocumentByMachineDocumentId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, parameters: input, token: token)
            );

            return task.GetAwaiter().GetResult();
        }

        public static string RemoveMachineSparePartByMachineSparePartId(
            InputRemoveMachineSparePartByMachineSparePartId input, string token)
        {
            var url = $"{BaseUrl}/MachineManagement/";
            const string methodName = nameof(RemoveMachineSparePartByMachineSparePartId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, parameters: input, token: token)
            );

            return task.GetAwaiter().GetResult();
        }

        public static string RemoveMachineSparePartDocumentByMachineSparePartDocumentId(
            InputRemoveMachineSparePartDocumentByMachineSparePartDocumentId input,
            string token)
        {
            var url = $"{BaseUrl}/MachineManagement/";
            const string methodName = nameof(RemoveMachineSparePartDocumentByMachineSparePartDocumentId);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, parameters: input, token: token)
            );

            return task.GetAwaiter().GetResult();
        }

        public static string RemoveUploadedAttachmentsOfMachine(InputRemoveUploadedAttachmentsOfMachine input,
            string token)
        {
            var url = $"{BaseUrl}/MachineManagement/";
            const string methodName = nameof(RemoveUploadedAttachmentsOfMachine);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, parameters: input, token: token)
            );

            return task.GetAwaiter().GetResult();
        }
    }
}