import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { CompanyRemoveDialogComponent } from '@main/app/modules/projects/components/company/company-remove-dialog/company-remove-dialog.component';
import { CurrentAppStateService } from '@main/app/services/current-app-state.service';
import { LoggerService } from '@shared/build-it-common/services/logger.service';
import { NavigatorService } from '@shared/build-it-common/services/navigator.service';
import { Observable } from 'rxjs';
import { CompanyViewModelFactory } from '../../modules/projects/factories/company-view-model-factory.service';
import { CompanyRepositoryService } from '../../modules/projects/services/company-repository.service';
import { CompanyViewModel } from '../../modules/projects/view-models/company-view-model';

@Component({
  selector: 'app-company-list',
  templateUrl: './company-list.component.html',
  styleUrls: ['./company-list.component.css'],
})
export class CompanyListComponent implements OnInit {
  private  _companyViewModels!: Observable< CompanyViewModel[]>;

  public get companyViewModels$(): Observable<CompanyViewModel[]> {
    return this._companyViewModels;
  }

  constructor(
    private readonly _companyRepository: CompanyRepositoryService,
    private readonly _loggerService: LoggerService,
    private readonly _navigator: NavigatorService,
    private readonly _currentAppState: CurrentAppStateService,
    private readonly _dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.setCompanies();
    this._currentAppState.companyDeleted.subscribe(() =>
      this.setCompanies());
  }
  setCompanies(): void {
    this._companyViewModels = this._companyRepository.provideCompanies$();
    }

  openDetailsFor(companyId: number): void {
    this._navigator.navigateToCompany(companyId);
  }

  onEditClicked(company: CompanyViewModel) {
    this._navigator.navigateToCompanyEdit(company.id);
  }
  onRemoveClicked(company: CompanyViewModel, $event : MouseEvent) {
    $event.cancelBubble = true;
    $event.preventDefault();

    const dialogRef = this._dialog.open(CompanyRemoveDialogComponent, {
      data: { companyName: company.name, comapnyId: company.id },
    });

    dialogRef.afterClosed().subscribe(async (result) => {
      if (result) {
        await this._companyRepository.removeCompany(company.id);
        this._currentAppState.companyDeleted.emit(company.id);
        this._navigator.navigateToHome();
      }
    });
  }
  createNewCompanyButtonClicked($event: any){
    this._navigator.navigateToCompanyCreation();
  }
}
