using System.Collections.Generic;

namespace TemplateService.Core.Models.ViewModels.UserManagement.User
{
    public class UserDepartmentListViewModel : Base.BaseViewModel
    {
        public long UserId { get; set; }
        public IList<NameValueViewModel> List { get; set; }
    }
    
}
