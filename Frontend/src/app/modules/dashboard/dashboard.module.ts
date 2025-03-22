import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { SharedComponentModule } from 'src/app/applicationFeatures/shared-components/shared-components.module';
import { SharedCommonModuleModule } from './../../applicationFeatures/shared-common-module/shared-common-module.module';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { DashboardComponent } from './dashboard.component';
import { ChartModule } from 'primeng/chart';
import { ChartCardComponent } from './components/chart-card/chart-card.component';

import { AdminDashboardComponent } from './components/admin/admin-dashboard/admin-dashboard.component';


@NgModule({
  declarations: [
    DashboardComponent,
    ChartCardComponent,
    AdminDashboardComponent

  ],
  imports: [
    CommonModule,
    SharedCommonModuleModule,
    DashboardRoutingModule,
    SharedComponentModule,
    ChartModule,
  ],
})
export class DashboardModule {}
