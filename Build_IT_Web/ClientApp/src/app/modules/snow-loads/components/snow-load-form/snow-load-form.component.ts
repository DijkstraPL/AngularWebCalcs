import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { SnowLoadsRepositoryService } from '../../services/snow-loads-repository.service';
import { CurrentAppStateService } from '@main/app/services/current-app-state.service';
import { ActiveNavigationService } from '@shared/build-it-common/services/active-navigation.service';
import { NavigatorService } from '@shared/build-it-common/services/navigator.service';
import { CreateSnowLoadCommand, RoofType } from '@shared/build-it-common/services/client';

@Component({
  selector: 'app-snow-load-form',
  templateUrl: './snow-load-form.component.html',
  styleUrls: ['./snow-load-form.component.css']
})
export class SnowLoadFormComponent implements OnInit {
  public get monopitch() {
  return  "Monopitch";
}
public get pitched() {
  return  "Pitched";
}
public get multiSpan() {
  return  "MultiSpan";
}
public get cylindrical() {
  return  "Cylindrical";
}
public get roofAbuttingToTallerConstruction() {
  return  "RoofAbuttingToTallerConstruction";
}
public get snowguards() {
  return  "Snowguards";
}
public get snowOverhanging() {
  return  "SnowOverhanging";
}
public get driftingAtProjectionsObstructions() {
  return  "DriftingAtProjectionsObstructions";
}

public RoofTypeEnum  = RoofType;

 snowLoadForm = new FormGroup({
    name: new FormControl('',Validators.required),
    roofType: new FormControl(RoofType.None,Validators.required),
  });

  constructor(private readonly _snowLoadRepositoryService: SnowLoadsRepositoryService,
    private readonly _currentAppState : CurrentAppStateService,
    private readonly _activeNavigationService : ActiveNavigationService,
    private readonly _navigator : NavigatorService) { }

  ngOnInit(): void {
  }

  public async onSubmit(){
    let snowLoadData = this.snowLoadForm.value;
    if(!snowLoadData.name)
     throw ('You are not able to create a snow load with empty name.');
     if(!snowLoadData.roofType)
      throw ('You are not able to create a snow load with no roof type selected.');

    let snowLoad = new CreateSnowLoadCommand()
    snowLoad.name = snowLoadData.name;
    snowLoad.roofType = snowLoadData.roofType;
    if(this._activeNavigationService.selectedCompanyId && this._activeNavigationService.selectedProjectId)
     await this._snowLoadRepositoryService.createSnowLoad(this._activeNavigationService.selectedCompanyId, this._activeNavigationService.selectedProjectId, snowLoad);


    // TODO: add this event and nav
    // this._currentAppState.newCompanyCreated.emit(newId);
    // this._navigator.navigateToCompany(newId);
  }
}
