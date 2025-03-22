using Framework.Common.Interfaces;
using System;
using System.Collections.Generic;

namespace TemplateService.Entity.Entities
{
    public class UserAuditLog : IEntityIdentity<long>
    {
        public long Id { get; set; }
        public DateTime CreationDate { get; set; }
        public long? UserId { get; set; }
        public string TableName { get; set; }
        public string Action { get; set; }            // INSERT, UPDATE, DELETE
        public string KeyValues { get; set; }         // JSON or string describing key
        public string OldValues { get; set; }         // JSON
        public string NewValues { get; set; }         // JSON
        public string IpAddress { get; set; }         // From your request, if available
        public string EndPoint { get; set; }         // From your request, if available




    }
}
