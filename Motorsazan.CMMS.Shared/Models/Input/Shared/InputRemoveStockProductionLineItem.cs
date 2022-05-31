using System.ComponentModel.DataAnnotations;

namespace Motorsazan.CMMS.Shared.Models.Input.Shared
{
    public class InputRemoveStockProductionLineItem
    {
        [Required]
        public long StockProductionLineID { get; set; }
    }
}