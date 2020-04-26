import { Component, OnInit, Input } from '@angular/core';
import { Artist } from 'src/app/_models/artist';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { RatesService } from 'src/app/_services/rates.service';
import { AuthService } from 'src/app/_services/auth.service';

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

  ngOnInit() {}

  hoveringOver(value: number): void {
    this.overStar = value;
  }

  resetStar(): void {
    this.overStar = this.rate;
  }

  saveRate(): void {
    console.log('Saving rate its not available yet');
    // const albumRate: AlbumRate = {
    //   rate: this.rate,
    //   ratedDate: new Date(),
    //   albumId: this.album.id,
    //   userId: this.authService.decodedToken.nameid,
    // };

    // this.ratesService.rateAlbum(albumRate).subscribe(
    //   (data) => {
    //     this.alertify.success('You have rated: ' + this.album.name);
    //   },
    //   (error) => {
    //     this.alertify.error(error);
    //   }
    // );
  }
}
