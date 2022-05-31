using Motorsazan.CMMS.Shared.Models.Output.Machine;
using System.Threading.Tasks;
using Motorsazan.CMMS.Shared.Models.Input.Machine;

namespace Motorsazan.CMMS.Client.Api
{
    public static partial class ApiList
    {
        public static OutputGetMachineLevelList[] GetMachineLevelList()
        {
            var url = $"{BaseUrl}/Machine/";
            const string methodName = nameof(GetMachineLevelList);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetMachineLevelList[]>.Post(
                        url,
                        methodName, parameters: null)
            );

            return task.GetAwaiter().GetResult();
        }

        public static string AddTopMachine(InputAddTopMachine values, string token)
        {
            var url = $"{BaseUrl}/Machine/";
            const string methodName = nameof(AddTopMachine);

            var task = Task.Run(
                async () =>
                    await ApiConnector<string>.Post(
                        url,
                        methodName, parameters: values, token: token)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetMachineTypeList[] GetMachineTypeList()
        {
            var url = $"{BaseUrl}/Machine/";
            const string methodName = nameof(GetMachineTypeList);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetMachineTypeList[]>.Post(
                        url,
                        methodName, parameters: null)
            );

            return task.GetAwaiter().GetResult();
        }

        public static OutputGetControlSystemTypeList[] GetControlSystemTypeList()
        {
            var url = $"{BaseUrl}/Machine/";
            const string methodName = nameof(GetControlSystemTypeList);

            var task = Task.Run(
                async () =>
                    await ApiConnector<OutputGetControlSystemTypeList[]>.Post(
                        url,
                        methodName, parameters: null)
            );

            return task.GetAwaiter().GetResult();
        }
    }
}