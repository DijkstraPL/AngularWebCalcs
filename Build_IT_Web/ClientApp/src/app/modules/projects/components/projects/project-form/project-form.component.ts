import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import {  ActivatedRoute, Router } from '@angular/router';
import { CreateProjectCommand } from '@shared/build-it-common/services/client';
import { NavigatorService } from '@shared/build-it-common/services/navigator.service';
import { ProjectsRepositoryService } from '../../../services/projects-repository.service';

@Component({
  selector: 'app-project-form',
  templateUrl: './project-form.component.html',
  styleUrls: ['./project-form.component.css']
})
export class ProjectFormComponent implements OnInit {

  projectForm = new FormGroup({
    name: new FormControl('',Validators.required),
    description: new FormControl(''),
  });

  private _selectedCompanyId: number = 0;

  constructor(private readonly _projectsRepositoryService: ProjectsRepositoryService,
    private readonly _activatedRoute: ActivatedRoute,
    private readonly _navigator: NavigatorService)
    { }

  ngOnInit(): void {
    this._activatedRoute.params.subscribe(params => {
      this._selectedCompanyId = params.companyId;
      if(!this._selectedCompanyId)
        this._navigator.navigateToHome();
    }, er => {this._navigator.navigateToHome();});
  }

  public async onSubmit(){
    let projectData = this.projectForm.value;
    if(!this._selectedCompanyId)
    throw ('You are not able to create a project without company.');
    if(!projectData.name)
     throw ('You are not able to create a project with empty name.');

    let project = new CreateProjectCommand()
    project.companyId = this._selectedCompanyId;
    project.name = projectData.name;
    project.description = projectData.description ?? '';
    let newId = await this._projectsRepositoryService.createProject( project);

    this._navigator.navigateToProject(this._selectedCompanyId, newId);
  }
}
