import { Component, OnInit, Input } from '@angular/core';
import { Album } from 'src/app/_models/album';

@Component({
  selector: 'app-album-card',
  templateUrl: './album-card.component.html',
  styleUrls: ['./album-card.component.css']
})
export class AlbumCardComponent implements OnInit {
  @Input() album: Album;
  max = 10;
  rate = 7;
  isReadonly = false;

  overStar: number | undefined;
  percent: number;

  hoveringOver(value: number): void {
    this.overStar = value;
    // this.percent = (value / this.max) * 100;
  }

  resetStar(): void {
    this.overStar = void 0;
  }
  constructor() {}

  ngOnInit() {}
}
