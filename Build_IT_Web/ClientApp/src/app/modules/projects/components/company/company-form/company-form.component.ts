import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { CurrentAppStateService } from '@main/app/services/current-app-state.service';
import { CreateCompanyCommand } from '@shared/build-it-common/services/client';
import { NavigatorService } from '@shared/build-it-common/services/navigator.service';
import { CompanyRepositoryService } from '../../../services/company-repository.service';

@Component({
  selector: 'app-company-form',
  templateUrl: './company-form.component.html',
  styleUrls: ['./company-form.component.css']
})
export class CompanyFormComponent implements OnInit {

  companyForm = new FormGroup({
    name: new FormControl('',Validators.required),
  });

  constructor(private readonly _companyRepositoryService: CompanyRepositoryService,
    private readonly _currentAppState : CurrentAppStateService,
    private readonly _navigator : NavigatorService) { }

  ngOnInit(): void {
  }

  public async onSubmit(){
    let companyData = this.companyForm.value;
    if(!companyData.name)
     throw ('You are not able to create a project with empty name.');

    let company = new CreateCompanyCommand()
    company.name = companyData.name;
    let newId = await this._companyRepositoryService.createCompany(company);

    this._currentAppState.newCompanyCreated.emit(newId);
    this._navigator.navigateToCompany(newId);
  }
}
