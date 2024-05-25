import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SnowLoadsRepositoryService } from './services/snow-loads-repository.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AngularMaterialModule } from '@shared/angular-material/angular-material.module';
import { RouterModule, Routes } from '@angular/router';
import { SnowLoadFormComponent } from './components/snow-load-form/snow-load-form.component';
import { SnowLoadDetailsComponent } from './components/snow-load-details/snow-load-details.component';


const routes: Routes = [
  // { path: '', component: SnowLoadsComponent },
  { path: 'create', component: SnowLoadFormComponent },
  { path: ':snowLoadId', component: SnowLoadDetailsComponent }
];

@NgModule({
  declarations: [
    SnowLoadFormComponent,
    SnowLoadDetailsComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    AngularMaterialModule,
    RouterModule.forChild(routes),
  ],
  providers: [
    SnowLoadsRepositoryService
  ]
})
export class SnowLoadsModule { }
