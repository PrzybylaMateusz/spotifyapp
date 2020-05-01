import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Artist } from '../_models/artist';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ArtistService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getArtist(id): Observable<Artist> {
    return this.http.get<Artist>(this.baseUrl + 'artists/' + id);
  }
}
