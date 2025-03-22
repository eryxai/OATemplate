using System.Collections.Generic;

namespace TemplateService.Core.Models.ViewModels
{
    public class UserRoleListViewModel : Base.BaseViewModel
    {
        public long UserId { get; set; }
        public IList<NameValueViewModel> List { get; set; }
    }
}
