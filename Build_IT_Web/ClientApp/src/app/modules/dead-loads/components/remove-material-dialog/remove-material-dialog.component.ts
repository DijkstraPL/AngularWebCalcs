import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { LayeredMaterialViewModel } from '../../view-models/layered-material-view-model';

@Component({
  selector: 'app-remove-material-dialog',
  templateUrl: './remove-material-dialog.component.html',
  styleUrls: ['./remove-material-dialog.component.css']
})
export class RemoveMaterialDialogComponent implements OnInit {

  constructor(public dialogRef: MatDialogRef<RemoveMaterialDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public material: LayeredMaterialViewModel,
    ) {}


  ngOnInit(): void {
  }

}
