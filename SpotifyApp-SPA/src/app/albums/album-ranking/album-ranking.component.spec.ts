/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { AlbumRankingComponent } from './album-ranking.component';

describe('AlbumRankingComponent', () => {
  let component: AlbumRankingComponent;
  let fixture: ComponentFixture<AlbumRankingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [AlbumRankingComponent]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AlbumRankingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
