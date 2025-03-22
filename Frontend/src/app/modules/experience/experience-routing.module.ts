import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/sharedFeatures/services/auth-guard.service';
import { PermissionEnum } from 'src/app/sharedFeatures/enum/permission-enum';
import { ExperienceListComponent } from './components/list/list.component';
import { ExperienceAddEditComponent } from './components/add-edit/add-edit.component';
import { ExperienceViewComponent } from './components/view/view.component';

const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: '/experience/list' },
  {
    path: 'list',
    component: ExperienceListComponent,
    data: { permissionCodes: [+PermissionEnum.ExperienceList] },
    canActivate: [AuthGuard],
  },
  {
    path: 'add',
    component: ExperienceAddEditComponent,
    data: { permissionCodes: [+PermissionEnum.ExperienceAdd] },
    canActivate: [AuthGuard],
  },
  {
    path: 'edit/:editId',
    component: ExperienceAddEditComponent,
    data: { permissionCodes: [+PermissionEnum.ExperienceEdit] },
    canActivate: [AuthGuard],
  },
  {
    path: 'view/:id',
    component: ExperienceViewComponent,
    data: { permissionCodes: [+PermissionEnum.ExperienceView] },
    canActivate: [AuthGuard],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ExperienceRoutingModule {}
