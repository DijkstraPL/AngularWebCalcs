import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LoggerService {

  constructor() { }

  public log(message: string|any) : void{
    console.log(message);
  }
  public error(errorMsg: string|any):void{
    console.error(errorMsg);
  }
}
