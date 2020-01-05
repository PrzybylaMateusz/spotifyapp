import { Routes } from '@angular/router';
import { AlbumDetailComponent } from './albums/album-detail/album-detail.component';
import { AlbumListComponent } from './albums/album-list/album-list.component';
import { AlbumRankingComponent } from './albums/album-ranking/album-ranking.component';
import { ArtistListComponent } from './artist-list/artist-list.component';
import { HomeComponent } from './home/home.component';
import { SearchPanelComponent } from './search-panel/search-panel.component';
import { UserBoardComponent } from './user-board/user-board.component';
import { AuthGuard } from './_guards/auth.guard';
import { AlbumDetailResolver } from './_resolvers/album-detail.resolver';

export const appRoutes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'albums', component: AlbumListComponent },
  {
    path: 'albums/:id',
    component: AlbumDetailComponent,
    resolve: { album: AlbumDetailResolver }
  },
  { path: 'search', component: SearchPanelComponent },
  {
    path: '',
    canActivate: [AuthGuard],
    children: [
      { path: 'my', component: UserBoardComponent, canActivate: [AuthGuard] }
    ]
  },
  { path: 'artists', component: ArtistListComponent },
  { path: 'rankings', component: AlbumRankingComponent },
  { path: 'users', component: UserBoardComponent },
  { path: '**', redirectTo: '', pathMatch: 'full' }
];
