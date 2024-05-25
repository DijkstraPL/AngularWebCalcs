import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ThemeOption } from '../models/theme-option';
import { StyleManagerService } from './style-manager.service';

@Injectable({
  providedIn: 'root'
})
export class ThemeService {

  static storageKey = 'theme-storage-current-name';

  constructor(
    private readonly _http: HttpClient, private readonly _styleManager: StyleManagerService
  ) {}

  public getThemeOptions(): Observable<Array<ThemeOption>> {
    return this._http.get<Array<ThemeOption>>("assets/theme-options.json");
  }

  public setTheme(themeToSet:string) {
    this._styleManager.setStyle(
      "theme",
      `assets/${themeToSet}.css`
    );
    this.storeTheme(themeToSet);
  }

  public setDefaultTheme(){
    let localTheme = this.getStoredThemeName();
    this.setTheme(localTheme ?? 'pink-bluegrey');
  }

  private storeTheme(themeName: string) {
    try {
      window.localStorage[ThemeService.storageKey] = themeName;
    } catch { }
  }

  private getStoredThemeName(): string | null {
    try {
      return window.localStorage[ThemeService.storageKey] || null;
    } catch {
      return null;
    }
  }

  public isDarkThemeSelected() : boolean{
    let currentTheme = window.localStorage[ThemeService.storageKey] ;
    if(currentTheme && (currentTheme == "pink-bluegrey" || currentTheme == "purple-green"))
      return true;
    return false;
  }

}
