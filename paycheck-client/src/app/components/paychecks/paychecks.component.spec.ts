import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PaychecksComponent } from './paychecks.component';

describe('PaychecksComponent', () => {
  let component: PaychecksComponent;
  let fixture: ComponentFixture<PaychecksComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PaychecksComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PaychecksComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
