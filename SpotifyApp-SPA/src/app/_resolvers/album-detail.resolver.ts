import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Album } from '../_models/album';
import { AlbumService } from '../_services/album.service';
import { AlertifyService } from '../_services/alertify.service';

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
