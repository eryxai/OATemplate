#region Using ...
using System.ComponentModel.DataAnnotations;
#endregion

/*


*/
namespace TemplateService.Core.Models.ViewModels
{
    public class ForgotPasswordModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
