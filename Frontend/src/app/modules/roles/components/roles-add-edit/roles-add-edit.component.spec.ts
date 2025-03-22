import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RolesAddEditComponent } from './roles-add-edit.component';

describe('RolesAddEditComponent', () => {
  let component: RolesAddEditComponent;
  let fixture: ComponentFixture<RolesAddEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [RolesAddEditComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(RolesAddEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
