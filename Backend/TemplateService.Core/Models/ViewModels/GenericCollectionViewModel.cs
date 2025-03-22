using TemplateService.Core.Models.ViewModels.Base;
using System.Collections.Generic;

namespace TemplateService.Core.Models.ViewModels
{
    public class GenericCollectionViewModel<TViewModel> where TViewModel : BaseViewModel
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of 
        /// type GenericCollectionViewModel.
        /// </summary>
        public GenericCollectionViewModel()
        {

        }
        /// <summary>
        /// Initializes a new instance of 
        /// type GenericCollectionViewModel.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        public GenericCollectionViewModel(IList<TViewModel> collection, long totalCount, int? pageIndex, int? pageSize)
        {
            this.Collection = collection;
            this.TotalCount = totalCount;
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
        }
        #endregion        

        #region Properties
        public IList<TViewModel> Collection { get; set; }
        public long TotalCount { get; set; }
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
        #endregion
    }
}
