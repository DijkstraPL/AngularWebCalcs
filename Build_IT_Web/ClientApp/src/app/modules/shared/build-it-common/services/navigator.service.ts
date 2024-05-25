import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class NavigatorService {

  constructor(private readonly _router: Router) { }

  public navigateToHome(){
    this._router.navigate(['']);
  }
  public navigateToNotFound(){
    this._router.navigate(['not-found']);
  }

  public navigateToCompany(companyId: number){
    this._router.navigate([companyId]);
  }
  public navigateToCompanyCreation(){
    this._router.navigate(['create']);
  }
  public navigateToCompanyEdit(companyId: number){
    this._router.navigate([companyId + '/edit']);
  }

  public navigateToProjectCreation(companyId: number){
    this._router.navigate([companyId + '/create']);
  }
  public navigateToProjectEdit(companyId: number, projectId: number){
    this._router.navigate([`${companyId}/${projectId}/edit`]);
  }
  public navigateToProject(companyId: number, projectId: number){
    this._router.navigate([ companyId +"/" + projectId]);
  }

  public navigateToDeadLoadCreation(companyId: number, projectId : number){
    this._router.navigate([`${companyId}/${projectId}/dead-loads/create`])
  }
  navigateToDeadLoad(companyId: number, projectId : number, deadLoadId : number){
    this._router.navigate([`${companyId}/${projectId}/dead-loads/${deadLoadId}`])
  }

  public navigateToSnowLoadCreation(companyId: number, projectId : number){
    this._router.navigate([`${companyId}/${projectId}/snow-loads/create`])
  }
  navigateToSnowLoad(companyId: number, projectId : number, snowLoadId : number){
    this._router.navigate([`${companyId}/${projectId}/snow-loads/${snowLoadId}`])
  }

}
