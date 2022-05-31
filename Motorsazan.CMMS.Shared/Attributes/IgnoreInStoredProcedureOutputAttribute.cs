using System;

namespace Motorsazan.CMMS.Shared.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class IgnoreInStoredProcedureOutputAttribute : Attribute
    {
    }
}