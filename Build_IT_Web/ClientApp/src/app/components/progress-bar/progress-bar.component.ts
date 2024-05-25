import { Component, OnInit } from '@angular/core';
import { ProgressBarService } from '@shared/build-it-common/services/progress-bar.service';

@Component({
  selector: 'build-it-progress-bar',
  templateUrl: './progress-bar.component.html',
  styleUrls: ['./progress-bar.component.css']
})
export class ProgressBarComponent implements OnInit {

private _progressBarValue: number = 0;

 public get progressBarValue() : number{
   return this._progressBarValue;
 }

 private set progressBarValue(newValue : number){
   this._progressBarValue = newValue;
}

  constructor(private readonly progressBarService: ProgressBarService) { }

  ngOnInit(): void {
    this.subscribeToProgressBarService();
  }


  private subscribeToProgressBarService() {
    this.progressBarService.valueChanged.subscribe(val => {
      this.progressBarValue = val;
    });
  }
}
