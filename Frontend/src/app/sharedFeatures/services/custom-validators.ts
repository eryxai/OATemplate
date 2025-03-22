import { AbstractControl } from '@angular/forms';

export function ValidateIsNotEmpty(
  control: AbstractControl
): { [key: string]: any } | null {
  if (control.value && control.value.length > 0 && control.value.trim() == '') {
    return { require: true };
  }
  return null;
}
