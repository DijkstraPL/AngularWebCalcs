import { Injectable } from '@angular/core';
import { ClaimResource } from '@shared/build-it-common/services/client';
import { FactoryService } from '../../dead-loads/factories/factory-service';
import { ClaimViewModel } from '../view-models/claim-view-model';

@Injectable({
  providedIn: 'root',
})
export class ClaimViewModelFactoryService extends FactoryService<
  ClaimResource,
  ClaimViewModel
> {
  constructor() {
    super();
  }

  public create(data: ClaimResource): ClaimViewModel {
    if (!data.id) throw 'Id is not provided.';
    if (!data.name) throw 'Name not provided.';
    return new ClaimViewModel(data.id, data.name);
  }
}
