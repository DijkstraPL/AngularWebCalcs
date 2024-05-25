import { Component, OnInit, ViewChild } from '@angular/core';
import { MatMenuTrigger } from '@angular/material/menu';
import { CompanyRepositoryService } from '@companies/services/company-repository.service';
import { AuthorizeGuard } from '@main/api-authorization/authorize.guard';
import { AuthorizeService } from '@main/api-authorization/authorize.service';
import { CompanyRemoveDialogComponent } from '@companies/components/company/company-remove-dialog/company-remove-dialog.component';
import { CompanyViewModelFactory } from '@main/app/modules/projects/factories/company-view-model-factory.service';
import { ProjectViewModelFactory } from '@main/app/modules/projects/factories/project-view-model-factory.service';
import { DeadLoadsRepositoryService } from '@companies/services/dead-loads-repository.service';
import { ProjectsRepositoryService } from '@companies/services/projects-repository.service';
import { CompanyViewModel } from '@companies/view-models/company-view-model';
import { DeadLoadViewModel } from '@companies/view-models/dead-load-view-model';
import { ProjectViewModel } from '@companies/view-models/project-view-model';
import { CurrentAppStateService } from '@main/app/services/current-app-state.service';
import { NavigatorService } from '@shared/build-it-common/services/navigator.service';
import { Observable, of } from 'rxjs';
import { RightMouseMenuDirective } from '../../modules/shared/build-it-common/directives/right-mouse-menu.directive';
import { ActiveNavigationService } from '../../modules/shared/build-it-common/services/active-navigation.service';
import { MatDialog } from '@angular/material/dialog';
import { ProjectRemoveDialogComponent } from '@main/app/modules/projects/components/projects/project-remove-dialog/project-remove-dialog.component';

@Component({
  selector: 'build-it-side-nav-menu',
  templateUrl: 'side-nav-menu.component.html',
  styleUrls: ['./side-nav-menu.component.scss'],
})
export class SideNavMenuComponent implements OnInit {


  public get selectedSideMenu() {
    return this._currentAppState.selectedSideMenu;
  }

  constructor(
    private readonly _currentAppState: CurrentAppStateService,
  ) {}

  ngOnInit() {
  }


}
