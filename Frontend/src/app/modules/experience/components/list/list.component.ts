import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FilterMetadata, MenuItem, SortMeta } from 'primeng/api';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { DeleteModalComponent } from 'src/app/applicationFeatures/shared-components/components/delete-modal/delete-modal.component';
import { PermissionEnum } from 'src/app/sharedFeatures/enum/permission-enum';
import { TableHeaderType } from 'src/app/sharedFeatures/enum/table/table-header-type';
import { BaseFilter } from 'src/app/sharedFeatures/models/base-filter.model';
import { ITableHeader } from 'src/app/sharedFeatures/models/itable-header';
import { Pagination } from 'src/app/sharedFeatures/models/pagination.model';
import { AuthGuard } from 'src/app/sharedFeatures/services/auth-guard.service';
import { DynamicDialogService } from 'src/app/sharedFeatures/services/dynamic-dialog/dynamic-dialog.service';
import { NotificationService } from 'src/app/sharedFeatures/services/notification.service';
import { PageTitleService } from 'src/app/sharedFeatures/services/page-title.service';
import { ExperienceService } from '../../services/experience.service';
import { renameKeys } from 'src/app/sharedFeatures/functions/rename-keys';
import { LanguageService } from 'src/app/sharedFeatures/services/language';
import { TranslateService } from '@ngx-translate/core';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss'],
})
export class ExperienceListComponent implements OnInit, OnDestroy {
  flitterDto: BaseFilter = new BaseFilter();

  tableColumns: ITableHeader[] = [];
  tableActions: MenuItem[] = [];
  dynamicDialogRef: DynamicDialogRef;
  fillter: BaseFilter = new BaseFilter();
  isLoading: boolean = true;
  dataList: any[];
  subscriptions: Subscription[] = [];
  permissionEnum: any = PermissionEnum;
  fieldsToReMap = {
    parentNameEn: 'Parent.NameEn',
    parentNameAr: 'Parent.NameAr',
  };
  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private gaurd: AuthGuard,
    private dynamicDialogService: DynamicDialogService,
    private notificationService: NotificationService,
    private experienceService: ExperienceService,
    private pageTitle: PageTitleService,
    public languageService: LanguageService,
    private translateService: TranslateService
  ) {}

  ngOnInit(): void {
    // debugger;
    this.setPageTitle();
    this.generateTableColumn();
    this.generateTableActions();
    this.initFilter();

    this.setLangChangeSubscriber();
    this.extractUrlParams();
  }
  extractUrlParams() {
    this.subscriptions.push(
      this.route.queryParams.subscribe(params => {
        this.flitterDto.pagination.pageIndex = +params['pageIndex'];
        this.flitterDto.pagination.pageSize = +params['pageSize'];
        // debugger;
        if (this.flitterDto.pagination.pageIndex) {
          this.getActiveExperience();
        } else {
          this.initFilter();
        }
      })
    );
  }
  setPageTitle(): void {
    this.pageTitle.setTitleTranslated(`experience.list`);
  }
  setLangChangeSubscriber(): void {
    this.subscriptions.push(
      this.translateService.onLangChange.subscribe((lang: any) => {
        this.setPageTitle();
        this.initFilter();
      })
    );
  }
  initFilter(): void {
    this.flitterDto = new BaseFilter();
    this.flitterDto.pagination = Pagination.newPagination(0, 10, 0, true);
    this.getActiveExperience();
  }
  generateTableColumn(): void {
    this.tableColumns = [
      {
        field: 'nameEn',
        name: 'experience.nameEn',
        type: TableHeaderType.Text,
      },
      {
        field: 'nameAr',
        name: 'experience.nameAr',
        type: TableHeaderType.Text,
      },
      {
        field: 'parentNameEn',
        name: 'experience.parentNameEn',
        type: TableHeaderType.Text,
      },
      {
        field: 'parentNameAr',
        name: 'experience.parentNameAr',
        type: TableHeaderType.Text,
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
    // debugger;
    this.router.navigate([url, id], {
      relativeTo: this.route,
      queryParams: { pageIndex, pageSize },
    });
  }
  generateTableActions(): void {
    this.tableActions = [
      {
        visible: this.gaurd.HasPermission([
          PermissionEnum.ExperienceView,
        ]) as boolean,

        iconClass: 'fa-regular fa-eye',
        tooltipOptions: {
          tooltipLabel: 'shared.view',
        },
        command: (event: any): void => {
          this.onPageChange(event.id, '/experience/view');
        },
      },
      {
        visible: this.gaurd.HasPermission([
          PermissionEnum.ExperienceEdit,
        ]) as boolean,

        iconClass: 'fa-regular fa-edit',
        tooltipOptions: {
          tooltipLabel: 'shared.edit',
        },
        command: (event: any): void => {
          this.router.navigate(['/experience/edit', event.id]);
        },
      },
      {
        visible: this.gaurd.HasPermission([
          PermissionEnum.ExperienceDelete,
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
    this.subscriptions.push(
      this.experienceService.delete(id).subscribe(
        (res: any) => {
          this.notificationService.showSuccessTranslated(
            'shared.dataDeletedSuccessfuly',
            'shared.succeed'
          );
          this.getActiveExperience();
        },
        (error: { message: any }) => {
          this.notificationService.showError(error.message, 'error');
        }
      )
    );
  }
  getActiveExperience(): void {
    this.subscriptions.push(
      this.experienceService.getActive(this.flitterDto).subscribe(res => {
        this.dataList = res.collection || [];
        this.flitterDto.pagination =
          this.flitterDto.pagination.setPaginationByInstance(res.pagination);
        // debugger;
        this.isLoading = false;
      })
    );
  }

  onSortChanged(requiredInfo: SortMeta[]): void {
    if (!requiredInfo) return;
    const info = requiredInfo[0];
    let field = info.field;
    if (Object.prototype.hasOwnProperty.call(this.fieldsToReMap, info.field)) {
      var Record = this.fieldsToReMap as Record<string, string>;
      field = Record[info.field];
    }
    this.flitterDto.sorting = info.order === 1 ? field : `${field} desc`;

    this.getActiveExperience();
  }

  onFilterChanged(event: { [s: string]: FilterMetadata[] }): void {
    this.flitterDto.filterMetadata = renameKeys(
      JSON.parse(JSON.stringify(event)),
      this.fieldsToReMap
    );
    this.getActiveExperience();
  }

  exportfile(data: any, fileName: string) {
    var a = document.createElement('a');
    document.body.appendChild(a);
    a.style.display = 'none';
    const blob = new Blob([data], {
      type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet',
    });
    const url = window.URL.createObjectURL(blob);
    a.href = url;
    a.download = fileName;
    a.click();
    window.URL.revokeObjectURL(url);
  }
  ngOnDestroy() {
    this.subscriptions.forEach(s => s.unsubscribe());
  }
}
