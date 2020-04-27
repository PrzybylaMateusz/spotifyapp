import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Album } from '../_models/album';
import { switchMap } from 'rxjs/operators';
import { ActivatedRoute } from '@angular/router';
import { SearchService } from '../_services/search.service';
import { Artist } from '../_models/artist';

@Component({
  selector: 'app-search-panel',
  templateUrl: './search-panel.component.html',
  styleUrls: ['./search-panel.component.css'],
})
export class SearchPanelComponent implements OnInit {
  albums$: Observable<Album[]>;
  artists$: Observable<Artist[]>;
  showAlbums: boolean;
  show = 'Albums';
  searchKey: string;
  constructor(
    private route: ActivatedRoute,
    private searchService: SearchService
  ) {}

  ngOnInit() {
    this.showAlbums = true;
    this.show = 'Albums';
    this.artists$ = this.route.paramMap.pipe(
      switchMap((params) => {
        // (+) before `params.get()` turns the string into a number
        this.searchKey = params.get('id');
        return this.searchService.searchArtist(this.searchKey);
      })
    );

    this.albums$ = this.route.paramMap.pipe(
      switchMap((params) => {
        // (+) before `params.get()` turns the string into a number
        this.searchKey = params.get('id');
        return this.searchService.searchAlbum(this.searchKey);
      })
    );
  }
}
