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
    public class RolePermissionsService : Base.BaseService, IRolePermissionsService
    {
        #region Data Members
        private readonly IMapper _mapper;
        private readonly ILoggerService _loggerService;
        private readonly ILanguageService _languageService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly IExcelService _excelService;
        private readonly IRolePermissionsRepositoryAsync _RolePermissionsRepositoryAsync;
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
        /// <param name="RolePermissionsRepositoryAsync"></param>
        public RolePermissionsService(
            IMapper mapper,
            ILoggerService logger,
            ILanguageService languageService,
            IUnitOfWorkAsync unitOfWorkAsync,
            IExcelService excelService,
            IRolePermissionsRepositoryAsync RolePermissionsRepositoryAsync
            )
        {
            this._mapper = mapper;
            this._loggerService = logger;
            this._languageService = languageService;
            this._unitOfWorkAsync = unitOfWorkAsync;
            this._excelService = excelService;
            this._RolePermissionsRepositoryAsync = RolePermissionsRepositoryAsync;
        }
        #endregion

        #region Methods


        public async Task<RolePermissionViewModel> AddAsync(RolePermissionViewModel model)
        {
            await this.ValidateModelAsync(model);

            var entity = model.ToEntity(this._mapper);
            entity = await this._RolePermissionsRepositoryAsync.AddAsync(entity);


            #region Commit Changes
            await this._unitOfWorkAsync.CommitAsync();
            #endregion

            var result = entity.ToModel(this._mapper);
            return result;
        }

        public async Task DeleteAsync(long  id)
        {
            await this._RolePermissionsRepositoryAsync.DeleteAsync(id);

            var listQuerable = await this._RolePermissionsRepositoryAsync.GetAsync(repositoryRequest:null);
            listQuerable = listQuerable.Where(entity => entity.Id == id);

            if (listQuerable != null)
            {
                foreach (var item in listQuerable)
                {
                    await this._RolePermissionsRepositoryAsync.DeleteAsync(item.Id);
                }
            }

            #region Commit Changes
            await this._unitOfWorkAsync.CommitAsync();
            #endregion
        }

        public async Task<GenericResult<IList<RolePermissionViewModel>>> GetAsync(BaseFilter baseFilter = null)
        {
            var lang = this._languageService.CurrentLanguage;
            RepositoryRequestConditionFilter<RolePermission, Guid> repositoryRequest = new RepositoryRequestConditionFilter<RolePermission, Guid>
            {
                Pagination = baseFilter?.Pagination,
                Sorting = baseFilter?.Sorting

            };
            var entityCollection = await this._RolePermissionsRepositoryAsync.GetAsync(repositoryRequest);
            var entityList = entityCollection.ToList();
            var result = new GenericResult<IList<RolePermissionViewModel>>
            {
                Pagination = repositoryRequest.Pagination,
                Collection = entityCollection.Select(entity => entity.ToModel(this._mapper)).ToList()
            };

            return result;
        }

        public async Task<RolePermissionViewModel> GetAsync(long  id)
        {

            var entity = await this._RolePermissionsRepositoryAsync.FirstOrDefaultAsync(x => x.Id == id);
            var model = entity.ToModel(this._mapper);
            return model;
        }


        public async Task<RolePermissionViewModel> UpdateAsync(RolePermissionViewModel model)
        {
            await this.ValidateModelAsync(model);
            var entity = model.ToEntity(this._mapper);

            #region Select Existing Entity

            var existEntity = await this._RolePermissionsRepositoryAsync.FirstOrDefaultAsync(x => x.Id == model.Id);
            #endregion

            this.UpdateExistEntityFromModel(existEntity, entity);
            existEntity = await this._RolePermissionsRepositoryAsync.UpdateAsync(existEntity);

            #region Commit Changes
            await this._unitOfWorkAsync.CommitAsync();
            #endregion

            var result = existEntity.ToModel(this._mapper);
            return result;
        }

        public async Task ValidateModelAsync(RolePermissionViewModel model)
        {
            await Task.Run(() => { });
        }

        public async Task ValidateModelAsync(IEnumerable<RolePermissionViewModel> modelCollection)
        {
            await Task.Run(() => { });
        }

        private RolePermission UpdateExistEntityFromModel(RolePermission existEntity, RolePermission newEntity)
        {
            #region Update Exist Entity Properties
            /*
             * Update properties that need updates here
             * Example:
             * existEntity.<property> = newEntity.<property>;
             */
            existEntity.PermissionId = newEntity.PermissionId;
            existEntity.RoleId = newEntity.RoleId;

            #endregion


            return existEntity;
        }

        #endregion
    }
}
