import { Component, OnInit } from '@angular/core';
import { ActiveNavigationService } from '@shared/build-it-common/services/active-navigation.service';
import { DeadLoadResource } from '@shared/build-it-common/services/client';
import { Observable } from 'rxjs';
import { DeadLoadsRepositoryService } from '../../services/dead-loads-repository.service';
import { EventAggregator } from '@shared/build-it-common/services/event-aggregator';
import { DeadLoadSelectedEvent } from '../../events/dead-load-selected-event';

@Component({
  selector: 'app-dead-loads-detail',
  templateUrl: './dead-loads-detail.component.html',
  styleUrls: ['./dead-loads-detail.component.css']
})
export class DeadLoadsDetailComponent implements OnInit {


  constructor() { }

  ngOnInit(): void {
  }

}
