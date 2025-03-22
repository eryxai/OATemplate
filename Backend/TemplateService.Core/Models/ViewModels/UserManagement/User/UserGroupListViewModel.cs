using System.Collections.Generic;

namespace TemplateService.Core.Models.ViewModels
{
    public class UserGroupListViewModel : Base.BaseViewModel
    {
        #region Properties
        public long UserId { get; set; }
        public IList<NameValueViewModel> List { get; set; }
        #endregion
    }
}
