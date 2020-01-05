import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { Album } from '../_models/album';
import { AlbumService } from '../_services/album.service';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class AlbumDetailResolver implements Resolve<Album> {
  constructor(
    private albumService: AlbumService,
    private router: Router,
    private alertify: AlertifyService
  ) {}

  resolve(route: ActivatedRouteSnapshot): Observable<Album> {
    return this.albumService.getAlbum(route.params['id']).pipe(
      catchError(error => {
        this.alertify.error('Problem retriving data');
        this.router.navigate(['/albums']);
        return of(null);
      })
    );
  }
}
