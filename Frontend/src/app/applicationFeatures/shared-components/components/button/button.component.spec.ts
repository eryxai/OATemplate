import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { ButtonComponent } from './button.component';
import { By } from '@angular/platform-browser';

describe('ButtonComponent', () => {
  let component: ButtonComponent;
  let fixture: ComponentFixture<ButtonComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ButtonComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('name should be as binded', () => {
    let compiled = fixture.debugElement.nativeElement;
    let el = compiled.querySelector('button');
    // component.name = 'submit';
    fixture.detectChanges();
    expect(el.innerText).toBe('submit');
  });

  it('background should be as binded', () => {
    let compiled = fixture.debugElement.nativeElement;
    let el = compiled.querySelector('button')
    // component.color = 'red';
    fixture.detectChanges();
    expect(el.style.backgroundColor).toBe('red');
  });
});
