import { Subscription } from 'rxjs';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { PermissionEnum } from 'src/app/sharedFeatures/enum/permission-enum';
import { Pagination } from 'src/app/sharedFeatures/models/pagination.model';
import { NotificationService } from 'src/app/sharedFeatures/services/notification.service';
import { PageTitleService } from 'src/app/sharedFeatures/services/page-title.service';
import { RoleSearchViewModel } from '../../models/role-search-view-model.model';
import { RoleService } from '../../services/role.service';
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
  selector: 'app-roles-list',
  templateUrl: './roles-list.component.html',
  styleUrls: ['./roles-list.component.scss'],
})
export class RolesListComponent implements OnInit {
  flitterDto: RoleSearchViewModel = new RoleSearchViewModel();

  tableColumns: ITableHeader[] = [];
  tableActions: MenuItem[] = [];
  dynamicDialogRef: DynamicDialogRef;
  fillter: BaseFilter = new BaseFilter();
  isLoading: boolean = true;
  dataList: any[];
  permissionEnum: any = PermissionEnum;

  fieldsToReMap = {
    name: this.languageService.isRtl() ? 'NameAr' : 'NameEn',
    description: this.languageService.isRtl() ? 'DescriptionAr' : 'DescriptionEn',
  };

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private gaurd: AuthGuard,
    private dynamicDialogService: DynamicDialogService,
    private notificationService: NotificationService,
    private pageTitle: PageTitleService,
    private roleService: RoleService,
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
        this.getActiveRole();
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
    this.pageTitle.setTitleTranslated(`Role.list.title`);
  }

  initFilter(): void {
    this.flitterDto = new RoleSearchViewModel();
    this.flitterDto.pagination = Pagination.newPagination(0, 10, 0, true);

    this.getActiveRole();
  }
  generateTableColumn(): void {
    this.tableColumns = [
      { field: 'id', name: 'ID', hidden: true, recordKey: true },

      {
        field: 'name',
        name: 'Role.name',
        type: TableHeaderType.Text,
      },
      {
        field: 'description',
        name: 'Role.description',
        type: TableHeaderType.Text,
      },
      {
        field: 'creationDate',
        name: 'Role.creationDate',
        type: TableHeaderType.Date,
      },
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
        visible: this.gaurd.HasPermission([PermissionEnum.RoleView]) as boolean,

        iconClass: 'fa-regular fa-eye',
        tooltipOptions: {
          tooltipLabel: 'shared.view',
        },
        command: (event: any): void => {
          this.onPageChange(event.id, '/roles/view');
        },
      },
      {
        visible: this.gaurd.HasPermission([PermissionEnum.RoleEdit]) as boolean,

        iconClass: 'fa-regular fa-edit',
        tooltipOptions: {
          tooltipLabel: 'shared.edit',
        },
        command: (event: any): void => {
          this.router.navigate(['/roles/edit', event.id]);
        },
      },
      {
        visible: this.gaurd.HasPermission([
          PermissionEnum.RoleDelete,
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
    this.roleService.delete(id).subscribe(
      res => {
        this.notificationService.showSuccessTranslated(
          'shared.dataDeletedSuccessfuly',
          'shared.succeed'
        );
        this.getActiveRole();
      },
      error => {
        this.notificationService.showError(error.message, 'error');
      }
    );
  }
  getActiveRole(): void {
    this.roleService.search(this.flitterDto).subscribe(res => {
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
    this.getActiveRole();
  }

  onFilterChanged(event: { [s: string]: FilterMetadata[] }): void {
    this.flitterDto.filterMetadata = renameKeys(
      JSON.parse(JSON.stringify(event)),
      this.fieldsToReMap
    );
    this.getActiveRole();
  }
}
