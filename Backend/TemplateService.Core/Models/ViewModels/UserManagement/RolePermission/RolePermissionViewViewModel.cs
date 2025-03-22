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
    public class RolePermissionViewViewModel : Base.BaseViewModel
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance from type
        /// RolePermissionViewViewModel
        /// </summary>
        public RolePermissionViewViewModel()
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
        public System.String RoleName { get; set; }
        public Nullable<long> RoleId { get; set; }
        public virtual RoleViewViewModel Role { get; set; }
        #endregion

        #region Relation: Permission
        public System.String PermissionName { get; set; }
        public Nullable<long> PermissionId { get; set; }
        public virtual PermissionViewViewModel Permission { get; set; }
        #endregion

        #endregion
    }
}
