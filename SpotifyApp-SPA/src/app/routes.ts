import { Routes } from '@angular/router';
import { AlbumDetailComponent } from './albums/album-detail/album-detail.component';
import { AlbumRankingComponent } from './albums/album-ranking/album-ranking.component';
import { HomeComponent } from './home/home.component';
import { SearchPanelComponent } from './search-panel/search-panel.component';
import { UserBoardComponent } from './users/user-board/user-board.component';
import { AuthGuard } from './_guards/auth.guard';
import { AlbumDetailResolver } from './_resolvers/album-detail.resolver';
import { UserEditComponent } from './users/user-edit/user-edit.component';
import { UserEditResolver } from './_resolvers/user-edit.resolver';
import { PreventUnsavedChanges } from './_guards/prevent-unsaved-changes.guard';
import { AlbumRankingResolver } from './_resolvers/album-ranking.resolver';
import { MyCornerResolver } from './_resolvers/my-corner.resolver';
import { ArtistRankingComponent } from './artist/artist-ranking/artist-ranking.component';
import { ArtistRankingResolver } from './_resolvers/artist-ranking.resolver';

export const appRoutes: Routes = [
  { path: '', component: HomeComponent },
  {
    path: 'albums/:id',
    component: AlbumDetailComponent,
    resolve: { album: AlbumDetailResolver },
  },
  { path: 'search', component: SearchPanelComponent },
  {
    path: '',
    canActivate: [AuthGuard],
    children: [
      {
        path: 'my',
        component: UserBoardComponent,
        canActivate: [AuthGuard],
        resolve: { myRates: MyCornerResolver },
      },
    ],
  },
  {
    path: 'artistsranking',
    component: ArtistRankingComponent,
    resolve: { artistRanking: ArtistRankingResolver },
  },
  {
    path: 'albumsranking',
    component: AlbumRankingComponent,
    resolve: { albumRanking: AlbumRankingResolver },
  },
  { path: 'users', component: UserBoardComponent },
  {
    path: 'user/edit',
    component: UserEditComponent,
    resolve: { user: UserEditResolver },
    canDeactivate: [PreventUnsavedChanges],
  },
  { path: '**', redirectTo: '', pathMatch: 'full' },
];
