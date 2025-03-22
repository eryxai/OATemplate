import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { Subscription } from 'rxjs';
import { LanguageService } from 'src/app/sharedFeatures/services/language';
import { NotificationService } from 'src/app/sharedFeatures/services/notification.service';
import { PageTitleService } from 'src/app/sharedFeatures/services/page-title.service';
import { LoginService } from '../../services/login.service';

@Component({
  selector: 'app-forget-password',
  templateUrl: './forget-password.component.html',
  styleUrls: ['./forget-password.component.scss'],
})
export class ForgetPasswordComponent implements OnInit, OnDestroy {
  language!: string;
  forgetPasswordForm!: FormGroup;
  subscriptionTranslate!: Subscription;
  subscriptions: Subscription[] = [];
  constructor(
    private fb: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private translateService: TranslateService,
    private notificationService: NotificationService,
    private loginService: LoginService,
    private pageTitle: PageTitleService,
    public languageService: LanguageService
  ) {}

  ngOnInit(): void {
    this.setPageTitle();
    this.buildForm();
    this.setLanguageSubscriber();
  }
  ngOnDestroy(): void {
    this.subscriptions.forEach(s => s.unsubscribe());
  }
  changeLang(lang: string) {
    this.translateService.use(lang);
    localStorage.setItem('currentLang', lang);
    this.languageService.setLanguage(lang);
  }
  submit(): void {
    if (this.forgetPasswordForm.valid) {
      let email = {
        email: this.forgetPasswordForm.controls['email'].value,
      };
      this.subscriptions.push(
        this.loginService.forgetPassword(email).subscribe(
          (res: any) => {
            this.notificationService.showSuccessTranslated(
              'shared.resetPasswordSuccess',
              ''
            );
            this.router.navigate(['/login']);
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
                'error.shared.unregisteredEmail',
                ''
              );
            }
          },
          () => {}
        )
      );
    } else {
      const forgetPasswordFormFormKeys = Object.keys(
        this.forgetPasswordForm.controls
      );
      forgetPasswordFormFormKeys.forEach(control => {
        this.forgetPasswordForm.controls[control].markAsTouched();
      });
    }
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
    this.pageTitle.setTitleTranslated(`forgetPassword.Title`);
  }
  buildForm(): void {
    this.forgetPasswordForm = this.fb.group({
      email: [
        null,
        [Validators.required, Validators.email, Validators.maxLength(200)],
      ],
    });
  }
  getControl(controlName: string): any {
    return this.forgetPasswordForm?.controls[controlName];
  }
}
