import { Injectable, Input } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProgressBarService {

  private _value: number = 0;
  private readonly _valueChanged: BehaviorSubject<number> = new BehaviorSubject(0);

  public get valueChanged(){
    return this._valueChanged;
  }

  public get value(){
    return this._value;
  }

  public set value(value: number){
    if(value > 100)
      value = 100;
    if(value < 0)
      value = 0;
    this._value = value;
    this._valueChanged.next(this._value);
  }

  constructor() { }

  public reset() : void{
    this.value = 0;
  }
}
