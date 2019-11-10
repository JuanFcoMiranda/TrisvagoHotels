import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Hotel } from '../models/hotel';

@Component({
    selector: 'hotel',
    templateUrl: './hotel.component.html'
})
export class HotelComponent {
    public hotel: Hotel;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {

    }
}