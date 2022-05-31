namespace Motorsazan.CMMS.Shared.Models.Output.Shared
{
    public class OutputGetProductionLineList
    {
        public int DepartmentId { get; set; }

        public string UnitCode { get; set; }

        public string Title { get; set; }

        public string ParentUnitCode { get; set; }
    }
}