import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  forwardRef,
} from '@angular/core';
import { NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
  selector: 'msn-dropdown',
  templateUrl: './custom-dropdwn.component.html',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => CustomDropdwnComponent),
      multi: true,
    },
  ],
})
export class CustomDropdwnComponent implements OnInit {
  @Input() placeholder: string = 'shared.select';
  @Input() label: string = '';
  @Input() labelIcon: string = '';
  @Input() options: any[] = [];
  @Input() optionLabel: string = 'name';
  @Input() optionValue: string = 'id';
  @Input() value: string | undefined | null;
  @Input() disabled: boolean = false;
  @Input() displayInRow: boolean = false;
  @Input() isMandatory: boolean = false;
  @Output() dropdownChanged: EventEmitter<any> = new EventEmitter<any>();
  @Output() labelIconClicked: EventEmitter<void> = new EventEmitter<void>();
  @Input() classname: string = '';

  constructor() {}

  ngOnInit(): void {}

  onChange = (value: any): any => value;
  onTouched = (value: any): any => value;

  writeValue(object: any): void {
    this.value = object;
  }

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  onDropdownChange(): void {
    if (this.value == 'null') {
      this.value = null;
      this.dropdownChanged.emit(this.value);
      this.onChange(this.value);
      this.onTouched(this.value);
    } else {
      this.dropdownChanged.emit(Number(this.value));
      this.onChange(Number(this.value));
      this.onTouched(Number(this.value));
    }
  }
}
