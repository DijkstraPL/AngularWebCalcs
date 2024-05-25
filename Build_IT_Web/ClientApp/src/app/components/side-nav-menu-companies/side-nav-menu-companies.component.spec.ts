import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SideNavMenuCompaniesComponent } from './side-nav-menu-companies.component';

describe('SideNavMenuCompaniesComponent', () => {
  let component: SideNavMenuCompaniesComponent;
  let fixture: ComponentFixture<SideNavMenuCompaniesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SideNavMenuCompaniesComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SideNavMenuCompaniesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
