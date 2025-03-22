import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { Subscription } from 'rxjs';
import { LookupModel } from 'src/app/sharedFeatures/models/lookup-model';
import { NotificationService } from 'src/app/sharedFeatures/services/notification.service';
import { PageTitleService } from 'src/app/sharedFeatures/services/page-title.service';
import { RoleDetailModel } from '../../models/role-details-model';
import { NameValueViewModel } from '../../models/role-permission-list.model';
import { RoleService } from '../../services/role.service';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-roles-view',
  templateUrl: './roles-view.component.html',
  styleUrls: ['./roles-view.component.scss'],
})
export class RolesViewComponent implements OnInit, OnDestroy {
  PermissionsList: any[] | undefined;
  formGroup!: FormGroup;
  model!: RoleDetailModel;
  domainUrl: string = this.roleService.domainUrl;
  language!: string;
  subscriptionTranslate!: Subscription;
  subscriptions: Subscription[] = [];
  Id:number;
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private fb: FormBuilder,
    private translateService: TranslateService,
    private notificationService: NotificationService,
    private roleService: RoleService,
    private pageTitle: PageTitleService
  ) {}

  ngOnInit(): void {
    this.buildForm();
    this.setPageTitle();
    this.extractUrlParams();
    this.setLangChangeSubscriber();
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach(s => s.unsubscribe());
  }
  buildForm(): void {
    this.formGroup = this.fb.group({
      nameAr: [null, []],
      nameEn: [null, []],
      descriptionAr: [null, []],
      descriptionEn: [null, []]
    });
  }
  setLangChangeSubscriber(): void {
    this.subscriptionTranslate = this.translateService.onLangChange.subscribe(
      (lang: any) => {
        this.setPageTitle();
        this.GetAllPermissions(this.Id);
      }
    );
    this.subscriptions.push(this.subscriptionTranslate);
  }

  setPageTitle(): void {
    this.pageTitle.setTitleTranslated(`Role.view.title`);
  }

  pageIndex:number=0;
  pageSize:number=0;
  extractUrlParams() {
    this.route.queryParams.subscribe((params) => {
      this.pageIndex = params['pageIndex'];
      
      this.pageSize = params['pageSize'];
    });
    this.Id = this.route.snapshot.params['id'];
    if (this.Id) {
      this.GetAllPermissions(this.Id);
      this.subscriptions.push(
        this.roleService.getDetailsById(this.Id).subscribe(
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
  bindModelToForm() {
    this.formGroup?.controls['nameAr']?.patchValue(this.model.nameAr);
    this.formGroup?.controls['nameEn']?.patchValue(this.model.nameEn);
    this.formGroup?.controls['descriptionAr']?.patchValue(this.model.descriptionAr);
    this.formGroup?.controls['descriptionEn']?.patchValue(this.model.descriptionEn);
    
  }
 
  GetAllPermissions(id: any) {
    this.subscriptions.push(
      this.roleService.GetRolePermissionById(id).subscribe(
        res => {
          this.PermissionsList = res.list;
        },
        error => {
          this.notificationService.showError(error.message, 'error');
        }
      )
    );
  }

  goToList() {
    const pageSize  = this.pageSize;
    const pageIndex=this.pageIndex;
    // Pass the pagination parameters back to the list component when navigating back
    this.router.navigate(['/roles/list'], {
      queryParams: { pageIndex, pageSize },
    });
  }
}
