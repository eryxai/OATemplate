import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { PermissionEnum } from '../enum/permission-enum';
import { NavItemModel } from '../models/nav-item-model';
import { Location } from '@angular/common';
import { Router, NavigationEnd } from '@angular/router';
@Injectable({
  providedIn: 'root',
})
export class NavigationService {
  private navList: NavItemModel[] = [];
  private history: string[] = [];

  constructor(
    private router: Router,
    private location: Location
  ) {
    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        this.history.push(event.urlAfterRedirects);
      }
    });
  }
  back(): void {
    this.history.pop();
    if (this.history.length > 0) {
      this.location.back();
    } else {
      this.router.navigateByUrl('/');
    }
  }
  getNavItems(): Observable<NavItemModel[]> {
    if (this.navList.length === 0) {
      this.loadNavItems();
    }
    return of(this.navList);
  }

  private loadNavItems(): void {
    this.navList.push(
      {
        order: 1,
        name: 'menu.dashboard',
        title: 'menu.dashboard',
        icon: '',
        img: '../../../../../../assets/media/icons/dashboard.svg',
        cssClass: 'active',
        link: '/dashboard/admin',
        childs: [],
      },

      

      {
        order: 5,
        name: 'menu.usersManagement',
        title: 'menu.usersManagement',
        icon: '',
        img: '../../../../../../assets/media/sidebar/users.svg',
        cssClass: '',
        childs: [
          {
            order: 1,
            link: `/users/list`,
            name: 'menu.usersList',
            title: 'menu.usersList',
            icon: '',
            img: '../../../../../../assets/media/sidebar/users.svg',
            cssClass: '',
            childs: [],
            data: { permissionCodes: [+PermissionEnum.UserList] },
          },
          {
            order: 2,
            link: `/roles/list`,
            name: 'menu.rolesList',
            title: 'menu.rolesList',
            icon: '',
            img: '../../../../../../assets/media/sidebar/roles.svg',
            cssClass: '',
            childs: [],
            data: { permissionCodes: [+PermissionEnum.RoleList] },
          },
        ],
      },
      {
        order: 6,
        name: 'menu.Lookups',
        title: 'menu.Lookups',
        icon: '',
        img: '../../../../../../assets/media/sidebar/lines.svg',
        cssClass: '',
        childs: [

           {
            order: 1,
            link: `/experience/list`,
            name: 'menu.experienceList',
            title: 'menu.experienceList',
            icon: '',
            img: '../../../../../../assets/media/sidebar/lock.svg',
            cssClass: '',
            childs: [],
            data: { permissionCodes: [+PermissionEnum.ExperienceList] },
          },

        ],
      }
    );
  }
}
