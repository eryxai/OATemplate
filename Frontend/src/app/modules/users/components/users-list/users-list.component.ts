import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { Subscription } from 'rxjs';
import { PermissionEnum } from 'src/app/sharedFeatures/enum/permission-enum';
import { FieldSort } from 'src/app/sharedFeatures/models/field-sort.model';
import { GenericResultModel } from 'src/app/sharedFeatures/models/generic-result.model';
import { Language } from 'src/app/sharedFeatures/models/language.enum';
import { LookupModel } from 'src/app/sharedFeatures/models/lookup-model';
import { Pagination } from 'src/app/sharedFeatures/models/pagination.model';
import { NotificationService } from 'src/app/sharedFeatures/services/notification.service';
import { PageTitleService } from 'src/app/sharedFeatures/services/page-title.service';

import { UserLightViewModel } from '../../models/user-light-view-model.model';
import { UserSearchViewModel } from '../../models/user-search-view-model.model';
import { UserService } from '../../services/user.service';
import { MenuItem, SortMeta, FilterMetadata } from 'primeng/api';
import { DynamicDialogRef, DynamicDialogConfig } from 'primeng/dynamicdialog';
import { DeleteModalComponent } from 'src/app/applicationFeatures/shared-components/components/delete-modal/delete-modal.component';

import { TableHeaderType } from 'src/app/sharedFeatures/enum/table/table-header-type';
import { renameKeys } from 'src/app/sharedFeatures/functions/rename-keys';
import { BaseFilter } from 'src/app/sharedFeatures/models/base-filter.model';
import { ITableHeader } from 'src/app/sharedFeatures/models/itable-header';
import { AuthGuard } from 'src/app/sharedFeatures/services/auth-guard.service';
import { DynamicDialogService } from 'src/app/sharedFeatures/services/dynamic-dialog/dynamic-dialog.service';
import { LanguageService } from 'src/app/sharedFeatures/services/language';

@Component({
  selector: 'app-users-list',
  templateUrl: './users-list.component.html',
  styleUrls: ['./users-list.component.scss'],
})
export class UsersListComponent implements OnInit {
  flitterDto: UserSearchViewModel = new UserSearchViewModel();

  tableColumns: ITableHeader[] = [];
  tableActions: MenuItem[] = [];
  dynamicDialogRef: DynamicDialogRef;
  fillter: BaseFilter = new BaseFilter();
  isLoading: boolean = true;
  dataList: any[];
  permissionEnum: any = PermissionEnum;

