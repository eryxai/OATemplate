import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  ViewChild,
} from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { FilterMetadata, SelectItem } from 'primeng/api';
import { OverlayPanel } from 'primeng/overlaypanel';

@Component({
  selector: 'ngx-enum-filter',
  templateUrl: './enum-filter.component.html',
})
export class EnumFilterComponent implements OnInit {
  @Input() enum: any;
  @Input() field: string;
  @Input() column: string;
  @Output() filterMetadata: EventEmitter<any> = new EventEmitter<any>();
  @ViewChild('overlayPanel') overlayPanel: OverlayPanel;

  options: SelectItem[] = [];
  selectedValue = '';
  constructor(private translate: TranslateService) {}

  ngOnInit(): void {
    this.getOptions();
  }

  reset(): void {
    this.selectedValue = '';
  }

  onValueChange(event: any): void {
    let filterMetadata: { [key: string]: FilterMetadata[] } = {};
    filterMetadata[this.field] = [
      {
        value: +event.value,
        operator: 'and',
        matchMode: 'equals',
      },
    ];

    this.filterMetadata.emit({ filters: filterMetadata });

    this.overlayPanel.hide();
  }

  showOverlay(event: MouseEvent) {
    this.overlayPanel.show(event);
  }

  getOptions(): void {
    this.options = Object.entries(this.enum)
      .map(([key, value]) => ({
        label: this.translate.instant(`${this.field}.${key}`),
        value,
      }))
      .filter((option: any) => typeof option.value === 'number');

    // 
    // this.options.unshift({value:null,})
  }
}
