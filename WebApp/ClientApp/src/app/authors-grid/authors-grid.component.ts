import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AuthorsDataService } from "../authors-data.service"
import { Author } from "../model.author";
import { GridComponent } from "../grid.component"

@Component({
    selector: 'app-authors-grid',
    template: `
<kendo-grid
  [loading]="dataService.loading"
  [pageSize]="dataService.state.take"
  [skip]="dataService.state.skip"
  [sort]="dataService.state.sort"
  [sortable]="true"
  [pageable]="true"
  [scrollable]="'none'"
  [data]="dataService.data"
  (dataStateChange)="dataService.dataStateChange($event)"
  (edit)="editHandler($event)" (cancel)="cancelHandler($event)"
  (save)="saveHandler($event)" (remove)="removeHandler($event)"
  (add)="addHandler($event)"
>
  <ng-template kendoGridToolbarTemplate>
    <button kendoGridAddCommand>Add new</button>
  </ng-template>
  <kendo-grid-column field="Id" [hidden]="true"></kendo-grid-column>
  <kendo-grid-column field="FirstName"[sortable]="true" title="First Name"></kendo-grid-column>
  <kendo-grid-column field="LastName" [sortable]="true" title="Last Name"></kendo-grid-column>
  <kendo-grid-command-column title="" width="220">
    <ng-template kendoGridCellTemplate let-isNew="isNew">
      <button kendoGridEditCommand [primary]="true">Edit</button>
      <button kendoGridRemoveCommand>Remove</button>
      <button kendoGridSaveCommand [disabled]="formGroup?.invalid">{{ isNew ? 'Add' : 'Update' }}</button>
      <button kendoGridCancelCommand>{{ isNew ? 'Discard changes' : 'Cancel' }}</button>
    </ng-template>
  </kendo-grid-command-column>
</kendo-grid>
   `
})
export class AuthorsGridComponent extends GridComponent<Author> implements OnInit {
    modelFactory(): Author { return new Author; }

    ngOnInit(): void { }

    constructor(dataService: AuthorsDataService) {
        super(dataService);
        this.fetch();
    }

    protected createFormGroup(author: Author) {
        this.formGroup = new FormGroup({
            'Id': new FormControl(author.Id),
            'FirstName': new FormControl(author.FirstName, Validators.compose([Validators.required, Validators.maxLength(20)])),
            'LastName': new FormControl(author.LastName, Validators.compose([Validators.required, Validators.maxLength(20)])),
        });
    }
}
