import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/_services/auth.service';
import { RatesService } from 'src/app/_services/rates.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { Artist } from 'src/app/_models/artist';
import { ArtistRate } from 'src/app/_models/artistRate';

@Component({
  selector: 'app-artist-detail',
  templateUrl: './artist-detail.component.html',
  styleUrls: ['./artist-detail.component.css'],
})
export class ArtistDetailComponent implements OnInit {
  artist: Artist;
  max = 10;
  rate = 0;
  isReadonly = false;

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

    this.loadRate();
  }

  hoveringOver(value: number): void {
    this.overStar = value;
  }

  resetStar(): void {
    this.overStar = void 0;
  }

  saveRate(): void {
    const artistRate: ArtistRate = {
      rate: this.rate,
      ratedDate: new Date(),
      artistId: this.artist.id,
      userId: this.authService.decodedToken.nameid,
    };

    this.ratesService.rateArtist(artistRate).subscribe(
      () => {},
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
