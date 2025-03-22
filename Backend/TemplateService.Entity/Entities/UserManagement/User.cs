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
    [DebuggerDisplay("Id={Id}, CreationDate={CreationDate}, FirstName={FirstName}, MiddleName={MiddleName}, LastName={LastName}, Username={Username}, Password={Password}, IsActive={IsActive}")]
    public class User : IEntityIdentity<long>, IDateTimeSignature/*, IEntityUserSignature*/, IDeletionSignature
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance from type
        /// User
        /// </summary>
        public User()
        {
            this.UserLogins = new HashSet<UserLogin>();
            this.UserPermissions = new HashSet<UserPermission>();
            this.UserRoles = new HashSet<UserRole>();
            
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
        public System.String Username { get; set; }
        public System.String Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        
        public Nullable<System.Boolean> IsActive { get; set; }
        public System.String ProfileImage { get; set; }
        public virtual ICollection<UserLogin> UserLogins { get; set; }
        public virtual ICollection<UserPermission> UserPermissions { get; set; }
        public virtual ICollection<UserRole> UserRoles { get ; set; }
        public bool IsSuperAdmin { get; set; }


        
        #endregion
    }
}