  fieldsToReMap = {
    name: this.languageService.isRtl() ? 'NameAr' : 'NameEn',
    roleName: this.languageService.isRtl()
      ? 'UserRoles.FirstOrDefault().Role.NameAr'
      : 'UserRoles.FirstOrDefault().Role.NameEn',
     
  };

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private gaurd: AuthGuard,
    private dynamicDialogService: DynamicDialogService,
    private notificationService: NotificationService,
    private pageTitle: PageTitleService,
    private userService: UserService,
    private translateService: TranslateService,
    public languageService: LanguageService
  ) {}

  ngOnInit(): void {
    this.setPageTitle();
    this.generateTableColumn();
    this.generateTableActions();
    this.initFilter();

    this.extractUrlParams();
    this.setLangChangeSubscriber();
  }
  extractUrlParams() {
    this.route.queryParams.subscribe(params => {
      this.flitterDto.pagination.pageIndex = params['pageIndex'];
      this.flitterDto.pagination.pageSize = params['pageSize'];
      if (this.flitterDto.pagination.pageIndex) {
        this.getActiveUser();
      } else {
        this.initFilter();
      }
    });
  }
  setLangChangeSubscriber(): void {
    this.translateService.onLangChange.subscribe((lang: any) => {
      this.setPageTitle();
      this.initFilter();
    });
  }

  setPageTitle(): void {
    this.pageTitle.setTitleTranslated(`User.list.title`);
  }

  initFilter(): void {
    this.flitterDto = new UserSearchViewModel();
    this.flitterDto.pagination = Pagination.newPagination(0, 10, 0, true);

    this.getActiveUser();
  }
  generateTableColumn(): void {
    this.tableColumns = [
      { field: 'id', name: 'ID', hidden: true, recordKey: true },

      {
        field: 'name',
        name: 'User.name',
        type: TableHeaderType.Text,
      },
      {
        field: 'email',
        name: 'User.email',
        type: TableHeaderType.Text,
      },
      {
        field: 'isActive',
        name: 'User.status',
        type: TableHeaderType.Boolean,
      },
      {
        field: 'roleName',
        name: 'User.role',
        type: TableHeaderType.Text,
        removeFlitter: true,
        removeSorting: true,
      },
      // {
      //   field: 'phoneNumber',
      //   name: 'User.phoneNumber',
      //   type: TableHeaderType.Text,
      // },
    ];
  }
  hasPermission(code: any): boolean {
    let data: any = { permissionCodes: [code] };

    let hasPermission = this.gaurd.showActivate(data);

    if (hasPermission) return true;

    return false;
  }
  onPageChange(id: number, url: string) {
    const pageIndex = this.flitterDto.pagination.pageIndex;
    const pageSize = this.flitterDto.pagination.pageSize;
    
    this.router.navigate([url, id], {
      relativeTo: this.route,
      queryParams: { pageIndex, pageSize },
    });
  }
  generateTableActions(): void {
    this.tableActions = [
      {
        visible: this.gaurd.HasPermission([PermissionEnum.UserView]) as boolean,

        iconClass: 'fa-regular fa-eye',
        tooltipOptions: {
          tooltipLabel: 'shared.view',
        },
        command: (event: any): void => {
          this.onPageChange(event.id, '/users/view');
        },
      },
      {
        visible: this.gaurd.HasPermission([PermissionEnum.UserEdit]) as boolean,

        iconClass: 'fa-regular fa-edit',
        tooltipOptions: {
          tooltipLabel: 'shared.edit',
        },
        command: (event: any): void => {
          this.router.navigate(['/users/edit/', event.id, 2]);
        },
      },
      {
        visible: this.gaurd.HasPermission([
          PermissionEnum.UserDelete,
        ]) as boolean,

        iconClass: 'fa-regular fa-trash-can',
        tooltipOptions: {
          tooltipLabel: 'shared.delete',
        },

        command: (event: any): void => {
          this.confirmDelete(event.id);
        },
      },
    ];
  }
  confirmDelete(id: string): void {
    const dDConfig: DynamicDialogConfig = {
      data: {
        bodyMessage: 'deleteConfirmationMessage',
        deleteAction: () => {
          this.dynamicDialogRef.close();
          this.delete(id);
        },
      },
      showHeader: false,
      contentStyle: { 'min-width': '470px' },
    };
    this.dynamicDialogRef = this.dynamicDialogService.showDynamicDialog(
      DeleteModalComponent,
      dDConfig
    );
  }
  delete(id: string): void {
    this.userService.delete(id).subscribe(
      res => {
        this.notificationService.showSuccessTranslated(
          'shared.dataDeletedSuccessfuly',
          'shared.succeed'
        );
        this.getActiveUser();
      },
      error => {
        this.notificationService.showError(error.message, 'error');
      }
    );
  }
  getActiveUser(): void {
    this.userService.search(this.flitterDto).subscribe(res => {
      this.dataList = res.collection || [];
      this.flitterDto.pagination =
        this.flitterDto.pagination.setPaginationByInstance(res.pagination);
      this.isLoading = false;
    });
  }

  onSortChanged(requiredInfo: SortMeta[]): void {
    if (!requiredInfo) return;
    const info = requiredInfo[0];

    if (Object.prototype.hasOwnProperty.call(this.fieldsToReMap, info.field)) {
      var Record = this.fieldsToReMap as Record<string, string>;
      info.field = Record[info.field];
    }
    this.flitterDto.sorting =
      info.order === 1 ? info.field : `${info.field} desc`;
    this.getActiveUser();
  }

  onFilterChanged(event: { [s: string]: FilterMetadata[] }): void {
    this.flitterDto.filterMetadata = renameKeys(
      JSON.parse(JSON.stringify(event)),
      this.fieldsToReMap
    );
    this.getActiveUser();
  }
} /* implements OnInit, OnDestroy {
  fieldSortList!: FieldSort[];
  searchModel: UserSearchViewModel = new UserSearchViewModel();
  isGettitngData: boolean = false;
  dataList: GenericResultModel<UserLightViewModel[]> = new GenericResultModel<
    UserLightViewModel[]
  >();
  searchForm!: FormGroup;
  permissionEnum: any = PermissionEnum;
  deleteId!: number;
  OrganizationList: LookupModel[] | undefined;
  DepartmentList: LookupModel[] | undefined;
  domainUrl: string = this._UsersService.domainUrl;
  subscriptions: Subscription[] = [];
  subscriptionTranslate!: Subscription;
  constructor(
    private _UsersService: UserService,
    private pageTitle: PageTitleService,
    private notificationService: NotificationService,
    private translateService: TranslateService,
    private fb: FormBuilder,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
     
    this.setPageTitle();
    this.initializeFieldSortList();
    this.searchModel.sorting =
      this.fieldSortList[6].name +
      ' ' +
      (this.fieldSortList[6].sortASC ? 'asc' : 'desc');
    this.buildForm();
 
    //this.getLookupOrganizationPaged(new OrganizationlookupSearchModel());
    this.setLangChangeSubscriber();
    this.extractUrlParams();
  }
  extractUrlParams() {
    this.route.queryParams.subscribe((params) => {
      this.searchModel.pagination.pageIndex = params['pageIndex'];
      this.searchModel.pagination.pageSize = params['pageSize'];
      if(!this.searchModel.pagination.pageIndex){
        this.searchModel.pagination = Pagination.newPagination(
          0,
          Pagination.DefaultPageSize,
          0,
          true
        );
      }
      this.getAll(this.searchModel);
    });
   
  }
  onPageChange(id:number,url:string) {
    
    const pageIndex=this.searchModel.pagination.pageIndex;
    const pageSize=this.searchModel.pagination.pageSize;
    
    this.router.navigate([url,id], {
      relativeTo: this.route,
      queryParams: { pageIndex, pageSize },
    });
  }
  ngOnDestroy(): void {
    this.subscriptions.forEach(s => s.unsubscribe());
  }

  setLangChangeSubscriber(): void {
    this.subscriptionTranslate = this.translateService.onLangChange.subscribe(
      (lang: any) => {
        this.setPageTitle();
        this.getAll(this.searchModel);
        //this.getLookupOrganizationPaged(new OrganizationlookupSearchModel());
      }
    );
    this.subscriptions.push(this.subscriptionTranslate);
  }
  
  setPageTitle(): void {
    this.pageTitle.setTitleTranslated(`User.list.title`);
  }
  //#region search

  buildForm() {
    this.searchForm = this.fb.group({
      username: [null],
      // name: [null],
      // email: [null],
      nameEmail: [null],
      phoneNumber: [null],
      departmentId: [null],
      isActive: [null],
    });
  }
  resetForm() {
    this.searchForm.reset();
    this.search()
  }
  search() {
    if (!this.searchModel) {
      this.searchModel = new UserSearchViewModel();
    }

    this.searchModel.username = this.searchForm?.controls['username']?.value;
    // this.searchModel.name = this.searchForm?.controls['name']?.value;
    // this.searchModel.email = this.searchForm?.controls['email']?.value;
    this.searchModel.nameEmail = this.searchForm?.controls['nameEmail']?.value;
    this.searchModel.phoneNumber =
      this.searchForm?.controls['phoneNumber']?.value;

    this.searchModel.isActive = this.searchForm?.controls['isActive']?.value;


    this.getAll(this.searchModel);
  }
  // getLookupOrganizationPaged(
  //   searchModel: OrganizationlookupSearchModel | null
  // ) {
  //   this.subscriptions.push(
  //     this._UsersService.getLookupOrganizationPaged(searchModel).subscribe(
  //       res => {
  //         this.OrganizationList = res.collection;
  //         //this.searchModel.pagination = this.searchModel.pagination.setPaginationByInstance(res.pagination);
  //       },
  //       error => {
  //         this.notificationService.showError(error.message, 'error');
  //       }
  //     )
  //   );
  // }
  // organizationChange() {
  //   var searchModel = new DepartmentlookupSearchModel();
  //   if (this.searchForm?.controls['organizationId'].value === 'null') {
  //     //in case 'select org' isn't disabled
  //     this.searchForm?.controls['organizationId']?.setValue(null);
  //     this.searchForm?.controls['departmentId']?.setValue(null);
  //     this.searchForm?.controls['categoryId']?.setValue(null);
  //     this.DepartmentList = [];
  //   } else {
   
  //     this.subscriptions.push(
  //       this._UsersService.getLookupDepartmentPaged(searchModel).subscribe(
  //         res => {
  //           this.searchForm?.controls['departmentId']?.setValue(null);
  //           this.DepartmentList = res.collection;
  //           //this.searchModel.pagination = this.searchModel.pagination.setPaginationByInstance(res.pagination);
  //         },
  //         error => {
  //           this.notificationService.showError(error.message, 'error');
  //         }
  //       )
  //     );
  //   }
  // }

  // departmentChange() {
  //   if (this.searchForm?.controls['departmentId'].value === 'null') {
  //     //in case 'select org' isn't disabled
  //     this.searchForm?.controls['departmentId']?.setValue(null);
  //   }
  // }

  statusChange() {
    if (this.searchForm?.controls['isActive'].value === 'null') {
      //in case 'select org' isn't disabled
      this.searchForm?.controls['isActive']?.setValue(null);
    }
  }
  //#endregion

  //#region list
  initializeFieldSortList(): void {
    let langSt = this.translateService.currentLang;
    let lang = langSt.charAt(0).toUpperCase() + langSt.slice(1);

    this.fieldSortList = [];
    this.fieldSortList.push({ name: 'name' + lang, sortASC: false });
    this.fieldSortList.push({ name: 'username', sortASC: false });
    this.fieldSortList.push({ name: 'isActive', sortASC: false });
    this.fieldSortList.push({ name: 'departmentId', sortASC: false });
    this.fieldSortList.push({ name: 'phoneNumber', sortASC: false });
    this.fieldSortList.push({ name: 'email', sortASC: false });
    this.fieldSortList.push({ name: 'creationDate', sortASC: true });
  }
  getAll(searchModel: UserSearchViewModel) {
    this.isGettitngData = true;
    this.dataList.collection = [];
    this.subscriptions.push(
      this._UsersService.search(searchModel).subscribe(
        res => {
          this.isGettitngData = false;
          this.dataList = res;
          this.searchModel.pagination =
            this.searchModel.pagination.setPaginationByInstance(res.pagination);
        },
        error => {
          this.isGettitngData = false;
          this.notificationService.showError(error.message, 'error');
        },
        () => {}
      )
    );
  }

  sort(fieldIndex: number) {
     
    if (fieldIndex != null) {
      let fieldSort = this.fieldSortList[fieldIndex];
      if (fieldSort) {
        fieldSort.sortASC = !fieldSort.sortASC;
        this.searchModel.sorting = `${fieldSort.name} ${
          fieldSort.sortASC ? 'asc' : 'desc'
        }`;
        this.getAll(this.searchModel);
      }
    }
  }

  searchFormChanged(model: UserSearchViewModel) {
    this.searchModel = model;
  }

  pageChanged(evnt: any) {
    this.searchModel.pagination = evnt;
    this.getAll(this.searchModel);
  }
  //#endregion
  deleteObject() {
    this.subscriptions.push(
      this._UsersService.delete(this.deleteId).subscribe(
        res => {
          this.notificationService.showSuccessTranslated(
            'shared.dataDeletedSuccessfuly',
            ''
          );
          this.getAll(this.searchModel);
        },
        error => {
          this.notificationService.showErrorTranslated(
            'shared.cantDeleteObject',
            ''
          );
        },
        () => {}
      )
    );
  }
}
 */
