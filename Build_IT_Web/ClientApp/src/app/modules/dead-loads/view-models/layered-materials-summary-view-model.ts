import { LoadUnit } from "@shared/build-it-common/services/client";
import { LayeredMaterialViewModel } from "./layered-material-view-model"

export class LayeredMaterialsSummaryViewModel {


  constructor(public readonly layeredMaterials: LayeredMaterialViewModel[]){
  }

  public isValid(){
    if(this.layeredMaterials.length == 0)
      return true;

    const unit = this.layeredMaterials[0].getLoadUnit();
    return this.layeredMaterials.every(m => m.getLoadUnit() == unit);
  }

  public minimumLoad(){
    return this.layeredMaterials.reduce((accumulator, m) => accumulator+ m.getMinimumLoad(),0);
  }

  public maximumLoad(){
    return this.layeredMaterials.reduce((accumulator, m) => accumulator+ m.getMaximumLoad(),0);
  }

  public add(layeredMaterial: LayeredMaterialViewModel){
    this.layeredMaterials.push(layeredMaterial);
  }

  public addRange(layeredMaterials: LayeredMaterialViewModel[]){
    this.layeredMaterials.push(...layeredMaterials);
  }

  public remove(material: LayeredMaterialViewModel) {
    this.layeredMaterials.splice(this.layeredMaterials.indexOf(material), 1);
  }

  public unit() : LoadUnit | undefined{
    if( this.layeredMaterials.length > 0 && this.isValid())
    return this.layeredMaterials[0].getLoadUnit();
    return undefined;
  }

  move(material: LayeredMaterialViewModel, offset: number) {
    const index = this.layeredMaterials.indexOf(material);
    this.remove(material);
    let newIndex = index + offset;
    if (newIndex < 0)
      newIndex = this.layeredMaterials.length;
    if (newIndex > this.layeredMaterials.length)
      newIndex = 0;
    this.layeredMaterials.splice(newIndex, 0, material);
  }

  clear() {
    this.layeredMaterials.splice(0, this.layeredMaterials.length);
  }
}
