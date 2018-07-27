import { FormGroup } from "@angular/forms";
import { DataService } from './data.service';

export abstract class GridComponent<TModel extends IdentityModel> {
  protected editedRowIndex: number;
  protected formGroup: FormGroup;

  constructor(public readonly dataService: DataService<TModel>) { }

  public removeHandler({ dataItem }) {
    this.dataService.remove(dataItem);
  }

  protected abstract createFormGroup(model: TModel): void;
  protected abstract modelFactory(): TModel;

  protected fetch(): void {
    this.dataService.fetch();
  }

  public saveHandler({ sender, rowIndex, formGroup, isNew }) {
    const product: TModel = formGroup.value;
    this.dataService.save(product, isNew);
    sender.closeRow(rowIndex);
  }
  public closeEditor(grid, rowIndex = this.editedRowIndex) {
    grid.closeRow(rowIndex);
    this.editedRowIndex = undefined;
    this.formGroup = undefined;
  }

  public addHandler({ sender }) {
    this.closeEditor(sender);
    this.createFormGroup(this.modelFactory());
    sender.addRow(this.formGroup);
  }
  public cancelHandler({ sender, rowIndex }) {
    this.fetch();
    this.closeEditor(sender, rowIndex);
  }

  public editHandler({ sender, rowIndex, dataItem }) {
    this.closeEditor(sender);
    const model = dataItem as TModel;
    this.createFormGroup(model);
    this.editedRowIndex = rowIndex;
    sender.editRow(rowIndex, this.formGroup);
  }
}
