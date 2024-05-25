import { ChangeDetectorRef, Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { MatTable } from '@angular/material/table';
import { LoggerService } from '@shared/build-it-common/services/logger.service';
import { UtilsService } from '@shared/build-it-common/services/utils.service';
import { DeadLoadsService } from '../../services/dead-loads.service';
import {  numberOnlyValidator } from '../../validators/number-only-validator';
import { LayeredMaterialViewModel } from '../../view-models/layered-material-view-model';
import {animate, state, style, transition, trigger} from '@angular/animations';
import { LayeredMaterialsSummaryViewModel } from '../../view-models/layered-materials-summary-view-model';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { RemoveMaterialDialogComponent } from '../remove-material-dialog/remove-material-dialog.component';
import { DeadLoadLayerResource, DeadLoadResource, LoadUnit, SaveDeadLoadLayer, SaveDeadLoadLayersCommand } from '@shared/build-it-common/services/client';
import { EventAggregator } from '@shared/build-it-common/services/event-aggregator';
import { DeadLoadSelectedEvent } from '../../events/dead-load-selected-event';
import { DeadLoadsRepositoryService } from '../../services/dead-loads-repository.service';
import { ActiveNavigationService } from '@shared/build-it-common/services/active-navigation.service';
import { MaterialResource } from '@shared/build-it-common/services/client';
import { MaterialResultResource } from '@shared/build-it-common/services/client';
import { Observable } from 'rxjs';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-dead-loads-summary',
  templateUrl: './dead-loads-summary.component.html',
  styleUrls: ['./dead-loads-summary.component.scss'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({height: '0px', minHeight: '0'})),
      state('expanded', style({height: '*'})),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ])]
})
export class DeadLoadsSummaryComponent implements OnInit {

  readonly _columnsToDisplay = ['position', 'name',  'width', 'length', 'height', 'minimumLoad', 'maximumLoad', 'unit', 'documentName', 'remove'];
  readonly LoadUnitEnum : typeof LoadUnit = LoadUnit ;

  @ViewChild(MatTable) table: MatTable<MaterialResultResource> | undefined;

  private  _layeredMaterialsSummary : LayeredMaterialsSummaryViewModel = new LayeredMaterialsSummaryViewModel([]) ;
  public get layeredMaterialsSummary() : LayeredMaterialsSummaryViewModel {
    return this._layeredMaterialsSummary;
  }
  private _getMaterials$!: Observable<MaterialResultResource[]>;

  @Output() public readonly _layeredMaterialsChanged: EventEmitter<LayeredMaterialsSummaryViewModel> =
    new EventEmitter();

  constructor(
    private readonly _deadLoadsService : DeadLoadsService,
    private readonly _loggerService: LoggerService,
    private readonly _deadLoadRepository : DeadLoadsRepositoryService,
    private readonly _dialog: MatDialog,
    private readonly _activeNavigationService  : ActiveNavigationService,
    private readonly _activatedRoute : ActivatedRoute,
    private readonly _eventAggregator : EventAggregator,
  ) {
   }

  async ngOnInit() {
    this._getMaterials$ = this._deadLoadsService.getMaterials();
    this._getMaterials$.subscribe(materials => {
      let layeredMaterials =  materials.map(m => new LayeredMaterialViewModel(this._layeredMaterialsSummary.layeredMaterials.length, m));
      this._layeredMaterialsSummary.addRange(layeredMaterials);
      this._layeredMaterialsChanged.emit(this._layeredMaterialsSummary);
      this.table?.renderRows();
    },
      err => this._loggerService.error(err),
      () => this._loggerService.log("Materials loaded."));

      this._activatedRoute.params.subscribe(async params => {
        await this.setupDeadLoads();
      });
  }

  changeUnit(unit: LoadUnit | undefined): string{
    if (unit || unit == 0)
    return this._deadLoadsService.unitAsString(unit);
    return '-';
  }

  remove(material: LayeredMaterialViewModel){
    const dialogRef = this.openDialog(material);
    dialogRef.afterClosed().subscribe(result =>{
        if(result){
          this.layeredMaterialsSummary.remove(material);
          this._layeredMaterialsChanged.emit(this._layeredMaterialsSummary);
          this.table?.renderRows();
        }
    });
  }

  openDialog(material: LayeredMaterialViewModel) : MatDialogRef<RemoveMaterialDialogComponent, boolean>  {
    return this._dialog.open(RemoveMaterialDialogComponent, {
      width: '250px',
      data: material
    });
  }

  move(material: LayeredMaterialViewModel, offset: number){
    this.layeredMaterialsSummary.move(material, offset);
    this._layeredMaterialsChanged.emit(this._layeredMaterialsSummary);
    this.table?.renderRows();
  }

  async setupDeadLoads() {
    this.layeredMaterialsSummary.clear();
     if(this._activeNavigationService.selectedCompanyId && this._activeNavigationService.selectedProjectId && this._activeNavigationService.selectedDeadLoadId)     {
      let deadLoadLayers = await this._deadLoadRepository.provideDeadLoadLayers(this._activeNavigationService.selectedCompanyId, this._activeNavigationService.selectedProjectId, this._activeNavigationService.selectedDeadLoadId);

       let orderedDeadLoadLayers = this.orderDeadLoadLayers(deadLoadLayers);

       for (let dll of orderedDeadLoadLayers) {
         if (!dll.materialId)
           return;
         let material = await this._deadLoadRepository.provideMaterial$(dll.materialId).toPromise();
         let layeredMaterial = new LayeredMaterialViewModel(this._layeredMaterialsSummary.layeredMaterials.length, material);
         layeredMaterial.length = dll.length ?? undefined;
         layeredMaterial.width = dll.width ?? undefined;
         layeredMaterial.height = dll.height ?? undefined;
         this._layeredMaterialsSummary.add(layeredMaterial);
       }

     this._layeredMaterialsChanged.emit(this._layeredMaterialsSummary);
       this.table?.renderRows();
     }
  }
  private orderDeadLoadLayers(deadLoadLayerResource: DeadLoadLayerResource[]): DeadLoadLayerResource[] {
    return deadLoadLayerResource.sort((a, b) => {
      if (a.previousDeadLoadId == b.id)
        return 1;
      if (a.id == b.previousDeadLoadId)
        return -1;
      return 0;
    });
  }

public saveDeadLoadLayers(){
  if(    this._activeNavigationService.selectedCompanyId &&
    this._activeNavigationService.selectedProjectId &&
     this._activeNavigationService.selectedDeadLoadId )
     {
    let saveDeadLoadLayers = new SaveDeadLoadLayersCommand();
    saveDeadLoadLayers.deadLoadLayers = [];
this._layeredMaterialsSummary.layeredMaterials.forEach(lm =>{
  let saveDeadLoadLayer = new SaveDeadLoadLayer();
  saveDeadLoadLayer.height = lm.height;
  saveDeadLoadLayer.width = lm.width;
  saveDeadLoadLayer.length = lm.length;
  saveDeadLoadLayer.materialId = lm.id;
  saveDeadLoadLayers.deadLoadLayers?.push(saveDeadLoadLayer);
});

      this._deadLoadRepository.saveDeadLoadLayers(
        this._activeNavigationService.selectedCompanyId,
        this._activeNavigationService.selectedProjectId,
         this._activeNavigationService.selectedDeadLoadId,
      saveDeadLoadLayers);
}
}
}
