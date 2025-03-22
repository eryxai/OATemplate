import { Component, OnInit, OnDestroy, ChangeDetectorRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { Subscription } from 'rxjs';
import { NotificationService } from '../../../../sharedFeatures/services/notification.service';
import { UserLoggedIn } from '../../../../sharedFeatures/models/user-login.model';
import { PageTitleService } from '../../../../sharedFeatures/services/page-title.service';
import { Login } from '../../models/login';
import { LoginService } from '../../services/login.service';
import { LanguageService } from 'src/app/sharedFeatures/services/language';
import { AuthGuard } from 'src/app/sharedFeatures/services/auth-guard.service';
import { loginSvg } from '../../svg/login';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit, OnDestroy {
  language!: string;
  loginForm!: FormGroup;
  subscriptionTranslate!: Subscription;
  currentUser: UserLoggedIn | null = null;
  subscriptions: Subscription[] = [];
  hidePassword: boolean = true;
  ChangeDetictionForceUpdateVar: boolean[] = [this.hidePassword];
  svg = loginSvg;
  constructor(
    private fb: FormBuilder,
    private router: Router,
    private translateService: TranslateService,
    public languageService: LanguageService,
    private notificationService: NotificationService,
    private loginService: LoginService,
    private pageTitle: PageTitleService,
    private changeDetectorRef: ChangeDetectorRef,
    private authGuard: AuthGuard
  ) {}
  changeLang(lang: string) {
    this.translateService.use(lang);
    localStorage.setItem('currentLang', lang);
    this.languageService.setLanguage(lang);
  }
  ngOnInit(): void {
    this.setPageTitle();
    this.buildForm();
    this.setLanguageSubscriber();
    // this.setSvg();
  }
  hidePasswordIcon() {
    this.hidePassword = !this.hidePassword;
    this.ChangeDetictionForceUpdateVar = [this.hidePassword];
    this.changeDetectorRef.detectChanges();
  }
  ngOnDestroy(): void {
    this.subscriptions.forEach(s => s.unsubscribe());
  }

  submit(): void {
    if (this.loginForm.valid) {
      let loginModel: Login = {
        userName: this.loginForm.controls['email'].value,
        password: this.loginForm.controls['password'].value,
      };
      this.subscriptions.push(
        this.loginService.login(loginModel).subscribe(
          (res: any) => {
            //
            this.currentUser = res;
            localStorage.setItem('currentUser', JSON.stringify(res));
            this.reRoute();
          },
          (error: any) => {
            console.log('FormErrors' + JSON.stringify(error));
            if (error.status == 400) {
              let key = 'error.400';
              this.translateService.get([key]).subscribe(res => {
                this.notificationService.showErrorTranslated(`${res[key]}`, '');
              });
            } else {
              this.notificationService.showErrorTranslated(
                'error.shared.operationFailed',
                ''
              );
            }
          },
          () => {}
        )
      );
    } else {
      const loginFormFormKeys = Object.keys(this.loginForm.controls);
      loginFormFormKeys.forEach(control => {
        this.loginForm.controls[control].markAsTouched();
      });
    }
  }


  forgetPassword(): void {
    this.router.navigate(['/forget-password']);

  }
  
  
  reRoute() {
    this.router.navigate(['/dashboard/admin']);
  }
  buildForm(): void {
    this.loginForm = this.fb.group({
      email: [
        null,
        [
          Validators.required,
          Validators.pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$'),
        ],
      ],
      password: [null, [Validators.required]],
    });
  }
  setLanguageSubscriber(): void {
    this.language = this.translateService.currentLang;
    this.subscriptionTranslate = this.translateService.onLangChange.subscribe(
      val => {
        this.language = val.lang;
      },
      error => {},
      () => {}
    );
    this.subscriptions.push(this.subscriptionTranslate);
  }
  setPageTitle(): void {
    this.pageTitle.setTitleTranslated(`login.Title`);
  }

  getControl(controlName: string): any {
    return this.loginForm?.controls[controlName];
  }
  setSvg() {
    const Logo = document.getElementById('FormLogo');
    const Background = document.getElementById('background');

    if (Logo) Logo.innerHTML = this.svg.logo;
    if (Background) Background.innerHTML = this.svg.backGround;
  }
}
