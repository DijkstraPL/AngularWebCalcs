import { BuildItEvent } from "@shared/build-it-common/services/build-it-event";
import { DeadLoadLayerResource, DeadLoadResource } from "@shared/build-it-common/services/client";

 export class DeadLoadSelectedEvent extends BuildItEvent< DeadLoadResource | undefined>{

  private _selectedDeadLoad : DeadLoadResource | undefined;
  public   get selectedDeadLoad() : DeadLoadResource | undefined
  {
    return this._selectedDeadLoad;
  }
   public   set selectedDeadLoad(value: DeadLoadResource | undefined)
  {
     this._selectedDeadLoad= value;
  }
}
