import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SideNavMenuDeadLoadsComponent } from './side-nav-menu-dead-loads.component';

describe('SideNavMenuDeadLoadsComponent', () => {
  let component: SideNavMenuDeadLoadsComponent;
  let fixture: ComponentFixture<SideNavMenuDeadLoadsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SideNavMenuDeadLoadsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SideNavMenuDeadLoadsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
