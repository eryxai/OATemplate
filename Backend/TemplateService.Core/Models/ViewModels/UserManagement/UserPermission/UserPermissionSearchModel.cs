#region Using ...
using System;
using System.Diagnostics;
using Framework.Core.Models;
#endregion

/*


*/
namespace TemplateService.Core.Models.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    [DebuggerDisplay("Pagination={Pagination}, Sorting={Sorting}")]
    public class UserPermissionSearchModel : BaseFilter
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance from type
        /// UserPermissionSearchModel
        /// </summary>
        public UserPermissionSearchModel()
        {
            
        }
        #endregion

        #region Properties

        #region IDateTimeSignature
        public DateTime? MinCreationDate { get; set; }
        public DateTime? MaxCreationDate { get; set; }
        public DateTime? MinFirstModificationDate { get; set; }
        public DateTime? MaxFirstModificationDate { get; set; }
        public DateTime? MinLastModificationDate { get; set; }
        public DateTime? MaxLastModificationDate { get; set; }
        #endregion

        #region IEntityUserSignature
        public long? CreatedByUserId { get; set; }
        public long? FirstModifiedByUserId { get; set; }
        public long? LastModifiedByUserId { get; set; }
        #endregion



        #region Relation: User
        public Nullable<long> UserId { get; set; }
        public virtual UserSearchModel User { get; set; }
        #endregion

        #region Relation: Permission
        public Nullable<long> PermissionId { get; set; }
        public virtual PermissionSearchModel Permission { get; set; }
        #endregion

        #endregion
    }
}
