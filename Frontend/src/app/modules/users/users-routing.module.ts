import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/sharedFeatures/services/auth-guard.service';
import { PermissionEnum } from 'src/app/sharedFeatures/enum/permission-enum';

import { LoginComponent } from './components/login/login.component';
import { ForgetPasswordComponent } from './components/forget-password/forget-password.component';
import { UsersAddEditComponent } from './components/users-add-edit/users-add-edit.component';
import { UsersListComponent } from './components/users-list/users-list.component';
import { UsersViewComponent } from './components/users-view/users-view.component';
import { ChangePasswordComponent } from './components/change-password/change-password.component';
import { CurrentUserService } from 'src/app/sharedFeatures/services/current-user.service';

const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: '/users/list' 
   
  },
  {
    path: 'list',
    component: UsersListComponent,
    data: { permissionCodes: [+PermissionEnum.UserList] },
    canActivate: [AuthGuard],
  },
  {
    path: 'add',
    component: UsersAddEditComponent,
    data: { permissionCodes: [+PermissionEnum.UserAdd] },
    canActivate: [AuthGuard],
  },
  {
    path: 'edit/:editId/:type',
    component: UsersAddEditComponent,
    data: { permissionCodes: [+PermissionEnum.UserEdit] },
    canActivate: [AuthGuard],
  },
  {
    path: 'view/:id',
    component: UsersViewComponent,
    data: { permissionCodes: [+PermissionEnum.UserView] },
    canActivate: [AuthGuard],
  },
  {
    path: 'change-password/:id/:type',
    component: ChangePasswordComponent,
    data: { permissionCodes: [+PermissionEnum.UserAdd] },
    canActivate: [AuthGuard],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class UsersRoutingModule {}
