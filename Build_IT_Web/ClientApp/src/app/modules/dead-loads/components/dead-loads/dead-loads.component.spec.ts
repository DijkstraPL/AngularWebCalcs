import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeadLoadsComponent } from './dead-loads.component';

describe('DeadLoadsComponent', () => {
  let component: DeadLoadsComponent;
  let fixture: ComponentFixture<DeadLoadsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeadLoadsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeadLoadsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
