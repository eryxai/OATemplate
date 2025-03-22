import { Component, OnDestroy, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  ValidationErrors,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { Subscription } from 'rxjs';
import { EditorMode } from 'src/app/sharedFeatures/enum/editor-mode.enum';
import { LookupModel } from 'src/app/sharedFeatures/models/lookup-model';
import { UserLoggedIn } from 'src/app/sharedFeatures/models/user-login.model';
import { CurrentUserService } from 'src/app/sharedFeatures/services/current-user.service';
import { NotificationService } from 'src/app/sharedFeatures/services/notification.service';
import { PageTitleService } from 'src/app/sharedFeatures/services/page-title.service';
import { ValidationService } from 'src/app/sharedFeatures/services/validation.service';
import { ChangePasswordViewModel } from '../../models/change-password-view-model';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.scss'],
})
export class ChangePasswordComponent implements OnInit, OnDestroy {
  currentUser: UserLoggedIn | null = null;
  currentUserUrl: any = `/users/edit/${this.currentUser?.id}`;
  language!: string;
  editForm!: FormGroup;
  subscriptionTranslate!: Subscription;
  subscriptions: Subscription[] = [];
  OrganizationList: LookupModel[] | undefined;
  DepartmentList: LookupModel[] | undefined;
  RoleList: LookupModel[] | undefined;
  type: number = 2;
  imageSource: any;
  mode: EditorMode = EditorMode.add;
  id!: number;
  model!: ChangePasswordViewModel;
  domainUrl: string = this.userService.domainUrl;
  isDisabled: boolean = false;
  constructor(
    private fb: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private translateService: TranslateService,
    private notificationService: NotificationService,
    private userService: UserService,
    private pageTitle: PageTitleService,
    private currentUserService: CurrentUserService,
    private validationService: ValidationService
  ) {}

  ngOnInit(): void {
    this.setPageTitle();
    this.buildForm();
    this.extractUrlParams();
    this.setLanguageSubscriber();
    this.currentUser = this.currentUserService.getCurrentUser();
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach(s => s.unsubscribe());
  }

  extractUrlParams() {
    let editId = this.route.snapshot.params['editId'];
    this.type = this.route.snapshot.params['type'];
    if (editId) {
      this.mode = EditorMode.edit;
      this.id = editId;
    } else {
      this.mode = EditorMode.add;
    }
    if (this.id) {
      this.subscriptions.push(
        this.userService.getById(this.id).subscribe(
          (res: any) => {
            this.model = res;
            this.bindModelToForm();
          },
          (error: any) => {
            this.notificationService.showErrorTranslated(
              'error.shared.operationFailed',
              ''
            );
          }
        )
      );
    }
  }

  submit(): void {
    this.isDisabled = true;
    if (this.editForm.valid) {
      let user: ChangePasswordViewModel = this.collectModel();
      if (this.currentUser?.id) {
        this.subscriptions.push(
          this.userService.changePassword(user).subscribe(
            (res: any) => {
              this.notificationService.showSuccessTranslated(
                `shared.dataSavedSuccessfuly`,
                ''
              );
              this.isDisabled = false;
              this.goToList();
            },
            (error: any) => {
              this.isDisabled = false;
              console.log('FormErrors' + JSON.stringify(error));
              if (error.status == 400) {
                let key = 'error.400';
                this.translateService.get([key]).subscribe(res => {
                  this.notificationService.showErrorTranslated(
                    `${res[key]}`,
                    ''
                  );
                });
              } else {
                this.notificationService.showErrorTranslated(
                  'error.shared.oldPasswordWrong',
                  ''
                );
              }
            }
          )
        );
      } else {
      }
    } else {
      this.isDisabled = false;
      const loginFormFormKeys = Object.keys(this.editForm.controls);
      loginFormFormKeys.forEach(control => {
        this.editForm.controls[control].markAsTouched();
      });
    }
  }

  collectModel(): ChangePasswordViewModel {
    let user = new ChangePasswordViewModel();
    user.userId = this.currentUser?.id;
    user.oldPassword = this.editForm?.controls['oldPassword'].value;
    user.newPassword = this.editForm?.controls['newPassword'].value;
    user.confirmPassword = this.editForm?.controls['confirmPassword'].value;
    return user;
  }
  bindModelToForm() {
    this.editForm?.controls['oldPassword']?.setValue(this.model.oldPassword);
    this.editForm?.controls['newPassword']?.setValue(this.model.newPassword);
    this.editForm?.controls['confirmPassword']?.setValue(
      this.model.confirmPassword
    );
  }
  buildForm(): void {
    this.editForm = this.fb.group({
      oldPassword: ['', [Validators.required, Validators.maxLength(200)]],
      newPassword: [
        '',
        [Validators.required, Validators.maxLength(200), this.shouldNotMatch],
      ],
      confirmPassword: [
        '',
        [
          Validators.required,
          Validators.maxLength(200),
          this.mustMatchPassword,
        ],
      ],
    });
  }

  mustMatchPassword(control: AbstractControl): ValidationErrors | null {
    let newPassword = control.parent?.get('newPassword');
    let confirmPassword = control.parent?.get('confirmPassword');
    if (
      newPassword?.value &&
      confirmPassword?.value &&
      newPassword?.value != confirmPassword?.value
    )
      return { mustMatchPassword: true };
    return null;
  }

  shouldNotMatch(control: AbstractControl): ValidationErrors | null {
    let oldPassword = control.parent?.get('oldPassword');
    let newPassword = control.parent?.get('newPassword');
    if (
      oldPassword?.value &&
      newPassword?.value &&
      newPassword?.value == oldPassword?.value
    )
      return { notMatcholdPassword: true };
    return null;
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
    this.pageTitle.setTitleTranslated(`User.ChangePassword`);
  }
  getControl(controlName: string): any {
    return this.editForm?.controls[controlName];
  }
  goToList() {
    if (this.currentUserUrl === this.router.url) {
      this.router.navigate([`/users/view/${this.currentUser?.id}`]);
    } else {
      this.router.navigate(['/dashboard']);
    }
  }
}
