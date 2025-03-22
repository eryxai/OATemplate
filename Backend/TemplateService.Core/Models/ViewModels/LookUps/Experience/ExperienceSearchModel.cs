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
    [DebuggerDisplay("Pagination={Pagination}, Sorting={Sorting}, Content={Content}, UserId={UserId}, IsPostive={IsPostive}")]
    public class ExperienceSearchModel : BaseFilter
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance from type
        /// ExperienceSearchModel
        /// </summary>
        public ExperienceSearchModel()
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

        #region IDeletionSignature
        public bool IsDeleted { get; set; }
        public DateTime? MinDeletionDate { get; set; }
        public DateTime? MaxDeletionDate { get; set; }
        public long? DeletedByUserId { get; set; }
        public bool? MustDeletedPhysical { get; set; }
        #endregion

        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public long? ParentId { get; set; }
        #endregion
    }
}
