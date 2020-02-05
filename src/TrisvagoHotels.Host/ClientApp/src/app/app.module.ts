import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { RouterModule } from '@angular/router';

import { NgxFileDropModule } from "ngx-file-drop";
import { MatTableModule, MatPaginatorModule, MatProgressSpinnerModule, MatSortModule } from '@angular/material'
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatIconModule } from "@angular/material/icon";
import { MatButtonModule } from "@angular/material/button";
import { MatDialogModule } from "@angular/material/dialog";

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { HotelAddEditComponent } from './hoteladdedit/hoteladdedit.component';
import { MatConfirmDialogComponent } from "./mat-confirm-dialog/mat-confirm-dialog.component";

import { DialogService } from "./services/dialog.service";
import { HotelService } from "./services/hotel.service";

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        HotelAddEditComponent,
        MatConfirmDialogComponent
    ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    MatTableModule,
    MatPaginatorModule,
    BrowserAnimationsModule,
    MatSortModule,
    MatIconModule,
    MatProgressSpinnerModule,
    MatDialogModule,
    ReactiveFormsModule,
    BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
    HttpClientModule,
    FormsModule,
    NgxFileDropModule,
    RouterModule.forRoot([
      {path: '', component: HomeComponent, pathMatch: 'full'}
    ]),
    MatButtonModule
  ],
    providers: [HotelService, DialogService],
    bootstrap: [AppComponent],
    entryComponents:[HomeComponent, MatConfirmDialogComponent]
})
export class AppModule { }
