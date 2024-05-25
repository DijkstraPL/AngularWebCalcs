import { TestBed } from '@angular/core/testing';

import { ActiveNavigationService } from './active-navigation.service';

describe('ActiveNavigationService', () => {
  let service: ActiveNavigationService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ActiveNavigationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
