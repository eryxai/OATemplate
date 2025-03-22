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
    [DebuggerDisplay("Id={Id}, CreationDate={CreationDate}, FirstName={FirstName}, MiddleName={MiddleName}, LastName={LastName}, Username={Username}, Password={Password}, IsActive={IsActive}")]
    public class UserLightViewModel : Base.BaseViewModel
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance from type
        /// UserLightViewModel
        /// </summary>
        public UserLightViewModel()
        {
            
        }
        #endregion

        #region Properties

        #region IEntityIdentity<long>
        public long Id { get; set; }
        #endregion

        public DateTime CreationDate { get; set; }

        public System.String Name { get; set; }
 
        public System.String Username { get; set; }

        public Nullable<System.Boolean> IsActive { get; set; }

        public System.String ProfileImage { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        #region Relation: User Products
        public System.String OrganizationName { get; set; }
        public System.String RoleName { get; set; }

        #endregion


        #endregion
    }
}
