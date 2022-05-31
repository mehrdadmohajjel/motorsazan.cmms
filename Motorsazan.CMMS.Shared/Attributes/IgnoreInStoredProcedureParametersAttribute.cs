using System;

namespace Motorsazan.CMMS.Shared.Attributes
{
    [AttributeUsage(System.AttributeTargets.Property)]
    public class IgnoreInStoredProcedureParametersAttribute : Attribute
    {
    }
}