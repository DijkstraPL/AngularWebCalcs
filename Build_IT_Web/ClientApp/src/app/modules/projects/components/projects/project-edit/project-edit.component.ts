import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { UpdateProjectCommand } from '@shared/build-it-common/services/client';
import { NavigatorService } from '@shared/build-it-common/services/navigator.service';
import { ProjectsRepositoryService } from '../../../services/projects-repository.service';

@Component({
  selector: 'app-project-edit',
  templateUrl: './project-edit.component.html',
  styleUrls: ['./project-edit.component.css']
})
export class ProjectEditComponent implements OnInit {
  projectForm = new FormGroup({
    name: new FormControl('',Validators.required),
    description: new FormControl(''),
  });

  private _selectedCompanyId: number = 0;
  private _selectedProjectId: number = 0;

  constructor(private readonly _projectsRepositoryService: ProjectsRepositoryService,
    private readonly _activatedRoute: ActivatedRoute,
    private readonly _navigator: NavigatorService)
    { }

  ngOnInit(): void {
    this._activatedRoute.params.subscribe(params => {
      this._selectedCompanyId = params.companyId;
      this._selectedProjectId = params.projectId;
      if(!this._selectedCompanyId)
        this._navigator.navigateToHome();
        this.setProjectForm();
    }, er => {this._navigator.navigateToHome();});
  }

  private setProjectForm() {
    this._projectsRepositoryService.provideProject$( this._selectedProjectId).subscribe(c => {
        this.projectForm.controls['name'].setValue(c.name);
        this.projectForm.controls['description'].setValue(c.description);
    },
        err => {
            if (err.status === 404)
                this._navigator.navigateToNotFound();
            else
                console.error(err);
        },
        () => { });
}

  public async onSubmit(){
    let projectData = this.projectForm.value;
    if(!this._selectedCompanyId)
    throw ('You are not able to create a project without company.');
    if(!projectData.name)
     throw ('You are not able to create a project with empty name.');

    let project = new UpdateProjectCommand();
    project.id = this._selectedProjectId;
    project.companyId = this._selectedCompanyId;
    project.name = projectData.name;
    project.description = projectData.description ?? '';
    let newId = await this._projectsRepositoryService.updateProject( project);

    this._navigator.navigateToProject(this._selectedCompanyId, newId);
  }
}
