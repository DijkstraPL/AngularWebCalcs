import { Injectable } from '@angular/core';
import { CompanyRepositoryService } from '@companies/services/company-repository.service';
import { Subscription } from 'rxjs';
import { Observable } from 'rxjs';
import { ProjectsRepositoryService } from '@companies/services/projects-repository.service';
import { DeadLoadsRepositoryService } from '@main/app/modules/dead-loads/services/dead-loads-repository.service';

@Injectable({
  providedIn: 'root'
})
export class NameProviderService {

  constructor(private readonly _companyRepository: CompanyRepositoryService,
    private readonly _projectRepository: ProjectsRepositoryService ,
    private readonly _deadLoadRepository: DeadLoadsRepositoryService
     ) { }

  async provideCompanyName(companyId: number): Promise<string | undefined>{
    let company = await this._companyRepository.provideCompany(companyId);
    return company.name;
  }
  async provideProjectName(projectId: number): Promise<string | undefined> {
    let project = await this._projectRepository.provideProject(projectId);
    return project.name;
  }
  async provideDeadLoadName(companyId : number, projectId : number, deadLoadId: number): Promise<string | undefined> {
    let deadLoads = await this._deadLoadRepository.provideDeadLoads(companyId, projectId);
    let deadLoad = deadLoads.find(dl => dl.id == deadLoadId);
    return deadLoad?.name;
  }
}
