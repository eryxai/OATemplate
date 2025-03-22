using System;

namespace TemplateService.Common.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class AuditLogPropertyAttribute : Attribute
    {
        public bool LogProperty { get; set; } = true;
        public AuditLogPropertyAttribute(bool logProperty)
        {
            LogProperty = logProperty;
        }
        public AuditLogPropertyAttribute()
        {
        }
    }

}
