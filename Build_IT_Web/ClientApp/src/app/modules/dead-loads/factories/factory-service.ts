export abstract class FactoryService<TResource ,TViewModel>{

  public abstract create(data: TResource) : TViewModel;

  public createMany(data: TResource[]) : TViewModel[]{
    return data.map(d => this.create(d));
  }
}
