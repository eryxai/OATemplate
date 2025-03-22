import { NgModule } from '@angular/core';

 
import { ErrorRoutingModule } from '../Routers/error-routing.module';
 import { CommonSharedModule } from 'src/app/Features/SharedFeatures/CommonShared/Modules/common-shared.module';
import { PageNotFoundComponent } from '../Components/page-not-found/page-not-found.component';

@NgModule({
  declarations: [
    PageNotFoundComponent   
  ],
  imports: [
    CommonSharedModule,
    ErrorRoutingModule
  ],
  providers: []
})
export class ErrorModule { }
