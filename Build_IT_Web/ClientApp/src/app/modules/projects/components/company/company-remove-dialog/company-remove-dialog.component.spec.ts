import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompanyRemoveDialogComponent } from './company-remove-dialog.component';

describe('CompanyRemoveDialogComponent', () => {
  let component: CompanyRemoveDialogComponent;
  let fixture: ComponentFixture<CompanyRemoveDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CompanyRemoveDialogComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CompanyRemoveDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
