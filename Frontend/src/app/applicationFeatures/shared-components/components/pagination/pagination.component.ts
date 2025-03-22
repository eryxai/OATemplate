import {
  Component,
  OnInit,
  OnChanges,
  SimpleChanges,
  Input,
  Output,
  EventEmitter,
} from '@angular/core';
import { Pagination } from 'src/app/sharedFeatures/models/pagination.model';

@Component({
  selector: 'pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.sass', './pagination.component.css'],
})
export class PaginationComponent implements OnInit {
  constructor() {}

  ngOnChanges(simpleChanges: SimpleChanges) {
    this.pages = [];
    if (
      this.pagination &&
      this.pagination.totalCount &&
      this.pagination.pageSize
    ) {
      let pagesCount = this.pagination.totalCount / this.pagination.pageSize;
      this.pageSizeOptions = [];
      let index = 1;
      do {
        if (index == 2 || index == 10) {
          index = index * 2.5;
          this.pageSizeOptions.push(index);
        } else {
          index = index * 2;
          this.pageSizeOptions.push(index);
        }
      } while (index <= this.pagination.totalCount);
      // if (pagesCount > 10) {
      //   pagesCount = 10;
      // }

      for (let index = 0; index < pagesCount; index++) {
        this.pages.push({
          value: index,
          label: (index + 1).toString(),
          isSelected: index == this.currentPage ? true : false,
        });
      }
    }
  }

  ngOnInit(): void {}

  displaySelectedPage() {
    if (this.pages) {
      // this.pages.filter(a => (a.isSelected = false));
      // this.pages[this.currentPage].isSelected = true;
      if(this.currentPage<this.sliceStart){
        this.sliceStart-=5;
        this.sliceEnd-=5
      }
      if(this.currentPage==this.sliceEnd){
        this.sliceStart+=5;
        this.sliceEnd+=5
      }
    }
    /*  if (this.pages) {
      for (let index = 0; index < this.pages.length; index++) {
        if (index == this.currentPage) {
          this.pages[this.currentPage].isSelected = true;
        }
        else {
          this.pages[index].isSelected = false;
        }
      }
    } */
  }

  /*   gotoPage(event:any){
    this.pagination.pageIndex=event?.pageIndex;
    this.pagination.pageSize=event?.pageSize;
    this.pageChanged.emit(this.pagination);

  }
 */
  setPageSizeOptions(setPageSizeOptionsInput: string) {
    if (setPageSizeOptionsInput) {
      this.pageSizeOptions = setPageSizeOptionsInput
        .split(',')
        .map(str => +str);
    }
  }

  gotoPage(pageIndex: number) {
    this.pagination.pageIndex = pageIndex;
    this.currentPage = pageIndex;
    this.displaySelectedPage();
    this.pageChanged.emit(this.pagination);
  }
  updatePageSize(pageSize: number) {
    this.pagination.pageIndex = 0;
    this.pagination.pageSize = pageSize;
    this.currentPage = 0;
    this.displaySelectedPage();
    this.pageChanged.emit(this.pagination);
  }

  // gotoPageDown() {
  //   if (this.pagination.pageIndex != null) {
  //     this.pagination.pageIndex = this.pagination.pageIndex - 10; // go down 10 pages
  //     if (this.pagination.pageIndex < 0) {
  //       this.pagination.pageIndex = 0;
  //     }

  //     this.currentPage = this.pagination.pageIndex;
  //     this.displaySelectedPage();
  //   }
  //   this.pageChanged.emit(this.pagination.pageIndex);
  // }
  gotoPageUp() {
    if (this.pagination.pageIndex != null) {
      this.pagination.pageIndex = this.pagination.pageIndex + 1;
      if (
        this.pagination.totalCount &&
        this.pagination.pageIndex > this.pagination.totalCount
      ) {
        this.pagination.pageIndex = this.pagination.totalCount - 1;
      }

      this.currentPage = this.pagination.pageIndex;
      this.displaySelectedPage();
    }
    this.pageChanged.emit(this.pagination);
  }
  gotoPagePerv() {
    if (this.pagination.pageIndex != null) {
      this.pagination.pageIndex = this.pagination.pageIndex - 1; // go up 10 pages
      if (
        this.pagination.totalCount &&
        this.pagination.pageIndex > this.pagination.totalCount
      ) {
        this.pagination.pageIndex = this.pagination.totalCount - 1;
      }

      this.currentPage = this.pagination.pageIndex;
      this.displaySelectedPage();
    }
    this.pageChanged.emit(this.pagination);
  }
  gotoPageFirst() {
    this.pagination.pageIndex = 0;
    this.currentPage = this.pagination.pageIndex;
    this.displaySelectedPage();
    this.pageChanged.emit(this.pagination);
  }
  gotoPageLast() {
    if (this.pages && this.pages.length > 0) {
      this.pagination.pageIndex = this.pages.length - 1;

      this.currentPage = this.pagination.pageIndex;
      this.displaySelectedPage();
    }
    this.pageChanged.emit(this.pagination);
  }

  @Input() pagination: Pagination = Pagination.newPagination(
    0,
    Pagination.DefaultPageSize,
    0,
    true
  );
  @Output() pageChanged: EventEmitter<any> = new EventEmitter<any>();
  pageSizeOptions: number[] = [];
  isGettitngData: boolean = false;
  pages: Page[] = [];
  currentPage: number = 0;
  sliceStart:number=0;
  sliceEnd:number=5;
}

export class Page {
  value: number = 0;
  label: string = '';
  isSelected: boolean = false;
}
