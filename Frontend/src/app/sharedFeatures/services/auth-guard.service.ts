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
import { CurrentUserService } from './current-user.service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private currentUserService: CurrentUserService
  ) { }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> | Promise<boolean> | boolean {
    return  true;
    /*
    // 
    let userString = localStorage.getItem(this.currentUser);
    if (userString == null) {
      this.currentUserService.logOut();
    } else {
      if (userString != null) {
        this.User = JSON.parse(userString);
        if (!this.User?.isSuperAdmin) {
          let permissions: [] = [];
          if (this.User) {
            let token = jwt_decode.jwtDecode(this.User.access_token) as any;
            if (token) {
              permissions = token.permissions.split(',');
            }
          }

          if (next.data['permissionCodes']) {
            if (this.User && permissions && next.data['permissionCodes']) {
              let isGrant: boolean = false;
              next.data['permissionCodes'].forEach((element: any) => {
                let permissionItem = permissions.find(x => x == element);
                if (permissionItem) {
                  isGrant = true;
                }
              });
              if (isGrant) {
                return true;
              } else {
                this.router.navigate(['/error/page-not-authorized']);
              }
            } else {
              this.currentUserService.logOut();
            }
          } else {
            return true;
          }
        } else {
          return true;
        }
      } else {
        this.currentUserService.logOut();
      }
    }
    return false;*/
  }

  showActivate(data: any): Observable<boolean> | boolean {
    return true;
    /*
    let userString = localStorage.getItem(this.currentUser);

    if (userString == null || userString == 'null') {
      this.currentUserService.logOut();
    } else {
      if (userString != null) {
        this.User = JSON.parse(userString);
        if (!this.User?.isSuperAdmin) {
          let permissions: [] = [];

          if (this.User) {
            let token = jwt_decode.jwtDecode(this.User.access_token) as any;
            if (token) {
              permissions = token.permissions.split(',');
            }
          }

          if (this.User && permissions && data?.permissionCodes) {
            let isGrant: boolean = false;
            if (data?.permissionCodes) {
              data?.permissionCodes.forEach((element: string) => {
                let permissionItem = permissions.find(x => x == element);
                if (permissionItem) {
                  isGrant = true;
                }
              });
              if (isGrant) {
                return true;
              } else {
                return false;
              }
            } else {
              return false;
            }
          } else {
            return false;
          }
        } else {
          return true;
        }
      } else {
        return false;
      }
    }
    return false;*/
  }

  HasPermission(permissionCode: number[]): Observable<boolean> | boolean {
    return true;
    /*
    let userString = localStorage.getItem(this.currentUser);
    if (userString != null) {
      this.User = JSON.parse(userString);
      if (!this.User?.isSuperAdmin) {
        let permissions: [] = [];

        if (this.User) {
          let token = jwt_decode.jwtDecode(this.User.access_token) as any;
          if (token) {
            permissions = token.permissions.split(',');
          }
        }

        if (this.User && permissions && permissionCode) {
          let x = permissions.find(x => x == permissionCode);
          let isGrant: boolean = false;
          permissionCode.forEach(element => {
            let permissionItem = permissions.find(x => x == +element);
            if (permissionItem) {
              isGrant = true;
            }
          });
          if (isGrant) {
            return true;
          } else {
            return false;
          }
        } else {
          return false;
        }
      } else {
        return true;
      }
    } else {
      return false;
    }
    return false;*/
  }
  hasNotPermission(permissionCode: []): Observable<boolean> | boolean {
    return true;
    /*
    let userString = localStorage.getItem(this.currentUser);
    if (userString != null) {
      this.User = JSON.parse(userString);
      if (!this.User?.isSuperAdmin) {
        let permissions: [] = [];

        if (this.User) {
          let token = jwt_decode.jwtDecode(this.User.access_token) as any;
          if (token) {
            permissions = token.permissions.split(',');
          }
        }

        if (this.User && permissions && permissionCode) {
          let isGrant: boolean = false;
          permissionCode.forEach(element => {
            let permissionItem = permissions.find(x => x == +element);
            if (permissionItem) {
              isGrant = true;
            }
          });
          if (isGrant) {
            return false;
          } else {
            return true;
          }
        } else {
          return true;
        }
      } else {
        return false;
      }
    } else {
      return true;
    }*/
  }

  private currentUser: string = 'currentUser';
  User: UserLoggedIn | undefined;
}
