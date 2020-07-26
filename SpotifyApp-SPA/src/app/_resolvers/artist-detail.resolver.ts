import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ArtistWithAlbums } from '../_models/artistWithAlbums';
import { AlertifyService } from '../_services/alertify.service';
import { ArtistService } from '../_services/artist.service';

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
