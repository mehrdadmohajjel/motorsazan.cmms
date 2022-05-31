namespace Motorsazan.CMMS.Shared.Models.Input.Shared
{
    public class InputGetAllFinalCodeListForUnderConstructionStocks
    {
        public long Skip { get; set; }

        public int PageCount { get; set; }

        public string FilterKeyWord { get; set; }
    }
}