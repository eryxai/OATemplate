import { Component, Input, Output, EventEmitter } from '@angular/core';
import { ButtonTypes } from 'src/app/sharedFeatures/enum/button-types.enum';

@Component({
  selector: 'msn-button',
  templateUrl: './button.component.html',
  styleUrls: ['./button.component.scss'],
})
export class ButtonComponent {
  constructor() {}
  /**
   * to check if which type to display in component view
   */
  BTN_TYPES = ButtonTypes;

  @Input() title: string = '';

  @Input() type: string = '';

  @Input() disabled: boolean | null = false;

  @Input() role: string = 'button';

  @Input() size: string = 'md';

  @Output() clickEvent = new EventEmitter();

  btnClick(event: any) {
    if (this.disabled == true) {
      return;
    } else {
      this.clickEvent.emit(event);
    }
  }
}
