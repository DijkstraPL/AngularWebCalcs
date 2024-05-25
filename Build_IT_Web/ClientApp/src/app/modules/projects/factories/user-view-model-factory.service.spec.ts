import { TestBed } from '@angular/core/testing';

import { UserViewModelFactoryService } from './user-view-model-factory.service';

describe('UserViewModelFactoryService', () => {
  let service: UserViewModelFactoryService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(UserViewModelFactoryService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
