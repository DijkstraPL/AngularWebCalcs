export class ProjectViewModel{

  constructor(
    public readonly id : number,
    public readonly name : string,
    public readonly description: string,
    public readonly created: Date,
    public readonly lastModified: Date) {
  }

  }

