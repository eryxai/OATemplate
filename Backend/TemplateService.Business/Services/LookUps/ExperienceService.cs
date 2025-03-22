#region Using ...
using System;
using AutoMapper;
using Framework.Common.Enums;
using Framework.Core.Common;
using Framework.Core.IRepositories;
using Framework.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TemplateService.Core;
using TemplateService.Core.IServices;
using TemplateService.Core.Models.ViewModels;
using ClosedXML.Excel;
using System.IO;
using TemplateService.DataAccess.Repositories;
using TemplateService.Entity.Entities;
#endregion

namespace TemplateService.Business.Services
{
    public class ExperienceService : Base.BaseService, IExperienceService
	{
		#region Data Members
		private readonly IMapper _mapper;
		private readonly ILanguageService _languageService;
		private readonly IUnitOfWorkAsync _unitOfWorkAsync;
		private readonly IExcelService _excelService;
		private readonly IExperienceRepositoryAsync _ExperienceRepositoryAsync;
		#endregion

		#region Constructors
		public ExperienceService(
			IMapper mapper,
			ILanguageService languageService,
			IUnitOfWorkAsync unitOfWorkAsync,
			IExcelService excelService,
			IExperienceRepositoryAsync ExperienceRepositoryAsync)
		{
			this._mapper = mapper;
			this._languageService = languageService;
			this._unitOfWorkAsync = unitOfWorkAsync;
			this._excelService = excelService;
			this._ExperienceRepositoryAsync = ExperienceRepositoryAsync;
		}
		#endregion

		#region Methods
		public async Task<GenericResult<IList<ExperienceLightViewModel>>> Search(ExperienceSearchModel searchModel)
		{
			var lang = this._languageService.CurrentLanguage;
			var query = await this._ExperienceRepositoryAsync.GetAsync(repositoryRequest: null);

			#region Set IncludedNavigationsList
			//query = await this._ExperienceRepositoryAsync.SetIncludedNavigationsListAsync(query, IncludedNavigationsList);
			#endregion

			#region Build Query
			query = query
				.Where(entity => entity.IsDeleted == false);

            query = query.PrimengTableFilter(searchModel, out int totalRecord);

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
		
			#endregion

			searchModel.Pagination = await this._ExperienceRepositoryAsync.SetPaginationCountAsync(query, searchModel.Pagination);

			query = await this._ExperienceRepositoryAsync.SetSortOrderAsync(query, searchModel.Sorting);

			query = await this._ExperienceRepositoryAsync.SetPaginationAsync(query, searchModel.Pagination);


            var entityList = query.Select(e => new ExperienceLightViewModel
            {
                Id = e.Id,
                CreationDate = e.CreationDate,
                NameEn = e.NameEn,
                NameAr = e.NameAr,
                ParentId = e.ParentId,
                ParentNameEn = e.ParentId != null ? e.Parent.NameEn : "",
                ParentNameAr = e.ParentId != null ? e.Parent.NameAr : ""

            }).ToList();
            var result = new GenericResult<IList<ExperienceLightViewModel>>
            {
                Pagination = searchModel.Pagination,
                Collection = entityList
            };


            return result;
		}

