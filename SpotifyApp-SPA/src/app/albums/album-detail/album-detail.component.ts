import { Component, OnInit } from '@angular/core';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { AlbumService } from 'src/app/_services/album.service';
import { Album } from 'src/app/_models/album';
import { RatesService } from 'src/app/_services/rates.service';
import { AlbumRate } from 'src/app/_models/albumRate';

@Component({
  selector: 'app-album-detail',
  templateUrl: './album-detail.component.html',
  styleUrls: ['./album-detail.component.css']
})
export class AlbumDetailComponent implements OnInit {
  album: Album;
  max = 10;
  rate = 0;
  isReadonly = false;

  overStar: number | undefined;
  percent: number;

  constructor(
    private albumService: AlbumService,
    private ratesService: RatesService,
    private alertify: AlertifyService,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.loadAlbum();
  }

  loadAlbum() {
    this.albumService.getAlbum(this.route.snapshot.params['id']).subscribe(
      (album: Album) => {
        this.album = album;
      },
      error => {
        this.alertify.error(error);
      }
    );
  }

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
      () => {},
      error => {
        this.alertify.error(error);
      }
    );
  }
}
