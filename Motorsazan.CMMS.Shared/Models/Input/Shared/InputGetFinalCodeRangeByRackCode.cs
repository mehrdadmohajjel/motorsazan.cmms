namespace Motorsazan.CMMS.Shared.Models.Input.Shared
{
    public class InputGetFinalCodeRangeByRackCode
    {
        public long Skip { get; set; }

        public int PageCount { get; set; }

        public string FilterKeyWord { get; set; }

        public string RackCodeGroup { get; set; }
    }
}