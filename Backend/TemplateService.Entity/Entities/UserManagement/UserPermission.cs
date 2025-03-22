#region Using ...
using Framework.Common.Interfaces;
using System;
using System.Diagnostics;
#endregion

/*


*/
namespace TemplateService.Entity.Entities
{
    /// <summary>
    /// 
    /// </summary>
    [DebuggerDisplay("Id={Id}, CreationDate={CreationDate}")]
    public class UserPermission : IEntityIdentity<long>, IDateTimeSignature, IEntityUserSignature
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance from type
        /// UserPermission
        /// </summary>
        public UserPermission()
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


        public Nullable<long> UserId { get; set; }
        public virtual User User { get; set; }
        public Nullable<long> PermissionId { get; set; }
        public virtual Permission Permission { get; set; }

        #endregion
    }
}
