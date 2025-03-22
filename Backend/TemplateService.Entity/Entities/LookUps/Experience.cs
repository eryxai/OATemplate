using Framework.Common.Interfaces;
using System;
using System.Collections.Generic;

namespace TemplateService.Entity.Entities
{
    public class Experience : IEntityIdentity<long>, IDateTimeSignature, IEntityUserSignature, IDeletionSignature
    {
        public long Id { get; set; }
        public DateTime? FirstModificationDate { get; set; }
        public DateTime? LastModificationDate { get; set; }
        public DateTime CreationDate { get; set; }
        public long? FirstModifiedByUserId { get; set; }
        public long? LastModifiedByUserId { get; set; }
        public long? CreatedByUserId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletionDate { get; set; }
        public long? DeletedByUserId { get; set; }
        public bool? MustDeletedPhysical { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public long? ParentId { get; set; }
        public Experience Parent { get; set; }
        public ICollection<Experience> Children { get; set; }
      
    }
}
