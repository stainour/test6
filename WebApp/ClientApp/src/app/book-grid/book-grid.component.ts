import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Author } from "../model.author";
import { Book } from '../model.book';
import { BooksDataService as BookDataService } from '../books-data.service'
import { AuthorsDataService } from "../authors-data.service"
import { GridComponent } from "../grid.component"
import { CookieService } from 'ngx-cookie-service';
import { DataStateChangeEvent } from '@progress/kendo-angular-grid';
import { SortDescriptor } from '@progress/kendo-data-query';

@Component({
  selector: 'app-book-grid',
  styleUrls: ['./book-grid.component.scss'],
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
  (dataStateChange)="dataStateChange($event)"
  (edit)="editHandler($event)" (cancel)="cancelHandler($event)"
  (save)="saveHandler($event)" (remove)="removeHandler($event)"
  (add)="addHandler($event)"
>
  <ng-template kendoGridToolbarTemplate>
    <button kendoGridAddCommand>Add new</button>
  </ng-template>
  <kendo-grid-column field="Id" [hidden]="true"></kendo-grid-column>
  <kendo-grid-column field="Title" [sortable]="true" title="Title"></kendo-grid-column>

  <kendo-grid-column width="220" field="Authors" [sortable]="false" title="Authors">
    <ng-template kendoGridCellTemplate let-dataItem let-rowIndex="rowIndex">
      {{ formatAuthors(dataItem)}}
    </ng-template>
    <ng-template kendoGridEditTemplate let-dataItem="dataItem" let-column="column" let-formGroup="formGroup" let-isNew="isNew">

      <kendo-multiselect
        #ddl="popupAnchor"
        popupAnchor
        [allowCustom]="true"
        [data]="allAuthors"
        [textField]="'FullName'"
        [valueField]="'Id'"
        [(ngModel)]="dataItem.Authors"
        [formControl]="formGroup.get('Authors')"
      >
      </kendo-multiselect>

      <kendo-popup
        [anchor]="ddl.element"
        *ngIf="formGroup.get(column.field).invalid && !(isNew && formGroup.get(column.field).untouched) ||  dataItem.Authors.length === 0"
        popupClass="k-widget k-tooltip k-tooltip-validation k-invalid-msg"
      >
        <span class="k-icon k-i-warning"></span>
       At least one author is required
      </kendo-popup>

    </ng-template>
  </kendo-grid-column>

  <kendo-grid-column field="PageCount" editor="numeric" [sortable]="true" title="Page Count">
    <ng-template kendoGridEditTemplate let-column="column" let-formGroup="formGroup" let-isNew="isNew">
      <kendo-numerictextbox [formControl]="formGroup.get(column.field)" [min]="1" [max]="10000" [spinners]="true"></kendo-numerictextbox>
    </ng-template>
  </kendo-grid-column>
  <kendo-grid-column field="Publisher" [sortable]="true" title="Publisher"></kendo-grid-column>
  <kendo-grid-column field="IssueDate" [sortable]="true"  title="Issue Date" format="{0:yyyy}">
    <ng-template kendoGridEditTemplate let-dataItem="dataItem" let-column="column" let-formGroup="formGroup" let-isNew="isNew">
    <kendo-datepicker
      #ddl="popupAnchor"
      popupAnchor
      [bottomView]="'decade'"
      [topView]="'decade'"
      [format]="'yyyy'"
      [(value)]="dataItem.IssueDate"
      [(ngModel)]="dataItem.IssueDate"
      [formControl]="formGroup.get('IssueDate')"
    ></kendo-datepicker>
    </ng-template>
</kendo-grid-column>
  <kendo-grid-column field="ISBN" [sortable]="true" title="ISBN"></kendo-grid-column>

  <kendo-grid-column field="Image" [sortable]="false" width="200" title="Image">
    <ng-template kendoGridCellTemplate let-dataItem="dataItem" let-rowIndex="rowIndex">
      <img
        *ngIf="dataItem.Image !== null"
        src="{{dataItem.Image}}" class="image" />
    </ng-template>

    <ng-template kendoGridEditTemplate let-dataItem="dataItem" let-column="column" let-formGroup="formGroup" let-isNew="isNew">
      <img *ngIf="dataItem.Image !== null"
           [src]="dataItem.Image" class="image" />
    <div>
      <label for="filePicker">Choose a file:</label><br>
      <input class="k-button" type="file" accept="image/*" (change)="handleFileSelect($event,dataItem,formGroup.get('Image'))">
    </div>
    </ng-template>

</kendo-grid-column>

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
export class BookGridComponent extends GridComponent<Book> implements OnInit {
  modelFactory(): Book { return new Book; }

  private allAuthors: Author[];
  private readonly cookieName = "bookGridSort";
  ngOnInit(): void { }

  constructor(dataService: BookDataService, private readonly authorsDataService: AuthorsDataService, private readonly cookieService: CookieService) {
    super(dataService);

    const sortsString = this.cookieService.get(this.cookieName);

    if (sortsString !== "") {
      const sorts = JSON.parse(sortsString) as Array<SortDescriptor>;
      dataService.setSort(sorts);
    }

    this.fetch();
    this.fetchAllAuthors();
  }

  private formatAuthor(author: Author): string {
    return `${author.FirstName} ${author.LastName}`;
  }

  private formatAuthors(dataItem: Book) {
    if (dataItem.Authors && dataItem.Authors.length > 0) {
      return dataItem.Authors.map(e => this.formatAuthor(e)).join(", ");
    }
  }
  public dataStateChange(state: DataStateChangeEvent): void {
    this.cookieService.set(this.cookieName, JSON.stringify(state.sort));
    this.dataService.dataStateChange(state);
  }
  private fetchAllAuthors(): void {
    this.authorsDataService.getAll().map((value: Author[], index) => {
      value.forEach((value, index, array) => {
        value.FullName = this.formatAuthor(value);
      });
      return value;
    }).subscribe((data) => { this.allAuthors = data; });
  }

  private handleFileSelect(evt, dataItem: Book, form: FormControl) {
    var files = evt.target.files;
    var file = files[0];
    if (files && file) {
      var reader = new FileReader();
      reader.onload = ev => {
        dataItem.Image = `data:${file.type};base64,${btoa((ev.target as any).result)}`;
        form.setValue(dataItem.Image);
      };
      reader.readAsBinaryString(file);
    }
  }

  protected createFormGroup(book: Book) {
    this.fetchAllAuthors();

    this.formGroup = new FormGroup({
      'Id': new FormControl(book.Id),
      'Title': new FormControl(book.Title, Validators.compose([Validators.required, Validators.maxLength(30)])),
      'Authors': new FormControl(book.Authors),
      'PageCount': new FormControl(book.PageCount, Validators.compose([Validators.required, Validators.max(10000), Validators.min(1)])),
      'Publisher': new FormControl(book.Publisher, Validators.compose([Validators.maxLength(20), Validators.minLength(0)])),
      'IssueDate': new FormControl(book.IssueDate),
      'ISBN': new FormControl(book.ISBN, Validators.pattern('(ISBN[-]*(1[03])*[ ]*(: ){0,1})*(([0-9Xx][- ]*){13}|([0-9Xx][- ]*){10})|(^$)')),
      'Image': new FormControl(book.Image),
    });
  }
}
