import { catchError } from 'rxjs/operators';
import { of, Observable } from 'rxjs';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { ArtistService } from '../_services/artist.service';
import { AlertifyService } from '../_services/alertify.service';
import { Injectable } from '@angular/core';
import { ArtistWithAlbums } from '../_models/artistWithAlbums';

@Injectable()
export class ArtistDetailResolver implements Resolve<ArtistWithAlbums> {
  constructor(
    private artistService: ArtistService,
    private router: Router,
    private alertify: AlertifyService
  ) {}

  resolve(route: ActivatedRouteSnapshot): Observable<ArtistWithAlbums> {
    return this.artistService.getArtist(route.params['id']).pipe(
      catchError((error) => {
        this.alertify.error('Problem retriving data');
        this.router.navigate(['/artists']);
        return of(null);
      })
    );
  }
}
