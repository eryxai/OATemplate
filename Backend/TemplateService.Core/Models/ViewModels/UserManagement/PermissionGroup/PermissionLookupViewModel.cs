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
    public class PermissionGroupLookupViewModel : Base.BaseViewModel
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance from type
        /// PermissionLookupViewModel
        /// </summary>
        public PermissionGroupLookupViewModel()
        {
            
        }
        #endregion

        #region Properties

        #region IEntityIdentity<long>
        public long Id { get; set; }
        #endregion

        public System.String Name { get; set; }

        public Nullable<long> ParentId { get; set; }
     


        #endregion
    }
}
