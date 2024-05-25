import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-project-remove-dialog',
  templateUrl: './project-remove-dialog.component.html',
  styleUrls: ['./project-remove-dialog.component.css']
})
export class ProjectRemoveDialogComponent {
  public get projectName(){
    return this._projectName ?? '';
  }
  public set projectName(value:string){
     this._projectName = value;
  }
  private _projectName: string | undefined;

  constructor(
    public dialogRef: MatDialogRef<ProjectRemoveDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData,
  ) {
  }

  onNoClick(): void {
    this.dialogRef.close();
  }
}

 export interface  DialogData{
  projectName: string;
  projectId: number;
}
