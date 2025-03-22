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
    [DebuggerDisplay("Id={Id}, CreationDate={CreationDate}, NameArabic={NameArabic}, NameEnglish={NameEnglish}, Code={Code}, Date={Date}, IsActive={IsActive}, DescriptionArabic={DescriptionArabic}, DescriptionEnglish={DescriptionEnglish}")]
    public class RoleViewViewModel : Base.BaseViewModel
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance from type
        /// RoleViewViewModel
        /// </summary>
        public RoleViewViewModel()
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

        public System.String NameArabic { get; set; }
        public System.String NameEnglish { get; set; }
        public System.String Code { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public System.String DescriptionAr { get; set; }
        public System.String DescriptionEn { get; set; }

        #region Relation: UserRole
        public virtual IList<UserRoleViewViewModel> UserRoles { get; set; }
        #endregion

        #region Relation: RolePermission
        public virtual IList<RolePermissionViewViewModel> RolePermissions { get; set; }
        #endregion



        #endregion
    }
}
