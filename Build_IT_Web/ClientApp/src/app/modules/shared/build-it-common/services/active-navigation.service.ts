import { Injectable } from '@angular/core';
import { ActivatedRoute, ActivatedRouteSnapshot, Params, Router, RoutesRecognized } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class ActiveNavigationService {
  private _selectedCompanyId: number | undefined = undefined;
  public get selectedCompanyId(): number | undefined {
    return this._selectedCompanyId;
  }
  private _selectedProjectId: number | undefined = undefined;
  public get selectedProjectId(): number | undefined {
    return this._selectedProjectId;
  }
  private _selectedDeadLoadId: number | undefined = undefined;
  public get  selectedDeadLoadId(): number | undefined {
    return this._selectedDeadLoadId;
  }

  constructor(private readonly _activatedRoute: ActivatedRoute,
    private readonly _router: Router) {
    _router.events.subscribe(val => {
      if (val instanceof RoutesRecognized && val.state.root.firstChild) {
        let params = this.getParams(val.state.root.firstChild);

        this._selectedCompanyId = params["companyId"] ?? undefined;
        this._selectedProjectId = params["projectId"] ?? undefined;
        this._selectedDeadLoadId = params["deadLoadId"] ?? undefined;
      }
    });


  }

  private getParams(route: ActivatedRouteSnapshot): Params {
    if (route.firstChild)
      return this.getParams(route.firstChild);
    return route.params;
  }
}

