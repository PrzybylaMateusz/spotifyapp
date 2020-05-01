import { catchError } from 'rxjs/operators';
import { of, Observable } from 'rxjs';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { Artist } from '../_models/artist';
import { ArtistService } from '../_services/artist.service';
import { AlertifyService } from '../_services/alertify.service';
import { Injectable } from '@angular/core';

@Injectable()
export class ArtistDetailResolver implements Resolve<Artist> {
  constructor(
    private artistService: ArtistService,
    private router: Router,
    private alertify: AlertifyService
  ) {}

  resolve(route: ActivatedRouteSnapshot): Observable<Artist> {
    return this.artistService.getArtist(route.params['id']).pipe(
      catchError((error) => {
        this.alertify.error('Problem retriving data');
        this.router.navigate(['/artists']);
        return of(null);
      })
    );
  }
}
