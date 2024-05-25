import { Component, ElementRef, OnInit, ViewChild, ViewChildren } from '@angular/core';
import { MatDrawer } from '@angular/material/sidenav';
import { AuthorizeService } from '@main/api-authorization/authorize.service';
import { ActiveNavigationService } from '@shared/build-it-common/services/active-navigation.service';
import { Observable } from 'rxjs';
import { CurrentAppStateService } from './services/current-app-state.service';
import { ThemeService } from './services/theme.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  title = 'app';
  showFiller = false ;

  @ViewChild('drawer') drawer!: ElementRef<MatDrawer>;

  public get selectedSection(){
    return this._currentAppStateService.selectedSideMenu;
  }
  public get isOpened(){
    if (this.drawer instanceof MatDrawer)
    return this.drawer.opened;
    return false;
  }

  private _isAuthenticated! : Observable<boolean>;
  public get isAuthenticated$(){
    return this._isAuthenticated;
  }

  constructor(private readonly _themeService: ThemeService,
    public readonly _activeNavigationService : ActiveNavigationService,
    private readonly _currentAppStateService : CurrentAppStateService,
    private readonly _authorizationService : AuthorizeService){

  }
  ngOnInit(): void {
    this._themeService.setDefaultTheme();
    this._isAuthenticated = this._authorizationService.isAuthenticated();
  }

  async drawerToggle(sideMenu?:string ){
    let sideManuChanged = this._currentAppStateService.selectedSideMenu  !== sideMenu;
    if(!sideMenu)
      sideManuChanged = false;

    if (this.drawer instanceof MatDrawer){
      if(sideManuChanged){
        this.drawer.toggle(false);
        await new Promise(f => setTimeout(f, 500));
        if(sideMenu)
          this._currentAppStateService.selectedSideMenu  = sideMenu;
        this.drawer.toggle(true);
      }
      else
        this.drawer.toggle();
    }
  }

  }
