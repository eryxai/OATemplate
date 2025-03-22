import { PermissionEnum } from 'src/app/sharedFeatures/enum/permission-enum';
import { NameValueViewModel } from './../../../roles/models/role-permission-list.model';
import { Component, OnInit, OnDestroy } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormControl,
  FormGroup,
  ValidationErrors,
  Validators,
} from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { Subscription } from 'rxjs';
import { EditorMode } from 'src/app/sharedFeatures/enum/editor-mode.enum';
import { LookupModel } from 'src/app/sharedFeatures/models/lookup-model';
import { UserLoggedIn } from 'src/app/sharedFeatures/models/user-login.model';
import { CurrentUserService } from 'src/app/sharedFeatures/services/current-user.service';
import { NotificationService } from 'src/app/sharedFeatures/services/notification.service';
import { PageTitleService } from 'src/app/sharedFeatures/services/page-title.service';

import { UserViewModel } from '../../models/user-view-model';
import { UserService } from '../../services/user.service';
import { RoleService } from 'src/app/modules/roles/services/role.service';

@Component({
  selector: 'app-users-add-edit',
  templateUrl: './users-add-edit.component.html',
  styleUrls: ['./users-add-edit.component.scss'],
})
export class UsersAddEditComponent implements OnInit, OnDestroy {
  language!: string;
  editForm!: FormGroup;
  subscriptionTranslate!: Subscription;
  RoleList: LookupModel[];
  subscriptions: Subscription[] = [];
  imageSource: any;
  mode: EditorMode = EditorMode.add;
  editId!: number;
  model!: UserViewModel;
  domainUrl: string = this.userService.domainUrl;
  isDisabled: boolean = false;
  statusList: any[];
  type: number = 2;
  currentUser: UserLoggedIn | null = null;
  permissionEnum: any = PermissionEnum;
  ReadersList: LookupModel[];
  lang: string = '';
  roleids: number[] = [];
  constructor(
    private roleService: RoleService,
    private fb: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private translateService: TranslateService,
    private notificationService: NotificationService,
    private userService: UserService,
    private pageTitle: PageTitleService,
    private currentUserService: CurrentUserService
  ) {}

  ngOnInit(): void {
    this.getRolesLookup();

    this.buildForm();
    this.extractUrlParams();
    this.setPageTitle();
    this.setLanguageSubscriber();
    this.currentUser = this.currentUserService.getCurrentUser();
  }
  ngOnDestroy(): void {
    this.subscriptions.forEach(s => s.unsubscribe());
  }

  getRolesLookup() {
    this.roleService.GetLookup().subscribe(result => {
      this.RoleList = result;
      if (this.roleids) {
        this.editForm?.controls['roleIds']?.setValue(
          this.RoleList.filter(e => this.roleids.includes(e.id))
        );
      }
    });
  }

  isHiden: boolean = false;
  // isHidenPassward(){
  //   this.userService.IsHidenPassword().subscribe((res)=>{
  //       this.isHiden=res;
  //   });

