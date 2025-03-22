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
    [DebuggerDisplay("Id={Id}, CreationDate={CreationDate}, Content={Content}")]
    public class ExperienceLookupViewModel : Base.BaseViewModel
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance from type
        /// ExperienceLookupViewModel
        /// </summary>
        public ExperienceLookupViewModel()
        {

        }
        #endregion

        #region Properties

        #region IEntityIdentity
        public long Id { get; set; }
        #endregion

        public string Name { get; set; }
        #endregion
    }
}
