import { TestBed } from '@angular/core/testing';

import { DeadLoadsService } from './dead-loads.service';

describe('DeadLoadsService', () => {
  let service: DeadLoadsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DeadLoadsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
