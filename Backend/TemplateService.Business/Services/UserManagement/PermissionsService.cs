#region Using ...
using AutoMapper;
using Framework.Common.Enums;
using Framework.Core.Common;
using Framework.Core.IRepositories;
using Framework.Core.Models;
using System;
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
    public class PermissionsService : Base.BaseService, IPermissionsService
	{
		#region Data Members
		private readonly IMapper _mapper;
		private readonly IPermissionGroupRepositoryAsync _permissionGroupRepositoryAsync;
		private readonly ILanguageService _languageService;
		private readonly IUnitOfWorkAsync _unitOfWorkAsync;
		private readonly IExcelService _excelService;
		private readonly IPermissionsRepositoryAsync _PermissionsRepositoryAsync;
		private readonly IRolePermissionsRepositoryAsync _rolePermissionsRepositoryAsync;
		private readonly IUserPermissionsRepositoryAsync _userPermissionsRepositoryAsync;
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
		/// <param name="PermissionsRepositoryAsync"></param>
		public PermissionsService(
			IMapper mapper,
			IPermissionGroupRepositoryAsync permissionGroupRepositoryAsync,
			ILanguageService languageService,
			IUnitOfWorkAsync unitOfWorkAsync,
			IExcelService excelService,
			IPermissionsRepositoryAsync PermissionsRepositoryAsync,
			IRolePermissionsRepositoryAsync rolePermissionsRepositoryAsync,
			IUserPermissionsRepositoryAsync userPermissionsRepositoryAsync)
		{
			this._mapper = mapper;
			this._permissionGroupRepositoryAsync = permissionGroupRepositoryAsync;
			this._languageService = languageService;
			this._unitOfWorkAsync = unitOfWorkAsync;
			this._excelService = excelService;
			this._PermissionsRepositoryAsync = PermissionsRepositoryAsync;
			this._rolePermissionsRepositoryAsync = rolePermissionsRepositoryAsync;
			this._userPermissionsRepositoryAsync = userPermissionsRepositoryAsync;
		}
		#endregion

		#region Methods
		/// <summary>
		/// 
		/// </summary>
		/// <param name="searchModel"></param>
		/// <returns></returns>
		public async Task<GenericResult<IList<PermissionLightViewModel>>> Search(PermissionSearchModel searchModel)
		{
			var lang = this._languageService.CurrentLanguage;
			var query = await this._PermissionsRepositoryAsync.GetAsync(repositoryRequest:null);

			var IncludedNavigationsList = new List<string>(new string[] {

			});

			#region Set IncludedNavigationsList
			query = await this._PermissionsRepositoryAsync.SetIncludedNavigationsListAsync(query, IncludedNavigationsList);
			#endregion

			#region Build Query
			query = query
				.Where(entity => entity.IsDeleted == false);


			if (searchModel.MinCreationDate != null)
			{
				query = query.Where(entity =>
					entity.CreationDate >= searchModel.MinCreationDate);
			}
			if (searchModel.MaxCreationDate != null)
			{
				query = query.Where(entity =>
					entity.CreationDate <= searchModel.MaxCreationDate);
			}
			if (string.IsNullOrEmpty(searchModel.Name) == false)
			{
				query = query.Where(entity => entity.NameAr.Contains(searchModel.Name) || entity.NameEn.Contains(searchModel.Name));
			}
			if (string.IsNullOrEmpty(searchModel.Description) == false)
			{
				query = query.Where(entity => entity.DescriptionAr.Contains(searchModel.Description) || entity.DescriptionEn.Contains(searchModel.Description));
			}
			if (string.IsNullOrEmpty(searchModel.Code) == false)
			{
				query = query.Where(entity =>
					entity.Code.Value.ToString().Contains(searchModel.Code));
			}

			if (searchModel.IsActive != null)
			{
				query = query.Where(entity =>
					entity.IsActive == searchModel.IsActive);
			}

			#endregion

			searchModel.Pagination = await this._PermissionsRepositoryAsync.SetPaginationCountAsync(query, searchModel.Pagination);

			query = await this._PermissionsRepositoryAsync.SetSortOrderAsync(query, searchModel.Sorting);

			query = await this._PermissionsRepositoryAsync.SetPaginationAsync(query, searchModel.Pagination);

			var entityList = query.ToList();
			var result = new GenericResult<IList<PermissionLightViewModel>>
			{
				Pagination = searchModel.Pagination,
				Collection = entityList.Select(entity => entity.ToLightModel(this._mapper)).ToList()
			};

			return result;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="searchModel"></param>
		/// <returns></returns>
		public async Task<byte[]> ExportExcel(PermissionSearchModel searchModel)
		{
			searchModel.Pagination = new Pagination { PageIndex = null, PageSize = null };

			var data = await this.Search(searchModel);
			byte[] result = null;

			using (var workbook = new XLWorkbook())
			{
				var worksheet = workbook.Worksheets.Add("Permission");
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

		public async Task<PermissionViewModel> AddAsync(PermissionViewModel model)
		{
			await this.ValidateModelAsync(model);

			var entity = model.ToEntity(this._mapper);
			entity = await this._PermissionsRepositoryAsync.AddAsync(entity);
			#region Commit Changes
			await this._unitOfWorkAsync.CommitAsync();
			#endregion

			var result = entity.ToModel(this._mapper);
			return result;
		}

		public async Task DeleteAsync(long id)
		{
			await this._PermissionsRepositoryAsync.DeleteAsync(id);

			#region Commit Changes
			await this._unitOfWorkAsync.CommitAsync();
			#endregion
		}

        public async Task<List< PermissionGroupLookupViewModel>> GetAllPermissions()
        {
            var lang = this._languageService.CurrentLanguage;

            var permissions = (await _PermissionsRepositoryAsync.GetAsync(conditionFilter: null));
			return permissions.Select(c => new PermissionGroupLookupViewModel()
			{

				Id = c.Id,
				Name = lang == Language.Arabic ? c.NameAr : c.NameEn

			}).ToList();

        }
        public async Task<GenericResult<IList<PermissionViewModel>>> GetAsync(BaseFilter baseFilter = null)
		{
			var lang = this._languageService.CurrentLanguage;
			RepositoryRequestConditionFilter<Permission, long> repositoryRequest = new RepositoryRequestConditionFilter<Permission, long>
			{
				Pagination = baseFilter?.Pagination,
				Sorting = baseFilter?.Sorting
			};
			var entityCollection = await this._PermissionsRepositoryAsync.GetAsync(repositoryRequest);
			var entityList = entityCollection.ToList();
			var result = new GenericResult<IList<PermissionViewModel>>
			{
				Pagination = repositoryRequest.Pagination,
				Collection = entityCollection.Select(entity => entity.ToModel(this._mapper)).ToList()
			};

			return result;
		}

		public async Task<PermissionViewModel> GetAsync(long id)
		{
			var entity = await this._PermissionsRepositoryAsync.FirstOrDefaultAsync(x => x.Id == id);
			var model = entity.ToModel(this._mapper);
			return model;
		}

		public async Task<PermissionViewModel> UpdateAsync(PermissionViewModel model)
		{
			await this.ValidateModelAsync(model);
			var entity = model.ToEntity(this._mapper);

			#region Select Existing Entity
			var include = new string[]
			{
			};
			var existEntity = await this._PermissionsRepositoryAsync.FirstOrDefaultAsync(x => x.Id == model.Id, include);
			#endregion

			this.UpdateExistEntityFromModel(existEntity, entity);
			existEntity = await this._PermissionsRepositoryAsync.UpdateAsync(existEntity);

			#region Commit Changes
			await this._unitOfWorkAsync.CommitAsync();
			#endregion

			var result = existEntity.ToModel(this._mapper);
			return result;
		}

		public async Task ValidateModelAsync(PermissionViewModel model)
		{
			await Task.Run(() =>
			{
				var existEntity = this._PermissionsRepositoryAsync.GetAsync(repositoryRequest:null).Result.FirstOrDefault(entity =>
								entity.Code == model.Code &&
								entity.Id != model.Id);

				if (existEntity != null)
					throw new ItemAlreadyExistException((int)ErrorCode.CodeAlreadyExist);
			});
		}

		public async Task ValidateModelAsync(IEnumerable<PermissionViewModel> modelCollection)
		{
			await Task.Run(() => { });
		}

		private Permission UpdateExistEntityFromModel(Permission existEntity, Permission newEntity)
		{
			#region Update Exist Entity Properties
			/*
             * Update properties that need updates here
             * Example:
             * existEntity.<property> = newEntity.<property>;
             */
			existEntity.Code = newEntity.Code;
			existEntity.NameAr = newEntity.NameAr;
			existEntity.NameEn = newEntity.NameEn;
			existEntity.DescriptionAr = newEntity.DescriptionAr;
			existEntity.DescriptionEn = newEntity.DescriptionEn;
			existEntity.IsActive = newEntity.IsActive;

			#endregion

			return existEntity;
		}


		public IList<PermissionLightViewModel> GetAllUnSelectedPermissions(long roleId)
		{
			var lang = this._languageService.CurrentLanguage;
			IList<PermissionLightViewModel> result = null;
			var list = this._rolePermissionsRepositoryAsync.GetAsync(repositoryRequest:null).Result.Where(x => x.RoleId == roleId);

			if (list.Count() > 0)
			{
				var entityColection = this._PermissionsRepositoryAsync.GetAsync(repositoryRequest:null).Result.Where(x => x.RolePermissions.FirstOrDefault(y => y.RoleId == roleId) == null
				  //list.Any(item => x.ParentTranslatedPermissionId != item.PermissionId)
				  ).ToList();

				var modelCollection = entityColection.Select(x => x.ToLightModel(_mapper)).ToList();
				result = modelCollection;
			}
			else
			{
				var entityColection = this._PermissionsRepositoryAsync.GetAsync(repositoryRequest:null).Result.ToList();

				var modelCollection = entityColection.Select(x => x.ToLightModel(_mapper)).ToList();
				result = modelCollection;
			}

			return result;
		}

		public IList<PermissionLightViewModel> GetAllUnSelectedPermissionsForUser(long userId)
		{
			var lang = this._languageService.CurrentLanguage;
			IList<PermissionLightViewModel> result = null;
			RepositoryRequestConditionFilter<Permission, long> conditionFilter = new RepositoryRequestConditionFilter<Permission, long>()
			{
				IncludedNavigationsList = new string[]
				{
					"ParentTranslatedPermission"
				}
			};
			var list = this._userPermissionsRepositoryAsync.GetAsync(repositoryRequest:null).Result.Where(x => x.UserId == userId);

			if (list.Count() > 0)
			{
				var entityColection = this._PermissionsRepositoryAsync.GetAsync(repositoryRequest:null).Result.Where(x =>
				  x.UserPermissions.FirstOrDefault(y => y.UserId == userId) == null
				  //list.Any(item => x.ParentTranslatedPermissionId != item.PermissionId)
				  )
					.OrderBy(x => x.Code)
					.ToList();

				try
				{
					var modelCollection = entityColection.Select(x => x.ToLightModel(_mapper)).ToList();
					result = modelCollection;
				}
				catch (Exception ex)
				{

				}

			}
			else
			{
				var entityColection = this._PermissionsRepositoryAsync.GetAsync(repositoryRequest:null).Result
					.OrderBy(x => x.Code)
					.ToList();

				var modelCollection = entityColection.Select(x => x.ToLightModel(_mapper)).ToList();
				result = modelCollection;
			}

			return result;
		}


		public async Task<IList<PermissionGroupLookupViewModel>> GetAllPermissionsGroups()
        {
			var lang = this._languageService.CurrentLanguage;

            IList<PermissionGroupLookupViewModel> result = new List<PermissionGroupLookupViewModel>();

            var groups = await _permissionGroupRepositoryAsync.GetAsync(null);
            foreach (var group in groups.ToList())
            {
                result.Add(new PermissionGroupLookupViewModel
                {
                    Id = group.Id,
                    Name = lang == Language.Arabic ? group.NameAr : group.NameEn,
                    ParentId = group.ParentId,
                });
            }
            return result;
        }

		public async Task<IList<NameValueNumericViewModel>> GetPermissionByGroupId(long Id)
		{
			var lang = this._languageService.CurrentLanguage;

			//IList<NameValueNumericViewModel> result = new List<NameValueNumericViewModel>();

			var Permissions = (await _PermissionsRepositoryAsync.GetAsync(conditionFilter:null))
								.Where(x => x.PermissionGroupId == Id).OrderBy(x => x.Code);

			var result = Permissions.Select(c => new NameValueNumericViewModel()
			{

				Id = c.Id,
				Name = lang == Language.Arabic ? c.NameAr : c.NameEn
			});


            return result.ToList();
		}
		#endregion
	}
}
