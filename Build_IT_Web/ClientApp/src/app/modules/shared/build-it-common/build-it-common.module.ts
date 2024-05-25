import { Inject, InjectionToken, LOCALE_ID, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { API_BASE_URL, ClaimsClient, CompaniesClient, DeadLoadsClient, ProjectsClient, SnowLoadsClient } from './services/client';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { ProgressBarService } from './services/progress-bar.service';
import { LoggerService } from './services/logger.service';
import { UtilsService } from './services/utils.service';
import { EnumAsStringPipe } from './pipes/enum-as-string.pipe';
import { NavigatorService } from './services/navigator.service';
import { ActiveNavigationService } from './services/active-navigation.service';
import { NameProviderService } from './services/name-provider.service';
import { RightMouseMenuDirective } from './directives/right-mouse-menu.directive';
import { registerLocaleData } from '@angular/common';
import localePl from '@angular/common/locales/pl';
import { EventAggregator } from './services/event-aggregator';
import { environment } from 'src/environments/environment';

@NgModule({
  declarations: [
    EnumAsStringPipe,
    RightMouseMenuDirective
  ],
  imports: [
  ],
  exports:[
    RightMouseMenuDirective
  ],
  providers: [
    { provide: API_BASE_URL, useValue: environment.apiUrl },
    { provide: ProjectsClient, useFactory: (httpClient: HttpClient, API_BASE_URL: string) => new ProjectsClient(httpClient, API_BASE_URL), deps: [HttpClient, API_BASE_URL] },
    { provide: CompaniesClient, useFactory: (httpClient: HttpClient, API_BASE_URL: string) => new CompaniesClient(httpClient, API_BASE_URL), deps: [HttpClient, API_BASE_URL] },
    { provide: DeadLoadsClient, useFactory: (httpClient: HttpClient, API_BASE_URL: string) => new DeadLoadsClient(httpClient, API_BASE_URL), deps: [HttpClient, API_BASE_URL] },
    { provide: SnowLoadsClient, useFactory: (httpClient: HttpClient, API_BASE_URL: string) => new SnowLoadsClient(httpClient, API_BASE_URL), deps: [HttpClient, API_BASE_URL] },
    { provide: ClaimsClient, useFactory: (httpClient: HttpClient, API_BASE_URL: string) => new ClaimsClient(httpClient, API_BASE_URL), deps: [HttpClient, API_BASE_URL] },
    ProgressBarService,
    LoggerService,
    EventAggregator,
    UtilsService,
    EnumAsStringPipe,
    NavigatorService,
    ActiveNavigationService,
    NameProviderService,
    { provide: LOCALE_ID,   useValue: 'pl'}
  ]
})
export class BuildItCommonModule {

  constructor() {
    registerLocaleData(localePl, 'pl');
  }
 }
