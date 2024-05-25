import { Injectable } from '@angular/core';
import { FactoryService } from '@shared/build-it-common/factories/factory-service';
import { UserResource } from '@shared/build-it-common/services/client';
import { UserViewModel } from '../view-models/user-view-model';

@Injectable({
  providedIn: 'root'
})
export class UserViewModelFactoryService  extends FactoryService<UserResource, UserViewModel> {

  constructor() {
    super();
  }

  public create(data: UserResource): UserViewModel {
    return new UserViewModel(data.id!, data.userName ?? '');
  }
}
