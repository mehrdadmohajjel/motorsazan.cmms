namespace Motorsazan.CMMS.Shared.Models.Output.MachineManagement
{
    public class OutputGetMachineSparePartDocumentListByMachineSparePartId
    {
        public long MachineSparePartDocumentId { get; set; }

        public string MechanicalSpecification { get; set; }

        public string ElectricalSpecification { get; set; }

        public string MadeInCompany { get; set; }

        public string FileTitle { get; set; }

        public string Description { get; set; }

        public string FileName { get; set; }
    }
}