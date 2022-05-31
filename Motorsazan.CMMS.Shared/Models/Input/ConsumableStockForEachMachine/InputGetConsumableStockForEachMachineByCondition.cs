using Motorsazan.CMMS.Shared.Attributes;
using System;
using System.Data;

namespace Motorsazan.CMMS.Shared.Models.Input.ConsumableStockForEachMachineByCondition
{
    public class InputGetConsumableStockForEachMachineByCondition
    {
        public int DepartmentId { get; set; }

        public long MachineId { get; set; }

        [StoredProcedureParameter(SqlDbType = SqlDbType.Date)]
        public DateTime StartDate { get; set; }

        [StoredProcedureParameter(SqlDbType = SqlDbType.Date)]
        public DateTime EndDate { get; set; }

    }
}
