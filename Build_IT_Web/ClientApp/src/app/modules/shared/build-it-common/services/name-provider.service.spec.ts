import { TestBed } from '@angular/core/testing';

import { NameProviderService } from './name-provider.service';

describe('NameProviderService', () => {
  let service: NameProviderService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(NameProviderService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
