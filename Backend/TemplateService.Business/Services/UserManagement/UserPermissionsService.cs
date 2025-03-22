#region Using ...
using AutoMapper;
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
#endregion

/*


*/
namespace TemplateService.Business.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class UserPermissionsService : Base.BaseService, IUserPermissionsService
    {
        #region Data Members
        private readonly IMapper _mapper;
        private readonly ILoggerService _loggerService;
        private readonly ILanguageService _languageService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly IExcelService _excelService;
        private readonly IUserPermissionsRepositoryAsync _UserPermissionsRepositoryAsync;
        private readonly IUserRolesRepositoryAsync _userRolesRepositoryAsync;
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
        /// <param name="UserPermissionsRepositoryAsync"></param>
        public UserPermissionsService(
            IMapper mapper,
            ILoggerService logger,
            ILanguageService languageService,
            IUnitOfWorkAsync unitOfWorkAsync,
            IExcelService excelService,
            IUserPermissionsRepositoryAsync UserPermissionsRepositoryAsync,
            IUserRolesRepositoryAsync UserRolesRepositoryAsync
            )
        {
            this._mapper = mapper;
            this._loggerService = logger;
            this._languageService = languageService;
            this._unitOfWorkAsync = unitOfWorkAsync;
            this._excelService = excelService;
            this._UserPermissionsRepositoryAsync = UserPermissionsRepositoryAsync;
            _userRolesRepositoryAsync = UserRolesRepositoryAsync;
        }
        #endregion

        #region Methods

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="searchModel"></param>
        ///// <returns></returns>
        //public async Task<byte[]> ExportExcel(UserPermissionSearchModel searchModel)
        //{
        //    searchModel.Pagination = new Pagination { PageIndex = null, PageSize = null };

        //    var data = await this.Search(searchModel);
        //    byte[] result = null;

        //    using (var workbook = new XLWorkbook())
        //    {
        //        var worksheet = workbook.Worksheets.Add("UserPermission");
        //        var currentRow = 1;

        //        worksheet = this._excelService.SetStyle(worksheet, currentRow, 9);

        //        worksheet.Cell(currentRow, 1).Value = "Id";
        //        worksheet.Cell(currentRow, 2).Value = "CreationDate";


        //        foreach (var item in data.Collection)
        //        {
        //            currentRow++;
        //            worksheet.Cell(currentRow, 1).Value = item.Id;
        //            worksheet.Cell(currentRow, 2).Value = item.CreationDate;

        //        }

        //        byte[] content = null;
        //        using (var stream = new MemoryStream())
        //        {
        //            workbook.SaveAs(stream);
        //            content = stream.ToArray();
        //        }
        //        result = content;
        //    }
        //    return result;
        //}

        public async Task<UserPermissionViewModel> AddAsync(UserPermissionViewModel model)
        {
            await this.ValidateModelAsync(model);

            var entity = model.ToEntity(this._mapper);
            entity = await this._UserPermissionsRepositoryAsync.AddAsync(entity);

            #region Commit Changes
            await this._unitOfWorkAsync.CommitAsync();
            #endregion

            var result = entity.ToModel(this._mapper);
            return result;
        }

        public async Task DeleteAsync(long  id)
        {
            await this._UserPermissionsRepositoryAsync.DeleteAsync(id);

            var listQuerable = await this._UserPermissionsRepositoryAsync.GetAsync(repositoryRequest:null);
            listQuerable = listQuerable.Where(entity => entity.Id == id);

            if (listQuerable != null)
            {
                foreach (var item in listQuerable)
                {
                    await this._UserPermissionsRepositoryAsync.DeleteAsync(item.Id);
                }
            }

            #region Commit Changes
            await this._unitOfWorkAsync.CommitAsync();
            #endregion
        }

        public async Task<GenericResult<IList<UserPermissionViewModel>>> GetAsync(BaseFilter baseFilter = null)
        {
            var lang = this._languageService.CurrentLanguage;
            RepositoryRequestConditionFilter<UserPermission, Guid> repositoryRequest = new RepositoryRequestConditionFilter<UserPermission, Guid>
            {
                Pagination = baseFilter?.Pagination,
                Sorting = baseFilter?.Sorting

            };
            var entityCollection = await this._UserPermissionsRepositoryAsync.GetAsync(repositoryRequest);
            var entityList = entityCollection.ToList();
            var result = new GenericResult<IList<UserPermissionViewModel>>
            {
                Pagination = repositoryRequest.Pagination,
                Collection = entityCollection.Select(entity => entity.ToModel(this._mapper)).ToList()
            };

            return result;
        }

        public async Task<UserPermissionViewModel> GetAsync(long  id)
        {
            var entity = await this._UserPermissionsRepositoryAsync.FirstOrDefaultAsync(x => x.Id == id);
            var model = entity.ToModel(this._mapper);
            return model;
        }



        public async Task<UserPermissionViewModel> UpdateAsync(UserPermissionViewModel model)
        {
            await this.ValidateModelAsync(model);
            var entity = model.ToEntity(this._mapper);

            #region Select Existing Entity
            var existEntity = await this._UserPermissionsRepositoryAsync.FirstOrDefaultAsync(x => x.Id == model.Id);
            #endregion

            this.UpdateExistEntityFromModel(existEntity, entity);
            existEntity = await this._UserPermissionsRepositoryAsync.UpdateAsync(existEntity);


            #region Commit Changes
            await this._unitOfWorkAsync.CommitAsync();
            #endregion

            var result = existEntity.ToModel(this._mapper);
            return result;
        }

        public async Task ValidateModelAsync(UserPermissionViewModel model)
        {
            await Task.Run(() => { });
        }

        public async Task ValidateModelAsync(IEnumerable<UserPermissionViewModel> modelCollection)
        {
            await Task.Run(() => { });
        }

        private UserPermission UpdateExistEntityFromModel(UserPermission existEntity, UserPermission newEntity)
        {
            #region Update Exist Entity Properties
            /*
             * Update properties that need updates here
             * Example:
             * existEntity.<property> = newEntity.<property>;
             */
            existEntity.PermissionId = newEntity.PermissionId;
            existEntity.UserId = newEntity.UserId;

            #endregion


            return existEntity;
        }

        #endregion
    }
}
