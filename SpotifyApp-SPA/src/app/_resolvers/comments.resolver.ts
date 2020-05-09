import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AlbumService } from '../_services/album.service';
import { AlertifyService } from '../_services/alertify.service';

@Injectable()
export class CommentsResolver implements Resolve<Comment[]> {
  pageNumber = 1;
  pageSize = 5;

  constructor(
    private albumService: AlbumService,
    private router: Router,
    private alertify: AlertifyService
  ) {}

  resolve(route: ActivatedRouteSnapshot): Observable<Comment[]> {
    return this.albumService
      .getComments(
        route.params['userId'],
        route.params['albumId'],
        this.pageNumber,
        this.pageSize
      )
      .pipe(
        catchError((error) => {
          this.alertify.error('Problem retriving comments');
          this.router.navigate(['/albums']);
          return of(null);
        })
      );
  }
}
