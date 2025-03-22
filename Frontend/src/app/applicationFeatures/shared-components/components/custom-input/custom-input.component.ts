import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  forwardRef,
} from '@angular/core';
import { NG_VALUE_ACCESSOR } from '@angular/forms';
import { Subject, debounceTime, distinctUntilChanged } from 'rxjs';

@Component({
  selector: 'msn-input',
  templateUrl: './custom-input.component.html',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => CustomInputComponent),
      multi: true,
    },
  ],
})
export class CustomInputComponent implements OnInit {
  @Input() placeholder: string = '';
  @Input() label: string = '';
  @Input() type: string = 'text';
  @Input() disabled: boolean = false;
  @Input() displayInRow: boolean = false;
  @Input() isMandatory: boolean = false;
  @Input() value: string | undefined;
  @Input() classname: string = '';

  @Output() inputTextChanged: EventEmitter<string> = new EventEmitter<string>();

  textChanged = new Subject<string>();

  constructor() {
    // This is intentional
    this.textChanged
      .pipe(debounceTime(400), distinctUntilChanged())
      .subscribe(value => {
        value = value.trim();
        this.inputTextChanged.emit(value);
        this.onChange(value);
        this.onTouched(value);
      });
  }

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

  emitInputTextValue(value: any): void {
    value = value.trim();
    this.inputTextChanged.emit(value);
    this.onChange(value);
    this.onTouched(value);
  }
}
