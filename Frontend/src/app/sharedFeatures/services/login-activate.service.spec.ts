import { TestBed } from '@angular/core/testing';

import { LoginActivate } from './login-activate.service';

describe('LoginActivateService', () => {
  let service: LoginActivate;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LoginActivate);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
