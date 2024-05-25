import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeadLoadFormComponent } from './dead-load-form.component';

describe('DeadLoadFormComponent', () => {
  let component: DeadLoadFormComponent;
  let fixture: ComponentFixture<DeadLoadFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeadLoadFormComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeadLoadFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
