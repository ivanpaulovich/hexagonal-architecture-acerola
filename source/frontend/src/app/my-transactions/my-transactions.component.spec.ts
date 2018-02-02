import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MyTransactionsComponent } from './my-transactions.component';

describe('MyTransactionsComponent', () => {
  let component: MyTransactionsComponent;
  let fixture: ComponentFixture<MyTransactionsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MyTransactionsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MyTransactionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
