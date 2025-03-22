import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RolesRoutingModule } from './roles-routing.module';
import { RolesListComponent } from './components/roles-list/roles-list.component';
import { RolesAddEditComponent } from './components/roles-add-edit/roles-add-edit.component';
import { RolesViewComponent } from './components/roles-view/roles-view.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { SharedComponentModule } from 'src/app/applicationFeatures/shared-components/shared-components.module';
import { SharedCommonModuleModule } from 'src/app/applicationFeatures/shared-common-module/shared-common-module.module';
import { NewGridComponent } from './components/new-grid/new-grid.component';

@NgModule({
  declarations: [
    RolesListComponent,
    RolesAddEditComponent,
    RolesViewComponent,
    NewGridComponent,
  ],
  imports: [
    CommonModule,
    RolesRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    SharedComponentModule,
    TranslateModule,
    SharedCommonModuleModule,
  ],
})
export class RolesModule {}
