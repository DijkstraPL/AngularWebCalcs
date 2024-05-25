import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AngularMaterialModule } from '@shared/angular-material/angular-material.module';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ProjectListComponent } from './components/projects/project-list/project-list.component';
import { ProjectFormComponent } from './components/projects/project-form/project-form.component';
import { ProjectsRepositoryService } from './services/projects-repository.service';
import { ProjectViewModelFactory } from './factories/project-view-model-factory.service';
import { FactoryService } from '@shared/build-it-common/factories/factory-service';
import { ProjectViewModel } from './view-models/project-view-model';
import { CompanyResource, DeadLoadResource, ProjectResource } from '@shared/build-it-common/services/client';
import { CompanyFormComponent } from './components/company/company-form/company-form.component';
import { CompanyRepositoryService } from './services/company-repository.service';
import { CompanyViewModel } from './view-models/company-view-model';
import { CompanyViewModelFactory } from './factories/company-view-model-factory.service';
import { ProjectDetailsComponent } from './components/projects/project-details/project-details.component';
import { DeadLoadViewModelFactory } from './factories/dead-load-view-model-factory.service';
import { DeadLoadViewModel } from './view-models/dead-load-view-model';
import { CompanyDataComponent } from './components/company/company-data/company-data.component';
import { CompanyEditComponent } from './components/company/company-edit/company-edit.component';
import { CompanyRemoveDialogComponent } from './components/company/company-remove-dialog/company-remove-dialog.component';
import { CompanyHeaderComponent } from './components/company/company-header/company-header.component';
import { UserListComponent } from './components/user-list/user-list.component';
import { ProjectRemoveDialogComponent } from './components/projects/project-remove-dialog/project-remove-dialog.component';
import { ProjectEditComponent } from './components/projects/project-edit/project-edit.component';
import { BuildItCommonModule } from '@shared/build-it-common/build-it-common.module';
import { ClaimRepositoryService } from './services/claim-repository.service';
import { ClaimViewModelFactoryService } from './factories/claim-view-model-factory.service';

const routes: Routes = [
  // { path: '', component: CompanyFormComponent },
  { path: '', component: CompanyDataComponent },
  { path: 'create', component: ProjectFormComponent },
  { path: 'edit', component: CompanyEditComponent },
  { path: ':projectId', component: ProjectDetailsComponent },
  { path: ':projectId/edit', component: ProjectEditComponent },
];


@NgModule({
  declarations: [
    ProjectListComponent,
    ProjectFormComponent,
    CompanyFormComponent,
    ProjectDetailsComponent,
    CompanyDataComponent,
    CompanyEditComponent,
    CompanyRemoveDialogComponent,
    CompanyHeaderComponent,
    UserListComponent,
    ProjectRemoveDialogComponent,
    ProjectEditComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    BuildItCommonModule,
    ReactiveFormsModule,
    AngularMaterialModule,
    RouterModule.forChild(routes),
  ],
  providers:[
        ProjectsRepositoryService,
        CompanyRepositoryService,
        ClaimRepositoryService,
        ProjectViewModelFactory,
        CompanyViewModelFactory,
        DeadLoadViewModelFactory,
        ClaimViewModelFactoryService,
        {provide: FactoryService<ProjectResource ,ProjectViewModel>, useClass: ProjectViewModelFactory},
        {provide: FactoryService<CompanyResource ,CompanyViewModel>, useClass: CompanyViewModelFactory},
        {provide: FactoryService<DeadLoadResource ,DeadLoadViewModel>, useClass: DeadLoadViewModelFactory},
  ]
})
export class ProjectsModule { }
