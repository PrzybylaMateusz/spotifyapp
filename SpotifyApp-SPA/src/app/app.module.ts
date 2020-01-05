import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { BsDropdownModule, RatingModule } from 'ngx-bootstrap';
import { RouterModule } from '@angular/router';
import { JwtModule } from '@auth0/angular-jwt';

import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { AuthService } from './_services/auth.service';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { ErrorInterceptorProvider } from './_services/error.interceptor';
import { AlertifyService } from './_services/alertify.service';
import { RankingsComponent } from './rankings/rankings.component';
import { ArtistComponent } from './artist/artist.component';
import { AlbumListComponent } from './albums/album-list/album-list.component';
import { ArtistListComponent } from './artist-list/artist-list.component';
import { appRoutes } from './routes';
import { UserBoardComponent } from './user-board/user-board.component';
import { AuthGuard } from './_guards/auth.guard';
import { AlbumCardComponent } from './albums/album-card/album-card.component';
import { SearchPanelComponent } from './search-panel/search-panel.component';
import { AlbumRankingComponent } from './albums/album-ranking/album-ranking.component';
import { AlbumDetailComponent } from './albums/album-detail/album-detail.component';

export function tokenGetter() {
  return localStorage.getItem('token');
}

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    RegisterComponent,
    RankingsComponent,
    ArtistComponent,
    AlbumListComponent,
    ArtistListComponent,
    UserBoardComponent,
    AlbumCardComponent,
    SearchPanelComponent,
    AlbumRankingComponent,
    AlbumDetailComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    BsDropdownModule.forRoot(),
    RatingModule.forRoot(),
    RouterModule.forRoot(appRoutes),
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        whitelistedDomains: ['localhost:5001'],
        blacklistedRoutes: ['localhost:5001/api/auth']
      }
    })
  ],
  providers: [
    AuthService,
    ErrorInterceptorProvider,
    AlertifyService,
    AuthGuard
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
