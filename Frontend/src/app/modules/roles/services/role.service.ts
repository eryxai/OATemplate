import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Observable } from 'rxjs';
import { BaseHttpServiceService } from './../../../sharedFeatures/services/base-http-service.service';
import { CurrentUserService } from './../../../sharedFeatures/services/current-user.service';
import { GenericResultModel } from 'src/app/sharedFeatures/models/generic-result.model';
import { LookupModel } from 'src/app/sharedFeatures/models/lookup-model';

import { OrganizationlookupSearchModel } from '../../users/models/lookup-search-model';
import { RoleViewModel } from '../models/role-model';
import { RoleDetailModel } from '../models/role-details-model';
import { RoleSearchViewModel } from '../models/role-search-view-model.model';
import { RoleLightViewModel } from '../models/role-light-view-model.model';
import { RolePermissionListViewModel } from '../models/role-permission-list.model';

@Injectable({
  providedIn: 'root',
})
export class RoleService  extends BaseHttpServiceService  {
  private controller: string = `${this.baseUrl}/Roles/`;

  constructor(
    http: HttpClient,
    translateService: TranslateService,
    currentUserService: CurrentUserService
  ) {
    super(http, translateService, currentUserService);
  }

  search(
    model: RoleSearchViewModel
  ): Observable<GenericResultModel<RoleLightViewModel[]>> {
    let url: string = `${this.controller}search`;
    return this.postData<GenericResultModel<RoleLightViewModel[]>>(url, model);
  }
  add(model: RoleViewModel): Observable<RoleViewModel> {
    let url: string = `${this.controller}add`;
    return this.postData<RoleViewModel>(url, model);
  }

  update(model: RoleViewModel): Observable<RoleViewModel> {
    let url: string = `${this.controller}update`;
    return this.postData<RoleViewModel>(url, model);
  }

  getLookupOrganizationPaged(
    model: OrganizationlookupSearchModel | null
  ): Observable<GenericResultModel<LookupModel[]>> {
    let url: string = `${this.baseUrl}/Organization/SearchLookup`;
    return this.postData<GenericResultModel<LookupModel[]>>(url, model);
  }

  GetAllPermissionsGroups(): Observable<LookupModel[]> {
    let url: string = `${this.baseUrl}/Permissions/get-all-permissions-Groups`;
    return this.getData<LookupModel[]>(url);
  }

  GetPermissionByGroupId(id: any): Observable<LookupModel[]> {
    let url: string = `${this.baseUrl}/Permissions/get-all-permissions-By-Group/${id}`;
    return this.getData<LookupModel[]>(url);
  }

  GetRolePermissionById(id: any): Observable<any> {
    let url: string = `${this.controller}get-role-permissions/${id}`;
    return this.getData<any>(url);
  }

  getById(id: any): Observable<RoleViewModel> {
    let url: string = `${this.controller}get/${id}`;
    return this.getData<RoleViewModel>(url);
  }

  getDetailsById(id: any): Observable<RoleDetailModel> {
    let url: string = `${this.controller}details/${id}`;
    return this.getData<RoleDetailModel>(url);
  }

  delete(id: any): Observable<any> {
    let url: string = `${this.controller}delete/${id}`;
    return this.postData<any>(url, null);
  }

  GetRolePermission(id: any): Observable<RolePermissionListViewModel> {
    let url: string = `${this.controller}get-role-permissions/${id}`;
    return this.getData<RolePermissionListViewModel>(url);
  }

  GetLookup():Observable<any>
  {
     
    let url: string = `${this.controller}GetLookup`;
    return this.getData<LookupModel[]>(url);
  }
}
