import { KeyValue } from '@angular/common';
import { Injectable, Type } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class EventAggregator {
  private values: { [key: string]: any } = {};

  public getEvent<T>(type: { new (): T }): T {
    const key = type.name;
    if (key in this.values) {
      return this.values[key] as T;
    } else {
      const newValue = new type();
      this.values[key] = newValue;
      return newValue;
    }
  }
}

