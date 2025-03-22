import { TestBed } from '@angular/core/testing';
import { DialogService } from 'primeng/dynamicdialog';

import { DynamicDialogService } from './dynamic-dialog.service';

describe('DynamicDialogService', () => {
  let service: DynamicDialogService;

  beforeEach(() => {
    const spy = jasmine.createSpyObj('DialogService', ['open']);
    TestBed.configureTestingModule({
      providers: [
        DynamicDialogService,
        {
          provide: DialogService,
          useValue: spy,
        },
      ],
    });
    service = TestBed.inject(DynamicDialogService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
