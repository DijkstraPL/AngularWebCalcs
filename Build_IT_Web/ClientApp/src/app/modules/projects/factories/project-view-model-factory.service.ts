import { Injectable } from "@angular/core";
import { FactoryService } from "@shared/build-it-common/factories/factory-service";
import { ProjectResource } from "@shared/build-it-common/services/client";
import { ProjectViewModel } from "../view-models/project-view-model";

@Injectable({
  providedIn: 'root'
})
export class ProjectViewModelFactory extends FactoryService<ProjectResource, ProjectViewModel> {

  constructor() {
    super();
  }

  public create(data: ProjectResource): ProjectViewModel {
    if(!data.created )
      throw('Creation date not provided.');
    if( !data.lastModified)
      throw('Modification date not provided.');
    return new ProjectViewModel(data.id!, data.name ?? '', data.description ?? '', data.created, data.lastModified);
  }
}
