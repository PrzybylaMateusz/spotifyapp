import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ArtistRate } from 'src/app/_models/artistRate';
import { ArtistWithAlbums } from 'src/app/_models/artistWithAlbums';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { AuthService } from 'src/app/_services/auth.service';
import { RatesService } from 'src/app/_services/rates.service';

@Component({
  selector: 'app-artist-detail',
  templateUrl: './artist-detail.component.html',
  styleUrls: ['./artist-detail.component.css'],
})
export class ArtistDetailComponent implements OnInit {
  artist: ArtistWithAlbums;
  max = 10;
  rate = 0;
  isReadonly = false;
  isUserLogged = false;

  overStar: number | undefined;
  percent: number;
  constructor(
    private authService: AuthService,
    private ratesService: RatesService,
    private alertify: AlertifyService,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.route.data.subscribe((data) => {
      this.artist = data['artist'];
    });

    if (this.authService.loggedIn()) {
      this.isUserLogged = true;
      this.loadRate();
    } else {
      this.isUserLogged = false;
      this.rate = 0;
      this.overStar = null;
    }
  }

  hoveringOver(value: number): void {
    this.overStar = value;
  }

  resetStar(): void {
    if (this.rate === 0) {
      this.overStar = void 0;
    } else {
      this.overStar = this.rate;
    }
  }

  saveRate(rate: any): void {
    if (this.isUserLogged) {
      this.rate = rate;
      const artistRate: ArtistRate = {
        rate: this.rate,
        ratedDate: new Date(),
        artistId: this.artist.id,
        userId: this.authService.decodedToken.nameid,
      };

      this.ratesService.rateArtist(artistRate).subscribe(
        () => {
          this.alertify.success('You have rated artist: ' + this.artist.name);
        },
        (error) => {
          this.alertify.error(error);
        }
      );
    } else {
      this.rate = 0;
      this.overStar = void 0;
      this.alertify.warning('Log in if you want to rate this artist.');
    }
  }

  loadRate() {
    this.ratesService
      .getArtistRateForUser(
        this.artist.id,
        this.authService.decodedToken.nameid
      )
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
