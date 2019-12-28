import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { AlbumOverallRate } from '../_models/albumOveralRate';

@Injectable({
  providedIn: 'root'
})
export class RatesService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  rateAlbum(model: any) {
    return this.http.post(this.baseUrl + 'rates/album', model);
  }

  getAlbumRanking(): Observable<AlbumOverallRate[]> {
    return this.http.get<AlbumOverallRate[]>(
      this.baseUrl + 'rates/albumranking'
    );
  }

  // getAlbums(): Observable<Album[]> {
  //   return this.http.get<Album[]>(this.baseUrl + 'albums');
  // }
}
