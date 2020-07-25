import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';
import { PlayerService } from '../_services/player.service';
import { Subscription, timer } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { CurrentlyPlayed } from '../_models/currentlyPlayed';
import { AlbumRate } from '../_models/albumRate';
import { RatesService } from '../_services/rates.service';

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
  rate = 0;
  isReadonly = false;
  isTrackPlayed = false;
  trackVisible = true;
  connected = false;

  constructor(
    public authService: AuthService,
    private alertify: AlertifyService,
    private playerService: PlayerService,
    private ratesService: RatesService,
    private router: Router
  ) {}

  ngOnInit() {
    this.alive = true;
    this.subscription = timer(0, 5000)
      .pipe(switchMap(() => this.playerService.getTrack()))
      .subscribe(
        (x) => {
          if (x) {
            this.currentylPlayed = x;
            this.loadRate();
          }
          this.isTrackPlayed = x !== null;
          this.connected = x !== null;
        },
        (error) => {
          this.connected = false;
        }
      );

    if (localStorage.getItem('tokenSpotify') === 'null') {
      this.connected = false;
    }
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
    return this.authService.connectWithSpotify();
  }

  getCurrentTrack() {
    this.playerService.getTrack().subscribe((x) => console.log(x));
  }

  saveRate(): void {
    const albumRate: AlbumRate = {
      rate: this.rate,
      ratedDate: new Date(),
      albumId: this.currentylPlayed.album.id,
      userId: this.authService.decodedToken.nameid,
    };

    this.ratesService.rateAlbum(albumRate).subscribe(
      (data) => {
        this.alertify.success(
          'You have rated album: ' + this.currentylPlayed.album.name
        );
      },
      (error) => {
        this.alertify.error(error);
      }
    );
  }

  loadRate() {
    this.ratesService
      .getAlbumRateForUser(
        this.currentylPlayed.album.id,
        this.authService.decodedToken.nameid
      )
      .subscribe(
        (rate: number) => {
          this.rate = rate;
        },
        (error) => {
          this.alertify.error(error);
        }
      );
  }

  onArrowUpClick() {
    this.trackVisible = false;
  }

  onArrowDownClick() {
    this.trackVisible = true;
  }
}
