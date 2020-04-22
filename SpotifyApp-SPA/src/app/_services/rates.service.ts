import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { AlbumOverallRate } from '../_models/albumOveralRate';
import { PaginatedResult } from '../_models/pagination';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class RatesService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  rateAlbum(model: any) {
    return this.http.post(this.baseUrl + 'rates/album', model);
  }

  getAlbumRanking(
    page?,
    itemsPerPage?,
    rankingParams?
  ): Observable<PaginatedResult<AlbumOverallRate[]>> {
    const paginatedResult: PaginatedResult<
      AlbumOverallRate[]
    > = new PaginatedResult<AlbumOverallRate[]>();

    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    if (rankingParams != null) {
      params = params.append('minYear', rankingParams.minYear);
      params = params.append('maxYear', rankingParams.maxYear);
    }

    return this.http
      .get<AlbumOverallRate[]>(this.baseUrl + 'rates/albumranking', {
        observe: 'response',
        params,
      })
      .pipe(
        map((response) => {
          paginatedResult.results = response.body;
          if (response.headers.get('Pagination') != null) {
            paginatedResult.pagination = JSON.parse(
              response.headers.get('Pagination')
            );
          }
          return paginatedResult;
        })
      );
  }

  getAlbumRateForUser(albumId: string, userId: number): Observable<number> {
    return this.http.get<number>(
      this.baseUrl + 'rates/' + albumId + '/' + userId
    );
  }

  // getAlbums(): Observable<Album[]> {
  //   return this.http.get<Album[]>(this.baseUrl + 'albums');
  // }
}
