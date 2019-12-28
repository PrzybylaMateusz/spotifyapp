import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';
import { tap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Album } from '../_models/album';

@Injectable({
  providedIn: 'root'
})
export class SearchService {
  constructor(private http: HttpClient) {}

  baseUrl = environment.apiUrl + 'search/';

  search(searchKey: string): Observable<Album[]> {
    return this.http.get<Album[]>(this.baseUrl + searchKey);
  }
}
