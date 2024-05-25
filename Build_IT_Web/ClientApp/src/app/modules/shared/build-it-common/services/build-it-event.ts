

export abstract class  BuildItEvent<TData> {
  private _actions : any[] = []

  public  subscribe(action:  Delegate<TData>){
    this._actions.push(action);
  }
  public publish(payload : TData){
    this._actions.forEach(a => a(payload));
  }

}

type Delegate<T> = (value: T) => void;
