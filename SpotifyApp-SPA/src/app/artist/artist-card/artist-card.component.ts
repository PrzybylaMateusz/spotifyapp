import { Component, Input, OnInit } from '@angular/core';
import { Artist } from 'src/app/_models/artist';
import { ArtistRate } from 'src/app/_models/artistRate';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { AuthService } from 'src/app/_services/auth.service';
import { RatesService } from 'src/app/_services/rates.service';

@Component({
  selector: 'app-artist-card',
  templateUrl: './artist-card.component.html',
  styleUrls: ['./artist-card.component.css'],
})
export class ArtistCardComponent implements OnInit {
  @Input() artist: Artist;
  max = 10;
  rate = null;
  isReadonly = false;

  overStar: number | undefined;
  percent: number;

  constructor(
    private alertify: AlertifyService,
    private ratesService: RatesService,
    private authService: AuthService
  ) {}

  ngOnInit() {
    this.loadRate();
  }

  hoveringOver(value: number): void {
    this.overStar = value;
  }

  resetStar(): void {
    this.overStar = this.rate;
  }

  saveRate(): void {
    const artistRate: ArtistRate = {
      rate: this.rate,
      ratedDate: new Date(),
      artistId: this.artist.id,
      userId: this.authService.decodedToken.nameid,
    };

    this.ratesService.rateArtist(artistRate).subscribe(
      (data) => {
        this.alertify.success('You have rated artist: ' + this.artist.name);
      },
      (error) => {
        this.alertify.error(error);
      }
    );
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
