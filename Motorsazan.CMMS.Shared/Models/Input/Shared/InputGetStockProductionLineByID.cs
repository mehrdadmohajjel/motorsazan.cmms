using System.ComponentModel.DataAnnotations;

namespace Motorsazan.CMMS.Shared.Models.Input.Shared
{
    public class InputGetStockProductionLineByID
    {
        [Required]
        public long StockProductionLineID { get; set; }
    }
}