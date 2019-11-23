import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AlbumListComponent } from './albums/album-list/album-list.component';
import { ArtistListComponent } from './artist-list/artist-list.component';
import { RankingsComponent } from './rankings/rankings.component';
import { UserBoardComponent } from './user-board/user-board.component';
import { AuthGuard } from './_guards/auth.guard';
import { SearchPanelComponent } from './search-panel/search-panel.component';

export const appRoutes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'albums', component: AlbumListComponent },
  { path: 'search', component: SearchPanelComponent },
  {
    path: '',
    canActivate: [AuthGuard],
    children: [
      { path: 'my', component: UserBoardComponent, canActivate: [AuthGuard] }
    ]
  },
  { path: 'artists', component: ArtistListComponent },
  { path: 'rankings', component: RankingsComponent },
  { path: 'users', component: UserBoardComponent },
  { path: '**', redirectTo: '', pathMatch: 'full' }
];
