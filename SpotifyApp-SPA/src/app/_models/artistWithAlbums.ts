import { Album } from './album';
export interface ArtistWithAlbums {
    id: string;
    name: string;
    photoUrl: string;
    albums: Album[];
}
