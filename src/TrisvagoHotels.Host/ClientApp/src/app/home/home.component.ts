import {Component, OnInit, ViewChild, ChangeDetectorRef} from '@angular/core';
import {HotelService} from '../services/hotel.service';
import {MatTableDataSource, MatSort, MatPaginator, MatDialog} from '@angular/material';
import {Hotel} from '../models/hotel';
import {DialogService} from "../services/dialog.service";

@Component({
  selector: 'app-home',
  styleUrls: ['./home.component.scss'],
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  dataSource: MatTableDataSource<Hotel>;
  displayedColumns: string[] = ['id', 'nombre', 'categoria', 'actions'];
  @ViewChild(MatSort, {static: true}) sort: MatSort;
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;

  constructor(private hotelService: HotelService, private dialogService: DialogService, private changeDetectorRefs: ChangeDetectorRef) {
  }

  ngOnInit() {
    this.loadHotels();
  }

  loadHotels() {
    this.dataSource = new MatTableDataSource();
    this.hotelService.getHotels().subscribe(
      response => {
        this.dataSource = new MatTableDataSource(response);
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
        this.changeDetectorRefs.detectChanges();
      }
    );
  }

  delete(postId: number) {
    this.dialogService.openConfirmDialog('¿Seguro que desea eliminar este registro?')
      .afterClosed().subscribe(res => {
      if (res) {
        this.hotelService.deleteHotel(postId).subscribe((data) => {
          this.loadHotels();
        });
      }
    });
  }

  favorite(id: number, hotel: Hotel) {
    this.dialogService.openConfirmDialog('Are you sure to mark as favorite this record?').afterClosed().subscribe(res => {
      if (res) {
        this.hotelService.markAsFavorite(id, hotel).subscribe((data) => {
          this.loadHotels();
        });
      }
    });
  }

  unfavorite(id: number, hotel: Hotel) {
    this.dialogService.openConfirmDialog('Are you sure to unmark as favorite this record?').afterClosed().subscribe(res => {
      if (res) {
        this.hotelService.unmarkAsFavorite(id, hotel).subscribe((data) => {
          this.loadHotels();
        });
      }
    });
  }
}
