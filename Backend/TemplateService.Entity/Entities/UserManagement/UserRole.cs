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
    public class UserRole : IEntityIdentity<long>, IDateTimeSignature, IEntityUserSignature
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance from type
        /// UserRole
        /// </summary>
        public UserRole()
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


        public Nullable<long> RoleId { get; set; }
        public virtual Role Role { get; set; }
        public Nullable<long> UserId { get; set; }
        public virtual User User { get; set; }

        #endregion
    }
}
