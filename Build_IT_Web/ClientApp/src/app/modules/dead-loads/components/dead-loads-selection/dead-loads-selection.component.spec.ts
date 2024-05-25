import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeadLoadsSelectionComponent } from './dead-loads-selection.component';

describe('DeadLoadsSelectionComponent', () => {
  let component: DeadLoadsSelectionComponent;
  let fixture: ComponentFixture<DeadLoadsSelectionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeadLoadsSelectionComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeadLoadsSelectionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
