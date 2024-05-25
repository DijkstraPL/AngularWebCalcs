import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BeamCalcComponent } from './beam-calc.component';

describe('BeamCalcComponent', () => {
  let component: BeamCalcComponent;
  let fixture: ComponentFixture<BeamCalcComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BeamCalcComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BeamCalcComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
