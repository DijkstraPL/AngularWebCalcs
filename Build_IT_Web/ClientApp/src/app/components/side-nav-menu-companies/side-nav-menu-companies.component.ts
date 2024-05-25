import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { AuthorizeService } from '@main/api-authorization/authorize.service';
import { CompanyRemoveDialogComponent } from '@main/app/modules/projects/components/company/company-remove-dialog/company-remove-dialog.component';
import { CompanyRepositoryService } from '@main/app/modules/projects/services/company-repository.service';
import { DeadLoadsRepositoryService } from '@main/app/modules/projects/services/dead-loads-repository.service';
import { ProjectsRepositoryService } from '@main/app/modules/projects/services/projects-repository.service';
import { CompanyViewModel } from '@main/app/modules/projects/view-models/company-view-model';
import { CurrentAppStateService } from '@main/app/services/current-app-state.service';
import { ActiveNavigationService } from '@shared/build-it-common/services/active-navigation.service';
import { NavigatorService } from '@shared/build-it-common/services/navigator.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-side-nav-menu-companies',
  templateUrl: './side-nav-menu-companies.component.html',
  styleUrls: ['./side-nav-menu-companies.component.scss']
})
export class SideNavMenuCompaniesComponent implements OnInit {
  private _companies$: Observable<CompanyViewModel[]> | undefined;
  public get companies$(): Observable<CompanyViewModel[]> {
    return this._companies$ ?? new Observable<CompanyViewModel[]>();
  }
  public get selectedCompanyId() {
    return this._activeNavigation.selectedCompanyId ?? 0;
  }

  constructor(
    private readonly _companyRepository: CompanyRepositoryService,
    private readonly _navigator: NavigatorService,
    private readonly _authorizeService: AuthorizeService,
    private readonly _activeNavigation: ActiveNavigationService,
    private readonly _currentAppState: CurrentAppStateService,
    private readonly _dialog: MatDialog) { }

  ngOnInit(): void {
     this.setCompanies();
    this._authorizeService
      .isAuthenticated()
      .subscribe((isAuthenticated) => this.setCompanies());
    this._currentAppState.newCompanyCreated.subscribe((companyId) =>
      this.setCompanies()
    );
    this._currentAppState.companyUpdated.subscribe((companyId) =>
      this.setCompanies()
    );
    this._currentAppState.companyDeleted.subscribe((companyId) =>
      this.setCompanies()
    );
  }

  private setCompanies() {
    this._companies$ = this._companyRepository.provideCompanies$();
  }

  companyClicked(companyId: number) {
    this._navigator.navigateToCompany(companyId);
  }

  addNewCompany() {
    this._navigator.navigateToCompanyCreation();
  }

  onEditCompany(companyMenu: any) {
    this._navigator.navigateToCompanyEdit(companyMenu.data.id);
  }
  onRemoveCompany(companyMenu: any) {
    const dialogRef = this._dialog.open(CompanyRemoveDialogComponent, {
      data: {
        companyName: companyMenu.data.name,
        comapnyId: companyMenu.data.id,
      },
    });

    dialogRef.afterClosed().subscribe(async (result) => {
      if (result) {
        await this._companyRepository.removeCompany(companyMenu.data.id);
        this._currentAppState.companyDeleted.emit(companyMenu.data.id);
        this._navigator.navigateToHome();
      }
    });
  }
}
