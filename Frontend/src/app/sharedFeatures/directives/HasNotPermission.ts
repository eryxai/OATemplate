import {
  Directive,
  ElementRef,
  Input,
  TemplateRef,
  ViewContainerRef,
} from '@angular/core';
import { AuthGuard } from '../services/auth-guard.service';

@Directive({
  selector: '[hasNotPermission]',
})
export class HasNotPermissionDirective {
  permission: any;

  constructor(
    private el: ElementRef,
    private templateRef: TemplateRef<any>,
    private viewContainer: ViewContainerRef,
    private adminGuard: AuthGuard
  ) {}

  ngOnInit() {}

  private updateView(permission: number[]) {
    var hasNotPermission = !this.adminGuard.HasPermission(permission);
    return hasNotPermission;
  }

  @Input() set hasNotPermission(val: number[]) {
    if (this.updateView(val) == true) {
      this.viewContainer.createEmbeddedView(this.templateRef);
    } else {
      this.viewContainer.clear();
    }
  }
}
