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
    [DebuggerDisplay("Id={Id}, CreationDate={CreationDate}, Name={Name}, Code={Code}, IsActive={IsActive}, Description={Description}")]
    public class PermissionViewModel : Base.BaseViewModel
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance from type
        /// PermissionViewModel
        /// </summary>
        public PermissionViewModel()
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

        public System.String NameAr { get; set; }
        public System.String NameEn { get; set; }

        public Nullable<System.Int32> Code { get; set; }
        public Nullable<System.Boolean> IsActive { get; set; }
        public System.String Description { get; set; }

        #region Relation: RolePermission
        public virtual IList<RolePermissionViewModel> RolePermissions { get; set; }
        #endregion

        #region Relation: UserPermission
        public virtual IList<UserPermissionViewModel> UserPermissions { get; set; }
        #endregion




        #endregion
    }
}
