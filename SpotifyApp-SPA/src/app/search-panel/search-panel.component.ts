import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Album } from '../_models/album';
import { switchMap } from 'rxjs/operators';
import { ActivatedRoute } from '@angular/router';
import { SearchService } from '../_services/search.service';

@Component({
  selector: 'app-search-panel',
  templateUrl: './search-panel.component.html',
  styleUrls: ['./search-panel.component.css'],
})
export class SearchPanelComponent implements OnInit {
  albums$: Observable<Album[]>;
  searchKey: string;
  constructor(
    private route: ActivatedRoute,
    private searchService: SearchService
  ) {}

  ngOnInit() {
    this.albums$ = this.route.paramMap.pipe(
      switchMap((params) => {
        // (+) before `params.get()` turns the string into a number
        this.searchKey = params.get('id');
        return this.searchService.searchAlbum(this.searchKey);
      })
    );
  }
}

// import { Component, OnInit } from '@angular/core';
// import { Album } from '../../_models/album';
// import { AlbumService } from '../../_services/album.service';
// import { AlertifyService } from '../../_services/alertify.service';

// @Component({
//   selector: 'app-album-list',
//   templateUrl: './album-list.component.html',
//   styleUrls: ['./album-list.component.css']
// })
// export class AlbumListComponent implements OnInit {
//   albums: Album[];

//   constructor(
//     private albumService: AlbumService,
//     private alertify: AlertifyService
//   ) {}

//   ngOnInit() {
//     this.loadAlbums();
//   }

//   loadAlbums() {
//     this.albumService.getAlbums().subscribe(
//       (albums: Album[]) => {
//         this.albums = albums;
//       },
//       error => {
//         this.alertify.error(error);
//       }
//     );
//   }
// }
