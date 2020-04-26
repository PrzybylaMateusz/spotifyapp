import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AlbumOverallRate } from 'src/app/_models/albumOveralRate';
import { PaginatedResult, Pagination } from 'src/app/_models/pagination';
import { RatesService } from 'src/app/_services/rates.service';
import { AlertifyService } from '../../_services/alertify.service';

@Component({
  selector: 'app-user-board',
  templateUrl: './user-board.component.html',
  styleUrls: ['./user-board.component.css'],
})
export class UserBoardComponent implements OnInit {
  albumRanking: AlbumOverallRate[];
  pagination: Pagination;
  isLoaded = false;

  constructor(
    private ratesService: RatesService,
    private alertifyService: AlertifyService,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.loadUsers();
  }

  loadUsers() {
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
    this.loadMoreAlbums();
  }

  loadMoreAlbums() {
    this.ratesService
      .getMyRates(this.pagination.currentPage, this.pagination.itemsPerPage)
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
}
