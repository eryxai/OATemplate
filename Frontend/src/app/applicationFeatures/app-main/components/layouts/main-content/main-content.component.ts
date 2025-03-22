import { Component, OnInit } from '@angular/core';
import {
  NavigationCancel,
  NavigationEnd,
  NavigationError,
  NavigationStart,
  Router,
} from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { UserLoggedIn } from 'src/app/sharedFeatures/models/user-login.model';
import { CurrentUserService } from 'src/app/sharedFeatures/services/current-user.service';
import { NavigationService } from 'src/app/sharedFeatures/services/navigation.service';

@Component({
  selector: 'app-main-content',
  templateUrl: './main-content.component.html',
  styleUrls: ['./main-content.component.scss'],
})
export class MainContentComponent implements OnInit {
  currentUser: UserLoggedIn | null = null;

  constructor(
    private translateService: TranslateService,
    private router: Router,
    private toastrService: ToastrService,
    private navigationService: NavigationService,
    private currentUserService: CurrentUserService
  ) {}

  ngOnInit(): void {
    this.subscribeOnRouterEvents();
    this.setLangChangeSubscriber();
    // 
    this.currentUser = this.currentUserService.getCurrentUser();
  }
  setLangChangeSubscriber(): void {
    this.translateService.onLangChange.subscribe((lang: any) => {
      if (lang.lang == 'ar') {
        this.directionRTL = true;
        this.toastrService.toastrConfig.positionClass = 'toast-bottom-left';
      } else {
        this.directionRTL = false;
        this.toastrService.toastrConfig.positionClass = 'toast-bottom-right';
      }
    });
  }
  subscribeOnRouterEvents(): void {
    this.router.events.subscribe(event => {
      if (event instanceof NavigationStart) {
        this.isNavigating = true;
      }

      if (event instanceof NavigationEnd) {
        this.isNavigating = false;
      }

      if (event instanceof NavigationCancel) {
        this.isNavigating = false;
      }

      if (event instanceof NavigationError) {
        this.isNavigating = false;

        if (event.error.name == 'ChunkLoadError') window.location.reload();
        else {
          if (this.currentUser) {
            let url = [`/dashboard`];
            this.router.navigate(url);
          } else {
            this.router.navigate(['/login']);
          }
        }
      }
    });
  }

  changeLang(lang: string) {
    this.translateService.use(lang);
  }

  toggleMenu() {
    this.isMenuCollapsed = !this.isMenuCollapsed;
  }

  isNavigating: boolean = false;
  title = 'SmartMentor';
  directionRTL: boolean = true;
  isMenuCollapsed: boolean = false;
}
