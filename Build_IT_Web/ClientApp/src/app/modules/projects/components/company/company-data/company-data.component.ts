import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NavigatorService } from '@shared/build-it-common/services/navigator.service';
import { Observable } from 'rxjs';
import { CompanyRepositoryService } from '../../../services/company-repository.service';
import { CompanyViewModel } from '../../../view-models/company-view-model';

@Component({
  selector: 'app-company-data',
  templateUrl: './company-data.component.html',
  styleUrls: ['./company-data.component.css']
})
export class CompanyDataComponent implements OnInit {
  userForm = new FormGroup({
    email: new FormControl('',Validators.required),
  });

  private  _selectedCompanyId: number = 0;
  private _company$!: Observable<CompanyViewModel>;
  public get Company$() : Observable<CompanyViewModel> {
    return this._company$;
  }

  constructor(private readonly _companyRepository: CompanyRepositoryService,
    private readonly _activatedRoute : ActivatedRoute,
    private readonly _navigator : NavigatorService) { }

  ngOnInit(): void {
    this._activatedRoute.params.subscribe(params => {
      this._selectedCompanyId = params.companyId;
      this._company$ = this._companyRepository.provideCompany$(this._selectedCompanyId);
      if(!this._selectedCompanyId)
        this._navigator.navigateToHome();
    }, er => { this._navigator.navigateToHome(); });
  }

  public async onSubmitUserForm(){
    let userData = this.userForm.value;
    if(!userData.email)
     throw ('You are not able to assign user without email.');
    if(!this._selectedCompanyId)
    throw ('Company not provided.');
    await this._companyRepository.assignToCompany(this._selectedCompanyId, userData.email );
  }
}
