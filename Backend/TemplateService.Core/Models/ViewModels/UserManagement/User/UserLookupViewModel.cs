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
    public class UserLookupViewModel : Base.BaseViewModel
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance from type
        /// UserLookupViewModel
        /// </summary>
        public UserLookupViewModel()
        {
            
        }
        #endregion

        #region Properties

        #region IEntityIdentity<long>
        public long Id { get; set; }
        #endregion

        public System.String FirstName { get; set; }
        public System.String MiddleName { get; set; }
        public System.String LastName { get; set; }
        public System.String Username { get; set; }
        public System.String Password { get; set; }

        #endregion
    }
}
