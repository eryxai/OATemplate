import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseHttpServiceService } from './base-http-service.service';
import { Observable } from 'rxjs';
import { RoomLookupViewModel } from '../models/generic.roomLookupModel';
@Injectable({
    providedIn: 'root'
  })
  export class RoomService extends BaseHttpServiceService
{
    getRoomLookup(id:any):Observable<RoomLookupViewModel[]>
    {
        let url: string = `${this.baseUrl}/room/GetRoomsLookupByRefrigeratorId/${id}`;
        return this.getData<RoomLookupViewModel[]>(url);
    }
}