import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UtilsService {

  constructor() { }


}

export function roundTo(n : number, place : number) : number{
  return Math.round((n + Number.EPSILON) * Math.pow(10, place)) / Math.pow(10, place);
}

export function nameof<T>(obj: T, expression: (x: { [Property in keyof T]: () => string }) => () => string): string
{
   const res: { [Property in keyof T]: () => string } = {} as { [Property in keyof T]: () => string };
   Object.keys(obj).map(k => res[k as keyof T] = () => k);
   return expression(res)();
}
