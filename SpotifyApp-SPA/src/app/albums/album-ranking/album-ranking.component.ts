import { Component, OnInit } from '@angular/core';
import { RatesService } from 'src/app/_services/rates.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { AlbumOverallRate } from 'src/app/_models/albumOveralRate';
import { ActivatedRoute } from '@angular/router';
import { Pagination, PaginatedResult } from 'src/app/_models/pagination';

@Component({
  selector: 'app-album-ranking',
  templateUrl: './album-ranking.component.html',
  styleUrls: ['./album-ranking.component.css'],
})
export class AlbumRankingComponent implements OnInit {
  albumRanking: AlbumOverallRate[];
  isLoaded = false;
  pagination: Pagination;
  rankingParams: any = {};

  constructor(
    private ratesService: RatesService,
    private alertifyService: AlertifyService,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.loadAlbumsRating();
    this.rankingParams.minYear = 0;
    this.rankingParams.maxYear = new Date().getFullYear();
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadMoreAlbums();
  }

  resetFilters() {
    this.rankingParams.minYear = 0;
    this.rankingParams.maxYear = new Date().getFullYear();
    this.loadMoreAlbums();
  }

  loadAlbumsRating() {
    this.route.data.subscribe(
      (data) => {
        this.albumRanking = data['albumRanking'].results;
        this.pagination = data['albumRanking'].pagination;
        this.isLoaded = true;
      },
      (error) => {
        this.alertifyService.error(error);
      }
    );
  }

  loadMoreAlbums() {
    this.ratesService
      .getAlbumRanking(
        this.pagination.currentPage,
        this.pagination.itemsPerPage,
        this.rankingParams
      )
      .subscribe(
        (res: PaginatedResult<AlbumOverallRate[]>) => {
          this.albumRanking = res.results;
          this.pagination = res.pagination;
        },
        (error) => {
          this.alertifyService.error(error);
        }
      );
  }

  // this.ratesService.getAlbums().subscribe(
  //   (albums: Album[]) => {
  //     this.albums = albums;
  //   },
  //   error => {
  //     this.alertify.error(error);
  //   }
  // );
}
