import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RemoveMaterialDialogComponent } from './remove-material-dialog.component';

describe('RemoveMaterialDialogComponent', () => {
  let component: RemoveMaterialDialogComponent;
  let fixture: ComponentFixture<RemoveMaterialDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RemoveMaterialDialogComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RemoveMaterialDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
