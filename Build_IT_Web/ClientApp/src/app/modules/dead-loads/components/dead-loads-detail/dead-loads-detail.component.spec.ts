import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeadLoadsDetailComponent } from './dead-loads-detail.component';

describe('DeadLoadsDetailComponent', () => {
  let component: DeadLoadsDetailComponent;
  let fixture: ComponentFixture<DeadLoadsDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeadLoadsDetailComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeadLoadsDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
