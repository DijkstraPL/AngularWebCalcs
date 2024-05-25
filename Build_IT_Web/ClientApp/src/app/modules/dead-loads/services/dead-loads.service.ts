import { Injectable } from '@angular/core';
import { LoadUnit, MaterialResultResource } from '@shared/build-it-common/services/client';
import { Observable, Observer, Subject, UnaryFunction } from 'rxjs';
import { Action } from 'rxjs/internal/scheduler/Action';

@Injectable({
  providedIn: 'root'
})
export class DeadLoadsService {

  constructor() { }

  private readonly _onAddMaterialActions$ : Subject<MaterialResultResource[]> = new Subject<MaterialResultResource[]>;

  public addMaterial(...materials: MaterialResultResource[]): void{
    this._onAddMaterialActions$.next(materials);
  }

  public getMaterials() : Observable<MaterialResultResource[]>{
    return this._onAddMaterialActions$.asObservable();
  }

  public unitAsString(unit: LoadUnit): string{
    switch(unit ){
      case LoadUnit.Kilonewton:
        return "kN";
        case LoadUnit.Kilonewton_per_meter:
          return "kN/m"
          case LoadUnit.Kilonewton_per_square_meter:
            return "kN/m<sup>2</sup>"
      case LoadUnit.Kilonewton_per_cubic_meter:
        return "kN/m<sup>3</sup>"
        default:
          return "";
    }

  }
}
