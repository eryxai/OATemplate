#region Using ...
using Framework.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
#endregion

/*


*/
namespace TemplateService.Entity.Entities
{
    /// <summary>
    /// 
    /// </summary>
    [DebuggerDisplay("Id={Id}, CreationDate={CreationDate}, Name={Name}, Code={Code}, Date={Date}, IsActive={IsActive}, Description={Description}")]
    public class Role : IEntityIdentity<long>, IDateTimeSignature, IEntityUserSignature, IDeletionSignature
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance from type
        /// Role
        /// </summary>
        public Role()
        {
            this.UserRoles = new HashSet<UserRole>();
            this.RolePermissions = new HashSet<RolePermission>();
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

        public System.String NameAr { get; set; }
        public System.String NameEn { get; set; }
        public System.String DescriptionAr { get; set; }
        public System.String DescriptionEn { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<RolePermission> RolePermissions { get; set; }


        

        

        #endregion
    }
}
