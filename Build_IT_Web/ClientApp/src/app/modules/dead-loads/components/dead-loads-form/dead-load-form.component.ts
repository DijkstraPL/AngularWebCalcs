import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { CurrentAppStateService } from '@main/app/services/current-app-state.service';
import { CreateDeadLoadCommand } from '@shared/build-it-common/services/client';
import { NavigatorService } from '@shared/build-it-common/services/navigator.service';
import { ActiveNavigationService } from '@shared/build-it-common/services/active-navigation.service';
import { DeadLoadsRepositoryService } from '../../../projects/services/dead-loads-repository.service';

@Component({
  selector: 'app-dead-load-form',
  templateUrl: './dead-load-form.component.html',
  styleUrls: ['./dead-load-form.component.css']
})
export class DeadLoadFormComponent implements OnInit {

  deadLoadForm = new FormGroup({
    name: new FormControl('',Validators.required),
  });

  constructor(private readonly _deadLoadRepositoryService: DeadLoadsRepositoryService,
    private readonly _currentAppState : CurrentAppStateService,
    private readonly _activeNavigationService : ActiveNavigationService,
    private readonly _navigator : NavigatorService) { }

  ngOnInit(): void {
  }

  public async onSubmit(){
    let deadLoadData = this.deadLoadForm.value;
    if(!deadLoadData.name)
     throw ('You are not able to create a project with empty name.');

    let deadLoad = new CreateDeadLoadCommand()
    deadLoad.name = deadLoadData.name;
    if(this._activeNavigationService.selectedCompanyId && this._activeNavigationService.selectedProjectId)
     await this._deadLoadRepositoryService.createDeadLoad(this._activeNavigationService.selectedCompanyId, this._activeNavigationService.selectedProjectId, deadLoad);


    // TODO: add this event and nav
    // this._currentAppState.newCompanyCreated.emit(newId);
    // this._navigator.navigateToCompany(newId);
  }
}
