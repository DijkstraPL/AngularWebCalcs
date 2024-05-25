import { Injectable } from "@angular/core";
import { CreateProjectCommand, ProjectResource, ProjectsClient, UpdateProjectCommand } from "@shared/build-it-common/services/client";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { ProjectViewModelFactory } from "../factories/project-view-model-factory.service";
import { ProjectViewModel } from "../view-models/project-view-model";

@Injectable({
  providedIn: 'root'
})
export class ProjectsRepositoryService {

  constructor(private readonly _projectClient: ProjectsClient, private readonly _projectViewModelFactory: ProjectViewModelFactory) { }

  public async provideAllProjects() : Promise<ProjectViewModel[]>{
    return await this.provideAllProjects$().toPromise();
  }
  public provideAllProjects$() : Observable<ProjectViewModel[]>{
    return this._projectClient.getAll().pipe(map(p => this._projectViewModelFactory.createMany(p)));
  }

  public async provideProjectsForCompany(companyId : number) : Promise<ProjectViewModel[]>{
    return await this.provideProjectsForCompany$(companyId).toPromise();
  }
  public provideProjectsForCompany$(companyId : number) : Observable<ProjectViewModel[]>{
    return this._projectClient.getAllProjectsForCompany(companyId).pipe(map(p => this._projectViewModelFactory.createMany(p)));
  }

  public async provideProject(projectId: number): Promise<ProjectViewModel> {
    return await this.provideProject$(projectId).toPromise();
  }
  public provideProject$(projectId: number): Observable<ProjectViewModel> {
    return this._projectClient.get(projectId).pipe(map(p => this._projectViewModelFactory.create(p)));
  }

  public  deleteProject$(projectId: number ) : Observable<void>{
    return  this._projectClient.delete( projectId);
  }
  public async deleteProject(projectId: number): Promise<void> {
    return await this._projectClient.delete(projectId).toPromise();
  }

  public async createProject(project: CreateProjectCommand ) : Promise<number>{
    return await this._projectClient.create( project).toPromise();
  }
  public  createProject$(project: CreateProjectCommand ) : Observable<number>{
    return  this._projectClient.create( project);
  }
  public async updateProject(project: UpdateProjectCommand ) : Promise<number>{
    return await this._projectClient.update( project).toPromise();
  }
  public  updateProject$(project: UpdateProjectCommand ) : Observable<number>{
    return  this._projectClient.update( project);
  }
}

