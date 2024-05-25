import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeadLoadsSummaryComponent } from './dead-loads-summary.component';

describe('DeadLoadsSummaryComponent', () => {
  let component: DeadLoadsSummaryComponent;
  let fixture: ComponentFixture<DeadLoadsSummaryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeadLoadsSummaryComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeadLoadsSummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
