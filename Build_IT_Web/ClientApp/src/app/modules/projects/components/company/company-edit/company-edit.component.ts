import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { CurrentAppStateService } from '@main/app/services/current-app-state.service';
import { UpdateCompanyCommand } from '@shared/build-it-common/services/client';
import { NavigatorService } from '@shared/build-it-common/services/navigator.service';
import { CompanyRepositoryService } from '../../../services/company-repository.service';

@Component({
  selector: 'app-company-edit',
  templateUrl: './company-edit.component.html',
  styleUrls: ['./company-edit.component.css']
})
export class CompanyEditComponent implements OnInit {
  companyForm = new FormGroup({
    name: new FormControl('',Validators.required),
  });

  private _selectedCompanyId! : number ;
  public get selectedCompanyId(){
    return this._selectedCompanyId;
  }

  constructor(private readonly _companyRepository: CompanyRepositoryService,
    private readonly _currentAppState : CurrentAppStateService,
    private readonly _activatedRoute: ActivatedRoute,
    private readonly _navigator : NavigatorService) { }

  ngOnInit(): void {
    this._activatedRoute.params.subscribe(params => {
      this._selectedCompanyId = params.companyId;
      if(!this._selectedCompanyId)
        this._navigator.navigateToHome();

      this.setCompanyForm();
    }, er => { this._navigator.navigateToHome(); });
  }

  private setCompanyForm() {
        this._companyRepository.provideCompany$(this._selectedCompanyId).subscribe(c => {
            this.companyForm.controls['name'].setValue(c.name);
        },
            err => {
                if (err.status === 404)
                    this._navigator.navigateToNotFound();
                else
                    console.error(err);
            },
            () => { });
    }

  public async onSubmit(){
    let companyData = this.companyForm.value;
    if(!companyData.name)
     throw ('You are not able to edit a project with empty name.');

    let company = new UpdateCompanyCommand()
    company.name = companyData.name;
    company.id = this.selectedCompanyId;
    let companyId = await this._companyRepository.updateCompany(company);

    this._currentAppState.companyUpdated.emit(companyId);
    this._navigator.navigateToCompany(companyId);
  }
}
