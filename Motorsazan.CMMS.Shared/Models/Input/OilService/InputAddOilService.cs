using System;
using System.Data;
using Motorsazan.CMMS.Shared.Attributes;
using Motorsazan.CMMS.Shared.Models.DomainModels;

namespace Motorsazan.CMMS.Shared.Models.Input.OilService
{
    public class InputAddOilService
    {
        [StoredProcedureParameter(SqlDbType = SqlDbType.Date)]
        public DateTime PreferDate { get; set; }

        public int DepartmentId { get; set; }

        public long MachineId { get; set; }

        public long StockId { get; set; }

        public long UserId { get; set; }

        public long OilServicePlaceId { get; set; }

        public long MeasurementUnitId { get; set; }

        [StoredProcedureParameter(SqlDbType = SqlDbType.Decimal)]
        public long Quantity { get; set; }

        [StoredProcedureParameter(SqlDbType = SqlDbType.Money)]
        public decimal Price { get; set; }

        [StoredProcedureParameter(SqlDbType = SqlDbType.Bit)]
        public bool IsUsedOilNormal { get; set; }

        [StoredProcedureParameter(SqlDbType = SqlDbType.Structured)]
        public TblEmployeeIdList[] EmployeeIdList { get; set; }

        public int PersonHour { get; set; }
    }
}