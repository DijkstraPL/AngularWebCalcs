import { TestBed } from '@angular/core/testing';

import { CompanyViewModelFactory } from './company-view-model-factory.service';

describe('CompanyViewModelFactory', () => {
  let service: CompanyViewModelFactory;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CompanyViewModelFactory);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
