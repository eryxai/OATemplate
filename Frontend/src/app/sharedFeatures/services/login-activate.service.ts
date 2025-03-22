import { Injectable } from '@angular/core';
import {
  ActivatedRoute,
  ActivatedRouteSnapshot,
  CanActivate,
  Router,
  RouterStateSnapshot,
  UrlTree,
} from '@angular/router';
import { CurrentUserService } from './current-user.service';
import { NotificationService } from './notification.service';
import { Observable } from 'rxjs';
import { UserLoggedIn } from '../models/user-login.model';

@Injectable({
  providedIn: 'root',
})
export class LoginActivate implements CanActivate {
  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private currentUserService: CurrentUserService,
    private notificationService: NotificationService
  ) {}

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> | Promise<boolean> | boolean {
    // return true;

    let userString = localStorage.getItem(this.currentUser);

    if (userString == null || userString == 'null') {
      //  this.currentUserService.logOut();

      return true;
    } else {
      this.router.navigate(['dashboard']);
      return false;
    }
    return true;
  }

  private currentUser: string = 'currentUser';
}
