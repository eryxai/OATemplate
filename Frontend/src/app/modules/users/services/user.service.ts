import { ChangePasswordViewModel } from './../models/change-password-view-model';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Observable } from 'rxjs';
import { UserLoggedIn } from './../../../sharedFeatures/models/user-login.model';
import { BaseHttpServiceService } from './../../../sharedFeatures/services/base-http-service.service';
import { CurrentUserService } from './../../../sharedFeatures/services/current-user.service';
import { Login } from '../models/login';
import { GenericResultModel } from 'src/app/sharedFeatures/models/generic-result.model';
import { LookupModel } from 'src/app/sharedFeatures/models/lookup-model';
import {
  DepartmentlookupSearchModel,
  OrganizationlookupSearchModel,
  RolelookupSearchModel,
} from '../models/lookup-search-model';
import { UserViewModel } from '../models/user-view-model';
import { UserDetailModel } from '../models/user-details-model';
import { UserSearchViewModel } from '../models/user-search-view-model.model';
import { UserLightViewModel } from '../models/user-light-view-model.model';
import { UserDepartmentListViewModel } from '../models/user-department-list.model';

@Injectable({
  providedIn: 'root',
})
export class UserService extends BaseHttpServiceService {
 // private controller: string = `${this.baseUrl}/Users/`;
  constructor(
    http: HttpClient,
    translateService: TranslateService,
    currentUserService: CurrentUserService
  ) {
    super(http, translateService, currentUserService);
  }
  search( model: UserSearchViewModel): Observable<GenericResultModel<UserLightViewModel[]>>
   {
     
    let url: string = `${this.baseUrl}/Users/search`;
    return this.postData<GenericResultModel<UserLightViewModel[]>>(url, model);
  }
  add(model: UserViewModel): Observable<UserViewModel> {
    
    let url: string = `${this.baseUrl}/Users/add`;
    return this.postData<UserViewModel>(url, model);
  }

  update(model: UserViewModel): Observable<UserViewModel> {
    console.log(model);
    let url: string = `${this.baseUrl}/Users/update`;
    return this.postData<UserViewModel>(url, model);
  }

  changePassword(model: ChangePasswordViewModel): Observable<any> {
    let url: string = `${this.baseUrl}/Users/change-password`;
    return this.postData<ChangePasswordViewModel>(url, model);
  }

  // getLookupPaged(pageIndex?: number, pageSize?: number, sort: any = null): Observable<GenericResultModel<LookupModel[]>> {
  //     let url: string = `${this.baseUrl}/api/Organization/`;
  //     return this.getData<GenericResultModel<LookupModel[]>>(url);
  // }
  getLookupOrganizationPaged(
    model: OrganizationlookupSearchModel | null
  ): Observable<GenericResultModel<LookupModel[]>> {
    let url: string = `${this.baseUrl}/Organization/SearchLookup`;
    return this.postData<GenericResultModel<LookupModel[]>>(url, model);
  }
  getLookupDepartmentPaged(
    model: DepartmentlookupSearchModel | null
  ): Observable<GenericResultModel<LookupModel[]>> {
    let url: string = `${this.baseUrl}/Department/SearchLookup`;
    return this.postData<GenericResultModel<LookupModel[]>>(url, model);
  }

  getLookupRolePaged(
    model: RolelookupSearchModel | null
  ): Observable<GenericResultModel<LookupModel[]>> {
    let url: string = `${this.baseUrl}/Roles/SearchLookup`;
    return this.postData<GenericResultModel<LookupModel[]>>(url, model);
  }

  getById(id: any): Observable<UserViewModel> {
    let url: string = `${this.baseUrl}/Users/get/${id}`;
    return this.getData<UserViewModel>(url);
  }

  getDetailsById(id: any): Observable<UserDetailModel> {
    let url: string = `${this.baseUrl}/Users/details/${id}`;
    return this.getData<UserDetailModel>(url);
  }
  delete(id: any): Observable<any> {
    let url: string = `${this.baseUrl}/Users/delete/${id}`;
    return this.postData<any>(url, null);
  }
  GetAllDepartmentOrginzations(): Observable<LookupModel[]> {
    let url: string = `${this.baseUrl}/Department/get-all-Department-Orginzations`;
    return this.getData<LookupModel[]>(url);
  }

  GetDepartmentByOrginzationsId(id: any): Observable<LookupModel[]> {
    let url: string = `${this.baseUrl}/Department/get-all-Department-By-Orginzation/${id}`;
    return this.getData<LookupModel[]>(url);
  }
  GetUserDepartments(id: any): Observable<UserDepartmentListViewModel> {
    let url: string = `${this.baseUrl}/Users/get-user-departments/${id}`;
    return this.getData<UserDepartmentListViewModel>(url);
  }
 IsHidenPassword():Observable<boolean> {
    let url: string = `${this.baseUrl}/Users/IsHidenPassword`;
    return this.getData<boolean>(url);
  }

  getLookUp(): Observable<any> {
    let url: string = `${this.baseUrl}/Users/GetLookup`;
    return this.getData<any>(url);
  }

}
