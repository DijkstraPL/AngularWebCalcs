import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SideNavMenuSnowLoadsComponent } from './side-nav-menu-snow-loads.component';

describe('SideNavMenuSnowLoadsComponent', () => {
  let component: SideNavMenuSnowLoadsComponent;
  let fixture: ComponentFixture<SideNavMenuSnowLoadsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SideNavMenuSnowLoadsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SideNavMenuSnowLoadsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
