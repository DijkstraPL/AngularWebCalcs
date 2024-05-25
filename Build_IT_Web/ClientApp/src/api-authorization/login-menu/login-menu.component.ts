import { Component, OnInit } from '@angular/core';
import { AuthorizeService } from '../authorize.service';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ThemeService } from '@main/app/services/theme.service';

@Component({
  selector: 'app-login-menu',
  templateUrl: './login-menu.component.html',
  styleUrls: ['./login-menu.component.scss']
})
export class LoginMenuComponent implements OnInit {
  public isAuthenticated?: Observable<boolean>;
  public userName?: Observable<string | null | undefined>;

  public get isDarkThemeSelected() : boolean{
    return this._themeService.isDarkThemeSelected();
  }

  constructor(private authorizeService: AuthorizeService,
    private readonly _themeService: ThemeService) { }

  ngOnInit() {
    this.isAuthenticated = this.authorizeService.isAuthenticated();
    this.userName = this.authorizeService.getUser().pipe(map(u => u && u.name));
  }
}
