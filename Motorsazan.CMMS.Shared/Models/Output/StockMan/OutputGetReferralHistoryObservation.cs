using System;

namespace Motorsazan.CMMS.Shared.Models.Output.StockMan
{
    public class OutputGetReferralHistoryObservation
    {
        public string Referring { get; set; }

        public string ReferralRecipient { get; set; }

        public string ReferralDate { get; set; }

        public string ReferralTime { get; set; }
    }
}
