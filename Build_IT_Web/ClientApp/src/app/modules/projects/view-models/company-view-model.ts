export class CompanyViewModel {

  constructor(
    public readonly id:number,
    public readonly name: string,
    public readonly created: Date,
    public readonly lastModified: Date) {
  }

}
