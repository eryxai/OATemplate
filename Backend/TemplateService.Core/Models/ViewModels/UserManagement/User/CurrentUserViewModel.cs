#region Using ...
#endregion

/*


*/
namespace TemplateService.Core.Models.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    //[DebuggerDisplay("Id={Id}, CreationDate={CreationDate}, FirstName={FirstName}, MiddleName={MiddleName}, LastName={LastName}, Username={Username}, Password={Password}, IsActive={IsActive}")]
    public class CurrentUserViewModel : Base.BaseViewModel
    {
       
        /// <summary>
        /// Initializes a new instance from type
        /// UserViewModel
        /// </summary>
        public CurrentUserViewModel()
        {
            
        }
        
        public long Id { get; set; }
      
        public System.String Name { get; set; }
        //public string Product { get; set; }
    }
}
