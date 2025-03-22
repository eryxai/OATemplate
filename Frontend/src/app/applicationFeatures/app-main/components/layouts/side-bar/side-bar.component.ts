import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { NavItemModel } from 'src/app/sharedFeatures/models/nav-item-model';
import { AuthGuard } from 'src/app/sharedFeatures/services/auth-guard.service';
import { CurrentUserService } from 'src/app/sharedFeatures/services/current-user.service';
import { NavigationService } from 'src/app/sharedFeatures/services/navigation.service';

@Component({
  selector: 'app-side-bar',
  templateUrl: './side-bar.component.html',
  styleUrls: ['./side-bar.component.scss'],
})
export class SideBarComponent implements OnInit {
  activeClass: boolean = true;
  navList: NavItemModel[] = [];
  currentUrl: string;

  subscriptions: Subscription[] = [];
  currentRoute: string;
  versionNo: string = 'Version 1.0.0';
  activeChildClass: boolean = true;
  activeChild2Class: boolean = true;
  constructor(
    private navigationService: NavigationService,
    public adminGuard: AuthGuard,
    private router: Router,
    private route: ActivatedRoute,
    private currentUserService: CurrentUserService
  ) {}

  ngOnInit(): void {
    this.openMenu();
    this.getNavItems();
    this.changeOfRoutes();
  }
  getNavItems() {
    this.navigationService.getNavItems().subscribe(res => {
      this.navList = res.sort(c => c.order);
    });
  }
  changeOfRoutes() {
    this.subscriptions.push(
      this.router.events.subscribe(ev => {
        if (ev instanceof NavigationEnd) {
          this.currentUrl = this.router.url;

          this.unActiveSubMenu();
          let navitem = this.navList.findIndex(a => {
            if (a.link) {
              if (a.link == ev.urlAfterRedirects) {
                this.currentRoute = ev.urlAfterRedirects;
                return true;
              } else if (a.link == '/' + ev.urlAfterRedirects.split('/')[1]) {
                this.currentRoute = '/' + ev.urlAfterRedirects.split('/')[1];
                return true;
              } else if (
                a.link ==
                '/' + ev.urlAfterRedirects.split('/')[1] + '/list'
              ) {
                this.currentRoute =
                  '/' + ev.urlAfterRedirects.split('/')[1] + '/list';
                return true;
              } else {
                return false;
              }
            } else {
              let z = a.childs.filter(x => {
                if (x.link == ev.urlAfterRedirects) {
                  this.currentRoute = ev.urlAfterRedirects;
                  return true;
                } else if (
                  a.link ==
                  '/' + ev.urlAfterRedirects.split('/')[1] + '/list'
                ) {
                  this.currentRoute =
                    '/' + ev.urlAfterRedirects.split('/')[1] + '/list';
                  return true;
                } else if (x.link == '/' + ev.urlAfterRedirects.split('/')[1]) {
                  this.currentRoute = '/' + ev.urlAfterRedirects.split('/')[1];
                  return true;
                } else {
                  return false;
                }
              });
              if (z.length) {
                if (z[0] != undefined) z[z.length - 1].activeClass = true;
                return true;
              }
            }
            return false;
          });
          if (this.navList[navitem] != undefined)
            this.navList[navitem].activeClass = true;
        }
      })
    );
  }
  showParentActivate(list: any[]): boolean {
    var hasAnyPermission = false;
    list.forEach(elements => {
      var hasPermission = this.adminGuard.showActivate(elements.data);
      if (hasPermission == true) hasAnyPermission = true;
    });
    return hasAnyPermission;
  }
  mouseenter() {
    this.activeClass = !this.activeClass;
    this.openMenu();
  }
  unActiveSubMenu() {
    //this.navList.filter(a=>a.childs.length!=0&& a.childs.filter(e=>e.link==this.router.url).length==0).filter(a => (a.activeClass = false));
    this.navList
      .filter(a => a.childs.length == 0 && a.link + '/list' != this.router.url)
      .filter(a => (a.activeClass = false));

    this.navList.filter(a =>
      a.childs
        .filter(e => e.link + '/list' != this.router.url)
        .filter(x => (x.activeClass = false))
    );

    this.navList
      .filter(
        a =>
          a.childs.length > 0 && a.childs.filter(e => e.activeClass).length > 0
      )
      .filter(a => !a.activeClass)
      .filter(a => (a.activeClass = true));
  }
  openMenu() {
    if (this.activeClass == true) {
      let header = document.getElementById('header');
      header?.classList.add('width-mod');
      let main = document.getElementById('main-wrapper');
      main?.classList.add('width-mod');
    } else {
      let header = document.getElementById('header');
      header?.classList.remove('width-mod');
      let element = document.getElementById('main-wrapper');
      element?.classList.remove('width-mod');
      this.navList.filter(a => (a.activeClass = false));
    }
  }
  onMenuItemContainerClick(item: any): void {
    this.navList.forEach(el => {
      el.activeClass = false;
      el.childs.forEach(el2 => {
        el2.activeClass = false;
        el2.childs.forEach(el3 => {
          el3.activeClass = false;
        });
      });
    });
    setTimeout(() => {
      if (!item.activeClass) item.activeClass = true;
    }, 0);
  }
  onMenuSubItemContainerClick(item: any, subItem: any): void {
    this.navList.forEach(el => {
      el.activeClass = false;
      el.childs.forEach(el2 => {
        el2.activeClass = false;
        el2.childs.forEach(el3 => {
          el3.activeClass = false;
        });
      });
    });
    setTimeout(() => {
      if (!item.activeClass) item.activeClass = true;

      if (!subItem.activeClass) subItem.activeClass = true;
    }, 0);
  }
  onMenuThirdItemContainerClick(item: any, subItem: any, thirdItem: any): void {
    this.navList.forEach(el => {
      el.activeClass = false;
      el.childs.forEach(el2 => {
        el2.activeClass = false;
        el2.childs.forEach(el3 => {
          el3.activeClass = false;
        });
      });
    });
    setTimeout(() => {
      if (!item.activeClass) item.activeClass = true;

      if (!subItem.activeClass) subItem.activeClass = true;

      if (!thirdItem.activeClass) thirdItem.activeClass = true;
    }, 0);
  }
  openChild2Menu() {
    if (this.activeChild2Class == true) {
      let header = document.getElementById('header');
      header?.classList.add('width-mod');
      let main = document.getElementById('main-wrapper');
      main?.classList.add('width-mod');
    } else {
      let header = document.getElementById('header');
      header?.classList.remove('width-mod');
      let element = document.getElementById('main-wrapper');
      element?.classList.remove('width-mod');
      this.navList.filter(a => (a.activeClass = false));
    }
  }

  userLogout() {
    this.currentUserService.logOut();
    this.router.navigate(['/login']);
  }
}
