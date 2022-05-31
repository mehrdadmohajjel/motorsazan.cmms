using Newtonsoft.Json;

namespace Motorsazan.CMMS.Shared.Models.Output.PLCReport
{
    public class OutputChangeJobStatus
    {
        [JsonProperty("d")] public string JobStatus { get; set; }

        [JsonIgnore] public bool IsSuccess { get; set; }
    }
}