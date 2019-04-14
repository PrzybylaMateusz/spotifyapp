import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-album',
  templateUrl: './album.component.html',
  styleUrls: ['./album.component.css']
})
export class AlbumComponent implements OnInit {
  albums: any;

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getAlbums();
  }

  getAlbums(){
    // this.http.get('https://api.spotify.com/v1/albums?ids=41MnTivkwTO3UUJ8DrqEJJ%2C6JWc4iAiJ9FjyK0B59ABb4%2C6UXCm6bOO4gFlDQZV5yL37').subscribe(response => {
    //   this.albums = response['albums'];
    // }, error => {
    //   console.log(error);
    // });

    this.http.get('http://localhost:5001/api/albums').subscribe(response => {
      this.albums = response;
    }, error => {
      console.log(error);
    });

  }
}
