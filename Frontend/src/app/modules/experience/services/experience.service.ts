import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Observable } from 'rxjs';
import { BaseFilter } from 'src/app/sharedFeatures/models/base-filter.model';
import { BaseHttpServiceService } from 'src/app/sharedFeatures/services/base-http-service.service';
import { CurrentUserService } from 'src/app/sharedFeatures/services/current-user.service';
import { AddEditExperienceViewModel } from '../models/AddEdit-experience.modal';
import { GenericResultModel } from 'src/app/sharedFeatures/models/generic-result.model';
import { ExperienceLightViewModel } from '../models/experience-light-view-model';
import { ExperienceViewModel } from '../models/experience-view-model';


@Injectable({
  providedIn: 'root',
})
export class ExperienceService extends BaseHttpServiceService {
  constructor(
    http: HttpClient,
    translateService: TranslateService,
    currentUserService: CurrentUserService
  ) {
    super(http, translateService, currentUserService);
  }

  getActive(flitterDto: BaseFilter): Observable<GenericResultModel<ExperienceLightViewModel[]>> {
    let url: string = `${this.baseUrl}/Experience/search`;
    return this.postData<any>(url, flitterDto);
  }

  AddExperience(model: ExperienceViewModel): Observable<boolean> {
    let url: string = `${this.baseUrl}/Experience/Add`;
    return this.postData<boolean>(url, model);
  }
  Update(model: ExperienceViewModel): Observable<boolean> {
    let url: string = `${this.baseUrl}/Experience/Update`;
    return this.postData<boolean>(url, model);
  }

  getByID(id: any): Observable<ExperienceViewModel> {
    let url: string = `${this.baseUrl}/Experience/get/${id}`;
    return this.getData<ExperienceViewModel>(url);
  }

  getLookUp(): Observable<any> {
    let url: string = `${this.baseUrl}/Experience/GetLookup`;
    return this.getData<any>(url);
  }
  delete(id: any): Observable<boolean> {
    let url: string = `${this.baseUrl}/Experience/delete/${id}`;
    return this.postData<any>(url, null);
  }
  ExportExcel(flitterDto: BaseFilter): Observable<any> {
    let url: string = `${this.baseUrl}/Experience/export-excel`;
    return this.exportData(url, flitterDto);
  }
  ExportPDF(flitterDto: BaseFilter): Observable<any> {
    let url: string = `${this.baseUrl}/Experience/export-pdf`;
    return this.exportData(url, flitterDto);
  }
}
