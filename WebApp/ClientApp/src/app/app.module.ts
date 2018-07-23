import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { BooksComponent } from './books/books.component';
import { AuthorsComponent } from './authors/authors.component';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { GridModule } from '@progress/kendo-angular-grid';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { DropDownsModule } from '@progress/kendo-angular-dropdowns';
import { PopupModule } from '@progress/kendo-angular-popup';
import { DatePickerModule } from '@progress/kendo-angular-dateinputs';

import { AuthorsGridComponent } from './authors-grid/authors-grid.component';
import { AuthorsDataService } from "./authors-data.service";
import { BookGridComponent } from './book-grid/book-grid.component'
import { BooksDataService as BookDataService } from "./books-data.service";
import { PopupAnchorDirective } from './popup-anchor.directive';
import { CookieService } from 'ngx-cookie-service';
@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        BooksComponent,
        AuthorsComponent,
        AuthorsGridComponent,
        BookGridComponent,
        PopupAnchorDirective
    ],
    imports: [
        BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
        HttpClientModule,
        FormsModule,
        BrowserAnimationsModule,
        GridModule,
        ReactiveFormsModule,
        InputsModule,
        DropDownsModule,
        PopupModule,
        DatePickerModule,
        RouterModule.forRoot([
            { path: '', pathMatch: 'full', redirectTo: 'books' },
            { path: 'books', component: BooksComponent },
            { path: 'authors', component: AuthorsComponent }
        ])
    ],
    providers: [AuthorsDataService, BookDataService, CookieService ],
    bootstrap: [AppComponent]
})
export class AppModule { }
