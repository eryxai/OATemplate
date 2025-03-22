#region Using ...
using AutoMapper;
using TemplateService.Business.Helper;
using TemplateService.Common.Enums;
using TemplateService.Core;
using TemplateService.Core.Common;
using TemplateService.Core.Helper;
using TemplateService.Core.IRepositories;
using TemplateService.Core.IServices;
using TemplateService.Core.Models.ViewModels;
using TemplateService.Entity.Entities;
using Framework.Common.Enums;
using Framework.Common.Exceptions;
using Framework.Core.Common;
using Framework.Core.IRepositories;
using Framework.Core.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;

#endregion

/*


*/
namespace TemplateService.Business.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class UsersService : Base.BaseService, IUsersService
    {
        #region Data Members
        private readonly IMapper _mapper;
        private readonly ILoggerService _loggerService;
        private readonly ILanguageService _languageService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly IExcelService _excelService;
        private readonly IUsersRepositoryAsync _UsersRepositoryAsync;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserPermissionsRepositoryAsync _UserPermissionsRepository;
        private readonly IUserRolesRepositoryAsync _userRolesRepository;
        private readonly IConfiguration Configuration;
        private readonly IHostingEnvironment _environment;
        private readonly IUserLoginsRepositoryAsync _userLoginsRepositoryAsync;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IJwtService _jwtService;
        private readonly ILocalizationService _localizationService;
        private readonly IRolesService _rolesService;
        private readonly IMailNotification _mailNotification;
        private readonly IRolePermissionsRepositoryAsync rolePermissionsRepository;



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
        /// <param name="UsersRepositoryAsync"></param>
        public UsersService(
            IRolePermissionsRepositoryAsync rolePermissionsRepository,
            IMapper mapper,
            ILoggerService logger,
            ILanguageService languageService,
            IUnitOfWorkAsync unitOfWorkAsync,
            IExcelService excelService,
            IUsersRepositoryAsync UsersRepositoryAsync,
            ICurrentUserService currentUserService,
            IUserPermissionsRepositoryAsync userPermissionsRepository,
            IUserRolesRepositoryAsync userRolesRepository,
            IConfiguration configuration,
            IHostingEnvironment environment,
            IUserLoginsRepositoryAsync userLoginsRepositoryAsync,
            IHttpContextAccessor httpContext,
            IJwtService jwtService,
            ILocalizationService localizationService,
            IMailNotification mailNotification,
            IRolesService rolesService)
        {
            this._mapper = mapper;
            this._loggerService = logger;
            this._languageService = languageService;
            this._unitOfWorkAsync = unitOfWorkAsync;
            this._excelService = excelService;
            this._UsersRepositoryAsync = UsersRepositoryAsync;
            this._currentUserService = currentUserService;
            this._UserPermissionsRepository = userPermissionsRepository;
            this._userRolesRepository = userRolesRepository;
            this.Configuration = configuration;
            this._userLoginsRepositoryAsync = userLoginsRepositoryAsync;
            this._httpContext = httpContext;
            _jwtService = jwtService;
            this._localizationService = localizationService;
            this._rolesService = rolesService;
            this._environment = environment;
            this._mailNotification = mailNotification;
            this.rolePermissionsRepository = rolePermissionsRepository;
        }
        #endregion

        #region Methods
        private User UpdateExistEntityFromModel(User existEntity, User newEntity)
        {
            #region Update Exist Entity Properties
            /*
             * Update properties that need updates here
             * Example:
             * existEntity.<property> = newEntity.<property>;
             */
            existEntity.Username = newEntity.Email;
           // existEntity.Password = newEntity.Password;
            existEntity.IsActive = newEntity.IsActive;
            existEntity.NameAr = newEntity.NameAr;
            existEntity.NameEn = newEntity.NameEn;
            existEntity.Email = newEntity.Email;
            existEntity.PhoneNumber = newEntity.PhoneNumber;
            existEntity.ProfileImage = newEntity.ProfileImage;
           
            existEntity.IsSuperAdmin = newEntity.IsSuperAdmin;
            #endregion

            return existEntity;
        }
        #endregion

        #region IUsersService
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task ValidateModelAsync(UserViewModel model)
        {
            await Task.Run(() =>
            {

                var existEntity = this._UsersRepositoryAsync.GetAsync(repositoryRequest:null).Result.FirstOrDefault(entity =>
                        (entity.Username == model.Username || entity.Email == model.Email) &&
                        entity.Id != model.Id && entity.IsDeleted != true);

                if (existEntity != null)
                   
                        throw new ItemAlreadyExistException((int)ErrorCode.UserNameAlreadyExist);
                    
                   
            });
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelCollection"></param>
        /// <returns></returns>
        public async Task ValidateModelAsync(IEnumerable<UserViewModel> modelCollection)
        {
            await Task.Run(() =>
            {

            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagination"></param>
        /// <returns></returns>
        public async Task<GenericResult<IList<UserViewModel>>> GetAsync(BaseFilter baseFilter = null)
        {
            var lang = this._languageService.CurrentLanguage;
            RepositoryRequestConditionFilter<User, long> repositoryRequest = new RepositoryRequestConditionFilter<User, long>
            {
                Pagination = baseFilter?.Pagination,
                Sorting = baseFilter?.Sorting,
                IncludedNavigationsList = new List<string>(new string[] {
                }),
                Query = e => e.IsDeleted == false
            };
            var entityCollection = await this._UsersRepositoryAsync.GetAsync(repositoryRequest);
            var entityList = entityCollection.ToList();
            var result = new GenericResult<IList<UserViewModel>>
            {
                Pagination = repositoryRequest.Pagination,
                Collection = entityCollection.Select(entity => entity.ToModel(this._mapper)).ToList()
            };
            return result;
        }


        /// <summary>
        /// Gets a lookup from entity.
        /// </summary>
        /// <param name="pagination"></param>
        /// <returns></returns>
        public async Task<GenericResult<IList<UserLookupViewModel>>> GetLookupAsync(BaseFilter baseFilter = null)
        {
            //var lang = this._languageService.CurrentLanguage;
            //RepositoryRequestConditionFilter<User, long> repositoryRequest = new RepositoryRequestConditionFilter<User, long>
            //{
            //	Pagination = baseFilter?.Pagination,
            //	Sorting = baseFilter?.Sorting,
            //	IncludedNavigationsList = new List<string>(new string[] {
            //	})
            //	,
            //	Query = (entity => entity.IsDeleted == false
            //		&& entity.IsActive == true

            //		)

            //};
            //var entityCollection = await this._UsersRepositoryAsync.GetAsync(repositoryRequest);
            //var entityList = entityCollection.ToList();
            //var result = new GenericResult<IList<UserLookupViewModel>>
            //{
            //	Pagination = repositoryRequest.Pagination,
            //	Collection = entityCollection.Select(entity => entity.ToLookupModel(this._mapper)).ToList()
            //};

            //return result;


            var lang = this._languageService.CurrentLanguage;

            var IncludedNavigationsList = new List<string>(new string[] {
            });

            var entityCollection = await this._UsersRepositoryAsync.GetAsync(repositoryRequest:null);

            #region Set IncludedNavigationsList
            entityCollection = await this._UsersRepositoryAsync.SetIncludedNavigationsListAsync(entityCollection, IncludedNavigationsList);
            #endregion

            entityCollection = entityCollection.Where(entity =>
                    //entity.Language == lang && 
                    entity.IsDeleted == false &&
                    entity.IsActive == true
                    );

            entityCollection = entityCollection.Select(entity => new User
            {
                Id = entity.Id,
                Username = entity.Username,
                NameAr = entity.NameAr,
                NameEn = entity.NameEn,
            });

            if (baseFilter != null)
            {
                if (baseFilter.Pagination != null) baseFilter.Pagination = await this._UsersRepositoryAsync.SetPaginationCountAsync(entityCollection, baseFilter.Pagination);

                if (baseFilter.Sorting != null) entityCollection = await this._UsersRepositoryAsync.SetSortOrderAsync(entityCollection, baseFilter.Sorting);

                if (baseFilter.Pagination != null) entityCollection = await this._UsersRepositoryAsync.SetPaginationAsync(entityCollection, baseFilter.Pagination);
            }

            var entityList = entityCollection.ToList();
            var result = new GenericResult<IList<UserLookupViewModel>>
            {
                Pagination = baseFilter?.Pagination,
                Collection = entityCollection.Select(entity => entity.ToLookupModel(this._mapper)).ToList()
            };

            return result;
        }

        /// <summary>
        /// Gets an entity by its id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UserViewModel> GetAsync(long id)
        {
            var include = new string[]
            {
                "UserRoles"
            };
            var query = await this._UsersRepositoryAsync.GetAsync(repositoryRequest:null);
            query = await this._UsersRepositoryAsync.SetIncludedNavigationsListAsync(query, include);
            var userId = _currentUserService.CurrentUserId;
            var CurrentUser = await _UsersRepositoryAsync.FirstOrDefaultAsync(u => u.Id == userId);
            query = query.Where(entity => entity.Id == id);
            //if (!CurrentUser.IsSuperAdmin)
            //{
            //    query = query.Where(entity => entity.Id == id && (entity.Departments.Users.Any(a => CurrentUser.IsSuperAdmin || (a.Id == userId)) || (entity.Organization.Users.Any(a => a.Id == userId && a.DepartmentId == null))));

            //}
            var entity = query.FirstOrDefault();
            if (entity == null)
                throw new Framework.Common.Exceptions.InvalidOperationException((int)ErrorCode.InvalidOperationException);
            var model = entity.ToModel(this._mapper);

            //get RolesIds
            foreach (var UserRole in entity.UserRoles)
            {
                model.RoleIds.Add(UserRole.RoleId);
            }

            return model;
        }

        public async Task<UserDetailViewModel> GetDetailsAsync(long id)
        {
            var lang = this._languageService.CurrentLanguage;
            var include = new string[]
           {
                "UserRoles.Role"
           };
            var entity = await this._UsersRepositoryAsync.FirstOrDefaultAsync(x => x.Id == id, include);
            var model = entity.ToDetailModel(this._mapper);
            foreach (var UserRole in entity.UserRoles)
            {
                model.RoleIds.Append(UserRole.RoleId);
            }
            model.RoleName = lang == Language.Arabic ? entity.UserRoles.FirstOrDefault()?.Role?.NameAr : entity.UserRoles.FirstOrDefault()?.Role?.NameEn;
            

            return model;
        }


        public async Task<List<UserLightViewModel>> GetWorkerUsersToNotifyThemAsync()
        {
            var query = await this._UsersRepositoryAsync.GetAsync(repositoryRequest:null);

            var IncludedNavigationsList = new List<string>(new string[] {
            });

            #region Set IncludedNavigationsList
            query = await this._UsersRepositoryAsync.SetIncludedNavigationsListAsync(query, IncludedNavigationsList);
            #endregion

            query = query.Where(entity =>
                entity.IsDeleted == false &&
                entity.IsActive == true);

            return query.Select(item => item.ToLightModel(this._mapper)).ToList();
        }

        /// <summary>
        /// Adds an entity.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<UserViewModel> AddAsync(UserViewModel model)
        {

            await this.ValidateModelAsync(model);
            var entity = model.ToEntity(this._mapper);
            entity.CreatedByUserId = _currentUserService.CurrentUserId;
            if (bool.Parse(Configuration["IsHidenPassword"])) {
                var Password = "Ads"+GenerateRandomString();
                entity.Password = HashPass.HashPassword(Password);
            } else {
                entity.Password = HashPass.HashPassword(model.Password); 
            }
        
            entity = await this._UsersRepositoryAsync.AddAsync(entity);
            var HtmlContent = $@" 
                            <p>Welcome to Ads Panal. <br> <br>
                             Kindly use the current password for your account as {entity.Password}. <br> <br>
                             Kind regards,<br>
                             Ads Panal
                            </p>";
            await _mailNotification.SendMail(entity.Email, null, null, "User Created", HtmlContent);
            #region Commit Changes
            await this._unitOfWorkAsync.CommitAsync();
            #endregion
            if (model.Base64Image != null)
            {
                UploadHelper upload = new UploadHelper(_environment, Configuration, ObjectTypeAttachmentEnum.User.ToString());
                entity.ProfileImage = upload.Upload(model.Base64Image, entity.Id, model.FileName);
                entity = await this._UsersRepositoryAsync.UpdateAsync(entity);

                #region Commit Changes
                await this._unitOfWorkAsync.CommitAsync();
                #endregion
            }
     
            //add User Role
            foreach (var RoleId in model.RoleIds)
            {
                await this._userRolesRepository.AddAsync(new UserRole
                {
                    CreationDate = DateTime.Now,
                    UserId = entity.Id,
                    RoleId = RoleId
                });
            }
            

            await this._unitOfWorkAsync.CommitAsync();


            var result = entity.ToModel(this._mapper);
            return result;
        }


        /// <summary>
        /// Updates an entity.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<UserViewModel> UpdateAsync(UserViewModel model)
        {
            await this.ValidateModelAsync(model);
            var entity = model.ToEntity(this._mapper);

            #region Select Existing Entity
            var existEntity = await this._UsersRepositoryAsync.FirstOrDefaultAsync(x => x.Id == model.Id);
            #endregion
            entity.Password = existEntity.Password;
            this.UpdateExistEntityFromModel(existEntity, entity);
            if (model.Base64Image != null)
            {
                UploadHelper upload = new UploadHelper(_environment, Configuration, ObjectTypeAttachmentEnum.User.ToString());
                existEntity.ProfileImage = upload.Upload(model.Base64Image, entity.Id, model.FileName);
            }
            existEntity = await this._UsersRepositoryAsync.UpdateAsync(existEntity);
            //add User Role
            //remove old roles
            var userRoles = await this._userRolesRepository.GetAsync(x => x.UserId == entity.Id);
            foreach (var item in userRoles)
            {
                await this._userRolesRepository.DeleteAsync(item);
            }
            await this._unitOfWorkAsync.CommitAsync();
            foreach (var RoleId in model.RoleIds)
            {
              
               
                    await this._userRolesRepository.AddAsync(new UserRole
                    {
                        CreationDate = DateTime.Now,
                        UserId = entity.Id,
                        RoleId = RoleId
                    });
                
            }

              

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
            var userId = _currentUserService.CurrentUserId;
            var CurrentUser = await _UsersRepositoryAsync.FirstOrDefaultAsync(u => u.Id == userId);
            //if (!CurrentUser.IsSuperAdmin)
            //{
            //    var entity = await this._UsersRepositoryAsync.FirstOrDefaultAsync(entity => entity.Id == id && (entity.Departments.Users.Any(a => a.Id == userId) || (entity.Organization.Users.Any(a => a.Id == userId && a.DepartmentId == null))));
            //    if (entity == null)
            //        throw new Framework.Common.Exceptions.InvalidOperationException((int)ErrorCode.InvalidOperationException);
            //}
            await this._UsersRepositoryAsync.DeleteAsync(id);

            #region Commit Changes
            await this._unitOfWorkAsync.CommitAsync();
            #endregion
        }

        public bool GetHasPermissionToChangePostedAccounts()
        {
            bool result = false;
            var userId = this._currentUserService.CurrentUserId;

            if (userId.HasValue)
            {
                var permission = this._UserPermissionsRepository.GetAsync(repositoryRequest:null).Result.Where(x =>
                    x.UserId == userId &&
                    x.PermissionId.HasValue &&
                    x.Permission.IsActive == true
                    //&&
                    //x.Permission.Code == (int)Permissions.ControlPostingJournalEntries
                    ).FirstOrDefault();

                result = (permission != null);

                if (result == false)
                {
                    var role = this._userRolesRepository.GetAsync(repositoryRequest:null).Result.Where(x =>
                        x.UserId == userId &&
                        x.RoleId.HasValue &&
                        x.Role.RolePermissions.FirstOrDefault(y =>
                            y.Permission.IsActive == true
                            //&&
                            //y.Permission.Code == (int)Permissions.ControlPostingJournalEntries
                            ) != null
                            );

                    result = (role != null);
                }
            }

            return result;
        }

        public bool IsUserHassPermission(long? userId, Permissions permissionItem)
        {
            var permisstion = this._UserPermissionsRepository
                .GetAsync(repositoryRequest:null).Result
                .Where(x =>
                    x.UserId == userId &&
                    x.Permission.Code == (int)permissionItem &&
                    x.Permission.IsActive == true)
                .FirstOrDefault();

            return (permisstion != null);
        }

        public bool IsCurrentUserHassPermission(Permissions permissionItem)
        {
            var userId = this._currentUserService.CurrentUserId;
            var result = this.IsUserHassPermission(userId, permissionItem);
            return result;
        }

        public async Task<UserPermissionListViewModel> GetUserPermissionAsync(long userId)
        {
            var lang = this._languageService.CurrentLanguage;
            var IncludedNavigationsList = new List<string>(new string[] {
                "Permission",
            });

            var query = await this._UserPermissionsRepository.GetAsync(repositoryRequest:null);
            query = await this._UserPermissionsRepository.SetIncludedNavigationsListAsync(query, IncludedNavigationsList);

            var entityCollection = query.Where(x => x.UserId == userId)
                .OrderBy(x => x.Permission.Code)
                .ToList();

            UserPermissionListViewModel result = new UserPermissionListViewModel
            {
                UserId = userId,
                List = new List<NameValueViewModel>()
            };



            foreach (var item in entityCollection)
            {
                result.List.Add(new NameValueViewModel
                {
                    Value = item.PermissionId.Value,
                    Name = lang == Language.Arabic ? item.Permission.NameAr : item.Permission.NameEn
                });
            }
            return result;
        }
        public async Task<UserPermissionListViewModel> UpdateUserPermissionAsync(UserPermissionListViewModel model)
        {
            var currentUserId = this._currentUserService.CurrentUserId;
            if (currentUserId == model.UserId)
            {
                bool userCanChangeHisPermissions = false;
                string userCanChangeHisPermissionsString = Configuration["CommonSettings.UserCanChangeHisPermissions"];

                if (string.IsNullOrEmpty(userCanChangeHisPermissionsString) == false)
                {
                    try
                    {
                        userCanChangeHisPermissions = bool.Parse(userCanChangeHisPermissionsString);
                    }
                    catch
                    {
                        // do nothing at all
                    }
                }

                if (userCanChangeHisPermissions == false)
                {
                    //throw new GeneralException((int)ErrorCodeEnum.CurrentUserCannotChangeHisPermissions);
                }
            }

            var entityCollection = this._UserPermissionsRepository.GetAsync(repositoryRequest:null).Result.Where(x => x.UserId == model.UserId);

            if (entityCollection.Count() > 0)
            {
                foreach (var item in entityCollection)
                {
                    if (!model.List.Any(x => x.Value == item.PermissionId))
                    {
                        await this._UserPermissionsRepository.DeleteAsync(item);
                    }
                    else
                    {
                        model.List.Remove(model.List.FirstOrDefault(x => x.Value == item.PermissionId));
                    }

                }
                await this._unitOfWorkAsync.CommitAsync();
            }

            if (model.List?.Count > 0)
            {
                foreach (var item in model.List)
                {
                    UserPermission newEntity = new UserPermission
                    {
                        UserId = model.UserId,
                        PermissionId = item.Value
                    };
                    await this._UserPermissionsRepository.AddAsync(newEntity);
                }
                try
                {
                    await this._unitOfWorkAsync.CommitAsync();
                }
                catch (Exception ex)
                {

                }
            }

            return model;
        }

        public async Task<UserRoleListViewModel> GetUserRole(long userId)
        {
            var lang = this._languageService.CurrentLanguage;
            var IncludedNavigationsList = new List<string>(new string[] {
                 "Role"
            });
            var query = await this._userRolesRepository.GetAsync(repositoryRequest:null);
            query = await this._userRolesRepository.SetIncludedNavigationsListAsync(query, IncludedNavigationsList);
            var entityCollection = query.Where(x => x.UserId == userId).ToList();


            UserRoleListViewModel result = new UserRoleListViewModel
            {
                UserId = userId,
                List = new List<NameValueViewModel>()
            };

            foreach (var item in entityCollection)
            {
                result.List.Add(new NameValueViewModel
                {
                    Value = item.RoleId.Value,
                    Name = lang == Language.Arabic ? item.Role.NameAr : item.Role.NameEn
                });
            }
            return result;
        }
        public async Task<UserRoleListViewModel> UpdateUserRole(UserRoleListViewModel model)
        {
            var currentUserId = this._currentUserService.CurrentUserId;
            if (currentUserId == model.UserId)
            {
                bool UserCanChangeHisRoles = false;
                string UserCanChangeHisRolesString = Configuration["CommonSettings.UserCanChangeHisRoles"];

                if (string.IsNullOrEmpty(UserCanChangeHisRolesString) == false)
                {
                    try
                    {
                        UserCanChangeHisRoles = bool.Parse(UserCanChangeHisRolesString);
                    }
                    catch
                    {
                        // do nothing at all
                    }
                }

                if (UserCanChangeHisRoles == false)
                {
                    //throw new GeneralException((int)ErrorCodeEnum.CurrentUserCannotChangeHisRoles);
                }
            }

            var entityCollection = this._userRolesRepository.GetAsync(repositoryRequest:null).Result.Where(x => x.UserId == model.UserId);

            if (entityCollection.Count() > 0)
            {
                foreach (var item in entityCollection)
                {
                    await this._userRolesRepository.DeleteAsync(item);
                }
                await this._unitOfWorkAsync.CommitAsync();
            }

            if (model.List?.Count > 0)
            {
                foreach (var item in model.List)
                {
                    UserRole newEntity = new UserRole
                    {
                        UserId = model.UserId,
                        RoleId = item.Value
                    };
                    await this._userRolesRepository.AddAsync(newEntity);

                    //   await SendNotificationWhenAssignedRoleToUser(model.UserId, item.Value);
                }
                await this._unitOfWorkAsync.CommitAsync();
            }

            return model;
        }

        public UserLoggedInViewModel Login(LoginViewModel model)
        {
            var lang =  this._languageService.CurrentLanguage;
            if (lang == Language.None) lang = Language.Arabic;
            UserLoggedInViewModel viewModel = new UserLoggedInViewModel();
            var user = this._UsersRepositoryAsync.Login(model.UserName);
            if (user != null)
            {
                //var IsAuthenticatedUser = _ldapValidatorService.Validate(model.UserName, model.Password);
                var IsAuthenticatedUser = VerifyHashedPassword(user.Password, model.Password);
                //if(user.Id==1)
                //{
                //    IsAuthenticatedUser = VerifyHashedPassword(user.Password, model.Password);
                //}
                if (IsAuthenticatedUser)
                {
                    #region Add User Login To History
                    UserLogin userLogin = new UserLogin
                    {
                        UserId = user.Id,
                        Username = user.Username,
                        IPV4 = this._httpContext.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString(),
                        IPV6 = this._httpContext.HttpContext.Connection.RemoteIpAddress.MapToIPv6().ToString()
                    };
                    userLogin = this._userLoginsRepositoryAsync.AddAsync(userLogin).Result;
                    var res = this._unitOfWorkAsync.CommitAsync().Result;
                    #region user InActive 
                    if (user.IsActive == false)
                        throw new DataDuplicateException((int)ErrorCode.InActiveUser);
                    #endregion

                    #endregion
                    var permissions = this._UserPermissionsRepository.GetAsync(repositoryRequest:null).Result.Where(x => x.UserId == user.Id).Select(x => x.Permission).Select(x => x.Code).ToList();
                    viewModel.Id = user.Id;
                    viewModel.UserName = user.Username;
                    viewModel.token_type = "Bearer";
                    viewModel.issued = DateTime.Now;
                    viewModel.PhoneNumber = user.PhoneNumber;
                    viewModel.ProfileImage = user.ProfileImage;
                    viewModel.Email = user.Email;
                    viewModel.NameAr = user.NameAr;
                    viewModel.NameEn = user.NameEn;
                    viewModel.IsSuperAdmin = user.IsSuperAdmin;

                    viewModel.Name = lang == Language.Arabic ? user.NameAr : user.NameEn;
                    viewModel.RoleName = lang == Language.Arabic ? user?.UserRoles?.FirstOrDefault()?.Role?.NameAr : user?.UserRoles?.FirstOrDefault()?.Role?.NameEn;

                    var rolesIds = user?.UserRoles.Select(c => c.RoleId);
                    HashSet<int?> PermissionsHashSet = new HashSet<int?>();
                    var Permissions = this.rolePermissionsRepository.GetAsync(repositoryRequest:null).Result
                        .Where(x => rolesIds.Contains( x.RoleId.Value ))
                        .Select(x => x.Permission)
                        .Select(c => c.Code).ToList();

                    foreach (var item in permissions)
                    {
                        PermissionsHashSet.Add(item);
                    }
                    var roles = _userRolesRepository.GetAsync(repositoryRequest: null).Result.Where(x => x.UserId == user.Id).Select(x => x.Role).Select(x => x.RolePermissions.Select(h => h.Permission.Code)).ToList();
                    foreach (var item in roles)
                    {
                        foreach (var role in item)
                        {
                            PermissionsHashSet.Add(role);
                        }

                    }
                    Permissions = PermissionsHashSet.ToList();

                    StringBuilder stringBuilder = new StringBuilder();
                    string userPermissions = string.Empty;
                    for (int i = 0; i < Permissions.Count; i++)
                    {
                        stringBuilder.Append(Permissions[i]);
                        if (i < Permissions.Count - 1) stringBuilder.Append(',');
                    }
                    userPermissions = stringBuilder.ToString();
                    viewModel.access_token = _jwtService.GenerateJWTToken(user.Id.ToString(), userPermissions);

                    #region	 Check if it frst time login
                    viewModel.IsFirstTimeLogin = !this._userLoginsRepositoryAsync.GetAsync(repositoryRequest:null)
                                                .Result.Any(x => !x.IsDeleted && x.ChangePassword && x.Username == user.Username);
                    #endregion
                    return viewModel;
                }
                else
                {
                    #region Add User Login To History
                    UserLogin userLogin = new UserLogin
                    {
                        UserId = user.Id,
                        Username = user.Username,
                        IPV4 = this._httpContext.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString(),
                        IPV6 = this._httpContext.HttpContext.Connection.RemoteIpAddress.MapToIPv6().ToString(),
                    };

                    userLogin = this._userLoginsRepositoryAsync.AddAsync(userLogin).Result;
                    var res = this._unitOfWorkAsync.CommitAsync().Result;
                    #endregion

                    return null;
                }
            }
            else // login false
            {
                #region Add User Login To History
                UserLogin userLogin = new UserLogin
                {
                    Username = model.UserName,
                    IPV4 = this._httpContext.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString(),
                    IPV6 = this._httpContext.HttpContext.Connection.RemoteIpAddress.MapToIPv6().ToString(),
                };

                userLogin = this._userLoginsRepositoryAsync.AddAsync(userLogin).Result;
                var res = this._unitOfWorkAsync.CommitAsync().Result;
                #endregion
                return null;
            }

        }

        public MobileUserLoggedInViewModel MobileLogin(MobileLoginViewModel model)
        {
            MobileUserLoggedInViewModel viewModel = new MobileUserLoggedInViewModel();
            var user = this._UsersRepositoryAsync.Login(model.UserName);
            if (user != null)
            {
                if (VerifyHashedPassword(user.Password, model.Password))
                {
                    #region Add User Login To History
                    UserLogin userLogin = new UserLogin
                    {
                        UserId = user.Id,
                        Username = user.Username,
                        IPV4 = this._httpContext.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString(),
                        IPV6 = this._httpContext.HttpContext.Connection.RemoteIpAddress.MapToIPv6().ToString(),
                    };
                    userLogin = this._userLoginsRepositoryAsync.AddAsync(userLogin).Result;
                    var res = this._unitOfWorkAsync.CommitAsync().Result;
                    #endregion
                    viewModel.UserId = user.Id;
                    #region Permissions
                    //HashSet<int?> PermissionsHashSet = new HashSet<int?>();
                    //var Permissions = this._UserPermissionsRepository.GetAsync(repositoryRequest:null).Result
                    //	.Where(x => x.UserId == user.Id)
                    //	.Select(x => x.Permission)
                    //	.Select(c => c.Code).ToList();

                    //foreach (var item in Permissions)
                    //{
                    //	PermissionsHashSet.Add(item);
                    //}
                    //var rolesPermissions = _userRolesRepository.GetAsync(repositoryRequest:null).Result.Where(x => x.UserId == user.Id).Select(x => x.Role).Select(x => x.RolePermissions.Select(h => h.Permission.Code)).ToList();



                    //foreach (var item in rolesPermissions)
                    //{
                    //	foreach (var role in item)
                    //	{
                    //		PermissionsHashSet.Add(role);
                    //	}

                    //}

                    //var groupsPermissions = _userGroupsRepository.GetAsync(repositoryRequest:null).Result.Where(x => x.UserId == user.Id).Select(x => x.Group).Select(x => x.GroupPermissions.Select(h => h.Permission.Code)).ToList();


                    //foreach (var item in groupsPermissions)
                    //{
                    //	foreach (var group in item)
                    //	{
                    //		PermissionsHashSet.Add(group);
                    //	}

                    //}


                    //Permissions = PermissionsHashSet.ToList();


                    //StringBuilder stringBuilder = new StringBuilder();
                    //string userPermissions = string.Empty;

                    //for (int i = 0; i < Permissions.Count; i++)
                    //{
                    //	stringBuilder.Append(Permissions[i]);
                    //	if (i < Permissions.Count - 1) stringBuilder.Append(',');
                    //}
                    //userPermissions = stringBuilder.ToString();
                    #endregion
                    #region GenerateJWTToken
                    viewModel.Token = _jwtService.GenerateJWTToken(user.Id.ToString(), String.Empty);
                    #endregion
                    return viewModel;
                }
                else
                {
                    #region Add User Login To History
                    UserLogin userLogin = new UserLogin
                    {
                        UserId = user.Id,
                        Username = user.Username,
                        IPV4 = this._httpContext.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString(),
                        IPV6 = this._httpContext.HttpContext.Connection.RemoteIpAddress.MapToIPv6().ToString(),
                    };
                    userLogin = this._userLoginsRepositoryAsync.AddAsync(userLogin).Result;
                    var res = this._unitOfWorkAsync.CommitAsync().Result;
                    #endregion
                    return null;
                }
            }
            else
            {
                #region Add User Login To History
                UserLogin userLogin = new UserLogin
                {
                    Username = model.UserName,
                    IPV4 = this._httpContext.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString(),
                    IPV6 = this._httpContext.HttpContext.Connection.RemoteIpAddress.MapToIPv6().ToString(),
                };

                userLogin = this._userLoginsRepositoryAsync.AddAsync(userLogin).Result;
                var res = this._unitOfWorkAsync.CommitAsync().Result;
                #endregion
                return null;
            }
        }
        public async Task ChangePasswordAsync(ChangePasswordViewModel model)
        {
            _loggerService.LogInfo(JsonSerializer.Serialize(model));
            User user = await this._UsersRepositoryAsync.GetAsync(model.UserId);
            string newPasswordHash = HashPass.HashPassword(model.NewPassword);
           
            if (VerifyHashedPassword(user.Password, model.OldPassword))
            {
                
                user.Password = newPasswordHash;
                await this._UsersRepositoryAsync.UpdateAsync(user);

                #region Update last login :  ChangePassword  = true

                if (_currentUserService.CurrentUserId == model.UserId)
                {
                    var query = await this._userLoginsRepositoryAsync.GetAsync(repositoryRequest:null);
                    query = query.Where(x => !x.IsDeleted && x.Username == user.Username);
                    var lastlogin = this._userLoginsRepositoryAsync.SetSortOrderAsync(query, "CreationDate desc").Result.FirstOrDefault();

                    lastlogin.ChangePassword = true;
                    await this._userLoginsRepositoryAsync.UpdateAsync(lastlogin);
                }

                #endregion

                await this._unitOfWorkAsync.CommitAsync();
            }
            else
            {
                ///throw new GeneralException((int)ErrorCodeEnum.PasswordIncorrect);
                try
                {
                    throw new NotImplementedException("error." + (int)ErrorCode.PasswordIncorrect);
                }catch(Exception e)
                {
                    throw new Exception("error", e);
                }
                
            }
        }

        public async Task ResetPasswordAsync(long userId)
        {
            User user = await this._UsersRepositoryAsync.GetAsync(userId);
            string passwordHash = HashPass.HashPassword("123456");
            user.Password = passwordHash;
            await this._UsersRepositoryAsync.UpdateAsync(user);
            await this._unitOfWorkAsync.CommitAsync();
        }


        #endregion

        #region Helper

        private static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            //return hashedPassword == password;

            byte[] buffer4;
            if (hashedPassword == null)
            {
                return false;
            }
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            byte[] src = Convert.FromBase64String(hashedPassword);
            if ((src.Length != 0x31) || (src[0] != 0))
            {
                return false;
            }
            byte[] dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            byte[] buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
            {
                buffer4 = bytes.GetBytes(0x20);
            }
            return ByteArraysEqual(buffer3, buffer4);
        }
        private static bool ByteArraysEqual(byte[] firstHash, byte[] secondHash)
        {
            int _minHashLength = firstHash.Length <= secondHash.Length ? firstHash.Length : secondHash.Length;
            var xor = firstHash.Length ^ secondHash.Length;
            for (int i = 0; i < _minHashLength; i++)
                xor |= firstHash[i] ^ secondHash[i];
            return 0 == xor;
        }

        public async Task<CurrentUserViewModel> GetCurrentUser()
        {
            var lang = this._languageService.CurrentLanguage;
            var res = await this._UsersRepositoryAsync.GetAsync(repositoryRequest:null);
            var currentUser = res.Where(x => x.Id == _currentUserService.CurrentUserId).Select(x => new CurrentUserViewModel()
            {
                Id = x.Id,
                Name = lang == Language.Arabic ? x.NameAr : x.NameEn
            }).FirstOrDefault();
            return currentUser;
        }




        private async Task<List<long>> GetUsersHasPermission(Permissions code)
        {
            List<long> users = new List<long>();

            var userPermissionsQuery = await _UserPermissionsRepository.GetAsync(repositoryRequest:null);

            var userPermissions = userPermissionsQuery.Where(x => x.User.IsActive == true && x.Permission.Code == (int)code).Select(x => x.UserId.Value).ToList();
            if (userPermissions != null && userPermissions.Any())
            {
                users.AddRange(userPermissions);
            }

            var usersRolesQuery = await _userRolesRepository.GetAsync(repositoryRequest:null);
            var userRoles = usersRolesQuery.Where(x => x.User.IsActive == true && x.Role.RolePermissions.Any(x => x.Permission.Code == (int)code)).Select(x => x.UserId.Value).ToList();
            if (userRoles != null && userRoles.Any())
            {
                users.AddRange(userRoles);
            }

            if (users.Any())
            {
                users = users.Distinct().ToList();
            }

            return users;
        }

        public async Task<GenericResult<IList<UserLightViewModel>>> Search(UserSearchModel searchModel)
        {
            var lang = this._languageService.CurrentLanguage;
            var query = await this._UsersRepositoryAsync.GetAsync(repositoryRequest:null);
            int totalRecord = 0;
            searchModel = searchModel == null ? new UserSearchModel() : searchModel;
            query = query.PrimengTableFilter(searchModel, out totalRecord);
            query = await _UsersRepositoryAsync.SetSortOrderAsync(query, searchModel.Sorting);
            searchModel.Pagination = await _UsersRepositoryAsync.SetPaginationCountAsync(query, searchModel.Pagination);
            query = await _UsersRepositoryAsync.SetPaginationAsync(query, searchModel.Pagination);
            var IncludedNavigationsList = new List<string>(new string[] {
                });

            #region Set IncludedNavigationsList
            query = await _UsersRepositoryAsync.SetIncludedNavigationsListAsync(query, IncludedNavigationsList);

            query = query
               .Where(entity => entity.IsDeleted == false);

            #endregion
            var entityList = query.Select(x => new UserLightViewModel
            {
                Id = x.Id,
                IsActive = x.IsActive,
                CreationDate = x.CreationDate,
                Name = lang == Language.Arabic ? x.NameAr : x.NameEn,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                Username = x.Username,               
                RoleName = lang == Language.Arabic ?
                String.Join(", ", x.UserRoles.Select(a => a.Role.NameAr).ToList() ):
                String.Join(", ", x.UserRoles.Select(a => a.Role.NameEn).ToList()),
                ProfileImage = x.ProfileImage

            }).ToList();
            var result = new GenericResult<IList<UserLightViewModel>>
            {
                Pagination = searchModel.Pagination,
                Collection = entityList
            };

            return result;
        }

        public async Task<bool> ForgotPassword(ForgotPasswordModel forgotPasswordModel)
        {
            var entity = await this._UsersRepositoryAsync.FirstOrDefaultAsync(x => x.Email == forgotPasswordModel.Email);

            if (entity != null)
            {
                var Pass = "NCRS@";
                Random rand = new Random();
                int number = rand.Next(100000, 1000000); //returns random number between 0-99
                Pass = Pass + number.ToString();
                string passwordHash = HashPass.HashPassword(Pass);
                entity.Password = passwordHash;
                entity = await this._UsersRepositoryAsync.UpdateAsync(entity);
                await this._unitOfWorkAsync.CommitAsync();
               // var Password = "\"{NCRS@12345}\"";
                var HtmlContent = $@" 
                            <p>Welcome to NCRS. <br> <br>
                             Kindly use the current password for your account as ' {Pass}' <br> <br>
                             Kind regards,<br>
                             NCRS
                            </p>";
                await _mailNotification.SendMail(entity.Email, null, null, "Forgot Password", HtmlContent);
                return true;

            }
            else
                throw new ItemNotFoundException();
        }

        public Task<bool> CheckPermissionForList(long id)
        {
            throw new NotImplementedException();
        }

        //public async Task<bool> CheckPermissionForList(long id)
        //{
        //    var userId = _currentUserService.CurrentUserId;
        //    var CurrentUser = await _UsersRepositoryAsync.FirstOrDefaultAsync(u => u.Id == userId);
        //    if (CurrentUser.IsSuperAdmin)
        //    {
        //        return true;
        //    }
        //   /* var entity = await this._UsersRepositoryAsync.FirstOrDefaultAsync(entity => entity.Id == id && (entity.Departments.Users.Any(a => a.Id == userId) || (entity.Organization.Users.Any(a => a.Id == userI*/d && a.DepartmentId == null))));
        //    //return entity != null;
        //}
        public async Task<bool> IsHidenPassword()
        {
            var IsHidenPassword = bool.Parse(Configuration["IsHidenPassword"]);
            return IsHidenPassword;
        }


        #endregion

        #region private method
        private string GenerateRandomString()
        {
            string path = Path.GetRandomFileName();
            string chars = path.Substring(0, 4);
            var rand = new Random();
            int numbers = rand.Next(1000, 10000);
            return numbers + chars;
        }
        #endregion
    }
}
