import { TestBed } from '@angular/core/testing';

import { GeneralAttachmentService } from './general-attachment.service';

describe('GeneralAttachmentService', () => {
  let service: GeneralAttachmentService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GeneralAttachmentService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
