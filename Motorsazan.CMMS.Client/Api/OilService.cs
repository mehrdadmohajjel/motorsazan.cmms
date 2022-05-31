using System.Threading.Tasks;
using Motorsazan.CMMS.Shared.Models.Input.OilService;
using Motorsazan.CMMS.Shared.Models.Output.OilService;

namespace Motorsazan.CMMS.Client.Api
{
    public static partial class ApiList
    {
        public static string AddOilService(InputAddOilService values, string token)
        {
            var url = $"{BaseUrl}/OilService/";
            const string methodName = nameof(AddOilService);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, parameters: values, token: token)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetMaterials[] GetMaterials()
        {
            var url = $"{BaseUrl}/OilService/";
            const string methodName = nameof(GetMaterials);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetMaterials[]>.Post(
                        url,
                        methodName, parameters: null)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetMeasurementUnitList[] GetMeasurementUnitList()
        {
            var url = $"{BaseUrl}/OilService/";
            const string methodName = nameof(GetMeasurementUnitList);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetMeasurementUnitList[]>.Post(
                        url,
                        methodName, parameters: null)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetOilServiceListByCondition[] GetOilServiceListByCondition(
            InputGetOilServiceListByCondition values)
        {
            var url = $"{BaseUrl}/OilService/";
            const string methodName = nameof(GetOilServiceListByCondition);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetOilServiceListByCondition[]>.Post(
                        url,
                        methodName, parameters: values)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetOilServicePlaceList[] GetOilServicePlaceList()
        {
            var url = $"{BaseUrl}/OilService/";
            const string methodName = nameof(GetOilServicePlaceList);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetOilServicePlaceList[]>.Post(
                        url,
                        methodName, parameters: null)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetServiceMaintenanceGroupMemberList[] GetServiceMaintenanceGroupMemberList()
        {
            var url = $"{BaseUrl}/OilService/";
            const string methodName = nameof(GetServiceMaintenanceGroupMemberList);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetServiceMaintenanceGroupMemberList[]>.Post(
                        url,
                        methodName, parameters: null)
            );

            return task.GetAwaiter().GetResult();
        }



        public static string RemoveOilService(InputRemoveOilService values, string token)
        {
            var url = $"{BaseUrl}/OilService/";
            const string methodName = nameof(RemoveOilService);

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