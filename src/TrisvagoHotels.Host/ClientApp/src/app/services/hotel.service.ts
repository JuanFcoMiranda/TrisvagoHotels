import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Hotel } from '../models/hotel';

@Injectable({
    providedIn: 'root'
})
export class HotelService {
    protected myAppUrl: string;
    protected myApiUrl: string;
    protected httpOptions = {
        headers: new HttpHeaders({
            'Content-Type': 'application/json; charset=utf-8;multipart/form-data',
            'Content-Disposition' : 'multipart/form-data',
            'enctype': 'multipart/form-data'
        })
    };

    constructor(private http: HttpClient) {
        this.myAppUrl = environment.appUrl;
        this.myApiUrl = 'api/hotels/';
    }

    getHotels(): Observable<Hotel[]> {
        return this.http.get<Hotel[]>(this.myAppUrl + this.myApiUrl)
            .pipe(
                retry(1),
                catchError(this.errorHandler)
            );
    }

    getHotel(hotelId: number): Observable<Hotel> {
        return this.http.get<Hotel>(this.myAppUrl + this.myApiUrl + hotelId)
            .pipe(
                retry(1),
                catchError(this.errorHandler)
            );
    }

    saveHotel(hotel): Observable<Hotel> {
        return this.http.post<Hotel>(this.myAppUrl + this.myApiUrl, JSON.stringify(hotel), this.httpOptions)
            .pipe(
                retry(1),
                catchError(this.errorHandler)
            );
    }

    updateHotel(hotelId: number, hotel): Observable<Hotel> {
        return this.http.put<Hotel>(this.myAppUrl + this.myApiUrl + hotelId, JSON.stringify(hotel), this.httpOptions)
            .pipe(
                retry(1),
                catchError(this.errorHandler)
            );
    }

    deleteHotel(hotelId: number): Observable<Hotel> {
        return this.http.delete<Hotel>(this.myAppUrl + this.myApiUrl + hotelId)
            .pipe(
                retry(1),
                catchError(this.errorHandler)
            );
    }

    uploadImage(hotelId: number, file: File) {
      const formData: FormData = new FormData();
      formData.append('file', file, file.name);
      return this.http.post(this.myAppUrl + this.myApiUrl +  'Upload' + "?hotelId=" + hotelId, formData);
    }

    errorHandler(error) {
        let errorMessage = '';
        if (error.error instanceof ErrorEvent) {
            // Get client-side error
            errorMessage = error.error.message;
        } else {
            // Get server-side error
            errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
        }
        console.log(errorMessage);
        return throwError(errorMessage);
    }
}
