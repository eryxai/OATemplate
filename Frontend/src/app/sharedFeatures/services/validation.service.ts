import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ValidationService {
  constructor() {}

  getMobilePattern(): string {
    return `^01[0-2][0-9]{8}$`;
  }

  getNationalIDPattern(): string {
    return `^([2-3]{1}(\\d{2}((0[13578]|1[02])(0[1-9]|[12]\\d|3[01])|(0[13456789]|1[012])(0[1-9]|[12]\\d|30)|02(0[1-9]|1\\d|2[0-9])))|([02468][048]|[13579][26])0229)[0-9]{1}[0'-9]{1}\\d{5}$`;
  }

  getEnglishPattern(): string {
    return '^[a-zA-Z]+([0-9a-zA-Z_\\s*])*$';
  }

  getVATPattern(): string {
    return '^[0-9]{1,2}([.][0-9]+)?$';
  }

  getNumberPattern(): string {
    return '^[0-9]*$';
  }
}
