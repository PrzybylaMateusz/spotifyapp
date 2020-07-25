import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  registerMode = false;

  constructor(private http: HttpClient) {}

  ngOnInit() {
    localStorage.removeItem('tokenSpotify');
    const match = window.location.hash.match(/#access_token=(.*?)&/);
    const token = match && match[1];
    localStorage.setItem('tokenSpotify', token);
  }

  registerToggle() {
    this.registerMode = true;
  }

  cancelRegisterMode(registerMode: boolean) {
    this.registerMode = registerMode;
  }
}
