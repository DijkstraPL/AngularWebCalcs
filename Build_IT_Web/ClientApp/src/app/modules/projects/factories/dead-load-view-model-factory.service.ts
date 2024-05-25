import { Injectable } from '@angular/core';
import { FactoryService } from '@shared/build-it-common/factories/factory-service';
import { DeadLoadResource } from '@shared/build-it-common/services/client';
import { DeadLoadViewModel } from '../view-models/dead-load-view-model';

@Injectable({
  providedIn: 'root',
})
export class DeadLoadViewModelFactory extends FactoryService<DeadLoadResource, DeadLoadViewModel> {
  constructor() {
    super();
  }

  public create(data: DeadLoadResource): DeadLoadViewModel {
    if (!data.id) throw 'Id is not provided.';
    if (!data.name) throw 'Name not provided.';
    return new DeadLoadViewModel(data.id, data.name);
  }
}
