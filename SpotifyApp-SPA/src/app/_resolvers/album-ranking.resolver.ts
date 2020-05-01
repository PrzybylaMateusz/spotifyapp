import { Resolve, ActivatedRouteSnapshot, Router } from '@angular/router';
import { AlbumAverageRate } from '../_models/albumAverageRate';
import { of, Observable } from 'rxjs';
import { RatesService } from '../_services/rates.service';
import { AlertifyService } from '../_services/alertify.service';
import { catchError } from 'rxjs/operators';
import { Injectable } from '@angular/core';

@Injectable()
export class AlbumRankingResolver implements Resolve<AlbumAverageRate[]> {
  pageNumber = 1;
  pageSize = 5;

  constructor(
    private ratesService: RatesService,
    private router: Router,
    private alertify: AlertifyService
  ) {}

  resolve(route: ActivatedRouteSnapshot): Observable<AlbumAverageRate[]> {
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
