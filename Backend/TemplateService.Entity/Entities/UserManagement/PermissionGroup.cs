
#region Using ...
using Framework.Common.Interfaces;
using System;
using System.Collections.Generic;
#endregion

namespace TemplateService.Entity.Entities
{
    public class PermissionGroup : IEntityIdentity<long>, IDateTimeSignature, IEntityUserSignature, IDeletionSignature
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance from type
        /// Permission
        /// </summary>
        public PermissionGroup()
        {
            this.Permissions = new HashSet<Permission>();
        }
        #endregion

        #region Properties

        #region IEntityIdentity<long>
        public long Id { get; set; }
        #endregion

        #region IDateTimeSignature
        public DateTime CreationDate { get; set; }
        public DateTime? FirstModificationDate { get; set; }
        public DateTime? LastModificationDate { get; set; }
        #endregion

        #region IEntityUserSignature
        public long? CreatedByUserId { get; set; }
        public long? FirstModifiedByUserId { get; set; }
        public long? LastModifiedByUserId { get; set; }
        #endregion

        #region IDeletionSignature
        public bool IsDeleted { get; set; }
        public DateTime? DeletionDate { get; set; }
        public long? DeletedByUserId { get; set; }
        public bool? MustDeletedPhysical { get; set; }
        #endregion
        public Nullable<long> ParentId { get; set; }
        public virtual PermissionGroup Parent { get; set; }
        public virtual ICollection<PermissionGroup> ChildPermissionGroup { get; set; }
        public System.String NameAr { get; set; }
        public System.String NameEn { get; set; }

        public Nullable<System.Boolean> IsActive { get; set; }

        public virtual ICollection<Permission> Permissions { get; set; }

        #endregion
    }
}
