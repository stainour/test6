import { Injectable } from '@angular/core';
import { DataService } from './data.service';
import { Book } from './model.book';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class BooksDataService extends DataService<Book> {
    constructor(httpClient: HttpClient) { super(httpClient, 'api/books'); }

    modelFormatter(models: Book[]): void {
        models.forEach((value, index, array) => {
            if (value.IssueDate !== null) {
                value.IssueDate = new Date(value.IssueDate);
            }
        });
    }
}
