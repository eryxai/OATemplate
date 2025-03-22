import {
  Component,
  OnInit,
  Input,
  OnChanges,
  SimpleChanges,
} from '@angular/core';
import { AbstractControl, FormControl } from '@angular/forms';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-control-error-display',
  templateUrl: './control-error-display.component.html',
  styleUrls: ['./control-error-display.component.sass'],
})
export class ControlErrorDisplayComponent implements OnInit {
  constructor(private translateService: TranslateService) {}

  ngOnInit(): void {
   }

  getControlErrors(): string[] {
 
    let result: string[] = [];
    if (this.control.errors) {
      for (const key in this.control.errors) {
        if (key.toString() == 'pattern') {
          result.push(
            `error.validation.${key.toString()}.${this.patternMessage}`
          );
        } else {
          result.push(`error.validation.${key.toString()}`);
        }
      }
    }
    return result;
  }

  @Input() control: AbstractControl = new FormControl();
  @Input() patternMessage: any;
}
