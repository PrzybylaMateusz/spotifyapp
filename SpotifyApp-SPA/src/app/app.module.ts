import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import {
  BsDropdownModule,
  RatingModule,
  TabsModule,
  PaginationModule,
  ButtonsModule,
} from 'ngx-bootstrap';
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
import { appRoutes } from './routes';
import { UserBoardComponent } from './users/user-board/user-board.component';
import { AuthGuard } from './_guards/auth.guard';
import { AlbumCardComponent } from './albums/album-card/album-card.component';
import { SearchPanelComponent } from './search-panel/search-panel.component';
import { AlbumRankingComponent } from './albums/album-ranking/album-ranking.component';
import { AlbumDetailComponent } from './albums/album-detail/album-detail.component';
import { AlbumDetailResolver } from './_resolvers/album-detail.resolver';
import { UserEditComponent } from './users/user-edit/user-edit.component';
import { UserEditResolver } from './_resolvers/user-edit.resolver';
import { PreventUnsavedChanges } from './_guards/prevent-unsaved-changes.guard';
import { AlbumRankingResolver } from './_resolvers/album-ranking.resolver';
import { ArtistRankingResolver } from './_resolvers/artist-ranking.resolver';
import { MyCornerResolver } from './_resolvers/my-corner.resolver';
import { ArtistCardComponent } from './artist/artist-card/artist-card.component';
import { ArtistRankingComponent } from './artist/artist-ranking/artist-ranking.component';

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
    UserBoardComponent,
    AlbumCardComponent,
    ArtistCardComponent,
    SearchPanelComponent,
    AlbumRankingComponent,
    ArtistRankingComponent,
    AlbumDetailComponent,
    UserEditComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    BsDropdownModule.forRoot(),
    PaginationModule.forRoot(),
    RatingModule.forRoot(),
    TabsModule.forRoot(),
    ButtonsModule.forRoot(),
    RouterModule.forRoot(appRoutes),
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        whitelistedDomains: ['localhost:5001'],
        blacklistedRoutes: ['localhost:5001/api/auth'],
      },
    }),
  ],
  providers: [
    AuthService,
    ErrorInterceptorProvider,
    AlertifyService,
    AuthGuard,
    AlbumDetailResolver,
    UserEditResolver,
    PreventUnsavedChanges,
    AlbumRankingResolver,
    ArtistRankingResolver,
    MyCornerResolver,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
