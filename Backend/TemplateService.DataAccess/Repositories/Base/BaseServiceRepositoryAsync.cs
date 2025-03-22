#region Using ...
using Framework.Common.Interfaces;
using Framework.Core.Common;
using Framework.DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TemplateService.Core.IRepositories.Base;
using TemplateService.DataAccess.Contexts;
#endregion

/*
 
 
 */
namespace TemplateService.DataAccess.Repositories.Base
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="TEntity"></typeparam>
	/// <typeparam name="TPrimeryKey"></typeparam>
	public class BaseServiceRepositoryAsync<TEntity, TPrimeryKey> : BaseFrameworkRepositoryAsync<TEntity, TPrimeryKey>,
		IBaseServiceRepositoryAsync<TEntity, TPrimeryKey>
		where TEntity : class, IEntityIdentity<TPrimeryKey>
	{
		#region Data Members
		private TemplateServiceContext _TemplateContext;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance from type 
		/// BaseServiceRepositoryAsync.
		/// </summary>
		/// <param name="context"></param>
		public BaseServiceRepositoryAsync(TemplateServiceContext context, ICurrentUserService currentUserService)
			: base(context, currentUserService)
		{
			this.TemplateContext = context;
		}
		#endregion

		#region Properties
		protected TemplateServiceContext TemplateContext
		{
			get { return this._TemplateContext; }
			private set { this._TemplateContext = value; }
		}
		#endregion
	}
}
