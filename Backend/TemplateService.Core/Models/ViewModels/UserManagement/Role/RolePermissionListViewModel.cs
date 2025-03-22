using System.Collections.Generic;

namespace TemplateService.Core.Models.ViewModels
{
    public class RolePermissionListViewModel : Base.BaseViewModel
    {
        public long RoleId { get; set; }
        public IList<NameValueViewModel> List { get; set; }
    }

    public class RolePermissionNumericListViewModel : Base.BaseViewModel
    {
        public long RoleId { get; set; }
        public IList<NameValueViewModel> List { get; set; }
    }
}
