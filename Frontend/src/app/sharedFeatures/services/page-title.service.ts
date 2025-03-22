import { Injectable } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { TranslateService } from '@ngx-translate/core';
import { Month } from '../enum/Month';

@Injectable({
  providedIn: 'root',
})
export class PageTitleService {
  constructor(
    private title: Title,
    private translateService: TranslateService
  ) {}
  getMonths(month: Month) {
    return month;
  }
  setTitleTranslated(titleKey: string): void {
    this.translateService.get([titleKey]).subscribe(res => {
      this.title.setTitle(res[titleKey]);
    });
  }

  setTitle(title: string): void {
    this.title.setTitle(`${title}`);
  }
}
