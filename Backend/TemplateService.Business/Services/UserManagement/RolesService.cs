#region Using ...
using AutoMapper;
using Framework.Common.Enums;
using Framework.Core.Common;
using Framework.Core.IRepositories;
using Framework.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TemplateService.Core;
using TemplateService.Core.IRepositories;
using TemplateService.Core.IServices;
using TemplateService.Core.Models.ViewModels;
using TemplateService.Entity.Entities;
using Framework.Common.Exceptions;
using TemplateService.Common.Enums;
using ClosedXML.Excel;
using System.IO;
#endregion

/*


*/
namespace TemplateService.Business.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class RolesService : Base.BaseService, IRolesService
	{
		#region Data Members
		private readonly IMapper _mapper;
		private readonly ILoggerService _loggerService;
		private readonly ILanguageService _languageService;
		private readonly IUnitOfWorkAsync _unitOfWorkAsync;
		private readonly IExcelService _excelService;
		private readonly IRolesRepositoryAsync _RolesRepositoryAsync;
		private readonly IUserRolesRepositoryAsync _userRolesRepositoryAsync;
		private readonly IRolePermissionsRepositoryAsync _rolePermissionsRepositoryAsync;
		private readonly IUsersRepositoryAsync _usersRepositoryAsync;
		private readonly  ICurrentUserService _currentUserService;

        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance from type
        /// UsersService.
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="languageService"></param>
        /// <param name="unitOfWorkAsync"></param>
        /// <param name="ExcelService"></param>
        /// <param name="RolesRepositoryAsync"></param>
        public RolesService(
			IMapper mapper,
			ILoggerService logger,
			ILanguageService languageService,
			IUnitOfWorkAsync unitOfWorkAsync,
			IExcelService excelService,
			IRolesRepositoryAsync RolesRepositoryAsync,
			IUserRolesRepositoryAsync userRolesRepositoryAsync,
			IRolePermissionsRepositoryAsync rolePermissionsRepositoryAsync,
			IUsersRepositoryAsync usersRepositoryAsync ,
			ICurrentUserService currentUserService 
			)
		{
			this._mapper = mapper;
			this._loggerService = logger;
			this._languageService = languageService;
			this._unitOfWorkAsync = unitOfWorkAsync;
			this._excelService = excelService;
			this._RolesRepositoryAsync = RolesRepositoryAsync;
			this._userRolesRepositoryAsync = userRolesRepositoryAsync;
			this._rolePermissionsRepositoryAsync = rolePermissionsRepositoryAsync;
			this._userRolesRepositoryAsync = userRolesRepositoryAsync;
			this._currentUserService = currentUserService;
			this._usersRepositoryAsync	= usersRepositoryAsync;
		}
		#endregion

		#region Methods
		/// <summary>
		/// 
		/// </summary>
		/// <param name="searchModel"></param>
		/// <returns></returns>
		public async Task<GenericResult<IList<RoleLightViewModel>>> Search(RoleSearchModel searchModel)
		{
			var lang = this._languageService.CurrentLanguage;
			var query = await this._RolesRepositoryAsync.GetAsync(repositoryRequest:null);
			query = query.Where(x => !x.IsDeleted);
            var userId = _currentUserService.CurrentUserId;

            int totalRecord = 0;
            searchModel = searchModel == null ? new RoleSearchModel() : searchModel;
            query = query.PrimengTableFilter(searchModel, out totalRecord);
            query = await _RolesRepositoryAsync.SetSortOrderAsync(query, searchModel.Sorting);
            searchModel.Pagination = await _RolesRepositoryAsync.SetPaginationCountAsync(query, searchModel.Pagination);
            query = await _RolesRepositoryAsync.SetPaginationAsync(query, searchModel.Pagination);



            var entityList = query.Select(x => new RoleLightViewModel
			{
				Id = x.Id,
				CreationDate = x.CreationDate,
				Name = lang == Language.Arabic ? x.NameAr : x.NameEn,
				Description = lang == Language.Arabic ? x.DescriptionAr : x.DescriptionEn,
				

			}).ToList();
			var result = new GenericResult<IList<RoleLightViewModel>>
			{
				Pagination = searchModel.Pagination,
				Collection = entityList
			};

			return result;

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="searchModel"></param>
		/// <returns></returns>
		public async Task<byte[]> ExportExcel(RoleSearchModel searchModel)
		{
			searchModel.Pagination = new Pagination { PageIndex = null, PageSize = null };

			var data = await this.Search(searchModel);
			byte[] result = null;

			using (var workbook = new XLWorkbook())
			{
				var worksheet = workbook.Worksheets.Add("Role");
				var currentRow = 1;

				worksheet = this._excelService.SetStyle(worksheet, currentRow, 9);

				worksheet.Cell(currentRow, 1).Value = "Id";
				worksheet.Cell(currentRow, 2).Value = "CreationDate";


				foreach (var item in data.Collection)
				{
					currentRow++;
					worksheet.Cell(currentRow, 1).Value = item.Id;
					worksheet.Cell(currentRow, 2).Value = item.CreationDate;

				}

				byte[] content = null;
				using (var stream = new MemoryStream())
				{
					workbook.SaveAs(stream);
					content = stream.ToArray();
				}
				result = content;
			}
			return result;
		}

		public async Task<RoleViewModel> AddAsync(RoleViewModel model)
		{
			var arabicCheck = await this._RolesRepositoryAsync.FirstOrDefaultAsync(a => a.NameAr == model.NameAr  && a.IsDeleted != true);
			if (arabicCheck != null)
				throw new DataDuplicateException((int)ErrorCode.DataDuplicate);
			var englishCheck = await this._RolesRepositoryAsync.FirstOrDefaultAsync(a => a.NameEn == model.NameEn  && a.IsDeleted != true);
			if (englishCheck != null)
				throw new DataDuplicateException((int)ErrorCode.DataDuplicate);
			var entity = model.ToEntity(this._mapper);
			entity = await this._RolesRepositoryAsync.AddAsync(entity);
			#region Commit Changes
			await this._unitOfWorkAsync.CommitAsync();
			#endregion
			await AddRolePermission(entity.Id, model.permissions);
			var result = entity.ToModel(this._mapper);
			return result;
		}

        private async Task AddRolePermission(long RoleId, IList<NameValueNumericViewModel> permissions)
        {
			foreach (var item in permissions)
			{
				RolePermission newEntity = new RolePermission
				{
					RoleId = RoleId,
					PermissionId = item.Id
				};
				await this._rolePermissionsRepositoryAsync.AddAsync(newEntity);
			}
			await this._unitOfWorkAsync.CommitAsync();
		}

        public async Task DeleteAsync(long  id)
		{
			await this._RolesRepositoryAsync.DeleteAsync(id);

			var listQuerable = await this._RolesRepositoryAsync.GetAsync(repositoryRequest:null);

			#region Commit Changes
			await this._unitOfWorkAsync.CommitAsync();
			#endregion
		}

		public async Task<GenericResult<IList<RoleViewModel>>> GetAsync(BaseFilter baseFilter = null)
		{
			var lang = this._languageService.CurrentLanguage;
			RepositoryRequestConditionFilter<Role, long> repositoryRequest = new RepositoryRequestConditionFilter<Role, long>
			{
				Pagination = baseFilter?.Pagination,
				Sorting = baseFilter?.Sorting,
				IncludedNavigationsList = new List<string>(new string[] {

				})
			};
			var entityCollection = await this._RolesRepositoryAsync.GetAsync(repositoryRequest);
			var entityList = entityCollection.ToList();
			var result = new GenericResult<IList<RoleViewModel>>
			{
				Pagination = repositoryRequest.Pagination,
				Collection = entityCollection.Select(entity => entity.ToModel(this._mapper)).ToList()
			};

			return result;
		}

		public async Task<RoleViewModel> GetAsync(long  id)
		{
			var include = new string[]
			{
			};
			var entity = await this._RolesRepositoryAsync.FirstOrDefaultAsync(x => x.Id == id, include);
			var model = entity.ToModel(this._mapper);
			return model;
		}

		public async Task<RoleViewModel> UpdateAsync(RoleViewModel model)
		{
			//await this.ValidateModelAsync(model);
			var entity = model.ToEntity(this._mapper);

			#region Select Existing Entity
			var include = new string[]
			{
			};
			var existEntity = await this._RolesRepositoryAsync.FirstOrDefaultAsync(x => x.Id == model.Id, include);
			#endregion


			this.UpdateExistEntityFromModel(existEntity, entity);
			existEntity = await this._RolesRepositoryAsync.UpdateAsync(existEntity);


			#region Commit Changes
			await this._unitOfWorkAsync.CommitAsync();
			#endregion
			await UpdateRolePermission(entity.Id, model.permissions);

			var result = existEntity.ToModel(this._mapper);
			return result;
		}

		private async Task UpdateRolePermission(long RoleId, IList<NameValueNumericViewModel> permissions)
		{
			var entityCollection = this._rolePermissionsRepositoryAsync.GetAsync(repositoryRequest:null).Result.Where(x => x.RoleId == RoleId);

			if (entityCollection.Count() > 0)
			{
				foreach (var item in entityCollection)
				{
					await this._rolePermissionsRepositoryAsync.DeleteAsync(item);
				}
				await this._unitOfWorkAsync.CommitAsync();
			}

			if (permissions?.Count > 0)
			{
				foreach (var item in permissions)
				{
					RolePermission newEntity = new RolePermission
					{
						RoleId = RoleId,
						PermissionId = item.Id
					};
					await this._rolePermissionsRepositoryAsync.AddAsync(newEntity);
				}
				await this._unitOfWorkAsync.CommitAsync();
			}

		}

		public async Task ValidateModelAsync(RoleViewModel model)
		{
			await Task.Run(() =>
			{
				var existEntity = this._RolesRepositoryAsync.GetAsync(repositoryRequest:null).Result.FirstOrDefault(entity =>
								//entity.Code == model.Code &&
								entity.Id != model.Id);

				if (existEntity != null)
					throw new ItemAlreadyExistException((int)ErrorCode.CodeAlreadyExist);
			});
		}

		public async Task ValidateModelAsync(IEnumerable<RoleViewModel> modelCollection)
		{
			await Task.Run(() => { });
		}

		private Role UpdateExistEntityFromModel(Role existEntity, Role newEntity)
		{
			#region Update Exist Entity Properties
			/*
             * Update properties that need updates here
             * Example:
             * existEntity.<property> = newEntity.<property>;
             */
			//existEntity.Code = newEntity.Code;
			existEntity.NameAr = newEntity.NameAr;
			existEntity.NameEn = newEntity.NameEn;
			//existEntity.Date = newEntity.Date;
			existEntity.DescriptionAr = newEntity.DescriptionAr;
			existEntity.DescriptionEn = newEntity.DescriptionEn;
			
			#endregion

			

			return existEntity;
		}

		public IList<RoleLightViewModel> GetAllUnSelectedRolesForUser(long userId)
		{
			var lang = this._languageService.CurrentLanguage;
			IList<RoleLightViewModel> result = null;
			var list = this._userRolesRepositoryAsync.GetAsync(repositoryRequest:null).Result.Where(x => x.UserId == userId);

			if (list.Count() > 0)
			{
				var entityColection = this._RolesRepositoryAsync.GetAsync(repositoryRequest:null).Result.Where(x =>
				  x.UserRoles.FirstOrDefault(y => y.UserId == userId) == null
				  //list.Any(item => x.ParentTranslatedRoleId != item.RoleId)
				  ).ToList();

				var modelCollection = entityColection.Select(x => x.ToLightModel(_mapper)).ToList();
				result = modelCollection;
			}
			else
			{
				var entityColection = this._RolesRepositoryAsync.GetAsync(repositoryRequest:null).Result.ToList();

				var modelCollection = entityColection.Select(x => x.ToLightModel(_mapper)).ToList();
				result = modelCollection;
			}

			return result;
		}

		

		public async Task<RolePermissionListViewModel> GetRolePermission(long roleId)
		{

		
			var IncludedNavigationsList = new List<string>(new string[] {
				"Permission"
			});

			var query = await this._rolePermissionsRepositoryAsync.GetAsync(repositoryRequest:null);
			query = await this._rolePermissionsRepositoryAsync.SetIncludedNavigationsListAsync(query, IncludedNavigationsList);
			var entityCollection = query.Where(x => x.RoleId == roleId).ToList();

			RolePermissionListViewModel result = new RolePermissionListViewModel
			{
				RoleId = roleId,
				List = new List<NameValueViewModel>()
			};
			foreach (var item in entityCollection)
			{
				result.List.Add(new NameValueViewModel
				{
					Value = item.PermissionId.Value,
					Name = _languageService.CurrentLanguage == Language.Arabic ? item.Permission.NameAr : item.Permission.NameEn,
                });
			}
			return result;
		}
		public async Task<RolePermissionListViewModel> UpdateRolePermissionAsync(RolePermissionListViewModel model)
		{
			var entityCollection = this._rolePermissionsRepositoryAsync.GetAsync(repositoryRequest:null).Result.Where(x => x.RoleId == model.RoleId);

			if (entityCollection.Count() > 0)
			{
				foreach (var item in entityCollection)
				{
					await this._rolePermissionsRepositoryAsync.DeleteAsync(item);
				}
				await this._unitOfWorkAsync.CommitAsync();
			}

			if (model.List?.Count > 0)
			{
				foreach (var item in model.List)
				{
					RolePermission newEntity = new RolePermission
					{
						RoleId = model.RoleId,
						PermissionId = item.Value
					};
					await this._rolePermissionsRepositoryAsync.AddAsync(newEntity);
				}
				await this._unitOfWorkAsync.CommitAsync();
			}

			return model;
		}

		public async Task<string> GetRoleName(long roleId)
		{
			var lang = this._languageService.CurrentLanguage;
			var include = new string[]
			 {

			 };
			var entity = await this._RolesRepositoryAsync.FirstOrDefaultAsync(x => x.Id == roleId, include);
			var name = lang == Language.Arabic ? entity.NameAr : entity.NameEn;
			return name != null ? name : null;
		}

		public async Task<List<RoleLookupViewModel>> GetLookup()
		{
            var lang = this._languageService.CurrentLanguage;
            var query = await this._RolesRepositoryAsync.GetAsync(conditionFilter: null);

			var result= query.Where(c=>!c.IsDeleted).Select(x => new RoleLookupViewModel() { Id = x.Id, Name = lang == Language.Arabic ? x.NameAr : x.NameEn });
			return result.ToList();
        }
      
		public async Task<GenericResult<IList<RoleLookupViewModel>>> SearchLookup(RoleLookupSearchModel searchModel)
		{
			var lang = this._languageService.CurrentLanguage;
			var query = await this._RolesRepositoryAsync.GetAsync(repositoryRequest:null);
			#region Build Query

			if (string.IsNullOrEmpty(searchModel.Name) == false)
			{
				query = query.Where(entity => entity.NameAr.Contains(searchModel.Name) || entity.NameEn.Contains(searchModel.Name));
			}
			
			query = query.Where(entity => entity.IsDeleted != true);
			#endregion Build Query
			if (searchModel.Paginated)
			{
				searchModel.Pagination = await this._RolesRepositoryAsync.SetPaginationCountAsync(query, searchModel.Pagination);
				query = await this._RolesRepositoryAsync.SetSortOrderAsync(query, searchModel.Sorting);
				query = await this._RolesRepositoryAsync.SetPaginationAsync(query, searchModel.Pagination);
			}
			var entityList = query.Select(x => new RoleLookupViewModel
			{
				Id = x.Id,
				Name = lang == Language.Arabic ? x.NameAr : x.NameEn
			}).ToList();
			var result = new GenericResult<IList<RoleLookupViewModel>>
			{
				Pagination = searchModel.Pagination,
				Collection = entityList
			};
			return result;
		}



		public async Task<RoleDetailViewModel> GetDetailsAsync(long id)
		{
			
			var entity = await this._RolesRepositoryAsync.FirstOrDefaultAsync(x => x.Id == id);
			var model = entity.ToDetailModel(this._mapper);

		

			return model;
		}

		#endregion
	}
}
