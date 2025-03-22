import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/sharedFeatures/services/auth-guard.service';

import { ForgetPasswordComponent } from 'src//app/modules/users/components/forget-password/forget-password.component';
import { LoginComponent } from 'src/app/modules/users/components/login/login.component';
import { LoginActivate } from '../../sharedFeatures/services/login-activate.service';
import { PageNotFoundComponent } from './components/layouts/Error/Components/page-not-found/page-not-found.component';

const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: '/login' },
  {
    path: 'dashboard',
    loadChildren: () =>
      import('src/app/modules/dashboard/dashboard.module').then(
        m => m.DashboardModule
      ),
  },
  { path: 'forget-password', component: ForgetPasswordComponent },
  { path: 'login', component: LoginComponent, canActivate: [LoginActivate] },
  {
    path: 'users',
    loadChildren: () =>
      import('src/app/modules/users/users.module').then(m => m.UsersModule),
  },

  {
    path: 'roles',
    loadChildren: () =>
      import('src/app/modules/roles/roles.module').then(m => m.RolesModule),
  },

  {
    path: 'experience',
    loadChildren: () =>
      import('src/app/modules/experience/experience.module').then(
        m => m.ExperienceModule
      ),
  },
  { path: '**', component: PageNotFoundComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
