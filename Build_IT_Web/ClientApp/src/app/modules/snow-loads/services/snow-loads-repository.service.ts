import { Injectable } from '@angular/core';
import { CreateSnowLoadCommand, SnowLoadResource, SnowLoadsClient } from '@shared/build-it-common/services/client';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SnowLoadsRepositoryService {

  constructor(private readonly snowLoadsClient: SnowLoadsClient) { }

  public provideSnowLoads$(companyId: number, projectId: number) : Observable<SnowLoadResource[]>{
    return this.snowLoadsClient.getAllSnowLoadsForProject(companyId, projectId);
  }
  public async provideSnowLoads(companyId: number, projectId: number) : Promise<SnowLoadResource[]>{
    return await this.snowLoadsClient.getAllSnowLoadsForProject(companyId, projectId).toPromise();
  }
  public async createSnowLoad(companyId: number, projectId: number, snowLoad: CreateSnowLoadCommand) {
    return await this.snowLoadsClient.create(companyId, projectId, snowLoad).toPromise();
  }
}
