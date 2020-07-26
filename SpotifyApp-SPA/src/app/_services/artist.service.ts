import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ArtistWithAlbums } from '../_models/artistWithAlbums';

@Injectable({
  providedIn: 'root',
})
export class ArtistService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getArtist(id: string): Observable<ArtistWithAlbums> {
    return this.http.get<ArtistWithAlbums>(this.baseUrl + 'artists/' + id);
  }
}
