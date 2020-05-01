import { Component, OnInit } from '@angular/core';
import { AlbumUserRate } from 'src/app/_models/albumUserRate';
import { Pagination, PaginatedResult } from 'src/app/_models/pagination';
import { RatesService } from 'src/app/_services/rates.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-my-latest-albums',
  templateUrl: './my-latest-albums.component.html',
  styleUrls: ['./my-latest-albums.component.css'],
})
export class MyLatestAlbumsComponent implements OnInit {
  albumRanking: AlbumUserRate[];
  pagination: Pagination;
  isLoaded = false;
  rankingNumber = 1;

  constructor(
    private ratesService: RatesService,
    private alertifyService: AlertifyService,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.loadAlbumRates();
  }

  loadAlbumRates() {
    this.route.data.subscribe(
      (data) => {
        this.albumRanking = data['myRates'].results;
        this.pagination = data['myRates'].pagination;
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
    this.loadMoreAlbums();
  }

  loadMoreAlbums() {
    this.ratesService
      .getMyRates(this.pagination.currentPage, this.pagination.itemsPerPage)
      .subscribe(
        (res: PaginatedResult<AlbumUserRate[]>) => {
          this.albumRanking = res.results;
          this.pagination = res.pagination;
        },
        (error) => {
          this.alertifyService.error(error);
        }
      );
  }
}
