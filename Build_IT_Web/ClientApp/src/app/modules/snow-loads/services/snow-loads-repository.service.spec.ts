import { TestBed } from '@angular/core/testing';

import { SnowLoadsRepositoryService } from './snow-loads-repository.service';

describe('SnowLoadsRepositoryService', () => {
  let service: SnowLoadsRepositoryService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SnowLoadsRepositoryService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
