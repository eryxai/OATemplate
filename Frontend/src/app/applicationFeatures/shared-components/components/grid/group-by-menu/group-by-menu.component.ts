import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ITableHeader } from 'src/app/sharedFeatures/models/itable-header';

@Component({
  selector: 'group-by-menu',
  templateUrl: './group-by-menu.component.html',
})
export class GroupByMenuComponent implements OnInit {
  @Input() columns: any[];
  @Input() selectedCol: ITableHeader[];

  @Output() changed: EventEmitter<ITableHeader[]> = new EventEmitter<
    ITableHeader[]
  >();

  constructor() {}
  ngOnInit(): void {}

  onGroupByChanged(): void {
    this.changed.emit(this.selectedCol);
  }
}
