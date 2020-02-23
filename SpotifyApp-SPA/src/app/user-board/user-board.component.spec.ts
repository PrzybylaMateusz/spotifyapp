/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from './node_modules/src/app/users/user-board/node_modules/@angular/core/testing';
import { By } from './node_modules/src/app/users/user-board/node_modules/@angular/platform-browser';
import { DebugElement } from './node_modules/src/app/users/user-board/node_modules/@angular/core';

import { UserBoardComponent } from './user-board.component';

describe('UserBoardComponent', () => {
  let component: UserBoardComponent;
  let fixture: ComponentFixture<UserBoardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserBoardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserBoardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
