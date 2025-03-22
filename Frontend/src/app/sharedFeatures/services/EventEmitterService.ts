import { Injectable, EventEmitter } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class EventEmitterService {
  public onReloadNotifications = new EventEmitter<boolean>();

  constructor() {}
}
