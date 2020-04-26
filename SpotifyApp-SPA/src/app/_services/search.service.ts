import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Album } from '../_models/album';

@Injectable({
  providedIn: 'root',
})
export class SearchService {
  constructor(private http: HttpClient) {}

  baseUrl = environment.apiUrl + 'search/';

  searchAlbum(searchKey: string): Observable<Album[]> {
    return this.http.get<Album[]>(this.baseUrl + searchKey);
  }
}
