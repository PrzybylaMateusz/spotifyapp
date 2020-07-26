import { Routes } from '@angular/router';
import { AlbumCommentsComponent } from './albums/album-comments/album-comments.component';
import { AlbumDetailComponent } from './albums/album-detail/album-detail.component';
import { AlbumRankingComponent } from './albums/album-ranking/album-ranking.component';
import { ArtistDetailComponent } from './artist/artist-detail/artist-detail.component';
import { ArtistRankingComponent } from './artist/artist-ranking/artist-ranking.component';
import { HomeComponent } from './home/home.component';
import { SearchPanelComponent } from './search-panel/search-panel.component';
import { UserBoardComponent } from './users/user-board/user-board.component';
import { UserEditComponent } from './users/user-edit/user-edit.component';
import { AuthGuard } from './_guards/auth.guard';
import { PreventUnsavedChanges } from './_guards/prevent-unsaved-changes.guard';
import { AlbumDetailResolver } from './_resolvers/album-detail.resolver';
import { AlbumRankingResolver } from './_resolvers/album-ranking.resolver';
import { ArtistDetailResolver } from './_resolvers/artist-detail.resolver';
import { ArtistRankingResolver } from './_resolvers/artist-ranking.resolver';
import { CommentsResolver } from './_resolvers/comments.resolver';
import { MyArtistsResolver } from './_resolvers/my-artists.resolver';
import { MyCornerResolver } from './_resolvers/my-corner.resolver';
import { UserEditResolver } from './_resolvers/user-edit.resolver';

export const appRoutes: Routes = [
  { path: '', component: HomeComponent },
  {
    path: 'albums/:id',
    component: AlbumDetailComponent,
    resolve: { album: AlbumDetailResolver },
  },
  {
    path: 'artists/:id',
    component: ArtistDetailComponent,
    resolve: { artist: ArtistDetailResolver },
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
        resolve: { myRates: MyCornerResolver, myArtists: MyArtistsResolver },
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
  {
    path: 'comments/:userId/:albumId',
    component: AlbumCommentsComponent,
    resolve: { comments: CommentsResolver },
  },
  { path: '**', redirectTo: '', pathMatch: 'full' },
];
