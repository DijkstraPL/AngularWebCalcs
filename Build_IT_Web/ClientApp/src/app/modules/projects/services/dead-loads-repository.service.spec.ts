import { TestBed } from '@angular/core/testing';

import { DeadLoadsRepositoryService } from './dead-loads-repository.service';

describe('DeadLoadsRepositoryService', () => {
  let service: DeadLoadsRepositoryService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DeadLoadsRepositoryService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
