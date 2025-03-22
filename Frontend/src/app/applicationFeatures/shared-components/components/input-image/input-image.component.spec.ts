import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InputImageComponent } from './input-image.component';
import { TranslateModule, TranslateService } from '@ngx-translate/core';

describe('InputImageComponent', () => {
  let component: InputImageComponent;
  let fixture: ComponentFixture<InputImageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [InputImageComponent],
      imports: [TranslateModule.forRoot()],
      providers: [TranslateService],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(InputImageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('removeImage', () => {
    component.removeImage();
    expect(component.url).toBe('');
  });

  
});
