import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class MapService {
  constructor(private http: HttpClient) {}

  getAddress(latlng: any): Observable<any> {
    const url = `https://nominatim.openstreetmap.org/reverse?format=json&lat=${
      latlng.lat
    }&lon=${latlng.lng}&accept-language=${'en'}`;
    return new Observable(observe => {
      this.http.get(url).subscribe(
        (res: any) => {
          observe.next(res.display_name);
        },
        error => {
          observe.error(error);
        }
      );
    });
  }
}
