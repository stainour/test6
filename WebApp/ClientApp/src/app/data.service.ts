import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { SortDescriptor } from '@progress/kendo-data-query';

import { GridDataResult, DataStateChangeEvent } from '@progress/kendo-angular-grid';
import {
  toDataSourceRequestString,
  DataSourceRequestState
} from '@progress/kendo-data-query';
import { Observable } from 'rxjs/Observable';

export abstract class DataService<TModel extends IdentityModel> {
  public state: DataSourceRequestState = {
    skip: 0,
    take: 5,
  };

  public data: GridDataResult;
  public loading: boolean;
  constructor(protected readonly httpClient: HttpClient, protected readonly baseUrl: string) {
  }

  remove(model: TModel): void {
    this.loading = true;
    this.subscribe(this.httpClient.delete(`${this.baseUrl}/${model.Id}`));
  }

  public fetch(): void {
    const queryStr = `${toDataSourceRequestString(this.state)}`;
    this.loading = true;

    this.httpClient
      .get(`${this.baseUrl}?${queryStr}`)
      .map(({ Data, Total }: any) =>
        (<GridDataResult>{
          data: Data,
          total: Total,
        })
      ).subscribe(r => {
        this.modelFormatter(r.data);
        this.data = r;
        this.loading = false;
      }, error => {
        this.error(error, this);
      });
  }

  private error(error: HttpErrorResponse, dataService: DataService<TModel>) {
    switch (error.status) {
      case 500:
        alert("There is an error during processing request");
        break;

      case 400:
        for (var field in error.error) {
          alert(error.error[field][0]);
        }
        break;

      case 404:
        alert("The entity have already deleted! Reloading data ");
        break;
    }

    dataService.loading = false;
    dataService.fetch();
  }

  protected modelFormatter(models: TModel[]): void { };
  public dataStateChange(state: DataStateChangeEvent): void {
    this.state = state;
    this.fetch();
  }

  private subscribe(observable: Observable<Object>) {
    observable.subscribe(() => {
      this.loading = false;
      this.fetch();
    }, error => {
      this.error(error, this);
    });
  }

  public save(model: TModel, isNew): void {
    if (isNew) {
      this.subscribe(this.httpClient.post(`${this.baseUrl}`, model));
    } else {
      this.subscribe(this.httpClient.put(`${this.baseUrl}`, model));
    }
  }

  public setSort(sort: SortDescriptor[]) {
    this.state.sort = sort;
  }
}
