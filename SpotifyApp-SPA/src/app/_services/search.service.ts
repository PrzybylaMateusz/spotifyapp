import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Album } from '../_models/album';
import { Artist } from '../_models/artist';

@Injectable({
  providedIn: 'root',
})
export class SearchService {
  constructor(private http: HttpClient) {}

  baseUrl = environment.apiUrl + 'search/';

  searchAlbum(searchKey: string): Observable<Album[]> {
    return this.http.get<Album[]>(this.baseUrl + 'album/' + searchKey);
  }

  searchArtist(searchKey: string): Observable<Artist[]> {
    return this.http.get<Artist[]>(this.baseUrl + 'artist/' + searchKey);
  }
}
