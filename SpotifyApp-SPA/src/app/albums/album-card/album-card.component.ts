import { Component, OnInit, Input } from '@angular/core';
import { Album } from 'src/app/_models/album';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { RatesService } from 'src/app/_services/rates.service';
import { AlbumRate } from 'src/app/_models/albumRate';

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

  saveRate(): void {
    const albumRate: AlbumRate = {
      rate: this.rate,
      ratedDate: new Date(),
      album: this.album.id,
      userId: 1
    };

    this.ratesService.rateAlbum(albumRate).subscribe(
      () => {
        this.alertify.success('registration successful');
      },
      error => {
        this.alertify.error(error);
      }
    );
  }

  constructor(
    private alertify: AlertifyService,
    private ratesService: RatesService
  ) {}

  ngOnInit() {}
}
