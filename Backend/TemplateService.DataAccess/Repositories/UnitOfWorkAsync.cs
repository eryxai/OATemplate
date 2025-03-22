#region Using ...
using Framework.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TemplateService.Core.IRepositories;
using TemplateService.DataAccess.Contexts;
#endregion

/*
 
 
 */
namespace TemplateService.DataAccess.Repositories
{
	/// <summary>
	/// 
	/// </summary>
	public class UnitOfWorkAsync : IUnitOfWorkAsync
	{
		#region Data Members
		private TemplateServiceContext _context;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance from type 
		/// UnitOfWorkAsync.
		/// </summary>
		/// <param name="context"></param>
		public UnitOfWorkAsync(TemplateServiceContext context)
		{
			this._context = context;
		}
		#endregion

		#region IUnitOfWork	
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public async Task<int> CommitAsync()
		{
			var result = await this._context.SaveChangesAsync();
			return result;
		}
		#endregion
	}
}
