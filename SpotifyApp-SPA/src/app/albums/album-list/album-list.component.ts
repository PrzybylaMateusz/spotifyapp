import { Component, OnInit } from '@angular/core';
import { Album } from '../../_models/album';
import { AlbumService } from '../../_services/album.service';
import { AlertifyService } from '../../_services/alertify.service';

@Component({
  selector: 'app-album-list',
  templateUrl: './album-list.component.html',
  styleUrls: ['./album-list.component.css']
})
export class AlbumListComponent implements OnInit {
  albums: Album[];

  constructor(
    private albumService: AlbumService,
    private alertify: AlertifyService
  ) {}

  ngOnInit() {
    this.loadAlbums();
  }

  loadAlbums() {
    this.albumService.getAlbums().subscribe(
      (albums: Album[]) => {
        this.albums = albums;
      },
      error => {
        this.alertify.error(error);
      }
    );
  }
}
