import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CurrentAppStateService } from '@main/app/services/current-app-state.service';
import { LoggerService } from '@shared/build-it-common/services/logger.service';
import { NavigatorService } from '@shared/build-it-common/services/navigator.service';
import { forkJoin, Observable, Subscriber } from 'rxjs';
import { map } from 'rxjs/operators';
import { ClaimRepositoryService } from '../../services/claim-repository.service';
import { ProjectsRepositoryService } from '../../services/projects-repository.service';
import { UserRepositoryService } from '../../services/user-repository.service';
import { ClaimViewModel } from '../../view-models/claim-view-model';
import { ProjectViewModel } from '../../view-models/project-view-model';
import { UserViewModel } from '../../view-models/user-view-model';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css'],
})
export class UserListComponent implements OnInit {
  private _selectedCompanyId: number = 0;
  private _users$!: Observable<UserViewModel[]>;
  public get users$(): Observable<UserViewModel[]> {
    return this._users$;
  }
  private _projects$!: Observable<ProjectViewModel[]>;
  public get projects$(): Observable<ProjectViewModel[]> {
    return this._projects$;
  }
  private _claims$!: Observable<ClaimViewModel[]>;
  public get claims$(): Observable<ClaimViewModel[]> {
    return this._claims$;
  }

  projectUserClaims: {
    user: UserViewModel;
    project: ProjectViewModel;
    claims: Observable<ClaimViewModel[]>;
  }[] = [];

  displayedColumns: string[] = [];
  visibleColumns: string[] = [];
  dataSource: {userId: string, data:any}[] = [];
  constructor(
    private readonly _userService: UserRepositoryService,
    private readonly _projectsRepositoryService: ProjectsRepositoryService,
    private readonly _claimRepositoryService: ClaimRepositoryService,
    private readonly _navigator: NavigatorService,
    private readonly _activatedRoute: ActivatedRoute,
    private readonly _currentAppState: CurrentAppStateService,
    private readonly _loggerService: LoggerService
  ) { }

  async ngOnInit() {
    this._activatedRoute.params.subscribe(
      (params) => {
        this._selectedCompanyId = params.companyId;
        if (!this._selectedCompanyId) this._navigator.navigateToHome();
      },
      (er) => {
        this._navigator.navigateToHome();
      }
    );

    this.setUsers();
    this.setProjects();
    this.setClaims();
   await  this.setClaimsForUsersForProjects();
   await this.setDisplayedColumns();
   this.setVisibleColumns();
   // this.setData();

    this._currentAppState.selectedCompanyChanged.subscribe(
      async (companyId) => {
        this.setUsers();
        this.setProjects();
        this.setClaims();
        await  this.setClaimsForUsersForProjects();
        await this.setDisplayedColumns();
        this.setVisibleColumns();
    //    this.setData();
      }
    );
  }
  setData() {
    this.projectUserClaims.forEach(async puc => {
     let claims =  await  puc.claims.toPromise();
      let claimsNames = claims.map(c => c.name);
      let data : any = { projectName: puc.project.name } ;
      claimsNames.forEach(claimName => data[claimName] = claims.some(claim => claim.name == claimName));
      this.dataSource.push({userId: puc.user.id, data});
    });
  }

  public  setVisibleColumns(){
    this.visibleColumns= ["Project Name", ...this.displayedColumns];
  }

  public async setDisplayedColumns(){
      this.displayedColumns = await this.claims$.pipe(map(c => c.map(c => c.name))).toPromise();
  }

  public getData(userId:string){
    return this.dataSource.filter(data => data.userId == userId).map(data => data.data);
  }

  public getClaims(userId: string, projectId: number): Observable<ClaimViewModel[]> | null {
    let claims = this.projectUserClaims.filter(puc => puc.project.id == projectId && puc.user.id == userId).map(puc => puc.claims);
    if (claims && claims.length > 0)
      return claims[0];
    return null;
  }

  private setUsers() {
    this._users$ = this._userService.provideAllUsersForCompany$(
      this._selectedCompanyId
    );
  }

  private setProjects() {
    this._projects$ =
      this._projectsRepositoryService.provideProjectsForCompany$(
        this._selectedCompanyId
      );
  }

  private setClaims() {
    this._claims$ = this._claimRepositoryService.provideClaims$();
  }

  private async setClaimsForUsersForProjects() {
    await forkJoin([this.users$, this.projects$]).pipe(
      map((usersProjects) => {
        usersProjects[0].forEach((user) => {
          usersProjects[1].forEach((project) => {
            let claims =
              this._claimRepositoryService.provideClaimsForUserAndProject$(
                user.id,
                project.id
              );
            this.projectUserClaims.push({ user, project, claims });
          });
        });
      })
    ).toPromise();
  }
}
