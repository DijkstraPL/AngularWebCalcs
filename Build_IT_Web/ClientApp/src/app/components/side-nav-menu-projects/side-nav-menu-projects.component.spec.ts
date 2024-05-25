import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SideNavMenuProjectsComponent } from './side-nav-menu-projects.component';

describe('SideNavMenuProjectsComponent', () => {
  let component: SideNavMenuProjectsComponent;
  let fixture: ComponentFixture<SideNavMenuProjectsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SideNavMenuProjectsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SideNavMenuProjectsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
