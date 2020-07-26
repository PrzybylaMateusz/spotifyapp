import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Comment } from 'src/app/_models/comment';
import { environment } from 'src/environments/environment';
import { Album } from '../_models/album';
import { PaginatedResult } from '../_models/pagination';

@Injectable({
  providedIn: 'root',
})
export class AlbumService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getAlbum(id: string): Observable<Album> {
    return this.http.get<Album>(this.baseUrl + 'albums/' + id);
  }

  getComments(userId: number, albumId: string, page?, itemsPerPage?) {
    const paginatedResult: PaginatedResult<Comment[]> = new PaginatedResult<
      Comment[]
    >();

    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    return this.http
      .get<Comment[]>(this.baseUrl + 'comments/' + userId + '/' + albumId, {
        observe: 'response',
        params,
      })
      .pipe(
        map((response) => {
          paginatedResult.results = response.body;
          if (response.headers.get('Pagination') !== null) {
            paginatedResult.pagination = JSON.parse(
              response.headers.get('Pagination')
            );
          }

          return paginatedResult;
        })
      );
  }

  addComment(userId: number, albumId: string, comment: Comment) {
    return this.http.post(
      this.baseUrl + 'comments/' + userId + '/' + albumId,
      comment
    );
  }

  deleteComment(userId: number, albumId: string, id: number) {
    return this.http.delete(
      this.baseUrl + 'comments/' + userId + '/' + albumId + '/' + id
    );
  }
}
