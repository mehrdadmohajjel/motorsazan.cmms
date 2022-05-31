using Microsoft.AspNet.SignalR;

namespace Motorsazan.CMMS.Client.Hubs.StockMan
{
    public class StockManHub: Hub<IStockManHub>
    {
        public void NewWorkOrderAdded() => Clients.Others.NewWorkOrderAdded();

        public void WorkOrderStatusChanged(int workOrderId) =>
            Clients.Others.WorkOrderStatusChanged(workOrderId);
    }
}