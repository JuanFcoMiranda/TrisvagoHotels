import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { HotelService } from '../services/hotel.service';
import { Hotel } from '../models/hotel';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
    hotels$: Observable<Hotel[]>;

    constructor(private hotelService: HotelService) {
    }

    ngOnInit() {
        this.loadHotels();
    }

    loadHotels() {
        this.hotels$ = this.hotelService.getHotels();
    }

    delete(postId) {
        const ans = confirm('Do you want to delete hotel with id: ' + postId);
        if (ans) {
            this.hotelService.deleteHotel(postId).subscribe((data) => {
                this.loadHotels();
            });
        }
    }
}
