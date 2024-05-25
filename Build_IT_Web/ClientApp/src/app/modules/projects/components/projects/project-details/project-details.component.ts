import { Component, OnInit } from '@angular/core';
import { CurrentAppStateService } from '@main/app/services/current-app-state.service';
import { ActiveNavigationService } from '@shared/build-it-common/services/active-navigation.service';
import { Observable } from 'rxjs';
import { ProjectsRepositoryService } from '../../../services/projects-repository.service';
import { ProjectViewModel } from '../../../view-models/project-view-model';

@Component({
  selector: 'app-project-details',
  templateUrl: './project-details.component.html',
  styleUrls: ['./project-details.component.css']
})
export class ProjectDetailsComponent implements OnInit {

  private _currentProject$ : Observable<ProjectViewModel> | undefined;
  public get currentProject$() : Observable<ProjectViewModel> {
    return this._currentProject$ ?? new Observable<ProjectViewModel>();
  }

  constructor(private readonly _projectsRepository: ProjectsRepositoryService,
    private readonly _activeNavigation: ActiveNavigationService,
    private readonly _currentAppState : CurrentAppStateService) { }

  ngOnInit(): void {
    this.setProject();

    this._currentAppState.selectedProjectChanged.subscribe(projectId => this.setProject());
  }


  private setProject() {
    this._currentProject$ = this._projectsRepository.provideProject$(this._activeNavigation.selectedProjectId ?? 0);
  }
}
