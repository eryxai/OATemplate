import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RolesViewComponent } from './roles-view.component';

describe('RolesViewComponent', () => {
  let component: RolesViewComponent;
  let fixture: ComponentFixture<RolesViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [RolesViewComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(RolesViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
