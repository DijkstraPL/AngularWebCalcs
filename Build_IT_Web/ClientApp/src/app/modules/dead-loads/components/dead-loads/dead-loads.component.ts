import { Component, Input, OnInit, Output } from '@angular/core';
import { DeadLoadResource } from '@shared/build-it-common/services/client';
import { LayeredMaterialsSummaryViewModel } from '../../view-models/layered-materials-summary-view-model';

@Component({
  selector: 'app-dead-loads',
  templateUrl: './dead-loads.component.html',
  styleUrls: ['./dead-loads.component.css'],
})
export class DeadLoadsComponent {

  showSummary: boolean = false;

  onLayeredMaterialsChanged(
    layeredMaterialsSummaryViewModel: LayeredMaterialsSummaryViewModel
  ) {
    this.showSummary =
      layeredMaterialsSummaryViewModel.layeredMaterials &&
      layeredMaterialsSummaryViewModel.layeredMaterials.length > 0;
  }
}
