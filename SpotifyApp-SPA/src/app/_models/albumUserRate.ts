import { Album } from './album';

export interface AlbumUserRate {
  album: Album;
  rate: number;
  dateOfRate: Date;
}
