import { TestBed } from '@angular/core/testing';

import { ClaimRepositoryService } from './claim-repository.service';

describe('ClaimRepositoryService', () => {
  let service: ClaimRepositoryService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ClaimRepositoryService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
