import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/sharedFeatures/services/auth-guard.service';
import { AdminDashboardComponent } from './components/admin/admin-dashboard/admin-dashboard.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: AdminDashboardComponent,
    data: {},
    canActivate: [AuthGuard],
  },

  {
    path: 'admin',
    component: AdminDashboardComponent,
    data: {},
    canActivate: [AuthGuard],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class DashboardRoutingModule {}
