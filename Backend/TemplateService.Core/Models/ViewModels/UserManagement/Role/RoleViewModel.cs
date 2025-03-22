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
    [DebuggerDisplay("Id={Id}, CreationDate={CreationDate}, Name={Name}, Code={Code}, Date={Date}, IsActive={IsActive}, Description={Description}")]
    public class RoleViewModel : Base.BaseViewModel
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance from type
        /// RoleViewModel
        /// </summary>
        public RoleViewModel()
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
      //  public System.String Code { get; set; }
      //  public Nullable<System.DateTime> Date { get; set; }
        public System.String DescriptionAr { get; set; }
        public System.String DescriptionEn { get; set; }
        public Nullable<int> OrganizationId { get; set; }



        #region Relation: UserRole
        public virtual IList<UserRoleViewModel> UserRoles { get; set; }
        #endregion

        #region Relation: RolePermission
        public virtual IList<NameValueNumericViewModel> permissions { get; set; }
        #endregion


        #endregion
    }
}
