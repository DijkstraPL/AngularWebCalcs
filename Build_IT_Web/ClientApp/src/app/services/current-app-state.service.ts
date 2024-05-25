import { EventEmitter, Injectable, OnInit } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ActiveNavigationService } from '../modules/shared/build-it-common/services/active-navigation.service';

@Injectable({
  providedIn: 'root'
})
export class CurrentAppStateService {
  private _selectedSideMenu : string = '';
  public get selectedSideMenu(): string{
    return this._selectedSideMenu;
  }
  public set selectedSideMenu(value: string){
    this._selectedSideMenu = value;
    this._selectedSideMenuChanged.emit(this.selectedSideMenu);
  }

  private _selectedCompanyChanged : EventEmitter<number> = new EventEmitter<number>();
  public get selectedCompanyChanged() : Observable<number>{
    return this._selectedCompanyChanged;
  }
  private _selectedSideMenuChanged : EventEmitter<string> = new EventEmitter<string>();
  public get selectedSideMenuChanged() : Observable<string>{
    return this._selectedSideMenuChanged;
  }

  private _selectedProjectChanged : EventEmitter<number> = new EventEmitter<number>();
  public get selectedProjectChanged() : Observable<number>{
    return this._selectedProjectChanged;
  }

  private _newCompanyCreated : EventEmitter<number> = new EventEmitter<number>();
  public get newCompanyCreated() : EventEmitter<number>{
    return this._newCompanyCreated;
  }
  private _companyUpdated : EventEmitter<number> = new EventEmitter<number>();
  public get companyUpdated() : EventEmitter<number>{
    return this._companyUpdated;
  }
  private _companyDeleted : EventEmitter<number> = new EventEmitter<number>();
  public get companyDeleted() : EventEmitter<number>{
    return this._companyDeleted;
  }

  private _projectDeleted : EventEmitter<number> = new EventEmitter<number>();
  public get projectDeleted() : EventEmitter<number>{
    return this._projectDeleted;
  }

  constructor( activeNavigation: ActiveNavigationService,
    private readonly _router: Router) {
    this._router.events.subscribe(async p => {
      if (p instanceof NavigationEnd && activeNavigation.selectedCompanyId) {
        this._selectedCompanyChanged.emit(activeNavigation.selectedCompanyId);
        if(activeNavigation.selectedProjectId)
        this._selectedProjectChanged.emit(activeNavigation.selectedProjectId);
      }
    });
  }
}
