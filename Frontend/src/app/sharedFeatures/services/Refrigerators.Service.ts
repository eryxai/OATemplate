import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseHttpServiceService } from './base-http-service.service';
import { RefrigeratorLookupViewModel } from '../models/generic-refrigeratorLookupModel';
import { Observable } from 'rxjs';
import { LookupModel } from '../models/lookup-model';
@Injectable({
  providedIn: 'root',
})
export class RefrigeratorsService extends BaseHttpServiceService {
  getRefrigeratorsLookup(): Observable<RefrigeratorLookupViewModel[]> {
    let url: string = `${this.baseUrl}/refrigerator/GetLookup`;
    return this.getData<RefrigeratorLookupViewModel[]>(url);
  }

  getItemTypesLookup(): Observable<LookupModel[]> {
    let url: string = `${this.baseUrl}/item-type/GetLookup`;
    return this.getData<LookupModel[]>(url);
  }
  getItemCategoriesLookup(): Observable<LookupModel[]> {
    let url: string = `${this.baseUrl}/item-category/GetItemCategoriesLookup`;
    return this.getData<LookupModel[]>(url);
  }
}
