import { Component, Input, OnInit } from '@angular/core';
import { Album } from 'src/app/_models/album';
import { AlbumRate } from 'src/app/_models/albumRate';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { AuthService } from 'src/app/_services/auth.service';
import { RatesService } from 'src/app/_services/rates.service';

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
  isUserLogged = false;
  overStar: number | undefined;
  percent: number;

  constructor(
    private alertify: AlertifyService,
    private ratesService: RatesService,
    private authService: AuthService
  ) {}

  ngOnInit() {
    if (this.authService.loggedIn()) {
      this.isUserLogged = true;
      this.loadRate();
    } else {
      this.isUserLogged = false;
    }
  }

  hoveringOver(value: number): void {
    this.overStar = value;
  }

  resetStar(): void {
    this.overStar = this.rate;
  }

  saveRate(): void {
    if (this.isUserLogged) {
      const albumRate: AlbumRate = {
        rate: this.rate,
        ratedDate: new Date(),
        albumId: this.album.id,
        userId: this.authService.decodedToken.nameid,
      };

      this.ratesService.rateAlbum(albumRate).subscribe(
        () => {
          this.alertify.success('You have rated album: ' + this.album.name);
        },
        (error) => {
          this.alertify.error(error);
        }
      );
    } else {
      this.alertify.warning('Log in if you want to rate this album.');
    }
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
