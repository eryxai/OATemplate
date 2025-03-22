import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TranslateService } from '@ngx-translate/core';
import { BaseHttpServiceService } from 'src/app/sharedFeatures/services/base-http-service.service';
import { CurrentUserService } from 'src/app/sharedFeatures/services/current-user.service';
import { Observable } from 'rxjs';
import { DropdownItem } from '../models/Dropdown-item';
import { AdvertisingBoardFilterModel } from '../../modules/advertisement-creation/models/AdvertisingBoardFilterModel';

@Injectable({
  providedIn: 'root',
})
export class LookUpService extends BaseHttpServiceService {
  constructor(
    http: HttpClient,
    translateService: TranslateService,
    currentUserService: CurrentUserService
  ) {
    super(http, translateService, currentUserService);
  }

  getGovernorate(): Observable<DropdownItem[]> {
    let url: string = `${this.baseUrl}/Governrate/GetLookUp`;
    return this.getData<DropdownItem[]>(url);
  }
  getCity(governorateId: any): Observable<DropdownItem[]> {
    let url: string = `${this.baseUrl}/City/GetLookUp/${governorateId}`;
    return this.getData<DropdownItem[]>(url);
  }
  getDistrict(cityID: any): Observable<DropdownItem[]> {
    let url: string = `${this.baseUrl}/District/GetLookUp/${cityID}`;
    return this.getData<DropdownItem[]>(url);
  }
  getRegion(districtID: any): Observable<DropdownItem[]> {
    let url: string = `${this.baseUrl}/Region/GetLookUp/${districtID}`;
    return this.getData<DropdownItem[]>(url);
  }
  getStreets(regionID: any): Observable<DropdownItem[]> {
    let url: string = `${this.baseUrl}/Street/GetLookUp/${regionID}`;
    return this.getData<DropdownItem[]>(url);
  }

  getAdvertisingSize(): Observable<DropdownItem[]> {
    let url: string = `${this.baseUrl}/AdvertisingSize/GetLookUp`;
    return this.getData<DropdownItem[]>(url);
  }
  getAdvertisingAvailableSize(
    locationId: number,
    categoryId: number,
    typeId: number
  ): Observable<DropdownItem[]> {
    let url: string = `${this.baseUrl}/AdvertisingSize/GetAvailableLookUp/${locationId}/${categoryId}/${typeId}`;
    return this.getData<DropdownItem[]>(url);
  }
  getAdvertisingType(): Observable<DropdownItem[]> {
    let url: string = `${this.baseUrl}/AdvertisingType/GetLookUp`;
    return this.getData<DropdownItem[]>(url);
  }
  getAdvertisingAvailableType(
    locationId: number,
    categoryId: number
  ): Observable<DropdownItem[]> {
    let url: string = `${this.baseUrl}/AdvertisingType/GetAvailableLookUp/${locationId}/${categoryId}`;
    return this.getData<DropdownItem[]>(url);
  }
  getBillboardCategory(): Observable<DropdownItem[]> {
    let url: string = `${this.baseUrl}/Category/GetLookUp`;
    return this.getData<DropdownItem[]>(url);
  }
  getBillboardAvailableCategory(
    locationId: number
  ): Observable<DropdownItem[]> {
    let url: string = `${this.baseUrl}/Category/GetAvailableLookUp/${locationId}`;
    return this.getData<DropdownItem[]>(url);
  }
  getPropertyTypes(): Observable<DropdownItem[]> {
    let url: string = `${this.baseUrl}/TypesOfProperty/GetLookUp`;
    return this.getData<DropdownItem[]>(url);
  }
  getLocations(isDeleted?: any): Observable<DropdownItem[]> {
    let url: string = `${this.baseUrl}/Location/GetLookUp`;
    if (isDeleted !== undefined) {
      url += `?isDeleted=${isDeleted}`;
    }
    return this.getData<DropdownItem[]>(url);
  }

  getAvailableLookUp(): Observable<DropdownItem[]> {
    let url: string = `${this.baseUrl}/Location/GetAvailableLookUp`;
    return this.getData<DropdownItem[]>(url);
  }

  getAdvertisingPeriod(): Observable<DropdownItem[]> {
    let url: string = `${this.baseUrl}/AdvertisingPeriod/GetLookUp`;
    return this.getData<DropdownItem[]>(url);
  }
  getAdvertisingBoard(
    advertisingBoardFilterModel: AdvertisingBoardFilterModel
  ): Observable<DropdownItem[]> {
    let url: string = `${this.baseUrl}/AdvertisingBoard/GetLookUp`;
    return this.postData<DropdownItem[]>(url, advertisingBoardFilterModel);
  }
  getCompanies(): Observable<DropdownItem[]> {
    let url: string = `${this.baseUrl}/Companies/GetLookUp`;
    return this.getData<DropdownItem[]>(url);
  }
  getLicense(): Observable<DropdownItem[]> {
    let url: string = `${this.baseUrl}/LicenseTypes/GetLookUp`;
    return this.getData<DropdownItem[]>(url);
  }
  getAvailableLicense(): Observable<any[]> {
    let url: string = `${this.baseUrl}/License/GetAvailableLookUp`;
    return this.getData<any[]>(url);
  }
}
