import { Injectable } from '@angular/core';
import { CreateDeadLoadCommand, DeadLoadResource, DeadLoadsClient, ProjectsClient, SaveDeadLoadLayersCommand } from '@shared/build-it-common/services/client';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { DeadLoadViewModelFactory } from '../factories/dead-load-view-model-factory.service';
import { DeadLoadViewModel } from '../view-models/dead-load-view-model';

@Injectable({
  providedIn: 'root'
})
export class DeadLoadsRepositoryService {
  constructor(private readonly _deadLoadsClient: DeadLoadsClient, private readonly _deadLoadViewModelFactory : DeadLoadViewModelFactory) { }

  public async provideDeadLoadsForProject(companyId: number, projectId : number) : Promise<DeadLoadViewModel[]>{
    return await this.provideDeadLoadsForProject$(companyId, projectId).toPromise();
  }
  public provideDeadLoadsForProject$(companyId: number, projectId : number) : Observable<DeadLoadViewModel[]>{
    return this._deadLoadsClient.getAllDeadLoadsForProject(companyId, projectId).pipe(map(p => this._deadLoadViewModelFactory.createMany(p)));
  }

  public async createDeadLoad(companyId: number, projectId: number, deadLoad: CreateDeadLoadCommand ) : Promise<number>{
    return await this._deadLoadsClient.create(companyId, projectId, deadLoad).toPromise();
  }

}
