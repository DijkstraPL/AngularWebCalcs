import { TestBed } from '@angular/core/testing';

import { CurrentAppStateService } from './current-app-state.service';

describe('CurrentAppStateService', () => {
  let service: CurrentAppStateService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CurrentAppStateService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
