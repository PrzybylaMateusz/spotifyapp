import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ArtistAverageRate } from 'src/app/_models/artistAverageRate';
import { PaginatedResult, Pagination } from 'src/app/_models/pagination';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { RatesService } from 'src/app/_services/rates.service';

@Component({
  selector: 'app-artist-ranking',
  templateUrl: './artist-ranking.component.html',
  styleUrls: ['./artist-ranking.component.css'],
})
export class ArtistRankingComponent implements OnInit {
  artistRanking: ArtistAverageRate[];
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
    this.loadArtistsRating();
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.rankingNumber =
      this.pagination.currentPage * this.pagination.itemsPerPage -
      this.pagination.itemsPerPage +
      1;
    this.loadMoreArtists();
  }

  loadArtistsRating() {
    this.route.data.subscribe(
      (data) => {
        this.artistRanking = data['artistRanking'].results;
        this.pagination = data['artistRanking'].pagination;
        this.isLoaded = true;
      },
      (error) => {
        this.alertifyService.error(error);
      }
    );
  }

  loadMoreArtists() {
    this.ratesService
      .getArtistRanking(
        this.pagination.currentPage,
        this.pagination.itemsPerPage,
        this.rankingParams
      )
      .subscribe(
        (res: PaginatedResult<ArtistAverageRate[]>) => {
          this.artistRanking = res.results;
          this.pagination = res.pagination;
        },
        (error) => {
          this.alertifyService.error(error);
        }
      );
  }
}
