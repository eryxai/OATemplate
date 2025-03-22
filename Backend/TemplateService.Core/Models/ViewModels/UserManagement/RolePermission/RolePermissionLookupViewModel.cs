#region Using ...
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
    public class RolePermissionLookupViewModel : Base.BaseViewModel
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance from type
        /// RolePermissionLookupViewModel
        /// </summary>
        public RolePermissionLookupViewModel()
        {
            
        }
        #endregion

        #region Properties

        #region IEntityIdentity<long>
        public long Id { get; set; }
        #endregion


        #endregion
    }
}
