import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UsersRoutingModule } from './users-routing.module';
import { UsersListComponent } from './components/users-list/users-list.component';
import { UsersAddEditComponent } from './components/users-add-edit/users-add-edit.component';
import { UsersViewComponent } from './components/users-view/users-view.component';
import { SharedComponentModule } from '../../applicationFeatures/shared-components/shared-components.module';
import { SharedCommonModuleModule } from 'src/app/applicationFeatures/shared-common-module/shared-common-module.module';
import { ChangePasswordComponent } from './components/change-password/change-password.component';

@NgModule({
  declarations: [
    UsersListComponent,
    UsersAddEditComponent,
    UsersViewComponent,
    ChangePasswordComponent,
  ],
  imports: [
    CommonModule,
    UsersRoutingModule,
    SharedComponentModule,
    SharedCommonModuleModule,
  ],
})
export class UsersModule {}
