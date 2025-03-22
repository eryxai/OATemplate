import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  QueryList,
  SimpleChanges,
  TemplateRef,
  ViewChild,
  ViewChildren,
  OnChanges,
} from '@angular/core';
import { FilterMetadata, MenuItem, PrimeNGConfig, SortMeta } from 'primeng/api';
import { Table, TableRowCollapseEvent, TableRowExpandEvent } from 'primeng/table';

import { EnumFilterComponent } from './enum-filter/enum-filter.component';
import { ITableHeader } from 'src/app/sharedFeatures/models/itable-header';
import { TableHeaderType } from 'src/app/sharedFeatures/enum/table/table-header-type';
import { TableViewMood } from 'src/app/sharedFeatures/enum/table/table-view-mode';
import { hasProperty } from 'src/app/sharedFeatures/functions/key-at-list-check';
import { groupBy } from 'src/app/sharedFeatures/functions/group-column-by';
import { TranslateService } from '@ngx-translate/core';
import { AuthGuard } from 'src/app/sharedFeatures/services/auth-guard.service';
import { Observable } from 'rxjs/internal/Observable';
import { modifyDateValues } from './helpers';

@Component({
  selector: 'msn-grid',
  templateUrl: './grid.component.html',
  styleUrls: ['./grid.component.scss'],
})
export class GridComponent implements OnInit, OnChanges {
  @Input() columns: ITableHeader[] = [];
  @Input() data: any[];
  @Input() actions: MenuItem[] = [];
  @Input() selectedRecords: any[] = [];
  @Input() pageIndex: number = 0;
  @Input() paginator: boolean = true;
  @Input() sortable: boolean = true;
  @Input() canSelect: boolean = false;
  @Input() showGroupMenu: boolean = false;
  @Input() isRowClickable: boolean = true;
  @Input() pageSize: number = 6;
  @Input() rowsPerPageOptions: number[] = [5, 10, 20, 50];
  @Input() totalRecordsLength: number;
  @Input() selectionMode: string;
  @Input() styleClass: string = 'p-datatable-gridlines';
  @Input() moduleTitle: string;
  @Input() module!: string;
  @Input() Template: TemplateRef<any>;
  @Input() RowExpansion: boolean = false;

  @Input() actionButtonClass: string =
    'aube-btn-outline rounded-circle btn-color-lightblue';
  @Input() scrollable: boolean;
  @Input() responsive: boolean = false;
  @Input() sendDateOnlyAtFilter: boolean = true;
  expandedRows = {};

  @Output() filterChanged: EventEmitter<{
    [s: string]: FilterMetadata[];
  }> = new EventEmitter<{ [s: string]: FilterMetadata[] }>();

  @Output() showGroupMenuChange: EventEmitter<boolean> =
    new EventEmitter<boolean>();
  @Output() pageIndexChange: EventEmitter<number> = new EventEmitter<number>();
  @Output() pageSizeChange: EventEmitter<number> = new EventEmitter<number>();
  @Output() sortChanged: EventEmitter<SortMeta[]> = new EventEmitter<
    SortMeta[]
  >();

  @Output() selectedRecordsChanged: EventEmitter<any[]> = new EventEmitter<
    any[]
  >();
  @Output() rowClicked: EventEmitter<any> = new EventEmitter<any>();

  @ViewChild('ADVGrid') ADVGrid: Table;
  @ViewChildren(EnumFilterComponent)
  enumFilters: QueryList<EnumFilterComponent>;

  dataList: any[];
  groupedData: any;
  recordKey: string = '';
  currentPageReportTemplate: string = '';
  groupColumnsOptions: any[];
  propertyGetters: ((item: any) => any)[]; // usedIn Grouping
  selectedGroupByCol: any[];
  viewMood: TableViewMood = TableViewMood.RecordsView;
  firstIndex: number = 0;
  isLoading: boolean = false;

  TableHeaderType = TableHeaderType;
  TableViewMood = TableViewMood;

  constructor(
    private adminGuard: AuthGuard,
    private config: PrimeNGConfig,
    private translateService: TranslateService
  ) {
    /**
     * intuitional
     */
  }

  ngOnInit(): void {
    this.dataList = JSON.parse(JSON.stringify(this.data));
    this.setPaginationTitle();
    this.getRecordKey();
    // this.getGroupByColumnOptions();
    this.getFilterMenuTranslation();
    this.translateService.onLangChange.subscribe(res => {
      this.isLoading = true;
      this.getFilterMenuTranslation();
      setTimeout(() => {
        this.isLoading = false;
      }, 200);
    });
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (!changes['data']?.firstChange && changes['data']?.currentValue) {
      this.dataList = JSON.parse(JSON.stringify(changes['data'].currentValue));
      this.setPaginationTitle();

      if (this.viewMood === TableViewMood.GroupView) {
        this.dataList = groupBy(this.data, [...this.propertyGetters]) as any;
      }
    }

    if (changes['pageIndex']?.currentValue) this.setFirstIndex();
  }

