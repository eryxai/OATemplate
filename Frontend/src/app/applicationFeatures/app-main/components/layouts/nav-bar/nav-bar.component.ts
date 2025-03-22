import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { LanguageService } from 'src/app/sharedFeatures/services/language';
import { UserService } from 'src/app/modules/users/services/user.service';
import { UserLoggedIn } from 'src/app/sharedFeatures/models/user-login.model';
import { CurrentUserService } from '../../../../../sharedFeatures/services/current-user.service';

import { NotificationService } from 'src/app/sharedFeatures/services/notification.service';
import { EventEmitterService } from 'src/app/sharedFeatures/services/EventEmitterService';
import { LogoSvg } from 'src/assets/media/svg-code/logo';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss'],
})
export class NavBarComponent implements OnInit {
  lang: string = 'ar';
  currentUser: UserLoggedIn | null = null;
  domainUrl: string = this._UsersService.domainUrl;
  currentDate: Date = new Date();
  logo = LogoSvg;

  /*   languagesList = [
    {
      label: 'العربية',
      command: () => {
        this.changeLang('ar');
      },
    },
    {
      label: 'English',
      command: () => {
        this.changeLang('en');
      },
    },
  ]; */
  constructor(
    private currentUserService: CurrentUserService,
    private router: Router,

    private notificationService: NotificationService,
    private _eventEmitterService: EventEmitterService,
    private _UsersService: UserService,
    private translateService: TranslateService,
    public languageService: LanguageService
  ) {
    this._eventEmitterService.onReloadNotifications.subscribe(res => {
      // this.getCountUnSeenNotifications();
    });
  }

  ngOnInit(): void {
    const Logo = document.getElementById('Logo');

    if (Logo) Logo.innerHTML = this.logo;

    this.lang = this.translateService.currentLang;
    this.currentUser = this.currentUserService.getCurrentUser();

    if (this.currentUser == null || this.currentUser == undefined) {
      this.router.navigate(['/login']);
    }
  }
  
  onScrollDown(s: any) {
    if (
      this.notifications.collection!.length <
      this.notifications!.pagination!.totalCount!
    ) {
      this.pageIndex = this.pageIndex + 1;
      // this.getNotificationsByUserId();
    }
  }

  goToLink(url: string) {
    if (url != null) {
      if (url?.includes('http')) {
        window.open(url, '_blank');
      } else {
        this.router.navigate([url]);
      }
    }
  }

  userLogout() {
    this.currentUserService.logOut();
    this.router.navigate(['/login']);
  }
  viewUser() {
    let url = '/users/view/' + this.currentUser?.id + '/1';
    this.router.navigate([url]);
  }
  
  changeLang(lang: string) {
    this.translateService.use(lang);
    localStorage.setItem('currentLang', lang);

    this.languageService.setLanguage(lang);
  }
  isOpenMessage: boolean = false;

  UnSeenNotifications: number = 0;
  pageIndex = 0;
  pageSize = 10;
  notifications: any;
}
