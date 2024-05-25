import { Injectable } from '@angular/core';
import { CompaniesClient, ProjectsClient } from '@shared/build-it-common/services/client';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { UserViewModelFactoryService } from '../factories/user-view-model-factory.service';
import { UserViewModel } from '../view-models/user-view-model';

@Injectable({
  providedIn: 'root'
})
export class UserRepositoryService {

  constructor(private readonly _companiesClient: CompaniesClient,
    private readonly _userViewModelFactory: UserViewModelFactoryService) { }

  public provideAllUsersForCompany$(commpanyId:number) : Observable<UserViewModel[]>{
    return this._companiesClient.getUsersForCompany(commpanyId).pipe(map(u => this._userViewModelFactory.createMany(u)));
  }
}
