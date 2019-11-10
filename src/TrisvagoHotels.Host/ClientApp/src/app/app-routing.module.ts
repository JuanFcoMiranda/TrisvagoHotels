import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { HotelComponent } from './hotel/hotel.component';
import { HotelAddEditComponent } from './hoteladdedit/hoteladdedit.component';

const routes: Routes = [
    { path: '', component: HomeComponent, pathMatch: 'full' },
    { path: 'hotel/:id', component: HotelComponent },
    { path: 'add', component: HotelAddEditComponent },
    { path: 'hotel/edit/:id', component: HotelAddEditComponent },
    { path: '**', redirectTo: '/' }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }