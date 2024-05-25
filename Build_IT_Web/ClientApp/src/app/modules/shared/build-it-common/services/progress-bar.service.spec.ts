import { TestBed } from '@angular/core/testing';

import { ProgressBarService } from './progress-bar.service';

describe('ProgressBarService', () => {
  let service: ProgressBarService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ProgressBarService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should set value', () => {
    service.value = 5;
    expect(service.value).toBe(5);
  });

  it('should raise an event', () => {
    let invoked : boolean = false;
    service.valueChanged.subscribe(val => {
      invoked = true;
      });
    service.value = 5;
    expect(invoked).toBeTrue();
  });

  it('should reset a value', () => {
    service.value = 5;
    service.reset();
    expect(service.value).toBe(0);
  });
});
