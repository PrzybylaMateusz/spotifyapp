import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';
import { tap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SearchService {
  constructor(private http: HttpClient) {}

  baseUrl = environment.apiUrl + 'search/';
  // url =
  //   "https://api.spotify.com/v1/search?q=album%3Aarrival%20artist%3Aabba&type=album";

  // httpOptions = {
  //   headers: new HttpHeaders({
  //     "Content-Type": "application/json",
  //     Authorization:
  //       "Bearer BQCbkh54UzptCcFuAW-cjO5VE5B0ZtZGwUU5KSPVfTAHSLUH9p-FUAFflXI2DcvBgr8b6GYG9TH59hdm_2-WOG3qpiIudoJf5gzyN_NKuHmCgSSlMLfkpxSEloWsnu64338UZdUg7tsxOKJQ9A"
  //   })
  // };

  search(searchKey: string) {
    console.log(searchKey);
    this.http.get(this.baseUrl + searchKey).subscribe(data => {
      console.log(data);
    });
  }
}
