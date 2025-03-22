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
    [DebuggerDisplay("Pagination={Pagination}, Sorting={Sorting}, FirstName={FirstName}, MiddleName={MiddleName}, LastName={LastName}, Username={Username}, Password={Password}, IsActive={IsActive}")]
    public class UserSearchModel : BaseFilter
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance from type
        /// UserSearchModel
        /// </summary>
        public UserSearchModel()
        {

        }
        #endregion

        #region Properties
        public string Username { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
        public string nameEmail { get; set; }
        public string PhoneNumber { get; set; }
        public int? OrganizationId { get; set; }

        public Nullable<bool> IsActive { get; set; }

        #endregion
    }
}
