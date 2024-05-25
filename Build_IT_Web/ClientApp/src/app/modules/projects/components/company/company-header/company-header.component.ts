import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NavigatorService } from '@shared/build-it-common/services/navigator.service';

@Component({
  selector: 'app-company-header',
  templateUrl: './company-header.component.html',
  styleUrls: ['./company-header.component.css']
})
export class CompanyHeaderComponent implements OnInit {
  private  _selectedCompanyId: number = 0;

  constructor(private readonly _navigator : NavigatorService, private readonly _activatedRoute : ActivatedRoute) { }

  ngOnInit(): void {
     this._activatedRoute.params.subscribe(params => {
    this._selectedCompanyId = params.companyId;
    if(!this._selectedCompanyId)
      this._navigator.navigateToHome();
  }, er => { this._navigator.navigateToHome(); });
  }

  createNewProjectButtonClicked($event: any){
    this._navigator.navigateToProjectCreation(this._selectedCompanyId);
  }
}
