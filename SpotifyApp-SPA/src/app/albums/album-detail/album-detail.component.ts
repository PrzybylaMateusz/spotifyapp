import { Component, OnInit } from '@angular/core';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { AlbumService } from 'src/app/_services/album.service';
import { Album } from 'src/app/_models/album';
import { RatesService } from 'src/app/_services/rates.service';
import { AlbumRate } from 'src/app/_models/albumRate';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-album-detail',
  templateUrl: './album-detail.component.html',
  styleUrls: ['./album-detail.component.css'],
})
export class AlbumDetailComponent implements OnInit {
  album: Album;
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
      this.album = data['album'];
    });
  }

  hoveringOver(value: number): void {
    this.overStar = value;
  }

  resetStar(): void {
    this.overStar = void 0;
  }

  saveRate(): void {
    const albumRate: AlbumRate = {
      rate: this.rate,
      ratedDate: new Date(),
      albumId: this.album.id,
      userId: this.authService.decodedToken.nameid,
    };

    this.ratesService.rateAlbum(albumRate).subscribe(
      () => {},
      (error) => {
        this.alertify.error(error);
      }
    );
  }
}
