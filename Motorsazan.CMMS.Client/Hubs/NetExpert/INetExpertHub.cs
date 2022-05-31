namespace Motorsazan.CMMS.Client.Hubs.NetExpert
{
    public interface INetExpertHub
    {
        void NewWorkOrderAdded();

        void WorkOrderReferredToAnotherMaintenanceGroup(int receiverMaintenanceGroupId);

        void WorkOrderReferredToAnotherPerson(int receiverEId);

        void WorkOrderStatusChanged(int workOrderId);
    }
}