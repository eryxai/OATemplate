import {
  Directive,
  Input,
  ElementRef,
  OnChanges,
  SimpleChanges,
} from '@angular/core';

@Directive({
  selector: '[replaceSvg]',
})
export class ReplaceSvgDirective implements OnChanges {
  @Input() replaceSvg: string = '';

  constructor(private elementRef: ElementRef) {}

  ngOnChanges(changes: SimpleChanges) {
    if (changes['replaceSvg']) {
      this.setSvg();
    }
  }

  private setSvg() {
    if (this.replaceSvg) {
      // Replace the element's content with the SVG string
      this.elementRef.nativeElement.innerHTML = this.replaceSvg;
    }
  }
}
