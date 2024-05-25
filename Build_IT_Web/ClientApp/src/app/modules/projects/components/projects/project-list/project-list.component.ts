import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { CurrentAppStateService } from '@main/app/services/current-app-state.service';
import { LoggerService } from '@shared/build-it-common/services/logger.service';
import { NavigatorService } from '@shared/build-it-common/services/navigator.service';
import { CompanyRepositoryService } from '../../../services/company-repository.service';
import { ProjectsRepositoryService } from '../../../services/projects-repository.service';
import { ProjectViewModel } from '../../../view-models/project-view-model';
import { ProjectRemoveDialogComponent } from '../project-remove-dialog/project-remove-dialog.component';
import { RightMouseMenuDirective } from '@shared/build-it-common/directives/right-mouse-menu.directive';

@Component({
  selector: 'app-project-list',
  templateUrl: './project-list.component.html',
  styleUrls: ['./project-list.component.scss'],
})
export class ProjectListComponent implements OnInit {
  private readonly _projectViewModels: ProjectViewModel[] = [];

  public get projectViewModels(): ProjectViewModel[] {
    return this._projectViewModels;
  }
  private set projectViewModels(categoryViewModels: ProjectViewModel[]) {
    this._projectViewModels.splice(0);
    this._projectViewModels.push(...categoryViewModels);
  }

  public get topProjectViewModels(): ProjectViewModel[] {
    return this.projectViewModelsOrdered.slice(0, 3);
  }
  public get projectViewModelsOrdered(): ProjectViewModel[] {
    return this.projectViewModels.sort(
      (a, b) =>
        new Date(b.lastModified).getTime() - new Date(a.lastModified).getTime()
    );
  }

  private _selectedCompanyId: number = 0;

  constructor(
    private readonly _projectsRepositoryService: ProjectsRepositoryService,
    private readonly _companyRepository: CompanyRepositoryService,
    private readonly _navigator: NavigatorService,
    private readonly _activatedRoute: ActivatedRoute,
    private readonly _currentAppState: CurrentAppStateService,
    private readonly _dialog: MatDialog,
    private readonly _loggerService: LoggerService
  ) {}

  ngOnInit(): void {
    this._activatedRoute.params.subscribe(
      (params) => {
        this._selectedCompanyId = params.companyId;
        if (!this._selectedCompanyId) this._navigator.navigateToHome();
      },
      (er) => {
        this._navigator.navigateToHome();
      }
    );

    this._companyRepository.provideCompany$(this._selectedCompanyId).subscribe(
      (c) => {},
      (err) => {
        if (err.status === 404) this._navigator.navigateToNotFound();
        else console.error(err);
      },
      () => {}
    );

    this.setProjects();

    this._currentAppState.selectedCompanyChanged.subscribe((companyId) =>
      this.setProjects()
    );
    this._currentAppState.projectDeleted.subscribe((projectId) =>
      this.setProjects()
    );
  }

  private setProjects() {
    this._projectsRepositoryService
      .provideProjectsForCompany$(this._selectedCompanyId)
      .subscribe(
        (projects) => {
          if (projects !== null) this.projectViewModels = projects;
        },
        (err) => this._loggerService.error(err),
        () => this._loggerService.log('Data loaded')
      );
  }

  openDetailsFor(projectId: number): void {
    this._navigator.navigateToProject(8, projectId);
  }

  onRemoveClicked(project: ProjectViewModel, $event: MouseEvent) {
    $event.preventDefault();

    const dialogRef = this._dialog.open(ProjectRemoveDialogComponent, {
      data: { projectName: project.name, projectId: project.id },
    });

    dialogRef.afterClosed().subscribe(async (result) => {
      if (result) {
        await this._projectsRepositoryService.deleteProject(project.id);
        this._currentAppState.projectDeleted.emit(project.id);
      }
    });
  }
  onEditClicked(project: ProjectViewModel) {
    this._navigator.navigateToProjectEdit(this._selectedCompanyId, project.id);
  }
  onEditProject(projectMenu: any) {
    this._navigator.navigateToProjectEdit(
      this._selectedCompanyId,
      projectMenu.data.id
    );
  }
  onRemoveProject(projectMenu: any) {
    const dialogRef = this._dialog.open(ProjectRemoveDialogComponent, {
      data: {
        projectName: projectMenu.data.name,
        projectId: projectMenu.data.id,
      },
    });

    dialogRef.afterClosed().subscribe(async (result) => {
      if (result) {
        await this._projectsRepositoryService.deleteProject(projectMenu.data.id);
        this._currentAppState.projectDeleted.emit(projectMenu.data.id);
        this._navigator.navigateToHome();
      }
    });
  }
}
