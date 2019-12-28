import { Component, OnInit } from '@angular/core';
import { RatesService } from 'src/app/_services/rates.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { AlbumOverallRate } from 'src/app/_models/albumOveralRate';

@Component({
  selector: 'app-album-ranking',
  templateUrl: './album-ranking.component.html',
  styleUrls: ['./album-ranking.component.css']
})
export class AlbumRankingComponent implements OnInit {
  albumRanking: AlbumOverallRate[];

  constructor(
    private ratesService: RatesService,
    private alertifyService: AlertifyService
  ) {}

  ngOnInit() {
    this.loadAlbumsRating();
  }

  loadAlbumsRating() {
    this.ratesService.getAlbumRanking().subscribe(
      (albumRanking: AlbumOverallRate[]) => {
        this.albumRanking = albumRanking;
      },
      error => {
        this.alertifyService.error(error);
      }
    );
    // this.ratesService.getAlbums().subscribe(
    //   (albums: Album[]) => {
    //     this.albums = albums;
    //   },
    //   error => {
    //     this.alertify.error(error);
    //   }
    // );
  }
}
