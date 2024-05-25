import { Injectable } from '@angular/core';
import { CompaniesClient, CompanyResource, CreateCompanyCommand, UpdateCompanyCommand } from '@shared/build-it-common/services/client';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { CompanyViewModelFactory } from '../factories/company-view-model-factory.service';
import { CompanyViewModel } from '../view-models/company-view-model';

@Injectable({
  providedIn: 'root'
})
export class CompanyRepositoryService {

  constructor(private readonly _companiesClient: CompaniesClient, private readonly _companyViewModelFactory : CompanyViewModelFactory) { }

  public async provideCompanies() : Promise<CompanyViewModel[]>{
    return await this.provideCompanies$().toPromise();
  }
  public provideCompanies$() : Observable<CompanyViewModel[]>{
    return this._companiesClient.getAllForCurrentUser().pipe(map(p => this._companyViewModelFactory.createMany(p)));
  }

  public async provideCompany(companyId:number) : Promise<CompanyViewModel>{
    return await this.provideCompany$(companyId).toPromise();
  }
  public provideCompany$(companyId:number) : Observable<CompanyViewModel>{
    return this._companiesClient.get(companyId).pipe(map(p => this._companyViewModelFactory.create(p)));
  }

  public async createCompany(company: CreateCompanyCommand ) : Promise<number>{
    return await this._companiesClient.create(company).toPromise();
  }
  public  createCompany$(company: CreateCompanyCommand ) : Observable<number>{
    return  this._companiesClient.create(company);
  }

  public async updateCompany(company: UpdateCompanyCommand ) : Promise<number>{
    return await this._companiesClient.update(company).toPromise();
  }
  public  updateCompany$(company: UpdateCompanyCommand ) : Observable<number>{
    return  this._companiesClient.update(company);
  }

  public async removeCompany(companyId: number ) : Promise<number>{
    return await this._companiesClient.delete(companyId).toPromise();
  }
  public  removeCompany$(companyId: number ) : Observable<number>{
    return  this._companiesClient.delete(companyId);
  }

  public assignToCompany$(companyId: number, userMail: string): Observable<boolean> {
   return this._companiesClient.assignUser(companyId, userMail);
  }
  public assignToCompany(companyId: number, userMail: string): Promise<boolean> {
    return this._companiesClient.assignUser(companyId, userMail).toPromise();
  }

}
