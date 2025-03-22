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
    public class UserPermissionLightViewModel : Base.BaseViewModel
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance from type
        /// UserPermissionLightViewModel
        /// </summary>
        public UserPermissionLightViewModel()
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


        #region Relation: User
        public System.String UserUsername { get; set; }
        public Nullable<long> UserId { get; set; }
        public virtual UserLightViewModel User { get; set; }
        #endregion

        #region Relation: Permission
        public Nullable<long> PermissionId { get; set; }
        public virtual PermissionLightViewModel Permission { get; set; }
        #endregion

        #endregion
    }
}
