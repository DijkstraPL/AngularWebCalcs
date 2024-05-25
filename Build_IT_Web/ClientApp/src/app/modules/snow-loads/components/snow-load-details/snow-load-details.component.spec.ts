import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SnowLoadDetailsComponent } from './snow-load-details.component';

describe('SnowLoadDetailsComponent', () => {
  let component: SnowLoadDetailsComponent;
  let fixture: ComponentFixture<SnowLoadDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SnowLoadDetailsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SnowLoadDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
