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
  selector: 'ngx-bool-filter',
  templateUrl: './bool-filter.component.html',
})
export class BoolFilterComponent implements OnInit {
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
    if (event.value == null) {
    } else {
      filterMetadata[this.field] = [
        {
          value: +event.value,
          operator: 'and',
          matchMode: 'equals',
        },
      ];
    }

    this.filterMetadata.emit({ filters: filterMetadata });

    this.overlayPanel.hide();
  }

  showOverlay(event: MouseEvent) {
    this.overlayPanel.show(event);
  }

  getOptions(): void {
    this.translate.instant('shared.yes');
    this.translate.instant('shared.no');
    this.options.push({
      label: this.translate.instant('shared.select'),
      value: null,
    });
    this.options.push({
      label: this.translate.instant('shared.yes'),
      value: true,
    });
    this.options.push({
      label: this.translate.instant('shared.no'),
      value: false,
    });
    // this.options.unshift({value:null,})
  }
}
