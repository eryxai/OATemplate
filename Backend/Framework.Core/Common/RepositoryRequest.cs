#region Using ...
using Framework.Common.Enums;
using System;
using System.Collections.Generic;
#endregion

/*
 
 
 */
namespace Framework.Core.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class RepositoryRequest
	{
		#region Data Members

		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance from type 
		/// ConditionFilter.
		/// </summary>
		/// <param name="repositoryRequest"></param>
		public RepositoryRequest(RepositoryRequest repositoryRequest)
		{
			if (repositoryRequest != null)
			{
				this.IncludedNavigationsList = repositoryRequest.IncludedNavigationsList;
				this.Order = repositoryRequest.Order;
				this.Pagination = repositoryRequest.Pagination;
			}
			else
			{
				this.IncludedNavigationsList = new List<string>();
			}
		}
		/// <summary>
		/// Initializes a new instance from type 
		/// ConditionFilter.
		/// </summary>
		public RepositoryRequest()
		{
			this.IncludedNavigationsList = new List<string>();
		}
		#endregion

		#region Properties
		public Pagination Pagination { get; set; }
		public IList<string> IncludedNavigationsList { get; set; }
		public Nullable<Order> Order { get; set; }
		public string Sorting { get; set; }
		#endregion
	}
}
