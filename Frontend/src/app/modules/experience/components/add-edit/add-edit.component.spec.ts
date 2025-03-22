import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExperienceAddEditComponent } from './experience-add-edit.component';

describe('ExperienceAddEditComponent', () => {
  let component: ExperienceAddEditComponent;
  let fixture: ComponentFixture<ExperienceAddEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExperienceAddEditComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ExperienceAddEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
