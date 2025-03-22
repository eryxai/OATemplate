import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExperienceViewComponent } from './experience-view.component';

describe('ExperienceViewComponent', () => {
  let component: ExperienceViewComponent;
  let fixture: ComponentFixture<ExperienceViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExperienceViewComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ExperienceViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
