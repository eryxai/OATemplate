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
    public class RoleLookupViewModel : Base.BaseViewModel
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance from type
        /// RoleLookupViewModel
        /// </summary>
        public RoleLookupViewModel()
        {
            
        }
        #endregion

        #region Properties

        #region IEntityIdentity<long>
        public long Id { get; set; }
        #endregion

        public System.String Name { get; set; }
        //public System.String Code { get; set; }
        //public System.String Description { get; set; }


        #endregion
    }
}
