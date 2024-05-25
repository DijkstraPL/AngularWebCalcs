import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { SideNavMenuComponent } from './components/side-nav-menu/side-nav-menu.component';


import { AngularMaterialModule } from '@shared/angular-material/angular-material.module';
import { ProgressBarComponent } from './components/progress-bar/progress-bar.component';
import { BuildItCommonModule } from '@shared/build-it-common/build-it-common.module';
import { CommonModule } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ThemeMenuComponent } from './components/theme-menu/theme-menu.component';
import { ThemeService } from './services/theme.service';
import { StyleManagerService } from './services/style-manager.service';
import { AuthorizeGuard } from '@main/api-authorization/authorize.guard';
import { TokenComponent } from './token/token.component';
import { ApiAuthorizationModule } from '@main/api-authorization/api-authorization.module';
import { AuthorizeInterceptor } from '@main/api-authorization/authorize.interceptor';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { CurrentAppStateService } from './services/current-app-state.service';
import { CompanyFormComponent } from './modules/projects/components/company/company-form/company-form.component';
import { ProjectsModule } from "./modules/projects/companies.module";
import { CompanyListComponent } from './components/company-list/company-list.component';
import { SideNavMenuCompaniesComponent } from './components/side-nav-menu-companies/side-nav-menu-companies.component';
import { SideNavMenuProjectsComponent } from './components/side-nav-menu-projects/side-nav-menu-projects.component';
import { SideNavMenuDeadLoadsComponent } from './components/side-nav-menu-dead-loads/side-nav-menu-dead-loads.component';
import { SideNavMenuSnowLoadsComponent } from './components/side-nav-menu-snow-loads/side-nav-menu-snow-loads.component';

const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'not-found', component: NotFoundComponent, pathMatch: 'full' },
  { path: 'beam-calc', loadChildren: () => import('./modules/beam-calc/beam-calc.module').then(m => m.BeamCalcModule), canActivate: [AuthorizeGuard] },
  { path: ':companyId/:projectId/dead-loads', loadChildren: () => import('./modules/dead-loads/dead-loads.module').then(m => m.DeadLoadsModule), canActivate: [AuthorizeGuard]  },
  { path: ':companyId/:projectId/snow-loads', loadChildren: () => import('./modules/snow-loads/snow-loads.module').then(m => m.SnowLoadsModule), canActivate: [AuthorizeGuard]  },
  { path: 'create', loadChildren: () => import('./modules/projects/companies.module').then(m => m.ProjectsModule), component: CompanyFormComponent, canActivate: [AuthorizeGuard]  },
  { path: 'token', component: TokenComponent, canActivate: [AuthorizeGuard] },
  { path: ':companyId', loadChildren: () => import('./modules/projects/companies.module').then(m => m.ProjectsModule), canActivate: [AuthorizeGuard]  }
];

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        SideNavMenuComponent,
        SideNavMenuCompaniesComponent,
        SideNavMenuDeadLoadsComponent,
        SideNavMenuProjectsComponent,
        SideNavMenuSnowLoadsComponent,
        HomeComponent,
        ProgressBarComponent,
        ThemeMenuComponent,
        CompanyListComponent,
    TokenComponent,
        NotFoundComponent
    ],
    providers: [
        ThemeService,
        StyleManagerService,
        { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true },
        CurrentAppStateService
    ],
    bootstrap: [AppComponent],
    imports: [
        BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
        HttpClientModule,
        FormsModule,
        CommonModule,
        ApiAuthorizationModule,
        BrowserAnimationsModule,
        AngularMaterialModule,
        BuildItCommonModule,
        ReactiveFormsModule,
        RouterModule.forRoot(routes),
    ]
})
export class AppModule { }
