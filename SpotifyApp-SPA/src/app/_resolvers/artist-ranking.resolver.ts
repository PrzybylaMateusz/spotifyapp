import { Injectable } from '@angular/core';
import { ArtistAverageRate } from '../_models/artistAverageRate';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { RatesService } from '../_services/rates.service';
import { AlertifyService } from '../_services/alertify.service';
import { of, Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ArtistRankingResolver implements Resolve<ArtistAverageRate[]> {
  pageNumber = 1;
  pageSize = 5;

  constructor(
    private ratesService: RatesService,
    private router: Router,
    private alertify: AlertifyService
  ) {}

  resolve(route: ActivatedRouteSnapshot): Observable<ArtistAverageRate[]> {
    return this.ratesService
      .getArtistRanking(this.pageNumber, this.pageSize)
      .pipe(
        catchError((error) => {
          this.alertify.error('Problem retriving data');
          this.router.navigate(['artistRanking']);
          return of(null);
        })
      );
  }
}
