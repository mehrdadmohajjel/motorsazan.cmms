using Microsoft.AspNet.SignalR;

namespace Motorsazan.CMMS.Client.Hubs.NetExpert
{
    public class NetExpertHub: Hub<INetExpertHub>
    {
        public void NewWorkOrderAdded() => Clients.Others.NewWorkOrderAdded();

        public void WorkOrderReferredToAnotherMaintenanceGroup(int receiverMaintenanceGroupId) =>
            Clients.Others.WorkOrderReferredToAnotherMaintenanceGroup(receiverMaintenanceGroupId);

        public void WorkOrderReferredToAnotherPerson(int receiverEId) => 
            Clients.Others.WorkOrderReferredToAnotherPerson(receiverEId);

        public void WorkOrderStatusChanged(int workOrderId) =>
            Clients.Others.WorkOrderStatusChanged(workOrderId);
    }
}