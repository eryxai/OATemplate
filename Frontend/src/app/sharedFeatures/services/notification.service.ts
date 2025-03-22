import { Injectable, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';

@Injectable({
  providedIn: 'root',
})
export class NotificationService {
  constructor(
    private toastrService: ToastrService,
    private translateService: TranslateService
  ) {}

  showSuccessTranslated(messageKey: any, titleKey: any) {
    this.translateService.get([messageKey, titleKey]).subscribe(res => {
      this.showSuccess(res[messageKey], res[titleKey]);
    });
  }

  showInfoTranslated(messageKey: any, titleKey: any) {
    this.translateService.get([messageKey, titleKey]).subscribe(res => {
      this.showInfo(res[messageKey], res[titleKey]);
    });
  }

  showErrorTranslated(messageKey: any, titleKey: any) {
    this.translateService.get([messageKey, titleKey]).subscribe(res => {
      this.showError(res[messageKey], res[titleKey]);
    });
  }

  showwarningTranslated(messageKey: any, titleKey: any) {
    this.translateService.get([messageKey, titleKey]).subscribe(res => {
      this.showwarning(res[messageKey], res[titleKey]);
    });
  }

  showSuccess(message: any, title: any) {
    this.toastrService.success(message, title);
  }

  showInfo(message: any, title: any) {
    this.toastrService.info(message, title);
  }

  showError(message: any, title: any) {
    this.toastrService.error(message, title);
  }

  showwarning(message: any, title: any) {
    this.toastrService.warning(message, title);
  }

  clear(toastId?: number) {
    this.toastrService.clear(toastId);
  }
  showDataMissingError() {
    this.showErrorTranslated('error.dataMissing', 'error.error');
  }
}
