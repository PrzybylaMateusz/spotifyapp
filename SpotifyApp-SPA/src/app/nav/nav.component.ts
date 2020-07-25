import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';
import { SearchService } from '../_services/search.service';
import { PlayerService } from '../_services/player.service';
import { Subscription, timer } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { CurrentlyPlayed } from '../_models/currentlyPlayed';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css'],
})
export class NavComponent implements OnInit {
  alive = true;
  model: any = {};
  searchKey: string;
  subscription: Subscription;
  currentylPlayed: CurrentlyPlayed;
  max = 10;
  rate = 2;
  isReadonly = false;
  isTrackPlayed = false;
  trackVisible = true;
  connected = false;

  overStar: 5;

  constructor(
    public authService: AuthService,
    private alertify: AlertifyService,
    private searchService: SearchService,
    private playerService: PlayerService,
    private router: Router
  ) {}

  ngOnInit() {
    this.alive = true;
    this.subscription = timer(0, 5000)
      .pipe(switchMap(() => this.playerService.getTrack()))
      .subscribe(
        (x) => {
          this.isTrackPlayed = x !== null;
          this.currentylPlayed = x;
          this.connected = true;
        },
        (error) => {
          this.connected = false;
        }
      );
  }

  login() {
    this.authService.login(this.model).subscribe(
      (next) => {
        this.alertify.success('Logged in successfully');
      },
      (error) => {
        this.alertify.error(error);
      },
      () => {
        this.router.navigate(['/my']);
      }
    );
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

  logout() {
    localStorage.removeItem('token');
    this.alertify.message('logged out');
    this.router.navigate(['/home']);
  }

  onKey(event: any) {
    this.router.navigate(['/search', { id: event.target.value }]);
  }

  connectWithSpotify() {
    console.log('Im here');
    return this.authService.connectWithSpotify();
  }

  getCurrentTrack() {
    this.playerService.getTrack().subscribe((x) => console.log(x));
  }

  hoveringOver($event) {
    console.log('hovering');
  }

  resetStar() {
    console.log('resetingStar');
  }

  saveRate() {
    console.log('savingRate');
  }

  onArrowUpClick() {
    this.trackVisible = false;
  }

  onArrowDownClick() {
    this.trackVisible = true;
  }
}
