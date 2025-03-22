#region Using ...
using AutoMapper;
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
using ClosedXML.Excel;
using System.IO;
using TemplateService.Core.Common;
#endregion

/*


*/
namespace TemplateService.Business.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class UserLoginsService : Base.BaseService, IUserLoginsService
	{
		#region Data Members
		private readonly IMapper _mapper;
		private readonly ILoggerService _loggerService;
		private readonly ILanguageService _languageService;
		private readonly IUnitOfWorkAsync _unitOfWorkAsync;
		private readonly IExcelService _excelService;
		private readonly IUserLoginsRepositoryAsync _UserLoginsRepositoryAsync;
		private readonly ILocalizationService _localizationService;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance from type
        /// UserLoginsService.
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        /// <param name="languageService"></param>
        /// <param name="unitOfWorkAsync"></param>
        /// <param name="ExcelService"></param>
        /// <param name="UserLoginsRepositoryAsync"></param>
        public UserLoginsService(
            IMapper mapper,
            ILoggerService logger,
            ILanguageService languageService,
            IUnitOfWorkAsync unitOfWorkAsync,
            IExcelService excelService,
            IUserLoginsRepositoryAsync UserLoginsRepositoryAsync
, ILocalizationService localizationService)
        {
            this._mapper = mapper;
            this._loggerService = logger;
            this._languageService = languageService;
            this._unitOfWorkAsync = unitOfWorkAsync;
            this._excelService = excelService;
            this._UserLoginsRepositoryAsync = UserLoginsRepositoryAsync;
            _localizationService = localizationService;
        }
        #endregion

        #region Methods
        private UserLogin UpdateExistEntityFromModel(UserLogin existEntity, UserLogin newEntity)
		{
			#region Update Exist Entity Properties
			/*
             * Update properties that need updates here
             * Example:
             * existEntity.<property> = newEntity.<property>;
             */
			existEntity.Username = newEntity.Username;

			#endregion

			return existEntity;
		}
		#endregion

		#region IUserLoginsService
		/// <summary>
		/// 
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public async Task ValidateModelAsync(UserLoginViewModel model)
		{
			await Task.Run(() =>
			{

			});
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="modelCollection"></param>
		/// <returns></returns>
		public async Task ValidateModelAsync(IEnumerable<UserLoginViewModel> modelCollection)
		{
			await Task.Run(() =>
			{

			});
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="searchModel"></param>
		/// <returns></returns>
		public async Task<GenericResult<IList<UserLoginLightViewModel>>> Search(UserLoginSearchModel searchModel)
		{
			var query = await this._UserLoginsRepositoryAsync.GetAsync(repositoryRequest:null);

			var IncludedNavigationsList = new List<string>(new string[] {
				"User.ChildTranslatedUsers",
			});

			#region Set IncludedNavigationsList
			query = await this._UserLoginsRepositoryAsync.SetIncludedNavigationsListAsync(query, IncludedNavigationsList);
			#endregion

			#region Build Query
			query = query.Where(entity => entity.IsDeleted == false);


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
			if (string.IsNullOrEmpty(searchModel.Username) == false)
			{
				query = query.Where(entity =>
					entity.Username.Contains(searchModel.Username));
			}


			#endregion

			searchModel.Pagination = await this._UserLoginsRepositoryAsync.SetPaginationCountAsync(query, searchModel.Pagination);

			query = await this._UserLoginsRepositoryAsync.SetSortOrderAsync(query, searchModel.Sorting);

			query = await this._UserLoginsRepositoryAsync.SetPaginationAsync(query, searchModel.Pagination);

			var entityList = query.ToList();
			var result = new GenericResult<IList<UserLoginLightViewModel>>
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
		public async Task<byte[]> ExportExcel(UserLoginSearchModel searchModel)
		{
			searchModel.Pagination = new Pagination { PageIndex = null, PageSize = null };

			var lang = _languageService.CurrentLanguage;

			var data = await this.Search(searchModel);
			byte[] result = null;

			using (var workbook = new XLWorkbook())
			{
				var worksheet = workbook.Worksheets.Add("UserLogin");
				var currentRow = 1;

				worksheet = this._excelService.SetStyle(worksheet, currentRow, 9);

				worksheet.Cell(currentRow, 1).Value = _localizationService.GetTranslate("Id", lang);
				worksheet.Cell(currentRow, 2).Value = _localizationService.GetTranslate("CreationDate", lang);


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

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pagination"></param>
		/// <returns></returns>
		public async Task<GenericResult<IList<UserLoginViewModel>>> GetAsync(BaseFilter baseFilter = null)
		{
			RepositoryRequestConditionFilter<UserLogin, long> repositoryRequest = new RepositoryRequestConditionFilter<UserLogin, long>
			{
				Pagination = baseFilter?.Pagination,
				Sorting = baseFilter?.Sorting,
				IncludedNavigationsList = new List<string>(new string[] {

				})

			};
			var entityCollection = await this._UserLoginsRepositoryAsync.GetAsync(repositoryRequest);
			var entityList = entityCollection.ToList();
			var result = new GenericResult<IList<UserLoginViewModel>>
			{
				Pagination = repositoryRequest.Pagination,
				Collection = entityCollection.Select(entity => entity.ToModel(this._mapper)).ToList()
			};

			return result;
		}

		/// <summary>
		/// Gets an entity by its id.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public async Task<UserLoginViewModel> GetAsync(long id)
		{
			var include = new string[]
			{

			};
			var entity = await this._UserLoginsRepositoryAsync.FirstOrDefaultAsync(x => x.Id == id, include);
			var model = entity.ToModel(this._mapper);
			return model;
		}

		/// <summary>
		/// Adds an entity.
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public async Task<UserLoginViewModel> AddAsync(UserLoginViewModel model)
		{
			await this.ValidateModelAsync(model);

			var entity = model.ToEntity(this._mapper);
			entity = await this._UserLoginsRepositoryAsync.AddAsync(entity);

			#region Commit Changes
			await this._unitOfWorkAsync.CommitAsync();
			#endregion

			var result = entity.ToModel(this._mapper);
			return result;
		}


		/// <summary>
		/// Updates an entity.
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public async Task<UserLoginViewModel> UpdateAsync(UserLoginViewModel model)
		{
			await this.ValidateModelAsync(model);
			var entity = model.ToEntity(this._mapper);

			#region Select Existing Entity
			var include = new string[]
			{
			};
			var existEntity = await this._UserLoginsRepositoryAsync.FirstOrDefaultAsync(x => x.Id == model.Id, include);
			#endregion

			this.UpdateExistEntityFromModel(existEntity, entity);
			existEntity = await this._UserLoginsRepositoryAsync.UpdateAsync(existEntity);

			#region Commit Changes
			await this._unitOfWorkAsync.CommitAsync();
			#endregion

			var result = existEntity.ToModel(this._mapper);
			return result;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public async Task DeleteAsync(long id)
		{
			await this._UserLoginsRepositoryAsync.DeleteAsync(id);

			#region Commit Changes
			await this._unitOfWorkAsync.CommitAsync();
			#endregion
		}

		#endregion
	}
}
