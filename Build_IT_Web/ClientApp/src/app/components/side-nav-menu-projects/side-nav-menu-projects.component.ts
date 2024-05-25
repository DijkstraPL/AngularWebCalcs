import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { AuthorizeService } from '@main/api-authorization/authorize.service';
import { ProjectRemoveDialogComponent } from '@main/app/modules/projects/components/projects/project-remove-dialog/project-remove-dialog.component';
import { CompanyRepositoryService } from '@main/app/modules/projects/services/company-repository.service';
import { DeadLoadsRepositoryService } from '@main/app/modules/projects/services/dead-loads-repository.service';
import { ProjectsRepositoryService } from '@main/app/modules/projects/services/projects-repository.service';
import { ProjectViewModel } from '@main/app/modules/projects/view-models/project-view-model';
import { CurrentAppStateService } from '@main/app/services/current-app-state.service';
import { ActiveNavigationService } from '@shared/build-it-common/services/active-navigation.service';
import { NavigatorService } from '@shared/build-it-common/services/navigator.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-side-nav-menu-projects',
  templateUrl: './side-nav-menu-projects.component.html',
  styleUrls: ['./side-nav-menu-projects.component.scss']
})
export class SideNavMenuProjectsComponent implements OnInit {
  private _projects$: Observable<ProjectViewModel[]> | undefined;
  public get projects$(): Observable<ProjectViewModel[]> {
    return this._projects$ ?? new Observable<ProjectViewModel[]>();
  }

  public get selectedCompanyId() {
    return this._activeNavigation.selectedCompanyId ?? 0;
  }
  public get selectedProjectId() {
    return this._activeNavigation.selectedProjectId ?? 0;
  }
  constructor(
    private readonly _projectRepository: ProjectsRepositoryService,
    private readonly _navigator: NavigatorService,
    private readonly _activeNavigation: ActiveNavigationService,
    private readonly _currentAppState: CurrentAppStateService,
    private readonly _dialog: MatDialog) { }

  ngOnInit(): void {
    this._currentAppState.projectDeleted.subscribe((projectId) =>
      this.setProjects()
    );

    this._currentAppState.selectedCompanyChanged.subscribe((companyId) => {
      this.setProjects();
    });
    this.setProjects();
  }

  private setProjects() {
    this._projects$ = this._projectRepository.provideProjectsForCompany$(
      this.selectedCompanyId ?? 0
    );
  }

  async projectClicked(projectId: number) {
    if (this.selectedCompanyId)
      this._navigator.navigateToProject(this.selectedCompanyId, projectId);
  }

  addNewProject() {
    this._navigator.navigateToProjectCreation(this.selectedCompanyId ?? 0);
  }

  onEditProject(projectMenu: any) {
    this._navigator.navigateToProjectEdit(
      this.selectedCompanyId,
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
        await this._projectRepository.deleteProject(projectMenu.data.id);
        this._currentAppState.projectDeleted.emit(projectMenu.data.id);
        this._navigator.navigateToHome();
      }
    });
  }
}