		public async Task<byte[]> ExportExcel(ExperienceSearchModel searchModel)
		{
			searchModel.Pagination = new Pagination { PageIndex = null, PageSize = null };

			var data = await this.Search(searchModel);
			byte[] result = null;

			using (var workbook = new XLWorkbook())
			{
				var worksheet = workbook.Worksheets.Add("Experience");
				var currentRow = 1;

				worksheet = this._excelService.SetStyle(worksheet, currentRow, 9);

				worksheet.Cell(currentRow, 1).Value = "Id";


				foreach (var item in data.Collection)
				{
					currentRow++;
					worksheet.Cell(currentRow, 1).Value = item.Id.ToString();

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

		public async Task<ExperienceViewModel> AddAsync(ExperienceViewModel model)
		{
			await this.ValidateModelAsync(model);

			var entity = model.ToEntity(this._mapper);
			entity = await this._ExperienceRepositoryAsync.AddAsync(entity);
			#region Commit Changes
			await this._unitOfWorkAsync.CommitAsync();
			#endregion

			var result = entity.ToModel(this._mapper);
			return result;
		}

		public async Task DeleteAsync(long id)
		{
			await this._ExperienceRepositoryAsync.DeleteAsync(id);

			#region Commit Changes
			await this._unitOfWorkAsync.CommitAsync();
			#endregion
		}

		public async Task<GenericResult<IList<ExperienceViewModel>>> GetAsync(BaseFilter baseFilter = null)
		{
			var lang = this._languageService.CurrentLanguage;
			RepositoryRequestConditionFilter<Experience, long> repositoryRequest = new RepositoryRequestConditionFilter<Experience, long>
			{
				Pagination = baseFilter?.Pagination,
				Sorting = baseFilter?.Sorting
			};
			var entityCollection = await this._ExperienceRepositoryAsync.GetAsync(repositoryRequest);
            var entityList = entityCollection.Select(e => new ExperienceViewModel
            {
                Id = e.Id,

                ParentId = e.ParentId,
              NameAr=e.NameAr,
			  NameEn=e.NameEn,
                ParentName = e.ParentId != null ? (lang == Language.Arabic ? e.Parent.NameAr : e.Parent.NameEn) : ""

            }).ToList();
			var result = new GenericResult<IList<ExperienceViewModel>>
			{
				Pagination = repositoryRequest.Pagination,
				Collection = entityList
            };

			return result;
		}

		public async Task<ExperienceViewModel> GetAsync(long id)
		{
			var entity = await this._ExperienceRepositoryAsync.FirstOrDefaultAsync(x => x.Id == id);
			var model = entity.ToModel(this._mapper);
			return model;
		}
       
        public async Task<ExperienceViewModel> UpdateAsync(ExperienceViewModel model)
		{
			await this.ValidateModelAsync(model);
			var entity = model.ToEntity(this._mapper);

			#region Select Existing Entity
			var include = new string[]
			{
			};
			var existEntity = await this._ExperienceRepositoryAsync.FirstOrDefaultAsync(x => x.Id == model.Id, include);
			#endregion

			this.UpdateExistEntityFromModel(existEntity, entity);
			existEntity = await this._ExperienceRepositoryAsync.UpdateAsync(existEntity);

			#region Commit Changes
			await this._unitOfWorkAsync.CommitAsync();
			#endregion

			var result = existEntity.ToModel(this._mapper);
			return result;
		}

		public async Task ValidateModelAsync(ExperienceViewModel model)
		{
			await Task.Run(() =>
			{
				//var existEntity = this._ExperienceRepositoryAsync.GetAsync(repositoryRequest: null).Result.FirstOrDefault(entity =>
				//				entity.NameAr == model.NameAr && entity.NameEn == model.NameEn
    //                            && entity.ParentId == model.ParentId 
    //                            && entity.Id != model.Id);

				//if (existEntity != null)
				//	throw new ItemAlreadyExistException((int)ErrorCode.CodeAlreadyExist);
			});
		}

		private Experience UpdateExistEntityFromModel(Experience existEntity, Experience newEntity)
		{
            #region Update Exist Entity Properties
            /*
             * Update properties that need updates here
             * Example:
             * existEntity.<property> = newEntity.<property>;
             */
            existEntity.NameEn = newEntity.NameEn;
            existEntity.NameAr = newEntity.NameAr;
            existEntity.ParentId = newEntity.ParentId;
            ;



            #endregion

            return existEntity;
		}
     
		public async Task<IList<ExperienceLookupViewModel>> GetLookup()
		{
            var lang = this._languageService.CurrentLanguage;

            var query = await this._ExperienceRepositoryAsync.GetAsync(repositoryRequest: null);
            var entityList = query.Select(e => new ExperienceLookupViewModel
            {
                Id = e.Id,
                Name = lang == Language.Arabic ? e.NameAr : e.NameEn,

            }).ToList();
            return entityList;
        }
        #endregion
    }
}
