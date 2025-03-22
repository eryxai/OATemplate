using System.Collections.Generic;

namespace TemplateService.Core.Models.ViewModels
{
    public class UserPermissionListViewModel : Base.BaseViewModel
	{
		public long UserId { get; set; }
		public IList<NameValueViewModel> List { get; set; }
	}

	public class UserPermissionNumericListViewModel : Base.BaseViewModel
	{
		public long UserId { get; set; }
		public IList<NameValueViewModel> List { get; set; }
	}


	public class UserStoreListViewModel : Base.BaseViewModel
	{
		public long UserId { get; set; }
		public IList<NameValueViewModel> List { get; set; }
	}
}
