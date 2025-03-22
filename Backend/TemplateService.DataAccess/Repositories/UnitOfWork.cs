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
	public class UnitOfWork : IUnitOfWork
	{
		#region Data Members
		private TemplateServiceContext _context;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance from type 
		/// UnitOfWork.
		/// </summary>
		/// <param name="context"></param>
		public UnitOfWork(TemplateServiceContext context)
		{
			this._context = context;
		}
		#endregion

		#region IUnitOfWork
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public int Commit()
		{
			var result = this._context.SaveChanges();
			return result;
		}		
		#endregion
	}
}
