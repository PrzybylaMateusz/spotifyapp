import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PlayerService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getTrack(): Observable<string> {
    return this.http.get<string>(this.baseUrl + 'player');
  }
}
