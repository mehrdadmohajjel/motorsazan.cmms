using System;

namespace Motorsazan.CMMS.Shared.Models.Output.MachineManagement
{
    public class OutputGetEmployeeListOfDepartment
    {
        public int Id { get; set; }

        public string EId { get; set; }

        public string FName { get; set; }

        public string LName { get; set; }

        public string IdNo { get; set; }

        public string MelliCode { get; set; }

        public string FatherName { get; set; }

        public DateTime BirthEnDate { get; set; }

        public string BornCity { get; set; }

        public string SodoorCity { get; set; }

        public bool Gender { get; set; }

        public int IdSerial { get; set; }

        public string IdPreSerial { get; set; }

        public string Nationality { get; set; }

        public string Address { get; set; }

        public string Telephone { get; set; }

        public string PrimaryMobile { get; set; }

        public string Jebhe { get; set; }

        public bool Marital { get; set; }

        public DateTime MaritalEnDate { get; set; }

        public string Body { get; set; }

        public string Blood { get; set; }

        public string ReferName { get; set; }

        public string ProfilePic { get; set; }

        public bool IsActive { get; set; }
    }
}