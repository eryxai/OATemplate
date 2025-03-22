#region Using ...
using System;
using System.Collections.Generic;
using System.Diagnostics;
#endregion

/*


*/
namespace TemplateService.Core.Models.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    [DebuggerDisplay("Id={Id}, CreationDate={CreationDate}, FirstNameArabic={FirstNameArabic}, FirstNameEnglish={FirstNameEnglish}, MiddleNameArabic={MiddleNameArabic}, MiddleNameEnglish={MiddleNameEnglish}, LastNameArabic={LastNameArabic}, LastNameEnglish={LastNameEnglish}, Username={Username}, Password={Password}, IsActive={IsActive}")]
    public class UserViewViewModel : Base.BaseViewModel
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance from type
        /// UserViewViewModel
        /// </summary>
        public UserViewViewModel()
        {
            
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

        public System.String Name { get; set; }
        public System.String Username { get; set; }
        public System.String Password { get; set; }
        public Nullable<System.Boolean> IsActive { get; set; }
        public string NationalID { get; set; }

        public string DepartmentName { get; set; }

        public bool IsSuperAdmin { get; set; }

        #region Relation: UserLogin
        public virtual IList<UserLoginViewViewModel> UserLogins { get; set; }
        #endregion

        #region Relation: UserPermission
        public virtual IList<UserPermissionViewViewModel> UserPermissions { get; set; }
        #endregion

        #region Relation: UserRole
        public virtual IList<UserRoleViewViewModel> UserRoles { get; set; }
        #endregion

       

        #endregion
    }
}
