using System.Collections.Generic;

namespace TemplateService.Core.Models.ViewModels.Base
{
    public class TreeNodeViewModel<T>
    {
        public T Data { get; set; }
        public List<TreeNodeViewModel<T>> Children { get; set; }
    }
}
