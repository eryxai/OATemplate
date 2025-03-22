import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedCommonModuleModule } from 'src/app/applicationFeatures/shared-common-module/shared-common-module.module';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedComponentModule } from 'src/app/applicationFeatures/shared-components/shared-components.module';
import { ExperienceListComponent } from './components/list/list.component';
import { ExperienceViewComponent } from './components/view/view.component';
import { ExperienceAddEditComponent } from './components/add-edit/add-edit.component';
import { ExperienceRoutingModule } from './experience-routing.module';



@NgModule({
  declarations: [
    ExperienceListComponent,
    ExperienceViewComponent,
    ExperienceAddEditComponent,
    ],
  imports: [
    CommonModule,
    ExperienceRoutingModule,
    SharedCommonModuleModule,
    SharedComponentModule,
    ReactiveFormsModule,
  ],
})
export class ExperienceModule {}
