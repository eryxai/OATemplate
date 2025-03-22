import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './components/app-main/app.component';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import {
  HttpClient,
  HTTP_INTERCEPTORS,
  provideHttpClient,
  withInterceptorsFromDi,
} from '@angular/common/http';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { FooterComponent } from './components/layouts/footer/footer.component';
import { MainContentComponent } from './components/layouts/main-content/main-content.component';
import { NavBarComponent } from './components/layouts/nav-bar/nav-bar.component';
import { SideBarComponent } from './components/layouts/side-bar/side-bar.component';
import { LoginComponent } from 'src/app/modules/users/components/login/login.component';
import { LoaderModule } from '../loader/loader.module';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ForgetPasswordComponent } from 'src/app/modules/users/components/forget-password/forget-password.component';
import { SharedComponentModule } from '../shared-components/shared-components.module';
import { RtlDirective } from 'src/app/sharedFeatures/directives/rtl.directive';
import { ToggleSideMenuDirective } from '../directives/toggleSideMenu';
import { ErrorInterceptor } from 'src/app/sharedFeatures/services/http-error-interceptor';

//import { StatsCardComponent } from 'src/app/modules/dashboard/components/stats-card/stats-card.component';
//import { PopularCoursesComponent } from 'src/app/modules/dashboard/components/popular-courses/popular-courses.component';
//import { CourseStatusComponent } from 'src/app/modules/dashboard/components/course-status/course-status.component';
//import { BestMentorsComponent } from 'src/app/modules/dashboard/components/best-mentors/best-mentors.component';
//import { UpcomingLessonsComponent } from 'src/app/modules/dashboard/components/upcoming-lessons/upcoming-lessons.component';
//import { MonthlyProgressComponent } from 'src/app/modules/dashboard/components/monthly-progress/monthly-progress.component';
//import { NoticeBoardComponent } from 'src/app/modules/dashboard/components/notice-board/notice-board.component';
// Import MSAL and MSAL browser libraries.
import {
  MsalGuard,
  MsalInterceptor,
  MsalModule,
  MsalRedirectComponent,
} from '@azure/msal-angular';
import { InteractionType, PublicClientApplication } from '@azure/msal-browser';

// Import the Azure AD B2C configuration
import { msalConfig, protectedResources } from './auth-config';

@NgModule({
  declarations: [
    AppComponent,
    FooterComponent,
    MainContentComponent,
    NavBarComponent,
    SideBarComponent,
    LoginComponent,
    ForgetPasswordComponent,
    RtlDirective,
    ToggleSideMenuDirective,
    /*StatsCardComponent,
        PopularCoursesComponent,
        CourseStatusComponent,
        BestMentorsComponent,
        UpcomingLessonsComponent,
        MonthlyProgressComponent,
        NoticeBoardComponent*/
  ],
  bootstrap: [AppComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    LoaderModule,
    SharedComponentModule,
    ReactiveFormsModule,
    FormsModule,
    // toaster is  from the buttom of the page
    ToastrModule.forRoot({
      closeButton: true,
      easeTime: 500,
      enableHtml: true,
      progressBar: true,
      progressAnimation: 'increasing',
      positionClass: 'toast-bottom-right', // Positioning options

    }),
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: createTranslateLoader,
        deps: [HttpClient],
      },
    }),
    BrowserAnimationsModule,
   
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    provideHttpClient(withInterceptorsFromDi()),
    provideClientHydration(),
  ],
})
export class AppModule {}

export function createTranslateLoader(http: HttpClient) {
  return new TranslateHttpLoader(http);
}