  getFilterMenuTranslation(): void {
    this.translateService.get('primeng.filter').subscribe((res: any) => {
      this.config.setTranslation(res);
    });
  }

  onPageChange(event: { first: number; rows: number }): void {
    let pageIndex = event.first / event.rows;
    this.pageSizeChange.emit(event.rows);
    this.pageIndexChange.emit(pageIndex);
  }
  setFirstIndex(): void {
    this.firstIndex = this.pageIndex * this.pageSize;
  }
  //{ filteredValue: any[];filters: { [s: string]: FilterMetadata[] };}
  // onFilterChange(event: any): void {
  //   if (this.sendDateOnlyAtFilter) modifyDateValues(event.filters);
  //   this.filterChanged.emit(event.filters);
  // }
  onFilterChange(event: any,isEnum: boolean): void {
    var filters=this.getFilterValues(event.filters,isEnum);
    this.filterChanged.emit(filters);
  }

  getFilterValues(newObj :any,isEnum: boolean):any 
  {
    if (this.ADVGrid) {
      const extendedFilters = this.ADVGrid.filters;
      if(isEnum){
        const firstKey = Object.keys(newObj)[0]; // Gets the first key
        const firstValue = newObj[firstKey]; // Gets the value of the first key       
        extendedFilters[firstKey]=firstValue;  
      }
      console.log(extendedFilters);
      return extendedFilters;

    }
  }

  onSort(event: SortMeta): void {
    if (this.isColumnSortable(event?.field)) this.sortChanged.emit([event]);
  }

  isColumnSortable(felid: string) {
    const col = this.columns.find(col => col.field === felid);
    return !col?.removeSorting;
  }
  onRowClicked(event: number | string): void {
    this.rowClicked.emit(event);
  }

  onSelectionChange(event: any): void {
    if (
      hasProperty(this.selectedRecords, 'key') &&
      this.viewMood === TableViewMood.GroupView
    )
      this.selectedRecords = JSON.parse(JSON.stringify(this.data));
    this.selectedRecordsChanged.emit(this.selectedRecords);
  }

  onTakeAction(item: MenuItem, recordData: any): void {
    if (recordData.id && item.command) item.command(recordData);
  }

  getRecordKey(): void {
    this.recordKey = this.columns.find(col => col.recordKey)?.field || '';
  }

  getGroupByColumnOptions(): void {
    this.groupColumnsOptions = this.columns.filter(item => item.canGroup);
  }

  trackByIndex(index: number): number {
    return index;
  }

  onGroupByChanged(col: ITableHeader[]): void {
    if (!col.length) {
      this.reset();
      return;
    }
    this.propertyGetters = col.map((key: any) => {
      return (item: any) => item[key.field];
    });

    this.dataList = groupBy(this.data, [...this.propertyGetters]) as any;
    this.viewMood = TableViewMood.GroupView;
  }

  reset(): void {
    this.selectedGroupByCol = [];
    this.viewMood = TableViewMood.RecordsView;
    this.ADVGrid.reset();
    this.enumFilters.forEach(enumFilter => {
      enumFilter.reset();
    });
  }

  getEnumDisplayValue(col: any, rowData: any): string {
    if (!rowData[col.field] || !col.enum) {
      return '';
    }

    const enumKey = this.getEnumKeyByEnumValue(col.enum, rowData[col.field]);
    if (this.module)
      return enumKey ? `${this.module}.${col.field}.${enumKey}` : '';
    else
      return enumKey ? `${col.field}.${enumKey}` : '';
  }

  getEnumKeyByEnumValue(
    enumObject: { [key: string]: string } | undefined,
    enumValue: string
  ): string {
    if (!enumObject) {
      return '';
    }

    const key = Object.keys(enumObject).find(k => enumObject[k] === enumValue);
    return key || '';
  }

  setPaginationTitle(): void {
    if (this.totalRecordsLength) {
      this.currentPageReportTemplate = `${this.totalRecordsLength
        .toString()
        .replace(/\B(?=(\d{3})+(?!\d))/g, ',')}  ${this.moduleTitle}`;
    } else {
      // Handle the case where `this.totalRecordsLength` is undefined
      this.currentPageReportTemplate = `0  ${this.moduleTitle}`;
    }
  }
  actionHasPermission(action: any): Observable<boolean> | boolean {
    if (action.permission) {
      return this.adminGuard.HasPermission([action.permission]);
    } else {
      return true;
    }
  }

  onRowExpand(event: TableRowExpandEvent) { }

  onRowCollapse(event: TableRowCollapseEvent) { }
}
