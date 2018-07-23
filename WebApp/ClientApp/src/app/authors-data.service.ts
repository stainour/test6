import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import 'rxjs/add/operator/map';
import { Author } from './model.author';
import { DataService } from './data.service';
import { Observable } from 'rxjs/Observable';
@Injectable()
export class AuthorsDataService extends DataService<Author> {
    constructor(httpClient: HttpClient) { super(httpClient, 'api/authors'); }

    public getAll(): Observable<Author[]> {
        return this.httpClient
            .get(this.baseUrl)
            .map(({ Data, Total }: any) =>
                Data as Author[]
            );
    }
}
