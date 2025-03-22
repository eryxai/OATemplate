import {
  Directive,
  ElementRef,
  HostListener,
  Input,
  Renderer2,
} from '@angular/core';

@Directive({
  selector: '[toggleSideMenu]',
})
export class ToggleSideMenuDirective {
  @Input() onlyOpen: boolean = false; // Default to false
  @Input() sideMenuClass: string = 'active';
  @Input() sideMenuElement: string = '#sideBarMenu';
  @Input() mainContent: string = '.main-wrapper';

  constructor(
    private elementRef: ElementRef,
    private renderer: Renderer2
  ) {}

  @HostListener('click') onClick() {
    const sideMenu = document.getElementById('sideBarMenu');
    const header = document.getElementById('header');
    const main = document.getElementById('main-wrapper');
    if (this.onlyOpen && sideMenu?.classList.contains('new-active-mode')) {
      return;
    }
    if (this.onlyOpen) {
      // If onlyOpen is true, ensure the side menu is open
      this.renderer.addClass(sideMenu, this.sideMenuClass);
      header && this.renderer.addClass(header, 'width-mod');
      header && this.renderer.addClass(header, 'full-width');
      main && this.renderer.addClass(main, 'width-mod');
    } else {
      // Toggle the side menu and other elements
      if (sideMenu) {
        if (sideMenu.classList.contains(this.sideMenuClass)) {
          this.renderer.removeClass(sideMenu, this.sideMenuClass);
          header && this.renderer.removeClass(header, 'width-mod');
          header && this.renderer.removeClass(header, 'full-width');
          main && this.renderer.removeClass(main, 'width-mod');
        } else {
          this.renderer.addClass(sideMenu, this.sideMenuClass);
          header && this.renderer.addClass(header, 'width-mod');
          header && this.renderer.addClass(header, 'full-width');
          main && this.renderer.addClass(main, 'width-mod');
        }
      }
    }

    this.hideItem();
    this.newCollapse();
  }

  hideItem(): void {
    const hideCollapse = document.querySelectorAll(
      '.hide-collapse'
    ) as NodeListOf<HTMLElement>;
    hideCollapse.forEach(each => {
      if (!each.classList.contains('d-none')) {
        this.renderer.addClass(each, 'd-none');
      } else {
        this.renderer.removeClass(each, 'd-none');
      }
    });
  }

  newCollapse(): void {
    const sideMenu = document.getElementById('sideBarMenu');

    if (sideMenu) {
      if (sideMenu.classList.contains('new-active-mode')) {
        this.renderer.removeClass(sideMenu, 'new-active-mode');
      } else {
        this.renderer.addClass(sideMenu, 'new-active-mode');
      }
    }
  }
}
