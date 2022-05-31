namespace Motorsazan.CMMS.Shared.Models.Output.MachineManagement
{
    public class OutputGetMachineDocumentListByMachineId
    {
        public long MachineDocumentId { get; set; }

        public string FileTypeShowName { get; set; }

        public string FileTitle { get; set; }

        public string FileName { get; set; }
    }
}