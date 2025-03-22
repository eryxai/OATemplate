import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { MenuItem, SortMeta } from 'primeng/api';
import { FilterMetadata } from 'primeng/api/filtermetadata';
import { TableHeaderType } from 'src/app/sharedFeatures/enum/table/table-header-type';
import { ITableHeader } from 'src/app/sharedFeatures/models/itable-header';
import { Pagination } from 'src/app/sharedFeatures/models/pagination.model';
import { RoleSortOrder } from '../../enums/sort-order';
import { RoleLightViewModel } from '../../models/role-light-view-model.model';
import { RoleSearchViewModel } from '../../models/role-search-view-model.model';
import { RoleService } from '../../services/role.service';

@Component({
  selector: 'app-new-grid',
  templateUrl: './new-grid.component.html',
  styleUrls: ['./new-grid.component.scss'],
})
export class NewGridComponent implements OnInit {
  dataList: RoleLightViewModel[];
  tableColumns: ITableHeader[] = [];
  tableActions: MenuItem[] = [];
  searchModel: RoleSearchViewModel;

  constructor(
    private _RoleService: RoleService,
    private translate: TranslateService
  ) { }

  ngOnInit(): void {
    this.generateTableColumn();
    this.generateTableActions();
    this.initFilter();
    this.getAll();
  }

  getAll() {
    // let a = this.searchModel;
    //  
    this._RoleService.search(this.searchModel).subscribe(res => {
      this.dataList = res.collection || [];
      this.searchModel.pagination =
        this.searchModel.pagination.setPaginationByInstance(res.pagination);
    });
  }

  generateTableActions(): void {
    this.tableActions = [
      {
        // label: this.translate.instant('shared.edit'),
        id: '',
        iconClass: 'fa fa-edit',
        command: (event: any): void => {
          /**
           * to do..
           */
        },
      },
      {
        //label: this.translate.instant('shared.view'),
        id: '',
        iconClass: 'fa fa-eye',
        command: (event: any): void => {
          /**
           * to do..
           */
        },
      },
      {
        //label: this.translate.instant('shared.delete'),
        id: '',
        iconClass: 'fa fa-check',
        command: (event): void => {
          /**
           * to do..
           */
        },
      },
    ];
  }
  generateTableColumn(): void {
    this.tableColumns = [
      { field: 'id', name: 'ID', hidden: true, recordKey: true },

      {
        field: 'name',
        name: 'Name' /**to be translated */,
        sortable: true,
        canGroup: true,
        type: TableHeaderType.Text,
      },
      {
        field: 'organizationName',
        name: 'Organization Name' /**to be translated */,
        sortable: true,
        canGroup: true,
        type: TableHeaderType.Text,
      },
      {
        field: 'creationDate',
        name: 'Creation Date' /**to be translated */,
        type: TableHeaderType.Date,
      },
      {
        field: 'description',
        name: 'description',
        sortable: true,

        type: TableHeaderType.Text,
      },
    ];
  }

  initFilter(): void {
    this.searchModel = new RoleSearchViewModel();
    this.searchModel.pagination = Pagination.newPagination(0, 10, 0, true);
  }

  onFilterChanged(event: { [s: string]: FilterMetadata[] }): void {
    this.searchModel.filterMetadata = event;
    this.getAll();
  }

  onSortChanged(requiredInfo: SortMeta[]): void {
    if (!requiredInfo) return;
    const info = requiredInfo[0];
    if (!Object.values(RoleSortOrder).includes(info.field as RoleSortOrder))
      return;

    this.searchModel.sorting =
      info.order === 1 ? info.field : `${info.field} desc`;
    this.getAll();
  }
}
