import {
  Directive,
  ElementRef,
  Inject,
  OnInit,
  Renderer2,
} from '@angular/core';
import { LanguageService } from '../services/language';
import { DOCUMENT } from '@angular/common';

@Directive({
  selector: '[AddsRtl]',
})
export class RtlDirective implements OnInit {
  constructor(
    private elem: ElementRef,
    private renderer: Renderer2,
    private languageService: LanguageService,
    @Inject(DOCUMENT) private document: Document
  ) {}

  ngOnInit(): void {
    this.languageService.language.subscribe((lang: any) => {
      this.setRtlDirection(lang);
      this.setLangAttribute(lang);
    });
  }
  setRtlDirection(lang: string): void {
    lang === 'ar'
      ? this.renderer.addClass(this.elem.nativeElement, 'rtl')
      : this.renderer.removeClass(this.elem.nativeElement, 'rtl');
  }

  setLangAttribute(lang: string): void {
    this.renderer.setAttribute(this.document.documentElement, 'lang', lang);
  }
}
