import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { BaseHttpServiceService } from 'src/app/sharedFeatures/services/base-http-service.service';
import { CurrentUserService } from 'src/app/sharedFeatures/services/current-user.service';
import { PendingApprovalRequest } from '../models';
import { GenericResultModel } from 'src/app/sharedFeatures/models/generic-result.model';
import { map, Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class DashboardService extends BaseHttpServiceService {
  private controller: string = `/Dashboard`;
  constructor(
    http: HttpClient,
    translateService: TranslateService,
    currentUserService: CurrentUserService
  ) {
    super(http, translateService, currentUserService);
  }

  getPendingApprovalRequest(model: PendingApprovalRequest): Observable<any> {
    const url: string = `${this.baseUrl}/Customer/search`;
    return this.postData<GenericResultModel<any[]>>(url, model).pipe(
      map((response: GenericResultModel<any[]>) => {
        // To do make interface
        response.collection = [
          {
            id: 1,
            employee: 'Employee Name',
            risk: 'Low',
            approvalLine: 'Line Name',
            date: '2024-07-03T13:50:18.8079682',
          },
          {
            id: 1,
            employee: 'Employee Name',
            risk: 'High',
            approvalLine: 'Line Name',
            date: '2024-07-03T13:50:18.8079682',
          },
          {
            id: 1,
            employee: 'Employee Name',
            risk: 'Low',
            approvalLine: 'Line Name',
            date: '2024-07-03T13:50:18.8079682',
          },
          {
            id: 1,
            employee: 'Employee Name',
            risk: 'Low',
            approvalLine: 'Line Name',
            date: '2024-07-03T13:50:18.8079682',
          },
          {
            id: 1,
            employee: 'Employee Name',
            risk: 'High',
            approvalLine: 'Line Name',
            date: '2024-07-03T13:50:18.8079682',
          },
          {
            id: 1,
            employee: 'Employee Name',
            risk: 'Low',
            approvalLine: 'Line Name',
            date: '2024-07-03T13:50:18.8079682',
          },
          {
            id: 1,
            employee: 'Employee Name',
            risk: 'Low',
            approvalLine: 'Line Name',
            date: '2024-07-03T13:50:18.8079682',
          },
        ];
        return response;
      })
    );
  }

  deletePendingApprovals(id: any): Observable<any> {
    let url: string = `${this.controller}delete/${id}`;
    // return this.postData<any>(url, null);

    return of('');
  }

  getActivity(): Observable<any> {
    const url: string = `${this.baseUrl}/Activity/GetlatestActivities`;
    return this.getData<any[]>(url);
  }

  GetDashboardStatistics(): Observable<any> {
    const url: string = `${this.baseUrl}/Activity/GetDashboardStatistics`;
    return this.getData<any[]>(url);
  }
  GetMakerProductivity(search :any): Observable<any> {
    const url: string = `${this.baseUrl}/Activity/GetMakerProductivity`;
    return this.postData<any>(url,search);
  }
}
