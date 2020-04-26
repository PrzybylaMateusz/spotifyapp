import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { AlbumAverageRate } from '../_models/albumAverageRate';
import { PaginatedResult } from '../_models/pagination';
import { AlbumUserRate } from '../_models/albumUserRate';

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
  ): Observable<PaginatedResult<AlbumAverageRate[]>> {
    const paginatedResult: PaginatedResult<
      AlbumAverageRate[]
    > = new PaginatedResult<AlbumAverageRate[]>();

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
      .get<AlbumAverageRate[]>(this.baseUrl + 'rates/albumranking', {
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

  getMyRates(
    page?,
    itemsPerPage?
  ): Observable<PaginatedResult<AlbumUserRate[]>> {
    const paginatedResult: PaginatedResult<
      AlbumUserRate[]
    > = new PaginatedResult<AlbumUserRate[]>();

    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    return this.http
      .get<AlbumUserRate[]>(this.baseUrl + 'rates/myrates', {
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
}
