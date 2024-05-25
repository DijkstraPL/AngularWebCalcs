import { Component, OnInit } from '@angular/core';
import { MatIconRegistry } from '@angular/material/icon';
import { DomSanitizer } from '@angular/platform-browser';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { ActiveNavigationService } from '@shared/build-it-common/services/active-navigation.service';
import { NameProviderService } from '@shared/build-it-common/services/name-provider.service';
import { Observable } from 'rxjs';
import { ThemeOption } from 'src/app/models/theme-option';
import { ThemeService } from 'src/app/services/theme.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent implements OnInit {
  isExpanded = false;
  themeOptions$: Observable<Array<ThemeOption>> = this._themeService.getThemeOptions();

  public get companyId(): number | undefined {
    return this._activeNavigation.selectedCompanyId;
  }
  public get projectId(): number | undefined {
    return this._activeNavigation.selectedProjectId;
  }
  public get deadLoadId(): number | undefined {
    return this._activeNavigation.selectedDeadLoadId;
  }
  private _companyName: string | undefined;
  public get companyName(): string | undefined {
    return this._companyName;
  }
  private _projectName: string | undefined;
  public get projectName(): string | undefined {
    return this._projectName;
  }
  private _deadLoadName: string | undefined;
  public get deadLoadName(): string | undefined {
    return this._deadLoadName;
  }

  constructor(private readonly _themeService: ThemeService,
    private readonly _router: Router,
    private readonly _nameProvider: NameProviderService,
    private readonly _activeNavigation: ActiveNavigationService) {

  }
  ngOnInit(): void {
    this._router.events.subscribe(async p => {
      if (p instanceof NavigationEnd ){
        this._companyName = this.companyId ? await this._nameProvider.provideCompanyName(this.companyId) : undefined;
        this._projectName = this.projectId ? await this._nameProvider.provideProjectName(this.projectId) : undefined;
        this._deadLoadName = this.deadLoadId && this.companyId && this.projectId ? await this._nameProvider.provideDeadLoadName(this.companyId, this.projectId, this.deadLoadId) : undefined;
      }
    });
  }

  themeChangeHandler(themeToSet: string) {
    this._themeService.setTheme(themeToSet);
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
