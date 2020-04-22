import { Resolve, ActivatedRouteSnapshot, Router } from '@angular/router';
import { AlbumOverallRate } from '../_models/albumOveralRate';
import { of, Observable } from 'rxjs';
import { RatesService } from '../_services/rates.service';
import { AlertifyService } from '../_services/alertify.service';
import { catchError } from 'rxjs/operators';
import { Injectable } from '@angular/core';

@Injectable()
export class RankingResolver implements Resolve<AlbumOverallRate[]> {
  pageNumber = 1;
  pageSize = 5;

  constructor(
    private ratesService: RatesService,
    private router: Router,
    private alertify: AlertifyService
  ) {}

  resolve(route: ActivatedRouteSnapshot): Observable<AlbumOverallRate[]> {
    return this.ratesService
      .getAlbumRanking(this.pageNumber, this.pageSize)
      .pipe(
        catchError((error) => {
          this.alertify.error('Problem retriving data');
          this.router.navigate(['albumRanking']);
          return of(null);
        })
      );
  }
}
