import { Component, OnInit } from '@angular/core';
import { ArtistUserRate } from 'src/app/_models/artistUserRate';
import { Pagination, PaginatedResult } from 'src/app/_models/pagination';
import { RatesService } from 'src/app/_services/rates.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-my-latest-artists',
  templateUrl: './my-latest-artists.component.html',
  styleUrls: ['./my-latest-artists.component.css'],
})
export class MyLatestArtistsComponent implements OnInit {
  artistRanking: ArtistUserRate[];
  pagination: Pagination;
  isLoaded = false;
  rankingNumber = 1;

  constructor(
    private ratesService: RatesService,
    private alertifyService: AlertifyService,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.loadArtistRates();
  }

  loadArtistRates() {
    this.route.data.subscribe(
      (data) => {
        this.artistRanking = data['myArtists'].results;
        this.pagination = data['myArtists'].pagination;
        this.isLoaded = true;
      },
      (error) => {
        this.alertifyService.error(error);
      }
    );
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.rankingNumber =
      this.pagination.currentPage * this.pagination.itemsPerPage -
      this.pagination.itemsPerPage +
      1;
    this.loadMoreArtists();
  }

  loadMoreArtists() {
    this.ratesService
      .getMyArtistsRates(
        this.pagination.currentPage,
        this.pagination.itemsPerPage
      )
      .subscribe(
        (res: PaginatedResult<ArtistUserRate[]>) => {
          this.artistRanking = res.results;
          this.pagination = res.pagination;
        },
        (error) => {
          this.alertifyService.error(error);
        }
      );
  }
}
