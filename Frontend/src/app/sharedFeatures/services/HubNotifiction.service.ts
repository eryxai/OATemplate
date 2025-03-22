import { environment } from './../../../environments/environment';
import { LanguageService } from './language';
import { NotificationReturn } from './../models/Notifiction.model';
import { NotificationService } from './notification.service';
import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { NotifyEntityTypeEnum } from '../enum/NotifyEntityTypeEnum';
import { EventEmitterService } from './EventEmitterService';
@Injectable({
  providedIn: 'root',
})
export class HubNotifictionService {
  public data!: NotificationReturn;
  private hubConnection!: signalR.HubConnection;
  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(environment.APIUrl + '/notifyTemplateHub', {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets,
      })
      .build();
    this.hubConnection
      .start()
      .then()
      .catch((err: string) => {});
  };
  constructor(
    public Notification: NotificationService,
    private _languageService: LanguageService,
    private _eventEmitterService: EventEmitterService
  ) {}
  public addDataSetReviewListener() {
    this.hubConnection.on('BroadcastMessage', data => {
      this.data = data;
      
      console.log(this.data);
      if (data.userId == this.userId) {
        console.log(this.data);
        if (this._languageService.getLanguage == 'en') {
          this.Notification.showInfo(this.data.dataEn, '');
        } else this.Notification.showInfo(this.data.dataAr, '');
      }

      switch (data.notifyEntityType) {
        case NotifyEntityTypeEnum.Wrong:
          break;

        case NotifyEntityTypeEnum.Notification:
          this._eventEmitterService.onReloadNotifications.emit();
          break;
      }
    });
  }

  public userId: number;
}
