import { Injectable } from '@angular/core';
import { FactoryService } from '@shared/build-it-common/factories/factory-service';
import { CompanyResource } from '@shared/build-it-common/services/client';
import { CompanyViewModel } from '../view-models/company-view-model';

@Injectable({
  providedIn: 'root'
})
export class CompanyViewModelFactory  extends FactoryService<CompanyResource, CompanyViewModel> {

  constructor() {
    super();
  }

  public create(data: CompanyResource): CompanyViewModel {
if(!data.id)
throw('Id is not provided.');
    if(!data.created )
      throw('Creation date not provided.');
    if( !data.lastModified)
      throw('Modification date not provided.');
    return new CompanyViewModel(data.id, data.name ?? '', data.created, data.lastModified);
  }
}
