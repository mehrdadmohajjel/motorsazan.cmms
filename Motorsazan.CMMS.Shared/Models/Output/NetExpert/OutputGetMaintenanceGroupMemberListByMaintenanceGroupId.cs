using System.Data;
using Motorsazan.CMMS.Shared.Attributes;

namespace Motorsazan.CMMS.Shared.Models.Output.NetExpert
{
    public class OutputGetMaintenanceGroupMemberListByMaintenanceGroupId
    {
        public long MaintenanceGroupMemberId { get; set; }

        public int EmployeeId { get; set; }

        public int EId { get; set; }

        public string Name { get; set; }

        public string EmployeeRole { get; set; }

        [StoredProcedureParameter(SqlDbType = SqlDbType.Bit)]
        public bool IsSuperviser { get; set; }
    }
}