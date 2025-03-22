import { AuthGuard } from 'src/app/sharedFeatures/services/auth-guard.service';
import { PermissionEnum } from 'src/app/sharedFeatures/enum/permission-enum';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RolesAddEditComponent } from './components/roles-add-edit/roles-add-edit.component';
import { RolesListComponent } from './components/roles-list/roles-list.component';
import { RolesViewComponent } from './components/roles-view/roles-view.component';
import { NewGridComponent } from './components/new-grid/new-grid.component';

const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: '/roles/list' 
  },
  {
    path: 'list',
    component: RolesListComponent,
    data: { permissionCodes: [+PermissionEnum.RoleList] },
    canActivate: [AuthGuard],
  },
  {
    path: 'add',
    component: RolesAddEditComponent,
    data: { permissionCodes: [+PermissionEnum.RoleAdd] },
    canActivate: [AuthGuard],
  },
  {
    path: 'edit/:editId',
    component: RolesAddEditComponent,
    data: { permissionCodes: [+PermissionEnum.RoleEdit] },
    canActivate: [AuthGuard],
  },
  {
    path: 'view/:id',
    component: RolesViewComponent,
    data: { permissionCodes: [+PermissionEnum.RoleView] },
    canActivate: [AuthGuard],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class RolesRoutingModule {}
