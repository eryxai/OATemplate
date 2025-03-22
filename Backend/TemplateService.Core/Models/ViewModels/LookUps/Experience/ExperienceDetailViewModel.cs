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
    [DebuggerDisplay("Id={Id}, CreationDate={CreationDate}, Content={Content}, UserId={UserId}, IsPostive={IsPostive}")]
    public class ExperienceDetailViewModel : Base.BaseViewModel
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance from type
        /// ExperienceDetailViewModel
        /// </summary>
        public ExperienceDetailViewModel()
        {

        }
        #endregion

        #region Properties

        #region IEntityIdentity
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

        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public long? ParentId { get; set; }

        #endregion
    }
}
