import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { AlbumAverageRate } from '../_models/albumAverageRate';
import { PaginatedResult } from '../_models/pagination';
import { AlbumUserRate } from '../_models/albumUserRate';
import { ArtistAverageRate } from '../_models/artistAverageRate';
import { ArtistUserRate } from '../_models/artistUserRate';

@Injectable({
  providedIn: 'root',
})
export class RatesService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  rateAlbum(model: any) {
    return this.http.post(this.baseUrl + 'rates/album', model);
  }

  rateArtist(model: any) {
    return this.http.post(this.baseUrl + 'rates/artist', model);
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

  getArtistRanking(
    page?,
    itemsPerPage?,
    rankingParams?
  ): Observable<PaginatedResult<ArtistAverageRate[]>> {
    const paginatedResult: PaginatedResult<
      ArtistAverageRate[]
    > = new PaginatedResult<ArtistAverageRate[]>();

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
      .get<ArtistAverageRate[]>(this.baseUrl + 'rates/artistranking', {
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

  getArtistRateForUser(artistId: string, userId: number): Observable<number> {
    return this.http.get<number>(
      this.baseUrl + 'rates/artist/' + artistId + '/' + userId
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

  getMyArtistsRates(
    page?,
    itemsPerPage?
  ): Observable<PaginatedResult<ArtistUserRate[]>> {
    const paginatedResult: PaginatedResult<
      ArtistUserRate[]
    > = new PaginatedResult<ArtistUserRate[]>();

    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    return this.http
      .get<ArtistUserRate[]>(this.baseUrl + 'rates/myartistsrates', {
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
