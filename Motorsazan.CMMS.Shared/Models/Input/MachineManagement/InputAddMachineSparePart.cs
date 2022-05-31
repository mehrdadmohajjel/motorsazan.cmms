using System;
using Motorsazan.CMMS.Shared.Attributes;

namespace Motorsazan.CMMS.Shared.Models.Input.MachineManagement
{
    public class InputAddMachineSparePart
    {
        public long MachineId { get; set; }

        public long StockId { get; set; }

        [StoredProcedureParameter(Size = 50)]
        public string WearhouseCode { get; set; }

        public bool IsInternalSupply { get; set; }

        public bool IsPrivate { get; set; }

        public DateTime UsedDate { get; set; }

        public int UserId { get; set; }

        [StoredProcedureParameter(Size = 50)]
        public string TechnicalCharacteristicsOfCatalog { get; set; }
    }
}