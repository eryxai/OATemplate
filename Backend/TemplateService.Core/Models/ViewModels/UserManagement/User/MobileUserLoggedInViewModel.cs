namespace TemplateService.Core.Models.ViewModels
{
    public class MobileUserLoggedInViewModel : Base.BaseViewModel
    {
        public string Token { get; set; }
        public long UserId { get; set; }
        public int DepartmentId { get; set; }

    }
}