  extractUrlParams() {
    this.editId = this.route.snapshot.params['editId'];
    this.type = this.route.snapshot.params['type'];
    if (this.editId) {
      this.mode = EditorMode.edit;
    } else {
      this.mode = EditorMode.add;
      this.editForm.addControl(
        'password',
        new FormControl('', [
          Validators.required,
          Validators.maxLength(200),
          Validators.pattern(
            '^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}'
          ),
        ])
      );
      this.editForm.addControl(
        'confirmPassword',
        new FormControl('', [
          Validators.required,
          Validators.maxLength(200),
          this.mustMatchPassword,
        ])
      );
    }
    if (this.editId) {
      this.subscriptions.push(
        this.userService.getById(this.editId).subscribe(
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
    console.log(this.editForm);
    if (this.editForm.valid) {
      
      let user: UserViewModel = this.collectModel();
      console.log(user);
      if (this.editId) {
        this.subscriptions.push(
          this.userService.update(user).subscribe(
            (res: any) => {
              
              this.notificationService.showSuccessTranslated(
                'shared.dataUpdatedSuccessfuly',
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
                  'error.' + error.error,
                  ''
                );
              }
            }
          )
        );
      } else {
        this.subscriptions.push(
          this.userService.add(user).subscribe(
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
                  'error.' + error.error,
                  ''
                );
              }
            }
          )
        );
      }
    } else {
      this.isDisabled = false;
      const loginFormFormKeys = Object.keys(this.editForm.controls);
      loginFormFormKeys.forEach(control => {
        this.editForm.controls[control].markAsTouched();
      });
    }
  }

  collectModel(): UserViewModel {
    
    let user = new UserViewModel();
    user.id = this.editId;
    user.username = this.editForm?.controls['email'].value;

    user.password = this.editForm?.controls['password']?.value;
    user.confirmPassword = this.editForm?.controls['password']?.value;
    user.nameAr = this.editForm?.controls['nameAr'].value;
    user.nameEn = this.editForm?.controls['nameEn'].value;
    //user.phoneNumber = this.editForm?.controls['phoneNumber'].value;
    user.email = this.editForm?.controls['email'].value;
    user.isActive = this.editForm?.controls['isActive'].value;
    user.roleIds = this.editForm?.controls['roleIds'].value.map(
      (e: { id: any }) => e.id
    );
    user.segementId = this.editForm?.controls['segementId'].value;
    user.readerId = this.editForm?.controls['readerId'].value;

    if (this.imageSource) {
      user.base64Image = this.imageSource;
      user.fileName = this.editForm?.controls['fileName'].value;
    } else {
      user.profileImage = this.editForm?.controls['profileImage'].value;
    }
    return user;
  }
  bindModelToForm() {
    // this.editForm?.controls['username']?.setValue(this.model.username);
    this.editForm?.controls['nameAr']?.setValue(this.model.nameAr);
    this.editForm?.controls['nameEn']?.setValue(this.model.nameEn);
    ///this.editForm?.controls['phoneNumber']?.setValue(this.model.phoneNumber);
    this.editForm?.controls['email']?.setValue(this.model.email);
    this.editForm?.controls['isActive']?.setValue(this.model.isActive);
    this.editForm?.controls['profileImage']?.setValue(this.model.profileImage);
    // this.editForm?.controls['roleIds']?.setValue(this.model.roleIds);
    this.editForm?.controls['segementId']?.setValue(this.model.segementId);
    this.editForm?.controls['readerId']?.setValue(this.model.readerId);
    this.roleids = this.model.roleIds;
    if (this.RoleList) {
      this.editForm?.controls['roleIds']?.setValue(
        this.RoleList.filter(e => this.model.roleIds.includes(e.id))
      );
    }
  }
  buildForm(): void {
    this.editForm = this.fb.group({
      //username: ['', [Validators.required, Validators.minLength(2)]],

      nameEn: [null, [Validators.required]],
      nameAr: [null, [Validators.required, Validators.maxLength(200)]],
      isActive: [true, [Validators.required]],
      // phoneNumber: [
      //   null,
      //   [Validators.required, Validators.pattern('\\+?2?01[0-9]{9}')],
      // ],
      email: [
        null,
        [
          Validators.required,
          Validators.pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$'),
          Validators.maxLength(200),
        ],
      ],
      profileImage: ['', []],
      fileName: ['', []],
      roleIds: [[], [Validators.required]],
      segementId: [null, []],
      readerId: [null, []],
    });
  }

  getOptionLabel() {
    return this.language === 'ar' ? 'nameAr' : 'nameEn';
  }

  mustMatchPassword(control: AbstractControl): ValidationErrors | null {
    let password = control.parent?.get('password');
    let confirmPassword = control.parent?.get('confirmPassword');
    if (
      password?.value &&
      confirmPassword?.value &&
      password?.value != confirmPassword?.value
    )
      return { mustMatchPassword: true };
    return null;
  }

  setLanguageSubscriber(): void {
    this.language = this.translateService.currentLang;
    this.lang = this.language;
    
    this.getOptionLabel();
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
    this.pageTitle.setTitleTranslated(
      this.editId ? `User.editUser` : `User.addNewUser`
    );
  }
  getControl(controlName: string): any {
    return this.editForm?.controls[controlName];
  }

  Upload(event: any) {
    const file = event.target.files[0];
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => {
      this.imageSource = reader.result;
      this.editForm?.controls['fileName'].setValue(file.name);
      console.log(reader.result);
    };
  }
  goToList() {
    let url = '/users/view/' + this.currentUser?.id + '/1';
    if (this.type == 1) this.router.navigate(['/dashboard']);
    else this.router.navigate(['/users/list']);
  }
  setNotEdit(): boolean {
    if (this.type == 1) return true;
    else return false;
  }

  deleteImg() {
    this.editForm?.controls['profileImage']?.setValue(null);
    this.imageSource = null;
  }
}
