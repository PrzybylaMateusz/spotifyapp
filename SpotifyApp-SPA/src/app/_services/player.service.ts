import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { CurrentlyPlayed } from '../_models/currentlyPlayed';

@Injectable({
  providedIn: 'root',
})
export class PlayerService {
  baseUrl = environment.apiUrl;
  spotifyToken = localStorage.getItem('tokenSpotify');
  constructor(private http: HttpClient) {}

  getTrack(): Observable<CurrentlyPlayed> {
    return this.http.get<CurrentlyPlayed>(
      this.baseUrl + 'player/' + this.spotifyToken
    );
  }
}
