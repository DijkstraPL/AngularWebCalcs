import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { BeamCalcComponent } from './beam-calc.component';
import { BuildItCommonModule } from '@shared/build-it-common/build-it-common.module';

const routes: Routes = [
  { path: '', component: BeamCalcComponent }
];

@NgModule({
  declarations: [
    BeamCalcComponent
  ],
  imports: [
    RouterModule.forChild(routes)
  ]
})
export class BeamCalcModule { }
