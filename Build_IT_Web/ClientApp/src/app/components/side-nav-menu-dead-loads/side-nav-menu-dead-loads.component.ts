import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { AuthorizeService } from '@main/api-authorization/authorize.service';
import { CompanyRepositoryService } from '@main/app/modules/projects/services/company-repository.service';
import { DeadLoadsRepositoryService } from '@main/app/modules/projects/services/dead-loads-repository.service';
import { ProjectsRepositoryService } from '@main/app/modules/projects/services/projects-repository.service';
import { DeadLoadViewModel } from '@main/app/modules/projects/view-models/dead-load-view-model';
import { CurrentAppStateService } from '@main/app/services/current-app-state.service';
import { ActiveNavigationService } from '@shared/build-it-common/services/active-navigation.service';
import { NavigatorService } from '@shared/build-it-common/services/navigator.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-side-nav-menu-dead-loads',
  templateUrl: './side-nav-menu-dead-loads.component.html',
  styleUrls: ['./side-nav-menu-dead-loads.component.scss']
})
export class SideNavMenuDeadLoadsComponent implements OnInit {

  private _deadLoads$: Observable<DeadLoadViewModel[]> | undefined;
  public get deadLoads$(): Observable<DeadLoadViewModel[]> {
    return this._deadLoads$ ?? new Observable<DeadLoadViewModel[]>();
  }

  public get selectedCompanyId() {
    return this._activeNavigation.selectedCompanyId ?? 0;
  }
  public get selectedProjectId() {
    return this._activeNavigation.selectedProjectId ?? 0;
  }
  private _selectedDeadLoadId: number = 0;
  public get selectedDeadLoadId() {
    return this._selectedDeadLoadId ?? 0;
  }
  public set selectedDeadLoadId(value) {
    this._selectedDeadLoadId = value;
  }

  constructor(
    private readonly _companyRepository: CompanyRepositoryService,
    private readonly _projectRepository: ProjectsRepositoryService,
    private readonly _deadLoadsRepository: DeadLoadsRepositoryService,
    private readonly _navigator: NavigatorService,
    private readonly _authorizeService: AuthorizeService,
    private readonly _activeNavigation: ActiveNavigationService,
    private readonly _currentAppState: CurrentAppStateService,
    private readonly _dialog: MatDialog) { }

  ngOnInit(): void {
    this.setDeadLoads();
    this._currentAppState.selectedProjectChanged.subscribe((projectId) => {
      this.setDeadLoads();
    });
  }

  private setDeadLoads() {
    this._deadLoads$ = this._deadLoadsRepository.provideDeadLoadsForProject$(
      this.selectedCompanyId,
      this.selectedProjectId
    );
  }

  deadLoadClicked(deadLoadId: number) {
    this.selectedDeadLoadId = deadLoadId;
    this._navigator.navigateToDeadLoad(
      this.selectedCompanyId,
      this.selectedProjectId,
      deadLoadId
    );
  }

  addNewDeadLoad() {
    this._navigator.navigateToDeadLoadCreation(
      this.selectedCompanyId ?? 0,
      this.selectedProjectId ?? 0
    );
  }
}
