namespace Motorsazan.CMMS.Shared.Models.Output.Inspection
{
    public class OutputGetSubDepartmentListByMainDepartmentId
    {
        public int DepartmentId { get; set; }

        public string Title { get; set; }

        public int ParentUnitCode { get; set; }

        public int UnitCode { get; set; }
    }
}
