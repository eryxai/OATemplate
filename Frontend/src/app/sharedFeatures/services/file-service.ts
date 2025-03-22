import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Observable, filter } from 'rxjs';
import { BaseFilter } from 'src/app/sharedFeatures/models/base-filter.model';
import { BaseHttpServiceService } from 'src/app/sharedFeatures/services/base-http-service.service';
import { CurrentUserService } from 'src/app/sharedFeatures/services/current-user.service';

@Injectable({
  providedIn: 'root',
})
export class FileService extends BaseHttpServiceService {
  constructor(
    http: HttpClient,
    translateService: TranslateService,
    currentUserService: CurrentUserService
  ) {
    super(http, translateService, currentUserService);
  }

  uploadLicenseFile(model: FormData): Observable<any> {
    let url: string = `${this.baseUrl}/License/upload-file`;
    return this.postDataUpload<any>(url, model);
  }

  uploadAdvertisingSerial(model: FormData): Observable<any> {
    let url: string = `${this.baseUrl}/Advertising/upload-file`;
    return this.postDataUpload<any>(url, model);  
  }
}
