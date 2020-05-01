import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ArtistWithAlbums } from '../_models/artistWithAlbums';

@Injectable({
  providedIn: 'root',
})
export class ArtistService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getArtist(id): Observable<ArtistWithAlbums> {
    return this.http.get<ArtistWithAlbums>(this.baseUrl + 'artists/' + id);
  }
}
