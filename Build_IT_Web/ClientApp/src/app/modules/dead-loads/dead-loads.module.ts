import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { DeadLoadsComponent } from './components/dead-loads/dead-loads.component';
import { AngularMaterialModule } from '@shared/angular-material/angular-material.module';
import { BuildItCommonModule } from '@shared/build-it-common/build-it-common.module';
import { DeadLoadsRepositoryService } from './services/dead-loads-repository.service';
import { CategoryResultResource, MaterialResultResource, SubcategoryResultResource } from '@shared/build-it-common/services/client';
import { FactoryService } from './factories/factory-service';
import { DeadLoadsSelectionComponent } from './components/dead-loads-selection/dead-loads-selection.component';
import { DeadLoadsSummaryComponent } from './components/dead-loads-summary/dead-loads-summary.component';
import { DeadLoadsService } from './services/dead-loads.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { RemoveMaterialDialogComponent } from './components/remove-material-dialog/remove-material-dialog.component';
import { DeadLoadFormComponent } from './components/dead-loads-form/dead-load-form.component';
import { DeadLoadsDetailComponent } from './components/dead-loads-detail/dead-loads-detail.component';


const routes: Routes = [
  { path: '', component: DeadLoadsComponent },
  { path: 'create', component: DeadLoadFormComponent },
  { path: ':deadLoadId', component: DeadLoadsDetailComponent },
];

@NgModule({
  declarations: [
    DeadLoadsComponent,
    DeadLoadsSelectionComponent,
    DeadLoadsSummaryComponent,
    RemoveMaterialDialogComponent,
    DeadLoadFormComponent,
    DeadLoadsDetailComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    AngularMaterialModule,
    RouterModule.forChild(routes),
  ],
  providers:[
    DeadLoadsService,
    DeadLoadsRepositoryService
  ]
})
export class DeadLoadsModule { }
