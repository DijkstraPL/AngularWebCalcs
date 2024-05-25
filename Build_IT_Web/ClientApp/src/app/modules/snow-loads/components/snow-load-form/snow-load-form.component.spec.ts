import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SnowLoadFormComponent } from './snow-load-form.component';

describe('SnowLoadFormComponent', () => {
  let component: SnowLoadFormComponent;
  let fixture: ComponentFixture<SnowLoadFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SnowLoadFormComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SnowLoadFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
