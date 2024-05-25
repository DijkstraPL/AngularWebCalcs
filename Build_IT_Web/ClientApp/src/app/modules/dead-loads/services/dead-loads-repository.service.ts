import { Injectable } from '@angular/core';
import { CategoryResultResource, DeadLoadLayerResource, DeadLoadResource, DeadLoadsClient, MaterialResultResource, SaveDeadLoadLayersCommand, SubcategoryResultResource } from '@shared/build-it-common/services/client';
import { from, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DeadLoadsRepositoryService {

  constructor(private readonly deadLoadsClient: DeadLoadsClient) { }

  public async provideCategories() : Promise<CategoryResultResource[]>{
    return await this.deadLoadsClient.getAllCategories().toPromise();
  }

  public provideCategories$() : Observable<CategoryResultResource[]>{
    return this.deadLoadsClient.getAllCategories();
  }

  public async provideSubcategories(categoryId: number) : Promise<SubcategoryResultResource[]>{
    return await this.deadLoadsClient.getAllSubcategories(categoryId).toPromise();
  }

  public provideSubcategories$(categoryId: number) : Observable<SubcategoryResultResource[]>{
    return this.deadLoadsClient.getAllSubcategories(categoryId);
  }

  public async provideMaterials(subcategoryId: number) : Promise<MaterialResultResource[]>{
    return await this.deadLoadsClient.getAllMaterials(subcategoryId).toPromise();
  }

  public provideMaterials$(subcategoryId: number) : Observable<MaterialResultResource[]>{
    return this.deadLoadsClient.getAllMaterials(subcategoryId);
  }

  public async provideMaterial(materialId: number) : Promise<MaterialResultResource>{
    return await this.deadLoadsClient.getMaterial(materialId).toPromise();
  }

  public provideMaterial$(materialId: number) : Observable<MaterialResultResource>{
    return this.deadLoadsClient.getMaterial(materialId);
  }

  public provideDeadLoads$(companyId: number, projectId: number) : Observable<DeadLoadResource[]>{
    return this.deadLoadsClient.getAllDeadLoadsForProject(companyId, projectId);
  }
  public async provideDeadLoads(companyId: number, projectId: number) : Promise<DeadLoadResource[]>{
    return await this.deadLoadsClient.getAllDeadLoadsForProject(companyId, projectId).toPromise();
  }
  public provideDeadLoadLayers$(companyId: number, projectId: number, deadLoadId: number) : Observable<DeadLoadLayerResource[]>{
    return this.deadLoadsClient.getAllDeadLoadLayers(companyId, projectId, deadLoadId);
  }
  public async provideDeadLoadLayers(companyId: number, projectId: number, deadLoadId : number) : Promise<DeadLoadLayerResource[]>{
    return await this.deadLoadsClient.getAllDeadLoadLayers(companyId, projectId,deadLoadId).toPromise();
  }
  public async saveDeadLoadLayers(companyId: number, projectId: number, deadLoadId: number, deadLoadLayers: SaveDeadLoadLayersCommand ) : Promise<number>{
    return await this.deadLoadsClient.saveDeadLoadLayers(companyId, projectId, deadLoadId,deadLoadLayers).toPromise();
  }

}
