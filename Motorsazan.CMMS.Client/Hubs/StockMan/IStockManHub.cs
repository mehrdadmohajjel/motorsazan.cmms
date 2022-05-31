namespace Motorsazan.CMMS.Client.Hubs.StockMan
{
    public interface IStockManHub
    {
        void NewWorkOrderAdded();

        void WorkOrderStatusChanged(int workOrderId);
    }
}