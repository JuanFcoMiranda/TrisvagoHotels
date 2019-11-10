import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HotelAddEditComponent } from './hoteladdedit.component';

describe('HotelAddEditComponent', () => {
    let component: HotelAddEditComponent;
    let fixture: ComponentFixture<HotelAddEditComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [HotelAddEditComponent]
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(HotelAddEditComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
