using Motorsazan.CMMS.Shared.Attributes;

namespace Motorsazan.CMMS.Shared.Models.Input.MachineManagement
{
    public class InputGetStockListByStoreCodeForSparePart
    {
        [StoredProcedureParameter(Size = 20)]
        public string StoreCode { get; set; }

        public int PageCount { get; set; }

        public long Skip { get; set; }

        [StoredProcedureParameter(Size = 200)]
        public string FilterKeyWord { get; set; }
    }
}