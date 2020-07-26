import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AlbumAverageRate } from 'src/app/_models/albumAverageRate';
import { PaginatedResult, Pagination } from 'src/app/_models/pagination';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { RatesService } from 'src/app/_services/rates.service';

@Component({
  selector: 'app-album-ranking',
  templateUrl: './album-ranking.component.html',
  styleUrls: ['./album-ranking.component.css'],
})
export class AlbumRankingComponent implements OnInit {
  albumRanking: AlbumAverageRate[];
  isLoaded = false;
  pagination: Pagination;
  rankingParams: any = {};
  rankingNumber = 1;

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
    this.rankingNumber =
      this.pagination.currentPage * this.pagination.itemsPerPage -
      this.pagination.itemsPerPage +
      1;
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
        (res: PaginatedResult<AlbumAverageRate[]>) => {
          this.albumRanking = res.results;
          this.pagination = res.pagination;
        },
        (error) => {
          this.alertifyService.error(error);
        }
      );
  }
}
