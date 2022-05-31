using DevExpress.Web;
using Motorsazan.CMMS.Client.Api;
using Motorsazan.CMMS.Shared.Models.Input.MachineManagement;
using Motorsazan.CMMS.Shared.Models.Input.Shared;
using Motorsazan.CMMS.Shared.Models.Output.MachineManagement;
using Motorsazan.CMMS.Shared.Models.Output.Shared;

namespace Motorsazan.CMMS.Client.Models.ViewModels
{
    public class PagedStocks
    {
        private static OutputGetAllFinalCodeListForUnderConstructionStocks[] GetAllFinalCodeListForUnderConstructionStocks(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            var apiParam = new InputGetAllFinalCodeListForUnderConstructionStocks
            {
                FilterKeyWord = args.Filter,
                PageCount = 100,
                Skip = args.BeginIndex
            };
            var result = ApiList.GetAllFinalCodeListForUnderConstructionStocks(apiParam);
            return result;
        }

        public static OutputGetFinalCodeRangeByRackCode GetFinalCodeRangeById(ListEditItemRequestedByValueEventArgs args)
        {
            return args.Value == null || !int.TryParse(args.Value.ToString(), out var CodeID)
                ? null
                : new OutputGetFinalCodeRangeByRackCode { Id = CodeID };
        }

        public static OutputGetStockListByStoreCodeForSparePart GetFinalMachineManagementSparePartCodeRangeById(ListEditItemRequestedByValueEventArgs args)
        {
            return args.Value == null || !int.TryParse(args.Value.ToString(), out var StockID)
                ? null
                : new OutputGetStockListByStoreCodeForSparePart { Id = StockID };
        }

        public static OutputGetFinalCodeRangeByRackCode[] GetFinalCodeRangeByRackCode(string rackCode, ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            var apiParam = new InputGetFinalCodeRangeByRackCode
            {
                FilterKeyWord = args.Filter,
                PageCount = 100,
                Skip = args.BeginIndex,
                RackCodeGroup = rackCode
            };
            var result = ApiList.GetFinalCodeRangeByRackCode(apiParam);
            return result;
        }

        public static OutputGetStockListByStoreCodeForSparePart[] GetFinalStockListByStoreCodeForSparePart(string storeCode, ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            var apiParam = new InputGetStockListByStoreCodeForSparePart
            {
                FilterKeyWord = args.Filter,
                PageCount = 100,
                Skip = args.BeginIndex,
                StoreCode = storeCode
            };
            var result = ApiList.GetStockListByStoreCodeForSparePart(apiParam);
            return result;
        }

        public static OutputGetFinalCodeRangeByRackCode[] GetFinalCodeRangeByRackCode11(ListEditItemsRequestedByFilterConditionEventArgs args) =>
            GetFinalCodeRangeByRackCode("11", args);

        public static OutputGetFinalCodeRangeByRackCode[] GetFinalCodeRangeByRackCode12(ListEditItemsRequestedByFilterConditionEventArgs args) =>
            GetFinalCodeRangeByRackCode("12", args);

        public static OutputGetFinalCodeRangeByRackCode[] GetFinalCodeRangeByRackCode14(ListEditItemsRequestedByFilterConditionEventArgs args) =>
            GetFinalCodeRangeByRackCode("14", args);

        public static OutputGetFinalCodeRangeByRackCode[] GetFinalCodeRangeByRackCode15(ListEditItemsRequestedByFilterConditionEventArgs args) =>
            GetFinalCodeRangeByRackCode("15", args);

        public static OutputGetFinalCodeRangeByRackCode[] GetFinalCodeRangeByRackCode61(ListEditItemsRequestedByFilterConditionEventArgs args) =>
            GetFinalCodeRangeByRackCode("61", args);

        public static OutputGetFinalCodeRangeByRackCode[] GetFinalCodeRangeByRackCode62(ListEditItemsRequestedByFilterConditionEventArgs args) =>
            GetFinalCodeRangeByRackCode("62", args);

        public static OutputGetFinalCodeRangeByRackCode[] GetFinalCodeRangeByRackCode73(ListEditItemsRequestedByFilterConditionEventArgs args) =>
            GetFinalCodeRangeByRackCode("73", args);

        public static OutputGetStockListByStoreCodeForSparePart[] GetFinalStockListByStoreCodeForSparePart11(ListEditItemsRequestedByFilterConditionEventArgs args) =>
            GetFinalStockListByStoreCodeForSparePart("29-421998", args);

        public static OutputGetStockListByStoreCodeForSparePart[] GetFinalStockListByStoreCodeForSparePart12(ListEditItemsRequestedByFilterConditionEventArgs args) =>
            GetFinalStockListByStoreCodeForSparePart("29-422998", args);

        public static OutputGetStockListByStoreCodeForSparePart[] GetFinalStockListByStoreCodeForSparePart14(ListEditItemsRequestedByFilterConditionEventArgs args) =>
            GetFinalStockListByStoreCodeForSparePart("29-423998", args);

        public static OutputGetStockListByStoreCodeForSparePart[] GetFinalStockListByStoreCodeForSparePart61(ListEditItemsRequestedByFilterConditionEventArgs args) =>
            GetFinalStockListByStoreCodeForSparePart("29-545998", args);

        public static OutputGetStockListByStoreCodeForSparePart[] GetFinalStockListByStoreCodeForSparePart62(ListEditItemsRequestedByFilterConditionEventArgs args) =>
            GetFinalStockListByStoreCodeForSparePart("29-546998", args);

        public static OutputGetStockListByStoreCodeForSparePart[] GetFinalStockListByStoreCodeForSparePart73(ListEditItemsRequestedByFilterConditionEventArgs args) =>
            GetFinalStockListByStoreCodeForSparePart("29-418998", args);

        public static OutputGetAllFinalCodeListForUnderConstructionStocks[] GetFinalCodeRangeByUnderConstruction(ListEditItemsRequestedByFilterConditionEventArgs args) =>
            GetAllFinalCodeListForUnderConstructionStocks(args);
    }
}
