#region Using ...
using System;
using System.Diagnostics;
#endregion

/*


*/
namespace TemplateService.Core.Models.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    [DebuggerDisplay("Id={Id}, CreationDate={CreationDate}")]
    public class UserRoleViewModel : Base.BaseViewModel
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance from type
        /// UserRoleViewModel
        /// </summary>
        public UserRoleViewModel()
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


        #region Relation: Role
        public Nullable<long> RoleId { get; set; }
        public virtual RoleViewModel Role { get; set; }
        #endregion

        #region Relation: User
        public Nullable<long> UserId { get; set; }
        public virtual UserViewModel User { get; set; }
        #endregion

        #endregion
    }
}
