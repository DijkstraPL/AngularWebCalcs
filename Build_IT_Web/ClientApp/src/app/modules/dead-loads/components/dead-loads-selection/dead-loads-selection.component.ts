import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { LoggerService } from '@shared/build-it-common/services/logger.service';
import { Observable } from 'rxjs';
import { CategoryResultResource,  MaterialResultResource,  SubcategoryResultResource } from '../../../shared/build-it-common/services/client';
import { DeadLoadsRepositoryService } from '../../services/dead-loads-repository.service';
import { DeadLoadsService } from '../../services/dead-loads.service';
import { flatMap, map, mergeMap } from 'rxjs/operators';

@Component({
  selector: 'app-dead-loads-selection',
  templateUrl: './dead-loads-selection.component.html',
  styleUrls: ['./dead-loads-selection.component.css'],
})
export class DeadLoadsSelectionComponent implements OnInit {
  private _selectedCategory: CategoryResultResource | undefined;
  private _selectedSubcategory: SubcategoryResultResource | undefined;
  private _selectedMaterial: MaterialResultResource | undefined;

  public get selectedCategory(): CategoryResultResource | undefined {
    return this._selectedCategory;
  }
  public set selectedCategory(
    categoryViewModel: CategoryResultResource | undefined
  ) {
    this._selectedCategory = categoryViewModel;
    this._selectedCategoryChanged.emit(categoryViewModel);
  }
  public get selectedSubcategory(): SubcategoryResultResource | undefined {
    return this._selectedSubcategory;
  }
  public set selectedSubcategory(
    selectedSubcategory: SubcategoryResultResource | undefined
  ) {
    this._selectedSubcategory = selectedSubcategory;
    this._selectedSubcategoryChanged.emit(selectedSubcategory);
  }

  public get selectedMaterial(): MaterialResultResource | undefined {
    return this._selectedMaterial;
  }
  public set selectedMaterial(selectedMaterial: MaterialResultResource | undefined) {
    this._selectedMaterial = selectedMaterial;
    this._selectedMaterialChanged.emit(selectedMaterial);
  }

  public get categories$(): Observable<CategoryResultResource[]> {
    return this._categories$;
  }
  private _categories$!: Observable<CategoryResultResource[]>;

  public get subcategories$(): Observable<SubcategoryResultResource[]> {
    return this._subcategories$;
  }
  private _subcategories$!: Observable<SubcategoryResultResource[]>;

  public get materials$(): Observable<MaterialResultResource[]> {
    return this._materials$;
  }
  private _materials$!: Observable<MaterialResultResource[]>;

  private readonly _selectedCategoryChanged: EventEmitter<
  CategoryResultResource | undefined
  > = new EventEmitter();
  private readonly _selectedSubcategoryChanged: EventEmitter<
  SubcategoryResultResource | undefined
  > = new EventEmitter();
  private readonly _selectedMaterialChanged: EventEmitter<
  MaterialResultResource | undefined
  > = new EventEmitter();
  private readonly _selectedMaterialAdded: EventEmitter<MaterialResultResource> =
    new EventEmitter();

  constructor(
    private readonly _deadLoadsService : DeadLoadsService,
    private readonly _deadLoadsRepositoryService: DeadLoadsRepositoryService,
    private readonly _loggerService: LoggerService
  ) {
    this._selectedCategoryChanged.subscribe((category) => {
      this.selectedMaterial = undefined;
      this.setupSubcategories(category!?.id);
    });
    this._selectedSubcategoryChanged.subscribe((subcategory) => {
      this.setupMaterials(subcategory!?.id);
      if (subcategory) subcategory.category = this.selectedCategory;
    });
    this._selectedMaterialChanged.subscribe((material)=>{
      if(material)
      material.subcategory = this.selectedSubcategory;
    });
  }

  ngOnInit(): void {
    this._categories$ = this._deadLoadsRepositoryService.provideCategories$();
  }

  public addMaterial(materialViewModel: MaterialResultResource): void {
    if (materialViewModel) {
      this._selectedMaterialAdded.emit(materialViewModel);
      this._deadLoadsService.addMaterial(materialViewModel);
    }
  }

  private setupSubcategories(categoryId: number | undefined): void {
    if (categoryId === null || categoryId === undefined) {
      this.selectedSubcategory = undefined;
      this.selectedMaterial = undefined;
      return;
    }
    this._subcategories$ = this.categories$.pipe(mergeMap(categories => categories.filter(c => c.id == categoryId).map(c => c.subcategories ?? [])));
  }

  private setupMaterials(subcategoryId: number | undefined): void {
    if (subcategoryId === null || subcategoryId == undefined) {
      this.selectedMaterial = undefined;
      return;
    }
    this._materials$ = this._subcategories$.pipe(mergeMap(subcategories => subcategories.filter(s => s.id == subcategoryId).map(s => s.materials ?? [])));
  }
}
