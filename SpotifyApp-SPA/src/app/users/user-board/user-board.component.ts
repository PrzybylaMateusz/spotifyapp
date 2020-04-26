import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PaginatedResult, Pagination } from 'src/app/_models/pagination';
import { RatesService } from 'src/app/_services/rates.service';
import { AlertifyService } from '../../_services/alertify.service';
import { AlbumUserRate } from 'src/app/_models/albumUserRate';

@Component({
  selector: 'app-user-board',
  templateUrl: './user-board.component.html',
  styleUrls: ['./user-board.component.css'],
})
export class UserBoardComponent implements OnInit {
  albumRanking: AlbumUserRate[];
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
        const ranking = data['myRates'].results;
        this.albumRanking = ranking.sort(
          (a, b) =>
            new Date(b.dateOfRate).getTime() - new Date(a.dateOfRate).getTime()
        );
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
        (res: PaginatedResult<AlbumUserRate[]>) => {
          const ranking = res.results;
          this.albumRanking = ranking.sort(
            (a, b) =>
              new Date(b.dateOfRate).getTime() -
              new Date(a.dateOfRate).getTime()
          );
          this.pagination = res.pagination;
        },
        (error) => {
          this.alertifyService.error(error);
        }
      );
  }

  private getTime(date?: Date) {
    return date != null ? date.getTime() : 0;
  }
}
