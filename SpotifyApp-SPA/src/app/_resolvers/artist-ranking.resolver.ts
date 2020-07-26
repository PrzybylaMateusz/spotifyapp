import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ArtistAverageRate } from '../_models/artistAverageRate';
import { AlertifyService } from '../_services/alertify.service';
import { RatesService } from '../_services/rates.service';

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
