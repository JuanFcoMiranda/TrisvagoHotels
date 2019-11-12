import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { HotelService } from '../services/hotel.service';
import { Hotel } from '../models/hotel';

@Component({
    selector: 'hoteladdedit',
    templateUrl: './hoteladdedit.component.html',
    styleUrls: ['./hoteladdedit.component.scss']
})
export class HotelAddEditComponent implements OnInit {
    form: FormGroup;
    fileData: File = null;
    previewUrl: any = null;
    fileUploadProgress: string = null;
    uploadedFilePath: string = null;
    actionType: string;
    formNombre: string;
    formCategoria: string;
    formDescripcion: string;
    formLocalidad: string;
    formCaracteristicas: string;
    hotelId: number;
    errorMessage: any;
    existingHotel: Hotel;

    constructor(private hotelService: HotelService, private formBuilder: FormBuilder, private avRoute: ActivatedRoute, private router: Router) {
        const idParam = 'id';
        this.actionType = 'Add';
        this.formNombre = 'nombre';
        this.formCategoria = 'categoria';
        this.formDescripcion = 'descripcion';
        this.formLocalidad = 'localidad';
        this.formCaracteristicas = 'caracteristicas';

        if (this.avRoute.snapshot.params[idParam]) {
            this.hotelId = this.avRoute.snapshot.params[idParam];
        }

        this.form = this.formBuilder.group(
            {
                hotelId: 0,
                nombre: ['', [Validators.required]],
                descripcion: ['', []],
                categoria: ['', []],
                foto: ['', []],
                localidad: ['', []],
                caracteristicas: ['', []]
            }
        )
    }

    ngOnInit() {
        if (this.hotelId > 0) {
            this.actionType = 'Edit';
            this.hotelService.getHotel(this.hotelId)
                .subscribe(data => (
                    this.existingHotel = data,
                    this.form.controls[this.formNombre].setValue(data.nombre),
                    this.form.controls[this.formCategoria].setValue(data.categoria),
                    this.form.controls[this.formDescripcion].setValue(data.descripcion),
                    this.form.controls[this.formLocalidad].setValue(data.localidad),
                    this.form.controls[this.formCaracteristicas].setValue(data.caracteristicas)
                ));
        }
    }

    save() {
        if (!this.form.valid) {
            return;
        }

        if (this.actionType === 'Add') {
            let hotel: Hotel = {
                nombre: this.form.get(this.formNombre).value,
                categoria: this.form.get(this.formCategoria).value,
                descripcion: this.form.get(this.formDescripcion).value,
                localidad: this.form.get(this.formLocalidad).value,
                caracteristicas: this.form.get(this.formCaracteristicas).value
            };
            this.hotelService.saveHotel(hotel)
                .subscribe((data) => {
                    if (this.fileData != null && data != null) {
                        this.uploadFile(data.id);
                    } else {
                        this.router.navigate(['/']);
                    }
                });
        }

        if (this.actionType === 'Edit') {
            let hotel: Hotel = {
                id: this.existingHotel.id,
                nombre: this.form.get(this.formNombre).value,
                categoria: this.form.get(this.formCategoria).value,
                descripcion: this.form.get(this.formDescripcion).value,
                localidad: this.form.get(this.formLocalidad).value,
                caracteristicas: this.form.get(this.formCaracteristicas).value
            };
            this.hotelService.updateHotel(hotel.id, hotel)
                .subscribe((data) => {
                    if (this.fileData != null && data != null) {
                        this.uploadFile(data.id);
                    } else {
                        this.router.navigate(['/']);
                    }
                });
        }
    }

    uploadFile(id: number) {
        this.hotelService.uploadImage(id, this.fileData).subscribe((data) => {
            this.router.navigate(['/']);
        });
    }

    cancel() {
        this.router.navigate(['/']);
    }

    fileProgress(fileInput: any) {
        this.fileData = <File>fileInput.target.files[0];
        this.preview();
    }

    preview() {
        // Show preview
        const mimeType = this.fileData.type;
        if (mimeType.match(/image\/*/) == null) {
            return;
        }

        const reader = new FileReader();
        reader.readAsDataURL(this.fileData);
        reader.onload = (_event) => {
            this.previewUrl = reader.result;
        }
    }

    get nombre() {
        return this.form.get(this.formNombre);
    }
}