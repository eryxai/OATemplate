import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { LanguagesLocalization } from '../enum/languages-localization.enum';

@Injectable({
  providedIn: 'root',
})
export class LanguageService {
  private languageSubject: BehaviorSubject<string>;
  public language: Observable<string>;

  constructor() {
    this.languageSubject = new BehaviorSubject<any>({});
    this.language = this.languageSubject.asObservable();
  }

  setLanguage(lang: string) {
    this.languageSubject.next(lang);
  }

  public get getLanguage() {
    return this.languageSubject.value;
  }

  isRtl(): boolean {
    return this.languageSubject.value === LanguagesLocalization.AR;
  }
}
