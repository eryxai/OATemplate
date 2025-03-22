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
    [DebuggerDisplay("Id={Id}, CreationDate={CreationDate}, Username={Username}")]
	public class UserLogin : IEntityIdentity<long>, IDateTimeSignature, IEntityUserSignature, IDeletionSignature
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance from type
		/// UserLogin
		/// </summary>
		public UserLogin()
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

		public System.String Username { get; set; }

		public string IPV4 { get; set; }
		public string IPV6 { get; set; }

		public Nullable<long> UserId { get; set; }
		public virtual User User { get; set; }

		public bool ChangePassword { get; set; }

		#endregion
	}
}
