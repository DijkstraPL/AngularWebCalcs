import { TestBed } from '@angular/core/testing';

import { ClaimViewModelFactoryService } from './claim-view-model-factory.service';

describe('ClaimViewModelFactoryService', () => {
  let service: ClaimViewModelFactoryService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ClaimViewModelFactoryService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
