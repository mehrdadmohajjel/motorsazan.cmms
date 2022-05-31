using System;
using System.Data;
using Motorsazan.CMMS.Shared.Attributes;

namespace Motorsazan.CMMS.Shared.Models.Input.OilService
{
    public class InputRemoveOilService
    {
        public long OilServiceId { get; set; }

        public long UserId { get; set; }
    }
}