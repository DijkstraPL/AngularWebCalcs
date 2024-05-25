import { Injectable } from '@angular/core';
import { ClaimsClient } from '@shared/build-it-common/services/client';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ClaimViewModelFactoryService } from '../factories/claim-view-model-factory.service';
import { ClaimViewModel } from '../view-models/claim-view-model';

@Injectable({
  providedIn: 'root'
})
export class ClaimRepositoryService {

  constructor(private readonly _claimsClient: ClaimsClient, private readonly _claimViewModelFactory : ClaimViewModelFactoryService) { }

  public async provideClaims() : Promise<ClaimViewModel[]>{
    return await this.provideClaims$().toPromise();
  }
  public provideClaims$() : Observable<ClaimViewModel[]>{
    return this._claimsClient.getAllClaims().pipe(map(p => this._claimViewModelFactory.createMany(p)));
  }

  public async provideClaimsForCurrentUserAndProject(projectId : number) : Promise<ClaimViewModel[]>{
    return await this.provideClaimsForCurrentUserAndProject$(projectId).toPromise();
  }
  public provideClaimsForCurrentUserAndProject$(projectId : number) : Observable<ClaimViewModel[]>{
    return this._claimsClient.getClaimsForProjectForCurrentUser(projectId).pipe(map(p => this._claimViewModelFactory.createMany(p)));
  }

  public async provideClaimsForUserAndProject(userId : string, projectId : number) : Promise<ClaimViewModel[]>{
    return await this.provideClaimsForUserAndProject$(userId, projectId).toPromise();
  }
  public provideClaimsForUserAndProject$(userId:string, projectId : number) : Observable<ClaimViewModel[]>{
    return this._claimsClient.getClaimsForProjectForUser(userId,projectId).pipe(map(p => this._claimViewModelFactory.createMany(p)));
  }

  public assignOrRemoveClaim$(userId:string, projectId : number, claimId: number, assign : boolean) : Observable<boolean>{
    if(assign)
      return this._claimsClient.assignClaimToDesignerForProject(userId,projectId, claimId);
    return this._claimsClient.removeClaimFromDesignerInProject(userId, projectId, claimId);
  }
}
