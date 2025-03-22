import { Injectable } from '@angular/core';
import {
  CanActivate,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  Router,
  ActivatedRoute,
} from '@angular/router';
import { Observable } from 'rxjs';
import * as jwt_decode from 'jwt-decode';
import { UserLoggedIn } from '../models/user-login.model';

@Injectable({
  providedIn: 'root',
})
export class CurrentUserService {
  constructor(
    private router: Router,
    private route: ActivatedRoute
  ) {}

  isLoggedIn(): boolean {
    let user = this.getCurrentUser();
    return user != null;
  }

  getCurrentUser(): UserLoggedIn | null {
    let item = localStorage.getItem(this.currentUser);

    let user: UserLoggedIn = item != null ? JSON.parse(item) : null;
    return user;
  }
  getCurrentUserIdString(): string | null {
    let result: string;
    let user = this.getCurrentUser();
    if (user) {
      let token = jwt_decode.jwtDecode(user.access_token) as any;
      if (token) {
        return token.unique_name;
      }
    }
    return null;
  }

  logOut(showAuthError: boolean = false) {
    localStorage.removeItem(this.currentUser);
    if (showAuthError) {
      // this.notificationService.showErrorTranslated('error.shared.operationFailed','');
    }
    this.router.navigate(['login']);
  }

  getToken(): any {
    let user = this.getCurrentUser();
    if (user) return user.access_token;
  }

  private currentUser: string = 'currentUser';
  //User: UserLoggedIn;
}
