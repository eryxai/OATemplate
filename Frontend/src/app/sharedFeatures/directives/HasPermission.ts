import {
  Directive,
  ElementRef,
  Input,
  TemplateRef,
  ViewContainerRef,
} from '@angular/core';
import { AuthGuard } from '../services/auth-guard.service';

@Directive({
  selector: '[hasPermission]',
})
export class HasPermissionDirective {
  permission: any;

  constructor(
    private el: ElementRef,
    private templateRef: TemplateRef<any>,
    private viewContainer: ViewContainerRef,
    private adminGuard: AuthGuard
  ) {}

  ngOnInit() {}

  private updateView(permission: number[]) {
    var hasPermission = this.adminGuard.HasPermission(permission);
    return hasPermission;
  }

  @Input() set hasPermission(val: number[]) {
    if (this.updateView(val) == true) {
      this.viewContainer.createEmbeddedView(this.templateRef);
    } else {
      this.viewContainer.clear();
    }
  }
}
