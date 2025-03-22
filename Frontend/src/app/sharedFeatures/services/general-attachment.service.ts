import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Observable } from 'rxjs';
import { GeneralAttachmentViewModel } from '../models/general-attachment-view-model.model';
import { BaseHttpServiceService } from './base-http-service.service';
import { CurrentUserService } from './current-user.service';

@Injectable({
  providedIn: 'root',
})
export class GeneralAttachmentService extends BaseHttpServiceService {
  /**
   * *************[Fields]
   */
  private controller: string = `${this.baseUrl}/GeneralAttachment`;

  /**
   * *************[Services]
   * @param http
   * @param translateService
   * @param currentUserService
   */
  constructor(
    http: HttpClient,
    translateService: TranslateService,
    currentUserService: CurrentUserService
  ) {
    super(http, translateService, currentUserService);
  }

  //#region ******[APIs]
  createOrEdit(
    model: GeneralAttachmentViewModel
  ): Observable<any> {
    let url: string = `${this.controller}/CreateOrEdit`;
    return this.postData<any>(url, model);
  }
  //#endregion
}
