import { EventEmitter } from "@angular/core";
import {  LoadUnit, MaterialAdditionResult, MaterialResultResource } from "@shared/build-it-common/services/client";
import { roundTo } from "@shared/build-it-common/services/utils.service";

export class LayeredMaterialViewModel extends MaterialResultResource{

  public position: number = 0;

  private _width: number | undefined = undefined;
  private _length: number | undefined = undefined;
  private _height: number | undefined = undefined;

  public get width(){
    return this._width;
  }

  public get length(){
    return this._length;
  }

  public get height(){
    return this._height;
  }

  public set width(width: number | undefined) {
    if (width?.toString() == '')
      width = undefined;
    if(width === undefined){
      this._width = width;
      return;
    }

    this._width = +width.toString().replace(',','.');
  }
  public set length(length: number | undefined) {
    if (length?.toString() == '')
    length = undefined;
    if(length === undefined){
      this._length = length;
      return;
    }

    this._length = +length.toString().replace(',','.');
  }
  public set height(height: number | undefined) {
    if (height?.toString() == '')
    height = undefined;
    if(height === undefined){
      this._height = height;
      return;
    }

    this._height = +height.toString().replace(',','.');
  }

  public override materialAdditions : LayeredAdditionViewModel[] = [];

  public getMinimumLoad(): number{
    const density = this.minimumDensity! + this.materialAdditions.filter(a => a.isSelected).reduce((accumulator, a) => accumulator + (a.value ?? 0) , 0);
    return roundTo( (this.width ?? 1) * (this.height ?? 1) * (this.length ?? 1) * density , 3);
  }
  public getMaximumLoad(): number{
    const density = this.maximumDensity! + this.materialAdditions.filter(a => a.isSelected).reduce((accumulator, a) => accumulator +  (a.value ?? 0) , 0);
    return roundTo((this.width ?? 1) * (this.height ?? 1) * (this.length ?? 1) * density, 3);
  }

  public getLoadUnit() : LoadUnit{
    let unit = this.unit;
    if (unit == undefined)
      throw ("No unit matching");

    if(this.width)
    unit = unit - 1;
      if(this.length)
      unit= unit - 1;
      if(this.height)
      unit=unit - 1;
      return unit!;
  }

  constructor(
    public readonly index : number,
    private readonly _material : MaterialResultResource
     ) {
      super(_material);
      this.subcategory = _material.subcategory;
      this.materialAdditions = _material.materialAdditions?.map(a => new LayeredAdditionViewModel(a)) ?? [];
  }
  }

  export class LayeredAdditionViewModel extends MaterialAdditionResult{

    private _isSelected: boolean = false;

    public get isSelected(){
      return this._isSelected;
    }

    public set isSelected(isSelected : boolean){
      this._isSelected = isSelected;
    }

    constructor(private readonly _additionViewModel: MaterialAdditionResult
       ) {
        super(_additionViewModel);
    }
  }
