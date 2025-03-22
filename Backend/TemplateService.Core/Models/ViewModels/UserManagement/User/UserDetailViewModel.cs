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
    [DebuggerDisplay("Id={Id}, CreationDate={CreationDate}, FirstName={FirstName}, MiddleName={MiddleName}, LastName={LastName}, Username={Username}, Password={Password}, IsActive={IsActive}")]
    public class UserDetailViewModel : Base.BaseViewModel
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance from type
        /// UserViewModel
        /// </summary>
        public UserDetailViewModel()
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

        public System.String Username { get; set; }
        public System.String Password { get; set; }
        public Nullable<System.Boolean> IsActive { get; set; }

        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public System.String ProfileImage { get; set; }



        public int? OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public string DepartmentName { get; set; }


        public List<long?> RoleIds { get; set; }

        public string RoleName { get; set; }


        #endregion
    }
}
