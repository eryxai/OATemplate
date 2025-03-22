import { LookupModel } from './../../../../sharedFeatures/models/lookup-model';
import { NameValueViewModel } from './../../../roles/models/role-permission-list.model';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { Subscription } from 'rxjs';
import { EditorMode } from 'src/app/sharedFeatures/enum/editor-mode.enum';
import { UserLoggedIn } from 'src/app/sharedFeatures/models/user-login.model';
import { CurrentUserService } from 'src/app/sharedFeatures/services/current-user.service';
import { NotificationService } from 'src/app/sharedFeatures/services/notification.service';
import { PageTitleService } from 'src/app/sharedFeatures/services/page-title.service';
import { UserService } from '../../services/user.service';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  ValidationErrors,
  Validators,
} from '@angular/forms';
import { UserViewModel } from '../../models/user-view-model';
import { PermissionEnum } from 'src/app/sharedFeatures/enum/permission-enum';
import { RoleService } from 'src/app/modules/roles/services/role.service';

@Component({
  selector: 'app-users-view',
  templateUrl: './users-view.component.html',
  styleUrls: ['./users-view.component.scss'],
})
export class UsersViewComponent implements OnInit, OnDestroy {
  language!: string;
  editForm!: FormGroup;
  subscriptionTranslate!: Subscription;
  RoleList: LookupModel[];
  subscriptions: Subscription[] = [];
  imageSource: any;
  mode: EditorMode = EditorMode.add;
  id!: number;
  model!: UserViewModel;
  domainUrl: string = this.userService.domainUrl;
  isDisabled: boolean = false;
  statusList: any[];
  SegmentLookup: [];
  type: number = 2;
  currentUser: UserLoggedIn | null = null;
  permissionEnum: any = PermissionEnum;
  ReadersList: LookupModel[];
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
    private currentUserService: CurrentUserService,
  ) {}

  ngOnInit(): void {
    this.setPageTitle();
    this.getLookupStatus();
    this.getRolesLookup();
    this.setLanguageSubscriber();
    this.buildForm();
    this.extractUrlParams();
    this.getOptionLabel();
    this.currentUser = this.currentUserService.getCurrentUser();
  }
  ngOnDestroy(): void {
    this.subscriptions.forEach(s => s.unsubscribe());
  }


  getLookupStatus() {
    this.statusList = [
      { id: true, name: 'Active' },
      { id: false, name: 'InActive' },
    ];
  }
  getOptionLabel() {
    return this.language === 'ar' ? 'nameAr' : 'nameEn';
  }
  isHiden: boolean = false;
  isHidenPassward() {
    this.userService.IsHidenPassword().subscribe(res => {
      
      this.isHiden = res;
    });
  }

  extractUrlParams() {
    this.id = this.route.snapshot.params['id'];

    if (this.id) {
      this.subscriptions.push(
        this.userService.getById(this.id).subscribe(
          (res: any) => {
            this.model = res;
            this.roleids = this.model.roleIds;

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

  bindModelToForm() {
    this.editForm.patchValue(this.model);
    this.roleids = this.model.roleIds;
    if (this.RoleList) {
      this.editForm?.controls['roleIds']?.setValue(
        this.RoleList.filter(e => this.model.roleIds.includes(e.id))
      );
    }
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
  buildForm(): void {
    this.editForm = this.fb.group({
      //username: ['', [Validators.required, Validators.minLength(2)]],

      nameEn: [null, []],
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
      roleIds: [null, [Validators.required]],
      readerId: [null, []],
    });
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
    this.pageTitle.setTitleTranslated(`User.view.title`);
  }
  getControl(controlName: string): any {
    return this.editForm?.controls[controlName];
  }

  goToList() {
    this.router.navigate(['/users/list']);
  }
}
