import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { AuthorizeService } from '@main/api-authorization/authorize.service';
import { CompanyRepositoryService } from '@main/app/modules/projects/services/company-repository.service';
import { ProjectsRepositoryService } from '@main/app/modules/projects/services/projects-repository.service';
import { SnowLoadsRepositoryService } from '@main/app/modules/snow-loads/services/snow-loads-repository.service';
import { CurrentAppStateService } from '@main/app/services/current-app-state.service';
import { ActiveNavigationService } from '@shared/build-it-common/services/active-navigation.service';
import { SnowLoadResource } from '@shared/build-it-common/services/client';
import { NavigatorService } from '@shared/build-it-common/services/navigator.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-side-nav-menu-snow-loads',
  templateUrl: './side-nav-menu-snow-loads.component.html',
  styleUrls: ['./side-nav-menu-snow-loads.component.scss']
})
export class SideNavMenuSnowLoadsComponent implements OnInit {


  private _snowLoads$: Observable<SnowLoadResource[]> | undefined;
  public get snowLoads$(): Observable<SnowLoadResource[]> {
    return this._snowLoads$ ?? new Observable<SnowLoadResource[]>();
  }

  public get selectedCompanyId() {
    return this._activeNavigation.selectedCompanyId ?? 0;
  }
  public get selectedProjectId() {
    return this._activeNavigation.selectedProjectId ?? 0;
  }
  private _selectedSnowLoadId: number = 0;
  public get selectedSnowLoadId() {
    return this._selectedSnowLoadId ?? 0;
  }
  public set selectedSnowLoadId(value) {
    this._selectedSnowLoadId = value;
  }

  constructor(
    private readonly _companyRepository: CompanyRepositoryService,
    private readonly _projectRepository: ProjectsRepositoryService,
    private readonly _snowLoadsRepository: SnowLoadsRepositoryService,
    private readonly _navigator: NavigatorService,
    private readonly _authorizeService: AuthorizeService,
    private readonly _activeNavigation: ActiveNavigationService,
    private readonly _currentAppState: CurrentAppStateService,
    private readonly _dialog: MatDialog) { }

  ngOnInit(): void {
    this.setSnowLoads();
    this._currentAppState.selectedProjectChanged.subscribe((projectId) => {
      this.setSnowLoads();
    });
  }

  private setSnowLoads() {
    this._snowLoads$ = this._snowLoadsRepository.provideSnowLoads$(
      this.selectedCompanyId,
      this.selectedProjectId
    );
  }

  snowLoadClicked(snowLoadId: number) {
    this.selectedSnowLoadId = snowLoadId;
    this._navigator.navigateToSnowLoad(
      this.selectedCompanyId,
      this.selectedProjectId,
      snowLoadId
    );
  }

  addNewSnowLoad() {
    this._navigator.navigateToSnowLoadCreation(
      this.selectedCompanyId ?? 0,
      this.selectedProjectId ?? 0
    );
  }
}
