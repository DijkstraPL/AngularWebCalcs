import { TestBed } from '@angular/core/testing';

import { DeadLoadViewModelFactory } from './dead-load-view-model-factory.service';

describe('DeadLoadViewModelFactory', () => {
  let service: DeadLoadViewModelFactory;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DeadLoadViewModelFactory);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
