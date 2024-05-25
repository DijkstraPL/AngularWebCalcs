import { Component } from '@angular/core';
import { AuthorizeService } from '@main/api-authorization/authorize.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  private _isAuthenticated!: Observable<boolean>;

  public get isAuthenticated$() : Observable<boolean>{
    return this._isAuthenticated;
  }

  constructor(private readonly _authorizeService : AuthorizeService) {
  }

  ngOnInit() {
    this._isAuthenticated = this._authorizeService.isAuthenticated();
  }
}
