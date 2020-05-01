import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Album } from '../_models/album';

@Injectable({
  providedIn: 'root',
})
export class AlbumService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getAlbum(id): Observable<Album> {
    return this.http.get<Album>(this.baseUrl + 'albums/' + id);
  }
}
