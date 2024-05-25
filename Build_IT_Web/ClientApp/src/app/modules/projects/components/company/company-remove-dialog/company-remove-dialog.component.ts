import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-company-remove-dialog',
  templateUrl: './company-remove-dialog.component.html',
  styleUrls: ['./company-remove-dialog.component.css']
})
export class CompanyRemoveDialogComponent  {
  public get companyName(){
    return this._companyName ?? '';
  }
  public set companyName(value:string){
     this._companyName = value;
  }
  private _companyName: string | undefined;

  constructor(
    public dialogRef: MatDialogRef<CompanyRemoveDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData,
  ) {
  }

  onNoClick(): void {
    this.dialogRef.close();
  }
}

 export interface  DialogData{
  companyName: string;
  companyId: number;
}
