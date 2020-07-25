import { Component } from '@angular/core';

@Component({
  selector: 'app-user-board',
  templateUrl: './user-board.component.html',
  styleUrls: ['./user-board.component.css'],
})
export class UserBoardComponent {
  constructor() {
    localStorage.removeItem('tokenSpotify');
    const match = window.location.hash.match(/#access_token=(.*?)&/);
    const token = match && match[1];
    localStorage.setItem('tokenSpotify', token);
  }
}
