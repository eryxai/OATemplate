using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateService.Common.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Class,AllowMultiple =false,Inherited =false)]
    public class AuditLogAttribute: Attribute
    {
        public string LogAction { get; set; }
        public bool CheckProperties { get; set; } = false;
        public bool LogAllProperties { get; set; } = false;
        public AuditLogAttribute(string LogAction, bool checkProperties=false, bool logAllProperties=false)
        {
            this.LogAction = LogAction;
            CheckProperties = checkProperties;
            LogAllProperties = logAllProperties;


        }
      

        public AuditLogAttribute(bool checkProperties)
        {
            CheckProperties = checkProperties;
        }

        public AuditLogAttribute()
        {
        }
    }

}
