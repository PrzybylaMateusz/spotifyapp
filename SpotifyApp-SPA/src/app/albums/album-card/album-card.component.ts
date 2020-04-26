import { Component, OnInit, Input } from '@angular/core';
import { Album } from 'src/app/_models/album';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { RatesService } from 'src/app/_services/rates.service';
import { AlbumRate } from 'src/app/_models/albumRate';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-album-card',
  templateUrl: './album-card.component.html',
  styleUrls: ['./album-card.component.css'],
})
export class AlbumCardComponent implements OnInit {
  @Input() album: Album;
  max = 10;
  rate = null;
  isReadonly = false;

  overStar: number | undefined;
  percent: number;

  hoveringOver(value: number): void {
    this.overStar = value;
    // this.percent = (value / this.max) * 100;
  }

  resetStar(): void {
    this.overStar = this.rate;
  }

  saveRate(): void {
    const albumRate: AlbumRate = {
      rate: this.rate,
      ratedDate: new Date(),
      albumId: this.album.id,
      userId: this.authService.decodedToken.nameid,
    };

    this.ratesService.rateAlbum(albumRate).subscribe(
      (data) => {
        this.alertify.success('You have rated: ' + this.album.name);
      },
      (error) => {
        this.alertify.error(error);
      }
    );
  }

  constructor(
    private alertify: AlertifyService,
    private ratesService: RatesService,
    private authService: AuthService
  ) {}

  ngOnInit() {
    this.loadRate();
  }

  loadRate() {
    this.ratesService
      .getAlbumRateForUser(this.album.id, this.authService.decodedToken.nameid)
      .subscribe(
        (rate: number) => {
          this.rate = rate;
          this.overStar = rate === 0 ? null : rate;
        },
        (error) => {
          this.alertify.error(error);
        }
      );
  }
}
