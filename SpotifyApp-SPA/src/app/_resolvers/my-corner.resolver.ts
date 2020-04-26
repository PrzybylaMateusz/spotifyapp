import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AlbumOverallRate } from '../_models/albumOveralRate';
import { AlertifyService } from '../_services/alertify.service';
import { RatesService } from '../_services/rates.service';

@Injectable()
export class MyCornerResolver implements Resolve<AlbumOverallRate[]> {
  pageNumber = 1;
  pageSize = 5;

  constructor(
    private ratesService: RatesService,
    private router: Router,
    private alertify: AlertifyService
  ) {}

  resolve(route: ActivatedRouteSnapshot): Observable<AlbumOverallRate[]> {
    return this.ratesService.getMyRates(this.pageNumber, this.pageSize).pipe(
      catchError((error) => {
        this.alertify.error('Problem retriving data');
        this.router.navigate(['albumRanking']);
        return of(null);
      })
    );
  }
}
